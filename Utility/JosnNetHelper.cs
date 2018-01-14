using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************

** 类名称：  JosnNetHelper

** 描述：********

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-09 22:13:58

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

/// <summary>
/// reference ：http://www.cnblogs.com/litian/p/3870975.html
/// reference ：http://www.cnblogs.com/JuneZhang/p/5114439.html
/// </summary>

namespace Utility
{
   public  class JosnNetHelper
    {
        /// [DefaultValue(value)]  默认值
        /// [JsonIgnore]  忽略
        /// [JsonProperty(PropertyName = "name")]  //输出的值
        ///  [JsonProperty] //用于private类型
        // [JsonObject(Newtonsoft.Json.MemberSerialization.OptIn)]  //用于整个类，配合JsonIgnore，主要用于显示少量的属性


        /// <summary>
        /// 将对象变为json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJson<T>(T t)
        {

            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter(); //设置日期的格式
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            return JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented, timeFormat);
            //return JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented); //带有缩进输出

            //return  JsonConvert.SerializeObject(t,
            //    Newtonsoft.Json.Formatting.Indented, //缩进   
            //new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });

            // new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }  小驼峰
        }

        /// <summary>
        /// 将字符串变为对象或者集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
