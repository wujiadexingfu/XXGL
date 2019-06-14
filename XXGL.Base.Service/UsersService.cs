using DbEntity.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.Models;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Models.UserViewModel;
using System.Web.Mvc;
using System.Web;
using Utility;
using XXGL.Base.Models.Permission;
using XXGL.Base.Models.OrganizationModel;
using Extends;

namespace XXGL.Base.Service
{
    public class UsersService
    {

        /// <summary>
        /// 根据ID获取到人员信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static UserModel GetUserByID(string ID)
        {
            var db = new XXGLEntities();
            var user = db.Sys_User.FirstOrDefault(x => x.ID == ID);
            if (user != null)
            {
                return new UserModel()
                {
                    UniqueID = user.UniqueID,
                    ID = user.ID,
                    Name = user.Name,
                    PassWord = user.PassWord,
                    State = user.State,
                    IsLogin = user.IsLogin,
                    StartExpiryDate = user.StartExpiryDate,
                    EndExpiryDate = user.EndExpiryDate,
                    BirthDay = user.BirthDay,
                    Email = user.Email,
                    LastLoginTime = user.LastLoginTime,
                    MobilePhone = user.MobilePhone,
                    OrganizationUniqueID = user.OrganizationUniqueID,
                    Other = user.Other,
                    Photo = user.Photo,
                    Title = user.Title
                };
            }
            else
            {
                return null;
            }
        }

