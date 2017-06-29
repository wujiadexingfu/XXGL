using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using XXGL.Base.IService;
using XXGL.Base.Models.Authenticated;

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
      

            return View();
        }


        public ActionResult Test()
        {
            return View();
        }

          [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginFormModel());
        }

        [AllowAnonymous]
        [HttpPost]
          public ActionResult Login(LoginFormModel model)
          {
              if (Session["ValidateCode"] != null && Convert.ToString(Session["ValidateCode"]).ToLower() == model.VerficationCode.ToLower())
              {
                  var user = _userService.GetUser(model.UserID);
                  if (user != null)
                  {
                      if (user.PassWord == model.PassWord)
                      {
                          if (user.State)
                          {
                              var account = _userService.GetAccount(model.UserID);
                              Session["Account"] = account;
                              return RedirectToAction("Index");
                          }
                          else
                          {
                              ModelState.AddModelError("PassWord", Resources.Resource.UserInvalid);   //用户失效
                              return View();
                          }
                      }
                      else
                      {
                          ModelState.AddModelError("PassWord", Resources.Resource.PassWordError);   //密码错误
                          return View();
                      }
                  }
                  else
                  {
                      ModelState.AddModelError("UserID", Resources.Resource.NoUser);   //用户不存在
                      return View();
                  }
              }
              else
              {
                  ModelState.AddModelError("VerficationCode", Resources.Resource.VerficationCodeError);
                  return View();
              }
          }


          public ActionResult GetValidateCode()
          {
              int width = 100;
              int height = 40;
            int fontsize = 20;
            string code = string.Empty;
            byte[] bytes = ValidateCode.CreateValidateGraphic(out code, 4, width, height, fontsize);
            Session["ValidateCode"] = code;
            return File(bytes, @"image/jpeg");
          }


    }
}