﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************

** 类名称：  CreateUserInputFormViewModel

** 描述：********

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-24 22:12:26

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Base.Models.UserViewModel
{
  public  class CreateUserInputFormViewModel
    {
      /// <summary> 
        ///   Guid
        /// </summary> 
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string CreateUerUniqueID { get; set; }

        /// <summary> 
        ///   用户编号
        /// </summary> 
        [Display(Name = "UserID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resource))]

        public string CreateUerID { get; set; }

        /// <summary> 
        ///   名字
        /// </summary> 
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string CreateUerName { get; set; }

        /// <summary> 
        ///   组织ID
        /// </summary> 
        [Display(Name = "Organization", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string CreateUserOrganizationUniqueID { get; set; }

        /// <summary> 
        ///   邮件地址
        /// </summary> 
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [RegularExpression("^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\\.[a-zA-Z0-9_-]+)+$", ErrorMessageResourceName = "EmailWorngFormatter", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string CreateUerEmail { get; set; }

        /// <summary> 
        ///   生日
        /// </summary> 
        [Display(Name = "BirthDay", ResourceType = typeof(Resources.Resource))]
        public DateTime? CreateUerBirthDay { get; set; }


        /// <summary> 
        ///   职称
        /// </summary> 
        [Display(Name = "Title", ResourceType = typeof(Resources.Resource))]
        public string CreateUerTitle { get; set; }

        /// <summary> 
        ///   电话
        /// </summary> 
        [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)", ErrorMessageResourceName = "MobilePhoneWorngFormatter", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Display(Name = "MobilePhone", ResourceType = typeof(Resources.Resource))]
        public string CreateUerMobilePhone { get; set; }

        /// <summary> 
        ///   其他
        /// </summary>
        [Display(Name = "Other", ResourceType = typeof(Resources.Resource))]
        public string CreateUerOther { get; set; }

        /// <summary>
        /// 有效开始日期
        /// </summary>
        [Display(Name = " 有效开始日期")]
        public DateTime? CreateUerStartExpiryDate { get; set; }


        /// <summary>
        /// 有效结束日期
        /// </summary>
        [Display(Name = " 有效结束日期")]
        public DateTime? CreateUerEndExpiryDate { get; set; }

        /// <summary>
        /// 是否可以登录
        /// </summary>
        [Display(Name = " 是否可以登录")]
        [Required(ErrorMessage = "{0}必填")]
        public bool CreateUerIsLogin { get; set; }

    }
}
