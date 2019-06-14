using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models.CommonModel;

namespace XXGL.Base.Models.UserViewModel
{
    public class UserParameter : BasePageParameter
    {

        //<Reference>https://www.cnblogs.com/artech/p/asp-net-mvc-validation-programming.html</Reference>
        ////[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Resource))]
        //  [Domain("M", "F", "m", "f", ErrorMessageResourceName = "Domain", ErrorMessageResourceType = typeof(Resources))]
         //[Range(18, 25, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(Resources))]

       [Display(Name = "UserID", ResourceType = typeof(Resources.Resource))]
        public string ID { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "OrganizationID", ResourceType = typeof(Resources.Resource))]
        public string OrganizationUniqueID { get; set; }
    }
}
