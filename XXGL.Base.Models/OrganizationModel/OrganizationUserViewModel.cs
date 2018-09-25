using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.OrganizationModel
{
    public class OrganizationUserViewModel
    {
        public OrganizationUserParameter Parameter { get; set; }

        public PagedList.StaticPagedList<OrganizationUserGridItem> GridItem { get; set; }

        public OrganizationUserViewModel()
        {
            Parameter = new OrganizationUserParameter();
        }

    }
}
