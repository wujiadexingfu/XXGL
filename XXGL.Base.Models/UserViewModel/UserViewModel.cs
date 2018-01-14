using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;


namespace XXGL.Base.Models.UserViewModel
{
    public  class UserViewModel
    {
        public UserParameter Parameter { get; set; }

        public PagedList.StaticPagedList<UserGridItem> GridItem { get; set; }

        public UserViewModel()
        {
            Parameter = new UserParameter();
        }
    }
}
