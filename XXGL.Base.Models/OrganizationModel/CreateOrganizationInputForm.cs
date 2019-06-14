using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.OrganizationModel
{
   public  class CreateOrganizationInputForm
    {
        /// <summary>
        /// 组织编号
        /// </summary>
        [Display(Name = "组织编号")]
        [Required(ErrorMessage = "{0}必填")]
        public string CreateOrganizationID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// </summary>
        [Display(Name = "组织名称")]
        [Required(ErrorMessage = "{0}必填")]
        public string CreateOrganizationName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标")]
        public string CreateOrganizationIcon { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        [Display(Name = "父节点")]
        [Required(ErrorMessage = "{0}必填")]
        public string CreateOrganizationParentUniqueID { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [Display(Name = "负责人")]
        public string CreateOrganizationManagerUniqueID { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Required(ErrorMessage = "{0}必填")]
        public string CreateOrganizationSeq { get; set; }
    }
}
