using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.IDAL;
using XXGL.Base.IService;
using XXGL.Base.Models;
using XXGL.Base.Models.Authenticated;


namespace XXGL.Base.Service
{
    public class UsersService : IUsersService
    {
        public static IUsersRepository _usersRepository { get; set; }
     
        public IWebFunctionsRepository _webFunctionsRepository { get; set; }

        public ILanguagesRepository _languagesRepository { get; set; }
       

     

        public IWebFunctionsService _webFunctionsService { get; set; }



        public UsersService(IUsersRepository usersRepository, IWebFunctionsRepository webFunctionsRepository,   IWebFunctionsService webFunctionsService,ILanguagesRepository languagesRepository)
        {
            _usersRepository = usersRepository;
            _webFunctionsRepository = webFunctionsRepository;
            _webFunctionsService = webFunctionsService;
            _languagesRepository = languagesRepository;
        }


        public UserModel GetUser(string ID)
        {
            var user = _usersRepository.GetEntity(x => x.ID == ID );
            if (user != null)
            {
                return new UserModel() { ID = user.ID, Name = user.Name, PassWord = user.PassWord, State = user.State };
            }
            else
            {
                return null;
            }
        }

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
    }
}
