using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models.Authenticated
{
     public class WebFunctionOperation
    {
         public string Operation { get; set; }

         public string WebFunctionID{get;set;}

         public string Area{get;set;}

         public string Controller { get; set; }

         public string Action { get; set; }

    }
}
