using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXGL.Base.Service;

namespace XXGL.Controllers
{
    public class WebFunctionController : Controller
    {
        // GET: WebFunction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InitTree()
        {

            return PartialView("_Tree");
        }


        
        public ActionResult GetTreeNodes(string uniqueID)
        {
           var result= WebFunctionService.GetWebPermissionTreeNodesByParentId(uniqueID);
           return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}