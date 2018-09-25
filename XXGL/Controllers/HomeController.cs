using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Utility;
using XXGL.Base.Models;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Models.HomeViewModel;
using XXGL.Base.Service;

namespace XXGL.Controllers
{
    public class HomeController : Controller
    {
   
        public ActionResult Index()
        {
            var user = UsersService.GetAccount("admin");
            Session["Account"] = user;
         //  HttpCookie authcookie = Request.Cookies[FormsAuthentication.FormsCookieName];
         // FormsAuthenticationTicket authticket = FormsAuthentication.Decrypt(authcookie.Value);

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
            return View(new LoginFormModel() {  UserID="admin", PassWord="admin"});
        }

        [AllowAnonymous]
        [HttpPost]
          public ActionResult Login(LoginFormModel model)
          {
              if (Session["ValidateCode"] != null && Convert.ToString(Session["ValidateCode"]).ToLower() == model.VerficationCode.ToLower())
              {
                  var user = UsersService.GetUserByID(model.UserID);
                  if (user != null)
                  {
                      if (user.PassWord == model.PassWord)
                      {
                          if (user.State)
                          {

                              if (!user.IsLogin)
                              {
                                  ModelState.AddModelError("UserID", "该用户不允许登录");   //该用户不允许登录
                                  return View();
                              }
                              else
                              {
                                  if (user.StartExpiryDate.HasValue && DateTime.Compare(user.StartExpiryDate.Value, DateTime.Now) > 0)
                                  {
                                      ModelState.AddModelError("UserID", "该用户有效期开始时间为:" + user.StartExpiryDate.Value.ToString("yyyy/MM/dd"));
                                      return View();
                                  }
                                  else
                                  {
                                      if (user.EndExpiryDate.HasValue && DateTime.Compare(user.EndExpiryDate.Value, DateTime.Now) < 0)
                                      {
                                          ModelState.AddModelError("UserID", "该用户已失效");
                                          return View();
                                      }
                                      else
                                      {
                                          var account = UsersService.GetAccount(model.UserID);
                                          Session["Account"] = account;

                                          var ticket = new FormsAuthenticationTicket(1, account.ID, DateTime.Now, DateTime.Now.AddHours(24), true, account.ID, FormsAuthentication.FormsCookiePath);
                                          string encTicket = FormsAuthentication.Encrypt(ticket);
                                          Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                                          return RedirectToAction("Index");
                                      }
                                  }
                              }
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
              int width = 80;
              int height = 30;
            int fontsize = 15;
            string code = string.Empty;
            byte[] bytes = ValidateCode.CreateValidateGraphic(out code, 4, width, height, fontsize);
            Session["ValidateCode"] = code;
            return File(bytes, @"image/jpeg");
          }


          public ActionResult ChangePassword()
          {
              return View(new PasswordInputFormViewModel());
          }

        public ActionResult EditPassword(PasswordInputFormViewModel passwordInputFormViewModel)
        {

            var uniqueID = (Session["Account"] as Account).UniqueID;
            var result = UsersService.ChangePassword(uniqueID, "123");
            return Json(JosnNetHelper.ObjectToJson<RequestResult>(result));
        }





    }
}