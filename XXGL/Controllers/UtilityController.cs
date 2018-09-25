using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XXGL.Controllers
{
    public class UtilityController : Controller
    {

        //public ActionResult GetLanguage([DefaultValue(1)]int pageNo, [DefaultValue(10)]int pageSize, string term)
        //{
        //    try
        //    {
        //        int totalCount = 0;
        //        var result= _LanguagesService.GetLanguages(pageSize, pageNo, term, out totalCount);
        //        var results = result.Select(x => new { id = x.ID, text = x.Text }).ToList();
        //        return Json(new { results = results, totalCount = totalCount }, "text/plain", JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return Json(new { success = false, message = "发生错误" }, "text/plain", JsonRequestBehavior.AllowGet);
        
       // }
    }
}