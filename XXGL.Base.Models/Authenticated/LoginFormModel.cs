using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.Authenticated
{
  public   class LoginFormModel
    {
      /// <summary>
      /// 用户名
      /// </summary>
      [Required(ErrorMessageResourceName = "UserIDRequired", ErrorMessageResourceType = typeof(Resource))]
      public string UserID { get; set; }

      /// <summary>
      /// 登陆密码
      /// </summary>
        [Required(ErrorMessageResourceName = "PassWordRequired", ErrorMessageResourceType = typeof(Resource))]
      public string PassWord { get; set; }

      /// <summary>
      /// 验证码
      /// </summary>
     [Required(ErrorMessageResourceName = "VerficationCodeRequired", ErrorMessageResourceType = typeof(Resource))]
      public string VerficationCode { get; set; }
    }
}
