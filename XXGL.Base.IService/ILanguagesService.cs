using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models;

namespace XXGL.Base.IService
{
   public  interface ILanguagesService
    {
       List<SelectItem> GetLanguages(int pageSize, int pageNo, string value, out int totalCount);
    }
}
