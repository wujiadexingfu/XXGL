using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.WebFunction
{
   public  class WebFunctionOperationModel
    {
       public string WebFunctionID { get; set; }

       public string Area { get; set; }

       public string Controller { get; set; }

       public string Action { get; set; }

       public string ParentID { get; set; }

       public string OperationID { get; set; }

    }
}
