using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;


/********************************************************************************

** 类名称：  InputHelperExtension

** 描述：Html扩展方法

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-21 10:45:31

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace Utility.Extension
{
   public static class InputHelperExtension
    { /// <summary>
      /// 该方法用于隐藏类中的所有数据
        /// http://lastattacker.blogspot.tw/2011/11/mvc-2-hidden-helper-extension.html
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString HiddenEntirelyFor<TModel, TResult>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            ModelMetadata modelMetadata =
               ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            List<string> htmlEntries =
               modelMetadata.Properties
                  .Select(property => htmlHelper.Hidden(ExpressionHelper.GetExpressionText(expression) + "." + property.PropertyName, property.Model, null))
                  .Select(mvcHtmlString => mvcHtmlString.ToHtmlString())
                  .ToList();
            return MvcHtmlString.Create(String.Join(Environment.NewLine, htmlEntries.ToArray()));
        }
    }
}
