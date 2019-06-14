using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.OrganizationModel
{
  public   class OrganizationItem
    {
        public string UniqueID { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        [Display(Name = "部门编号")]
        public string ID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Display(Name = "部门名称")]
        public string Name { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [Display(Name = "负责人")]
        public string Manager { get; set; }

        /// <summary>
        /// 创建人员
        /// </summary>
        [Display(Name = "创建人员")]
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentUniqueId { get; set; }

    }
}
