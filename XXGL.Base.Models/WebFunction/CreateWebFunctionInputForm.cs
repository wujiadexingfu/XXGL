using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.WebFunction
{
   public class CreateWebFunctionInputForm
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "菜单编号")]
        [Required(ErrorMessage ="{0}必填")]
        public string CreateWebFunctionId { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        [Display(Name = "父节点")]
        [Required(ErrorMessage = "{0}必填")]
        public string CreateWebFunctionParentId { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Display(Name = "区域")]
        public string CreateWebFunctionArea { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [Display(Name = "控制器")]
        public string CreateWebFunctionController { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        [Display(Name = "Action")]
        public string CreateWebFunctionAction { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标")]
        public string CreateWebFunctionIcon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Required(ErrorMessage = "{0}必填")]
        public int CreateWebFunctionSeq { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Display(Name = "是否显示")]
        public bool CreateWebFunctionIsDisplay { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [Required(ErrorMessage = "{0}必填")]
        public string CreateWebFunctionDescription { get; set; }
    }
}
