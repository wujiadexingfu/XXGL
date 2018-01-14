using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************

** 类名称：  EntityValidationErrorExtensions

** 描述：记得在Nuget中增加EF，因为System.Data.Entity.Validation 程序集属于EF 的
**  该类主要是用于EF更新失败了可以查看详细的错误信息
 *将错误信息汇总成IEnumerable<string>集合

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-09 22:35:45

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace Utility.Extension
{
   public static  class EntityValidationErrorExtensions
    {

        /// <summary>
        /// 将错误信息汇总成IEnumerable<string>集合
        /// </summary>
        /// <param name="Exception"></param>
        /// <returns></returns>
       public static IEnumerable<string> GetErrorMessage(this DbEntityValidationException Exception)
       {
           List<string> list = new List<string>();
           string errorMessageFromat = "栏位:{0},错误信息:{1}";
           foreach (var validationErrors in Exception.EntityValidationErrors)
           {
               foreach (var validationError in validationErrors.ValidationErrors)
               {
                   list.Add(string.Format(errorMessageFromat, validationError.PropertyName, validationError.ErrorMessage));
               }
           }
           return list;
       }
    }
}
