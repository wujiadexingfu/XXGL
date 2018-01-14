using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************

** 类名称：  PasswordInputFormViewModel

** 描述：修改密码的类

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-07 21:52:50

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Base.Models.HomeViewModel
{
   public class PasswordInputFormViewModel
    {
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Resource))]
        [StringLength( 16,MinimumLength=5, ErrorMessageResourceName = "PasswordRange", ErrorMessageResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resource))]
       public string NewPassword { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resource))]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceName = "CompareNewPassword", ErrorMessageResourceType = typeof(Resources.Resource))]
       public string ConfirmPassword { get; set; }
    }
}
