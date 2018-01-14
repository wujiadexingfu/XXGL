using DbEntity.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.IDAL;
using XXGL.Base.IService;
using XXGL.Base.Models;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Models.UserViewModel;
using XXGL.Interface;
using System.Web.Mvc;
using System.Web;


namespace XXGL.Base.Service
{
    public class UsersService : IUsersService
    {
        public static IUsersRepository _usersRepository { get; set; }

        public IWebFunctionsRepository _webFunctionsRepository { get; set; }

        public ILanguagesRepository _languagesRepository { get; set; }

        public IWebFunctionsService _webFunctionsService { get; set; }

        public IOrganizationRepository _organizationRepository { get; set; }

        public IUnitOfWork _unitOfWork { get; set; }



        public UsersService(IUsersRepository usersRepository, IWebFunctionsRepository webFunctionsRepository, IWebFunctionsService webFunctionsService, ILanguagesRepository languagesRepository, IOrganizationRepository organizationRepository, IUnitOfWork unitOfWork)
        {
            _usersRepository = usersRepository;
            _webFunctionsRepository = webFunctionsRepository;
            _webFunctionsService = webFunctionsService;
            _languagesRepository = languagesRepository;
            _organizationRepository = organizationRepository;
            _unitOfWork = unitOfWork;
        }


