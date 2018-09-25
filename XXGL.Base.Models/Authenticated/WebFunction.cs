using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************

** 类名称：  WebFunction

** 描述：********

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-09-17 19:47:37

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.42000      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Base.Models.Authenticated
{
  public  class WebFunction
    {
        /// <summary>
       /// ID
       /// </summary>
        public string ID { get; set; }

       /// <summary>
       /// 父ID
       /// </summary>
        public string ParentID { get; set; }

       /// <summary>
       /// 区域
       /// </summary>
        public string Area { get; set; }

       /// <summary>
       /// 控制器
       /// </summary>
        public string Controller { get; set; }

       /// <summary>
       /// 动作
       /// </summary>
        public string Action { get; set; }
      
       /// <summary>
       /// 图标
       /// </summary>
        public string Icon { get; set; }

       /// <summary>
       /// 排序
       /// </summary>
        public int? Seq { get; set; }

       /// <summary>
       /// 描述
       /// </summary>
        public string Description { get; set; }

       /// <summary>
       /// 操作菜单
       /// </summary>

        public List<WebFunction> SubWebFuntionList { get; set; }


        public WebFunction()
       {
           SubWebFuntionList = new List<WebFunction>();
       }
    }
}
