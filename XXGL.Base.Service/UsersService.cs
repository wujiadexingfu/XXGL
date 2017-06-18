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
    public class UsersService :IUsersService
    {
        public  static IUsersRepository _usersRepository { get; set; }
        public  IRolesRepository _rolesRepository { get; set; }
        public  ILnkUsersRolesRepository _lnkUsersRolesRepository { get; set; }
        public  IWebFunctionsRepository _webFunctionsRepository { get; set; }

        public  ILnkWebFunctionsLanguagesRepository _lnkWebFunctionsLanguagesRepository { get; set; }

        public  ILanguagesRepository _languagesRepository { get; set; }

        public  IOperationsRepository _operationsRepository { get; set; }

        public  ILnkOperationsLanguagesRepository _lnkOperationsLanguagesRepository { get; set; }

        public   ILnkRolesWebFunctionsRepository _lnkRolesWebFunctionsRepository { get; set; }

        public IWebFunctionsService _webFunctionsService { get; set; }



        public UsersService(IUsersRepository usersRepository, IRolesRepository rolesRepository, ILnkUsersRolesRepository lnkUsersRolesRepository, IWebFunctionsRepository webFunctionsRepository, ILnkWebFunctionsLanguagesRepository lnkWebFunctionsLanguagesRepository,
            ILanguagesRepository languagesRepository, IOperationsRepository operationsRepository, ILnkOperationsLanguagesRepository lnkOperationsLanguagesRepository, ILnkRolesWebFunctionsRepository lnkRolesWebFunctionsRepository, IWebFunctionsService webFunctionsService)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _lnkUsersRolesRepository = lnkUsersRolesRepository;
            _webFunctionsRepository = webFunctionsRepository;
            _lnkWebFunctionsLanguagesRepository = lnkWebFunctionsLanguagesRepository;
            _languagesRepository = languagesRepository;
            _operationsRepository = operationsRepository;
            _lnkOperationsLanguagesRepository = lnkOperationsLanguagesRepository;
            _lnkRolesWebFunctionsRepository = lnkRolesWebFunctionsRepository;
            _webFunctionsService = webFunctionsService;
        }


        public UserModel Login(string ID, string name)
        {
            var user = _usersRepository.GetEntity(x => x.ID == ID && x.Name == name);

            return new UserModel() { ID = user.ID, Name = user.Name };
         
        }

        public  Account GetAccount(string ID)
        {
            Account account = new Account();
            var user= _usersRepository.GetEntity(x => x.ID == ID);

            account.UniqueID = user.UniqueID;
            account.ID = user.ID;
            account.Name = user.Name;
            account.Title = user.Title;
            account.Lanuage = user.Lanuage;
            account.Photo = user.Photo;



            var WebFunctionsOperations  = (from lnkRolesWebFunctions in _lnkRolesWebFunctionsRepository.GetAsQueryable()
                     join roles in _rolesRepository.GetAsQueryable() on lnkRolesWebFunctions.RolesUniqueID equals roles.UniqueID
                     join lnkUsersRoles in _lnkUsersRolesRepository.GetAsQueryable() on roles.UniqueID equals lnkUsersRoles.RolesUniqueID
                     join users in _usersRepository.GetAsQueryable() on lnkUsersRoles.UsersUniqueID equals users.UniqueID
                     join webFunctions in _webFunctionsRepository.GetAsQueryable() on lnkRolesWebFunctions.WebFunctionUniqueID equals webFunctions.ID
                     join operations in _operationsRepository.GetAsQueryable() on lnkRolesWebFunctions.OperationsUniqueID equals operations.UniqueID
                     where users.ID == "admin" 
                     select new  
                     { 
                         WebFunctionID=webFunctions.ID,
                         Area=webFunctions.Area,
                         Controller=webFunctions.Controller,
                         Action=webFunctions.Controller,
                         Operation=operations.ID,
                         ParentID=webFunctions.ParentID
                     } ).ToList();  




            account.WebFunctionOperationList = WebFunctionsOperations.Select(x => new WebFunctionOperation()
            {
                WebFunctionID = x.WebFunctionID,
                Area = x.Area,
                Controller = x.Controller,
                Action = x.Area,
                Operation = x.Operation
            }).ToList();


            var webFunctionsDistinct = WebFunctionsOperations.Select(x => new
            {
                WebFunctionID = x.WebFunctionID,
                ParentID = x.ParentID
            }).Distinct().ToList(); //获取到所有的子菜单，以便后续的查询

            var functionItemList = new List<FunctionItem>();

            foreach (var item in webFunctionsDistinct)
            {
                if (item.ParentID == "*") //ParentID 如果为* 则表示这个菜单为一级菜单
                {
                    var ancestorFunctionItem = functionItemList.FirstOrDefault(x => x.ID == item.WebFunctionID);
                    if (ancestorFunctionItem == null)  //为空则表示这个一级菜单不存在
                    {
                        var functionItem = _webFunctionsService.GetFunctionItem(item.WebFunctionID, user.Lanuage);
                        functionItemList.Add(functionItem);  //一级菜单
                    }

                }
                else 
                {
                    var parentFunctionItem = _webFunctionsRepository.GetEntity(x => x.ID == item.ParentID);
                    if (parentFunctionItem.ParentID == "*")   //表示这个节点是一个二级菜单
                    {
                        var ancestorFunctionItem = functionItemList.FirstOrDefault(x => x.ID == parentFunctionItem.ID); //查看一级菜单是否存在

                        if (ancestorFunctionItem == null)
                        {
                            var fistLevelFunctionItem = _webFunctionsService.GetFunctionItem(parentFunctionItem.ID, user.Lanuage);
                            functionItemList.Add(fistLevelFunctionItem);
                        }
                        var secondLevelFunctionItem = _webFunctionsService.GetFunctionItem(item.WebFunctionID, user.Lanuage);

                        functionItemList.Where(x => x.ID == parentFunctionItem.ID).First().SubFunctionItemList.Add(secondLevelFunctionItem);



                    }
                    else   //表示这个节点是一个三级菜单
                    {
                        var parentWebFunction = _webFunctionsRepository.GetEntity(x => x.ID == item.ParentID);//二级菜单
                        var ancestorWebFunction = _webFunctionsRepository.GetEntity(x => x.ID == parentWebFunction.ID);//一级菜单

                        var firstLevelFunctionItem=functionItemList.FirstOrDefault(x=>x.ID==ancestorWebFunction.ID);

                        if (firstLevelFunctionItem == null)
                        {
                            firstLevelFunctionItem = _webFunctionsService.GetFunctionItem(ancestorWebFunction.ID, user.Lanuage);
                                
                            functionItemList.Add(firstLevelFunctionItem);
                        }

                        var secondLevelFunctionItem = firstLevelFunctionItem.SubFunctionItemList.FirstOrDefault(x => x.ID == parentWebFunction.ID);
                        if (secondLevelFunctionItem == null)
                        {
                            secondLevelFunctionItem = _webFunctionsService.GetFunctionItem(parentWebFunction.ID, user.Lanuage);
                            
                            firstLevelFunctionItem.SubFunctionItemList.Add(secondLevelFunctionItem);
                        }
                        var thirdLevelFunctionItem = _webFunctionsService.GetFunctionItem(item.WebFunctionID, user.Lanuage); 

                        secondLevelFunctionItem.SubFunctionItemList.Add(thirdLevelFunctionItem);

                    }

                
                }
            
            }





            //account.WebFunctionOperationList = (from lnkRolesWebFunctions in _lnkRolesWebFunctionsRepository.GetAsQueryable()
            //                                    join roles in _rolesRepository.GetAsQueryable() on lnkRolesWebFunctions.RolesUniqueID equals roles.ID
            //                                    join lnkUsersRoles in _lnkUsersRolesRepository.GetAsQueryable() on roles.ID equals lnkUsersRoles.RolesUniqueID
            //                                    join users in _usersRepository.GetAsQueryable() on lnkUsersRoles.UsersUniqueID equals users.UniqueID
            //                                    join webFunctions in _webFunctionsRepository.GetAsQueryable() on lnkRolesWebFunctions.WebFunctionUniqueID equals webFunctions.ID
            //                                    join operations in _operationsRepository.GetAsQueryable() on lnkRolesWebFunctions.OperationsUniqueID equals operations.UniqueID
            //                                    join lnkWebFunctionsLanguages in _lnkWebFunctionsLanguagesRepository.GetAsQueryable() on webFunctions.ID equals lnkWebFunctionsLanguages.WebFunctionUniqueID
            //                                    join lanuages in _languagesRepository.GetAsQueryable() on lnkWebFunctionsLanguages.LanguagesUniqueID equals lanuages.UniqueID
            //                                    where users.ID == "admin"
            //                                    select lnkRolesWebFunctions



            //                                  );




            return account;

        }


    }
}
