/*************************************************************************************
* CLR版本：        4.0.30319.42000
* 类 名 称：       IQueryableExtension
* 命名空间：       Extends
* 文 件 名：       IQueryableExtension
* 创建时间：       2019/6/12 11:38:22
* 作    者：       zhangyang
* 说   明：
* 修改时间：
* 修 改 人：
*************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Extends
{
 public  static class IQueryableExtension
    {
        //https://www.cnblogs.com/dabaopku/archive/2012/06/14/2549945.html
     public static IQueryable<T> OrderByField<T>(this IQueryable<T> data, string fieldName, string order)
     {
         if (fieldName == null || typeof(T).GetProperty(fieldName) == null)
             return data;

         var type = typeof(T);
         var property = type.GetProperty(fieldName);
         var param = Expression.Parameter(typeof(T), "i");
         var propertyAcess = Expression.MakeMemberAccess(param, property);
         var sortExpression = Expression.Lambda(propertyAcess, param);
         var cmd = order.ToUpper() == "ASC" ? "OrderBy" : "OrderByDescending";
         var result = Expression.Call(typeof(Queryable),cmd,new Type[] { type, property.PropertyType }, data.Expression, Expression.Quote(sortExpression));
         return data.Provider.CreateQuery<T>(result);
     }
    }
}
