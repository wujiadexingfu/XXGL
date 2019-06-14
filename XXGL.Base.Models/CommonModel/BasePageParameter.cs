using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.CommonModel
{
  public  class BasePageParameter
    {
        public int   Offset { get; set; }

        public int  Limit { get; set; }

        public string Order { get; set; }

        public string OrderName { get; set; }



    }
}
