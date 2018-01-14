using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


/********************************************************************************

** 类名称：  ModelStateExtensions

** 描述：ModelStateDictionary扩展方法

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-14 09:51:13

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace Utility.Extension
{
  public static  class ModelStateExtensions
    {
        /// <summary>
        /// 将控制器中的ModelState错误信息变为IEnumerable集合获取出来
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetErrorMessages(this ModelStateDictionary source)
        {
            return source.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        /// <summary>
        /// 将错误信息汇总成一个字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetErrorsString(this ModelStateDictionary source)
        {
            return string.Join(";", GetErrorMessages(source));
        }

        /// <summary>
        /// 将键值对信息变为string类型然后汇总为IEnumerable集合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetErrorMessagesWithKey(this ModelStateDictionary source)
        {
            List<string> list = new List<string>();
            var keys = source.Keys;  //键集合
            var values = source.Values;//值集合
            for (int i = 0; i < keys.Count(); i++)
            {
                foreach (var error in values.ElementAt(i).Errors)
                {
                    list.Add(keys.ElementAt(i) + ":" + error.ErrorMessage); //样式 key:value
                }
            }
            return list;
        }


      /// <summary>
      /// 带有换行符和1,2,3的信息
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public static string GetErrorsStringWithNumber(this ModelStateDictionary source)
        {
            string result = null;
            var errorMessage = GetErrorMessages(source);
            for (int i = 0; i < errorMessage.Count(); i++)
            {
                result += (i+1).ToString() +"."+ errorMessage.ElementAt(i)+"</br>";
            }
            return result;

        }
    }
}
