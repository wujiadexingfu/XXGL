using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.WebFunction
{
   public  class WebFunctionTreeNode
    {
        /// <summary>
      /// 唯一编码
      /// </summary>
        [JsonProperty(PropertyName = "uniqueid")]
        public string UniqueID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 是否包含子节点
        /// </summary>
        [JsonProperty(PropertyName = "isParent")]
        public bool IsParent { get; set; }

        /// <summary>
        /// 图案
        /// </summary>
        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        [JsonProperty(PropertyName = "isOpen")]
        public bool IsOpen { get; set; }

    }
}
