using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.Authenticated
{
   public  class FunctionItem
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

        public List<FunctionItem> SubFunctionItemList { get; set; }


       public FunctionItem()
       {
           SubFunctionItemList = new List<FunctionItem>();
       }
       
    }
}
