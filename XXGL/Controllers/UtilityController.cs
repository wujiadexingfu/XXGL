using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Service;
using PagedList;

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


        public ActionResult GetDownOrganizationsByParentUniqueID([DefaultValue(1)]int pageNo, [DefaultValue(10)]int pageSize, string term)
        {
            try
            {
                var account=(Account)Session["Account"];
                var result = OrganizationService.GetDownOrganizationsByParentUniqueID(account.OrganizationUniqueID);
             
                if (!string.IsNullOrEmpty(term))
                {
                    result = result.Where(x => x.ID.Contains(term) || x.Name.Contains(term)).ToList();
                }

                int  count = result.Count();
                var results = result.Select(x => new { id = x.UniqueID, text = x.ID + "/" + x.Name }).Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
            
                return Json(new { results = results, total = count }, "text/plain", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var message = ex.ToString();
                return Json(new { success = false, message = "发生错误" }, "text/plain", JsonRequestBehavior.AllowGet);
            }
        }



    }
}