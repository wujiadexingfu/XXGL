using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models.Authenticated;

namespace XXGL.Base.IService
{
  public   interface IWebFunctionsService
    {
       FunctionItem GetFunctionItem(string WebFunctionID, string Language);
    }
}
