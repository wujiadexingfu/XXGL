using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.OrganizationModel
{
   public  class OrganizationUserGridItem
    {
        /// <summary> 
        ///   编号
        /// </summary> 
        public string ID { get; set; }

        /// <summary> 
        ///   名字
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 组织的GUID
        /// </summary>
        public string OrganizationUniqueID { get; set; }

        /// <summary> 
        ///   职称
        /// </summary> 
        public string Title { get; set; }

       
        /// <summary> 
        ///   状态
        /// </summary> 
        public bool  State { get; set; }

    }
}