        public UserModel GetUser(string ID)
        {
            var user = _usersRepository.GetEntity(x => x.ID == ID);
            if (user != null)
            {
                return new UserModel() { ID = user.ID, Name = user.Name, PassWord = user.PassWord, State = user.State };
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
        public Account GetAccount(string ID)
        {
            Account account = new Account();
            var user = _usersRepository.GetEntity(x => x.ID == ID);

            account.UniqueID = user.UniqueID;
            account.ID = user.ID;
            account.Name = user.Name;
            account.Title = user.Title;
            account.LanuageUniqueID = user.Lanuage;
            account.Photo = user.Photo;
            account.LanuageID = _languagesRepository.GetEntity(x => x.UniqueID == user.Lanuage).ID;

            var WebFunctionsOperations = _webFunctionsService.GetWebFunctionsByUserUnqiueID(user.UniqueID);

            account.WebFunctionOperationList = WebFunctionsOperations.Select(x => new WebFunctionOperation()
            {
                WebFunctionID = x.WebFunctionID,
                Area = x.Area,
                Controller = x.Controller,
                Action = x.Area,
                Operation = x.OperationID
            }).ToList();


            var webFunctionsDistinct = WebFunctionsOperations.Select(x => new
            {
                WebFunctionID = x.WebFunctionID,
                ParentID = x.ParentID
            }).Distinct().ToList(); //获取到所有的子菜单，以便后续的查询


            foreach (var item in webFunctionsDistinct)
            {
                var parentWebFunction = _webFunctionsRepository.GetEntity(x => x.ID == item.ParentID);
                var currentWebFunction = _webFunctionsRepository.GetEntity(x => x.ID == item.WebFunctionID);

                if (parentWebFunction.ParentID == "*") //如果不为空则表示当前节点为二级节点
                {
                    var ancestorFunctionItem = account.FunctionItemList.Where(x => x.ID == item.ParentID).FirstOrDefault();
                    if (ancestorFunctionItem == null)
                    {
                        ancestorFunctionItem = new FunctionItem()
                            {
                                ID = parentWebFunction.ID,
                                Area = string.Empty,
                                Controller = string.Empty,
                                Action = string.Empty,
                                Icon = parentWebFunction.Icon,
                                Seq = parentWebFunction.Seq,
                                Description = _webFunctionsService.GetWebFunctionDescription(parentWebFunction.ID, user.Lanuage),
                                ParentID = parentWebFunction.ParentID
                            };

                        account.FunctionItemList.Add(ancestorFunctionItem);
                    }
                    var currnentFunctionItem = new FunctionItem()
                    {
                        Area = currentWebFunction.Area,
                        Controller = currentWebFunction.Controller,
                        Action = currentWebFunction.Action,
                        Icon = currentWebFunction.Icon,
                        ID = currentWebFunction.ID,
                        Seq = currentWebFunction.Seq,
                        Description = _webFunctionsService.GetWebFunctionDescription(currentWebFunction.ID, user.Lanuage),
                        ParentID = currentWebFunction.ParentID
                    };
                    ancestorFunctionItem.SubFunctionItemList.Add(currnentFunctionItem);
                }
                else  //如果为空则表示当前节点为三级菜单，则从一级菜单开始遍历
                {
                    var ancestorWebFunction = _webFunctionsRepository.GetEntity(x => x.ID == parentWebFunction.ParentID);   //一级菜单

                    var ancestorFunctionItem = account.FunctionItemList.Where(x => x.ID == ancestorWebFunction.ID).FirstOrDefault();  //判断一级菜单是否为空

                    if (ancestorFunctionItem == null)
                    {
                        ancestorFunctionItem = new FunctionItem()
                        {
                            ID = ancestorWebFunction.ID,
                            Area = string.Empty,
                            Controller = string.Empty,
                            Action = string.Empty,
                            Description = _webFunctionsService.GetWebFunctionDescription(ancestorWebFunction.ID, user.Lanuage),
                            Seq = ancestorWebFunction.Seq,
                            Icon = ancestorWebFunction.Icon,
                            ParentID = ancestorWebFunction.ParentID
                        };
                        account.FunctionItemList.Add(ancestorFunctionItem);
                    }

                    var parentFunctionItem = ancestorFunctionItem.SubFunctionItemList.Where(x => x.ID == parentWebFunction.ID).FirstOrDefault();  //判断二级菜单是否为空
                    if (parentFunctionItem == null)
                    {
                        parentFunctionItem = new FunctionItem()
                        {
                            Area = string.Empty,
                            Controller = string.Empty,
                            Action = string.Empty,
                            Icon = parentWebFunction.Icon,
                            Seq = parentWebFunction.Seq,
                            Description = _webFunctionsService.GetWebFunctionDescription(parentWebFunction.ID, user.Lanuage),
                            ID = parentWebFunction.ID,
                            ParentID = parentWebFunction.ParentID,
                        };
                        ancestorFunctionItem.SubFunctionItemList.Add(parentFunctionItem);
                    }

                    ///加入三级菜单
                    parentFunctionItem.SubFunctionItemList.Add(new FunctionItem()
                    {
                        ID = currentWebFunction.ID,
                        Area = currentWebFunction.Area,
                        Controller = currentWebFunction.Controller,
                        Action = currentWebFunction.Action,
                        Icon = currentWebFunction.Icon,
                        Seq = currentWebFunction.Seq,
                        Description = _webFunctionsService.GetWebFunctionDescription(currentWebFunction.ID, user.Lanuage),
                        ParentID = currentWebFunction.ParentID
                    });


                }
            }
            return account;
        }

        /// <summary>
        /// 获取人员信息集合
        /// </summary>
        /// <param name="Parameters"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public List<UserGridItem> GetUserList(UserParameter Parameters, out int TotalCount)
        {
            var query = (from user in _usersRepository.GetAsQueryable()
                         join organization in _organizationRepository.GetAsQueryable() on user.OrganizationUniqueID equals organization.UniqueID
                         select new
                             {
                                 user.UniqueID,
                                 user.ID,
                                 user.Name,
                                 user.LastLoginTime,
                                 OrganizationUniqueID = organization.Name,
                                 user.Photo,
                                 user.State,
                                 user.Title,
                                 user.Email,
                                 user.MobilePhone
                             });

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
                query = query.Where(x => x.OrganizationUniqueID == Parameters.OrganizationUniqueID);
            }

            TotalCount = query.Count();  //获取总共的数量

            query = query.OrderBy(x => x.ID).Skip((Parameters.PageNo - 1) * Parameters.PageSize).Take(Parameters.PageSize); //获取需要的分页数据

            var list = query.Select(x => new UserGridItem
            {
                UniqueID = x.UniqueID,
                ID = x.ID,
                Name = x.Name,
                LastLoginTime = x.LastLoginTime,
                OrganizationUniqueID = x.OrganizationUniqueID,
                Photo = x.Photo,
                State = x.State,
                Title = x.Title,
                Email = x.Email,
                MobilePhone = x.MobilePhone
            }).ToList();

            return list;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UniqueID"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public RequestResult ChangePassword(string UniqueID, string NewPassword)
        {
            RequestResult result = new RequestResult();
            //DbEntityValidationException
            var user = _usersRepository.GetEntity(x => x.UniqueID == UniqueID);
            user.PassWord = NewPassword;

            // Users user=new Users(){UniqueID=UniqueID,PassWord=NewPassword};
            _usersRepository.UpdateEntity(user);
            if (_unitOfWork.SaveChage())
            {
                result.RetunSuccessMessage(Resources.Resource.EditSuccess);  //修改成功
            }
            else
            {
                result.ReturnFailMessage(Resources.Resource.EditFailed);  //修改失败
            }
            return result;
        }

        /// <summary>
        /// 根据UniqueID获取人员信息
        /// </summary>
        /// <param name="uniqueID"></param>
        /// <returns></returns>
        public EditUserInputFormViewModel GetUserByUniqueID(string uniqueID)
        {
            var user = _usersRepository.GetEntity(x => x.UniqueID == uniqueID);
            EditUserInputFormViewModel editUserInputFormViewModel = new EditUserInputFormViewModel()
            {
                UniqueID = user.UniqueID,
                ID = user.ID,
                Name = user.Name,
                Email = user.Email,
                BirthDay = user.BirthDay,
                Lanuage = user.Lanuage,
                MobilePhone = user.MobilePhone,
                OrganizationUniqueID = user.OrganizationUniqueID,
                Other = user.Other,
                Title = user.Title,
            };
            return editUserInputFormViewModel;
        }

        public RequestResult EditUser(EditUserInputFormViewModel editUserInputFormViewModel)
        {
            RequestResult result = new RequestResult();
            var existsUserID = _usersRepository.GetAsQueryable().Where(x => x.ID == editUserInputFormViewModel.ID && x.UniqueID == editUserInputFormViewModel.UniqueID).FirstOrDefault();
            if (existsUserID != null)
            {
                result.ReturnFailMessage(Resources.Resource.ExistsUserID);  //用户编号重复

            }
            else
            {
                var user = _usersRepository.GetEntity(x => x.UniqueID == editUserInputFormViewModel.UniqueID);
                user.ID = editUserInputFormViewModel.ID;
                user.Name = editUserInputFormViewModel.Name;
                user.Email = editUserInputFormViewModel.Email;
                user.Lanuage = editUserInputFormViewModel.Lanuage;
                user.MobilePhone = editUserInputFormViewModel.MobilePhone;
                user.OrganizationUniqueID = editUserInputFormViewModel.OrganizationUniqueID;
                user.Other = user.Other;
                user.Title = user.Title;
                user.ModifyTime = DateTime.Now;
                user.ModifyUser = (HttpContext.Current.Session["Account"] as Account).UniqueID;  //需要调用System.Web这个程序集才能调用到Session 而且世界使用Session是错误的
                _usersRepository.UpdateEntity(user);
                if (_unitOfWork.SaveChage())
                {
                    result.RetunSuccessMessage(Resources.Resource.EditSuccess);
                }
                else
                {
                    result.ReturnFailMessage(Resources.Resource.EditFailed);
                }

            }
            return result;
        }
    }
}
