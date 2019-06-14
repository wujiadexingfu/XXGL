using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.WebFunction
{
   public class EditWebFunctionInputForm
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "菜单编号")]
        [Required(ErrorMessage = "{0}必填")]
        public string EditWebFunctionId { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        [Display(Name = "父节点")]
        [Required(ErrorMessage = "{0}必填")]
        public string EditWebFunctionParentId { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Display(Name = "区域")]
        public string EditWebFunctionArea { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [Display(Name = "控制器")]
        public string EditWebFunctionController { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        [Display(Name = "Action")]
        public string EditWebFunctionAction { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标")]
        public string EditWebFunctionIcon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Required(ErrorMessage = "{0}必填")]
        public int? EditWebFunctionSeq { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Display(Name = "是否显示")]
        public bool EditWebFunctionIsDisplay { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [Required(ErrorMessage = "{0}必填")]
        public string EditWebFunctionDescription { get; set; }
    }
}
