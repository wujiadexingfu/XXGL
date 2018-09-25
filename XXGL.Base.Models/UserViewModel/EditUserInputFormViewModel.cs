using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************

** 类名称：  EditUserInputFormViewModel

** 描述：********

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-11 19:09:17

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Base.Models.UserViewModel
{
  public  class EditUserInputFormViewModel
    {     
      /// <summary> 
        ///   Guid
        /// </summary> 
          [Required(ErrorMessage = "{0}必填")]
        public string UniqueID { get; set; }

        /// <summary> 
        ///   用户编号
        /// </summary> 
        [Display(Name = "用户编号")]
        [Required(ErrorMessage = "{0}必填")]
  
        public string ID { get; set; }

        /// <summary> 
        ///   用户名称
        /// </summary> 
         [Display(Name = "用户名称")]
         [Required(ErrorMessage = "{0}必填")]
        public string Name { get; set; }

        /// <summary> 
        ///   组织ID
        /// </summary> 
         [Display(Name = "部门")]
         [Required(ErrorMessage = "{0}必填")]
        public string OrganizationUniqueID { get; set; }

        /// <summary> 
        ///   邮件地址
        /// </summary> 
         [Display(Name = " 邮件地址")]
         [RegularExpression("^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\\.[a-zA-Z0-9_-]+)+$", ErrorMessage = "{0}格式错误")]
        public string Email { get; set; }

        /// <summary> 
        ///   出生日期
        /// </summary> 
        [Display(Name = "出生日期")]
        public DateTime? BirthDay { get; set; }


        /// <summary> 
        ///   职称
        /// </summary> 
        [Display(Name = "职称")]
        public string Title { get; set; }

        /// <summary> 
        ///   电话
        /// </summary> 
          [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)", ErrorMessage = "{0}格式错误")]
          [Display(Name = "电话")]
        public string MobilePhone { get; set; }

        /// <summary> 
        ///   其他
        /// </summary>
        [Display(Name = "其他")]
        public string Other { get; set; }

      /// <summary>
      /// 有效开始日期
      /// </summary>
        [Display(Name = " 有效开始日期")]
        public DateTime? StartExpiryDate { get; set; }


        /// <summary>
        /// 有效结束日期
        /// </summary>
         [Display(Name = " 有效结束日期")]
        public DateTime? EndExpiryDate { get; set; }

      /// <summary>
      /// 是否可以登录
      /// </summary>
         [Display(Name = " 是否可以登录")]
         [Required(ErrorMessage = "{0}必填")]
         public bool IsLogin { get; set; }

      
    }
}
