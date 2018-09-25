using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models.CommonModel;

namespace XXGL.Base.Models.OrganizationModel
{
   public  class OrganizationUserParameter: BasePageModel
    {

        [Display(Name = "用户编号/姓名")]
        public string ID { get; set; }


        public string OrganizationUniqueID { get; set; }

    }
}
