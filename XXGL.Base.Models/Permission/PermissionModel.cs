using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************

** 类名称：  PermissionModel

** 描述：********

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-09-17 19:34:44

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.42000      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Base.Models.Permission
{
  public   class PermissionModel
    {
        /// <summary> 
        ///   唯一编码
        /// </summary> 
        public string UniqueID { get; set; }

        /// <summary> 
        ///   区域
        /// </summary> 
        public string Area { get; set; }

        /// <summary> 
        ///   控制器
        /// </summary> 
        public string Controller { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        public string Action { get; set; }

        /// <summary> 
        ///   描述
        /// </summary> 
        public string Description { get; set; }

        /// <summary> 
        ///   值
        /// </summary> 
        public string Value { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        public int? Seq { get; set; }

        /// <summary> 
        ///   排序
        /// </summary> 
        public bool IsDelete { get; set; }

        /// <summary> 
        ///  WebFunction的ID
        /// </summary> 
        public string WebFunctionID { get; set; }

      /// <summary>
      /// WebFunction的父节点
      /// </summary>
        public string WebFunctionParentID { get; set; }
    }
}
