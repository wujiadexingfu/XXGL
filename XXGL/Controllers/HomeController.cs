using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XXGL.Base.IService;

namespace XXGL.Controllers
{
    public class HomeController : Controller
    {
        IUsersService _userService{get;set;}
        public HomeController(IUsersService userService)
        {
            _userService = userService;
        }

        // GET: Home
        public ActionResult Index()
        {
           // var user = _userService.Login("admin", "系统管理员");
           var m= _userService.GetAccount("admin");

            return View();
        }

       

    }
}