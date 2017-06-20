using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Models.WebFunction;

namespace XXGL.Base.IService
{
  public   interface IWebFunctionsService
    {
      string GetWebFunctionDescription(string WebFunctionID, string Language);

      List<WebFunctionOperationModel> GetWebFunctionsByUserUnqiueID(string UserUniqueID);
    }
}