        public static UserModel GetUserByUniqueID(string UniqueID)
        {
            var db = new XXGLEntities();
            var user = db.Sys_User.FirstOrDefault(x => x.UniqueID == UniqueID);
            if (user != null)
            {
                return new UserModel()
                {
                    UniqueID = user.UniqueID,
                    ID = user.ID,
                    Name = user.Name,
                    PassWord = user.PassWord,
                    State = user.State,
                    IsLogin = user.IsLogin,
                    StartExpiryDate = user.StartExpiryDate,
                    EndExpiryDate = user.EndExpiryDate,
                    BirthDay = user.BirthDay,
                    Email = user.Email,
                    LastLoginTime = user.LastLoginTime,
                    MobilePhone = user.MobilePhone,
                    OrganizationUniqueID = user.OrganizationUniqueID,
                    Other = user.Other,
                    Photo = user.Photo,
                    Title = user.Title
                };
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 登录者的人员信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Account GetAccount(string ID)
        {

            Account account = new Account();
            var db = new XXGLEntities();
            var user = db.Sys_User.Where(x => x.ID == ID).First();  //获取人员的基本信息

            account.UniqueID = user.UniqueID;
            account.ID = user.ID;
            account.Name = user.Name;
            account.Title = user.Title;
            account.Photo = user.Photo;
            account.OrganizationUniqueID = user.OrganizationUniqueID;


            var permissionList = PermissionService.GetPermissionByUserId(user.ID);  //人员所有的操作权限

            account.PermissionList = permissionList.Select(x => new PermissionModel()
            {
                WebFunctionID = x.WebFunctionID,
                Area = x.Area,
                Controller = x.Controller,
                Action = x.Area,
                Value = x.Value
            }).Distinct().ToList();  //操作权限去重后保存在account中


            var floorWebFunctionList = permissionList.Select(x => new { x.WebFunctionID, x.WebFunctionParentID }).Distinct().ToList(); //获取到所有的菜单节点及其父节点

            var webFunctionList = WebFunctionService.GetWebFunctionList();

            foreach (var item in floorWebFunctionList)
            {
                var parentWebFunction = webFunctionList.Where(x => x.ID == item.WebFunctionParentID).FirstOrDefault();
                var currentWebFunction = webFunctionList.Where(x => x.ID == item.WebFunctionID).FirstOrDefault();

                if (parentWebFunction.ParentID == "*") //如果父节点的ID为*，则代表当前的目录是一个二级目录，不为*，则代表了是一个三级目录
                {
                    var ancestorWebFunctionItem = account.WebFunctionList.Where(x => x.ID == item.WebFunctionParentID).FirstOrDefault();  //判断该一级父节点的数据是否已经存在
                    if (ancestorWebFunctionItem == null)  //不存在则新增
                    {
                        ancestorWebFunctionItem = new WebFunction
                            {
                                ID = parentWebFunction.ID,
                                Area = string.Empty,
                                Controller = string.Empty,
                                Action = string.Empty,
                                Icon = parentWebFunction.Icon,
                                Seq = parentWebFunction.Seq,
                                Description = webFunctionList.Where(x => x.ID == item.WebFunctionParentID).FirstOrDefault().Description,
                                ParentID = parentWebFunction.ParentID
                            };

                        account.WebFunctionList.Add(ancestorWebFunctionItem);  //新增一级目录
                    }

                    var currnentWebFunctionItem = new WebFunction()
                    {
                        Area = currentWebFunction.Area,
                        Controller = currentWebFunction.Controller,
                        Action = currentWebFunction.Action,
                        Icon = currentWebFunction.Icon,
                        ID = currentWebFunction.ID,
                        Seq = currentWebFunction.Seq,
                        Description = webFunctionList.Where(x => x.ID == item.WebFunctionID).FirstOrDefault().Description,
                        ParentID = currentWebFunction.ParentID
                    };  //生成二级目录
                    ancestorWebFunctionItem.SubWebFuntionList.Add(currnentWebFunctionItem);  //将二级目录添加到一级目录中
                }
                else  //如果为空则表示当前节点为三级菜单，则从一级菜单开始遍历
                {
                    var ancestorWebFunction = webFunctionList.Where(x => x.ID == parentWebFunction.ParentID).FirstOrDefault();   //一级菜单

                    var ancestorFunctionItem = account.WebFunctionList.Where(x => x.ID == ancestorWebFunction.ID).FirstOrDefault();  //判断一级菜单是否存在于account对象中

                    if (ancestorFunctionItem == null)  //不存在则新增
                    {
                        ancestorFunctionItem = new WebFunction()
                        {
                            ID = ancestorWebFunction.ID,
                            Area = string.Empty,
                            Controller = string.Empty,
                            Action = string.Empty,
                            Description = webFunctionList.Where(x => x.ID == parentWebFunction.ParentID).FirstOrDefault().Description,
                            Seq = ancestorWebFunction.Seq,
                            Icon = ancestorWebFunction.Icon,
                            ParentID = ancestorWebFunction.ParentID
                        };
                        account.WebFunctionList.Add(ancestorFunctionItem);  //将节点添加到一级菜单中
                    }

                    var parentFunctionItem = ancestorFunctionItem.SubWebFuntionList.Where(x => x.ID == parentWebFunction.ID).FirstOrDefault();  //判断二级菜单是否存在
                    if (parentFunctionItem == null)
                    {
                        parentFunctionItem = new WebFunction()
                        {
                            Area = string.Empty,
                            Controller = string.Empty,
                            Action = string.Empty,
                            Icon = parentWebFunction.Icon,
                            Seq = parentWebFunction.Seq,
                            Description = webFunctionList.Where(x => x.ID == parentWebFunction.ID).FirstOrDefault().Description,
                            ID = parentWebFunction.ID,
                            ParentID = parentWebFunction.ParentID,
                        };
                        ancestorFunctionItem.SubWebFuntionList.Add(parentFunctionItem);
                    }

                    ///加入三级菜单
                    parentFunctionItem.SubWebFuntionList.Add(new WebFunction()
                    {
                        ID = currentWebFunction.ID,
                        Area = currentWebFunction.Area,
                        Controller = currentWebFunction.Controller,
                        Action = currentWebFunction.Action,
                        Icon = currentWebFunction.Icon,
                        Seq = currentWebFunction.Seq,
                        Description = webFunctionList.Where(x => x.ID == currentWebFunction.ID).FirstOrDefault().Description,
                        ParentID = currentWebFunction.ParentID
                    });
                }
            }


            foreach (var menu in account.WebFunctionList)
            {
                foreach (var item in menu.SubWebFuntionList)
                {
                    if (item.SubWebFuntionList != null)
                    {
                        item.SubWebFuntionList = item.SubWebFuntionList.OrderBy(x => x.Seq).ToList();
                    }
                }

                menu.SubWebFuntionList = menu.SubWebFuntionList.OrderBy(x => x.Seq).ToList();
            }

            account.WebFunctionList = account.WebFunctionList.OrderBy(x => x.Seq).ToList();





            return account;
        }

        /// <summary>
        /// 获取人员信息集合
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static List<UserGridItem> GetUserList(UserParameter Parameters, out int TotalCount)
        {
            var db = new XXGLEntities();
            var query = db.Sys_User.AsQueryable();



            if (!string.IsNullOrEmpty(Parameters.ID)) //查询ID
            {
                query = query.Where(x => x.ID == Parameters.ID);
            }

            if (!string.IsNullOrEmpty(Parameters.Name))
            {
                query = query.Where(x => x.Name.Contains(Parameters.Name));
            }
            if (!string.IsNullOrEmpty(Parameters.OrganizationUniqueID))
            {
                //  query = query.Where(x => x.OrganizationUniqueID == Parameters.OrganizationUniqueID);
            }

            TotalCount = query.Count();  //获取总共的数量

           var result= query.OrderByField<Sys_User>(Parameters.OrderName, Parameters.Order).Skip(Parameters.Offset).Take(Parameters.Limit).ToList();


            List<UserGridItem> list = new List<UserGridItem>();
            foreach (var item in result)
            {
                UserGridItem userGridItem = new UserGridItem();
                userGridItem.UniqueID = item.UniqueID;
                 userGridItem.ID = item.ID;
                userGridItem.Name = item.Name;
                userGridItem.LastLoginTime = item.LastLoginTime;
                //  OrganizationUniqueID = item.OrganizationUniqueID,
               userGridItem. Photo =item.Photo;
                userGridItem.State = item.State;
                userGridItem.Title = item.Title;
                userGridItem.Email = item.Email;
                userGridItem.MobilePhone = item.MobilePhone;
                userGridItem.IsLogin = item.IsLogin;
                if (item.StartExpiryDate.HasValue && DateTime.Compare(item.StartExpiryDate.Value, DateTime.Now) > 0)
                {
                    userGridItem.ExpiryDateStatus = "未生效"; //未生效
                     userGridItem.ExpiryDateStatusColor="bg-yellow";
                   
                }
                else {
                    if (item.EndExpiryDate.HasValue && DateTime.Compare(DateTime.Now, item.EndExpiryDate.Value) > 0)
                    {
                        userGridItem.ExpiryDateStatus = "已失效"; //已失效
                         userGridItem.ExpiryDateStatusColor="bg-red";
                    }
                    else
                    {
                        userGridItem.ExpiryDateStatus = "正常"; //正常
                        userGridItem.ExpiryDateStatusColor = "bg-light-blue";
                    }
                }
                list.Add(userGridItem);
                
            }
            return list;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UniqueID"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public static RequestResult ChangePassword(string UniqueID, string NewPassword)
        {
            var db = new XXGLEntities();
            RequestResult result = new RequestResult();
            try
            {

                var user = db.Sys_User.Where(x => x.UniqueID == UniqueID).FirstOrDefault();
                user.PassWord = NewPassword;
                db.SaveChanges();
                result.RetunSuccessMessage("修改成功");
            }
            catch (Exception ex)
            {
                result.ReturnFailMessage("修改失败");
            }
            return result;
        }

        /// <summary>
        /// 根据UniqueID获取人员信息
        /// </summary>
        /// <param name="uniqueID"></param>
        /// <returns></returns>
        public static EditUserInputFormViewModel GetEditUserInputFormByUniqueID(string uniqueID)
        {

            var db = new XXGLEntities();
            var user = db.Sys_User.Where(x => x.UniqueID == uniqueID).FirstOrDefault();
            EditUserInputFormViewModel editUserInputFormViewModel = new EditUserInputFormViewModel()
            {
                UniqueID = user.UniqueID,
                ID = user.ID,
                Name = user.Name,
                Email = user.Email,
                BirthDay = user.BirthDay,
                MobilePhone = user.MobilePhone,
                OrganizationUniqueID = user.OrganizationUniqueID,
                Other = user.Other,
                Title = user.Title,
                IsLogin=user.IsLogin,
                StartExpiryDate=user.StartExpiryDate,
                EndExpiryDate=user.EndExpiryDate
            };
            return editUserInputFormViewModel;
        }

        public static RequestResult EditUser(EditUserInputFormViewModel editUserInputFormViewModel)
        {
            RequestResult result = new RequestResult();
            var db = new XXGLEntities();
            var existsUserID = db.Sys_User.Where(x => x.ID == editUserInputFormViewModel.ID && x.UniqueID != editUserInputFormViewModel.UniqueID).FirstOrDefault();
            if (existsUserID != null)
            {
                result.ReturnFailMessage("用户编号重复");  //用户编号重复
            }
            else
            {
                var user = db.Sys_User.Where(x => x.UniqueID == editUserInputFormViewModel.UniqueID).FirstOrDefault();
                user.ID = editUserInputFormViewModel.ID;
                user.Name = editUserInputFormViewModel.Name;
                user.Email = editUserInputFormViewModel.Email;
                user.MobilePhone = editUserInputFormViewModel.MobilePhone;
                user.OrganizationUniqueID = editUserInputFormViewModel.OrganizationUniqueID;
                user.Other = user.Other;
                user.Title = user.Title;
                user.ModifyTime = DateTime.Now;
                user.ModifyUser = (HttpContext.Current.Session["Account"] as Account).UniqueID;  //需要调用System.Web这个程序集才能调用到Session 而且世界使用Session是错误的

                db.SaveChanges();
                result.RetunSuccessMessage(Resources.Resource.EditSuccess);
            }
            return result;
        }

        public static RequestResult RevertUser(string uniqueID)
        {

            RequestResult result = new RequestResult();
            try
            {
                var db = new XXGLEntities();
                var user = db.Sys_User.Where(x => x.UniqueID == uniqueID).FirstOrDefault();
                user.State = true;
                user.ModifyTime = DateTime.Now;
                user.ModifyUser = (HttpContext.Current.Session["Account"] as Account).UniqueID;
                db.SaveChanges();
                result.RetunSuccessMessage("修改成功");
            }
            catch (Exception ex)
            {
                result.ReturnFailMessage("修改失败");
            }
            return result;
        }

        public static RequestResult Delete(List<string> selectedUniqueIDs)
        {
            RequestResult result = new RequestResult();
            try
            {
                var db = new XXGLEntities();
                var userList = db.Sys_User.Where(x => selectedUniqueIDs.Contains(x.UniqueID));

                db.Sys_User.RemoveRange(userList);
                db.SaveChanges();

                result.RetunSuccessMessage("删除成功");

            }
            catch (Exception ex)
            {
                result.ReturnFailMessage("删除失败");

            }

            return result;
        }

        public static RequestResult Create(CreateUserInputFormViewModel createUserInputFormViewModel)
        {
            RequestResult result = new RequestResult();

            try
            {
                var db = new XXGLEntities();
                var exitsUser = db.Sys_User.Where(x => x.ID == createUserInputFormViewModel.CreateUerID).FirstOrDefault();
                if (exitsUser != null)
                {
                    result.ReturnFailMessage("用户编号重复");  //用户编号重复
                }
                else
                {
                    Sys_User user = new Sys_User();
                    user.UniqueID = Guid.NewGuid().ToString();
                    user.ID = createUserInputFormViewModel.CreateUerID;
                    user.Name = createUserInputFormViewModel.CreateUerName;
                    user.OrganizationUniqueID = createUserInputFormViewModel.CreateUserOrganizationUniqueID;
                    user.BirthDay = createUserInputFormViewModel.CreateUerBirthDay;
                    user.Email = createUserInputFormViewModel.CreateUerEmail;
                    user.MobilePhone = createUserInputFormViewModel.CreateUerMobilePhone;
                    user.Other = createUserInputFormViewModel.CreateUerOther;
                    user.PassWord = Define.InitialPassword;
                    user.State = true;
                    user.Title = createUserInputFormViewModel.CreateUerTitle;
                    db.Sys_User.Add(user);
                    db.SaveChanges();
                    result.RetunSuccessMessage("新增成功");
                }
            }
            catch (Exception ex)
            {

                result.ReturnFailMessage("新增失败");
            }

            return result;
        }



        /// <summary>
        /// 获取人员信息集合
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static List<OrganizationUserGridItem> GetUserListByOrganizationUniqueID(OrganizationUserParameter Parameters, out int TotalCount)
        {
            var db = new XXGLEntities();
            var query = (from user in db.Sys_User
                         select new OrganizationUserGridItem
                         {
                            ID=user.ID,
                            Name=user.Name,
                            State=user.State,
                            Title=user.Title,
                            OrganizationUniqueID=user.OrganizationUniqueID

                         });

            if (!string.IsNullOrEmpty(Parameters.OrganizationUniqueID))
            {
                 query = query.Where(x => x.OrganizationUniqueID == Parameters.OrganizationUniqueID);
            }

            if (!string.IsNullOrEmpty(Parameters.ID))
            {
                query = query.Where(x => x.ID.Contains(Parameters.ID) || x.Name.Contains(Parameters.ID));
            }


            TotalCount = query.Count();  //获取总共的数量

            query = query.OrderBy(x => x.ID).Skip((Parameters.PageNo - 1) * Parameters.PageSize).Take(Parameters.PageSize); //获取需要的分页数据



            return query.ToList() ;
        }




    }
}
