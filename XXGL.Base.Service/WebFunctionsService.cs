using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.IDAL;
using XXGL.Base.IService;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Models.WebFunction;

namespace XXGL.Base.Service
{
    public class WebFunctionsService : IWebFunctionsService
    {
        public IWebFunctionsRepository _webFunctionsRepository { get; set; }

        public ILnkWebFunctionsLanguagesRepository _lnkWebFunctionsLanguagesRepository { get; set; }

        public ILanguagesRepository _languagesRepository { get; set; }

        public ILnkRolesWebFunctionsRepository _lnkRolesWebFunctionsRepository { get; set; }

        public IRolesRepository _rolesRepository { get; set; }

        public ILnkUsersRolesRepository _lnkUsersRolesRepository { get; set; }

        public IOperationsRepository _operationsRepository { get; set; }


        public WebFunctionsService(IWebFunctionsRepository webFunctionsRepository, ILnkWebFunctionsLanguagesRepository lnkWebFunctionsLanguagesRepository, ILanguagesRepository languagesRepository,
            ILnkRolesWebFunctionsRepository lnkRolesWebFunctionsRepository, IRolesRepository rolesRepository, ILnkUsersRolesRepository LnkUsersRolesRepository,
        IOperationsRepository operationsRepository
            )
        {
            _lnkRolesWebFunctionsRepository = lnkRolesWebFunctionsRepository;
            _webFunctionsRepository = webFunctionsRepository;
            _lnkWebFunctionsLanguagesRepository = lnkWebFunctionsLanguagesRepository;
            _languagesRepository = languagesRepository;
            _rolesRepository = rolesRepository;
            _lnkUsersRolesRepository = LnkUsersRolesRepository;
            _operationsRepository = operationsRepository;

        }


        /// <summary>
        /// 根据WebFunctionID和语言获取菜单的描述
        /// </summary>
        /// <param name="WebFunctionID">WebFunction表的ID</param>
        /// <param name="Language">语言UniqueID</param>
        /// <returns></returns>
        public string GetWebFunctionDescription(string WebFunctionID, string Language)
        {
            var description = (from lnkWebFunctionsLanguages in _lnkWebFunctionsLanguagesRepository.GetAsQueryable()
                               join lanuages in _languagesRepository.GetAsQueryable() on lnkWebFunctionsLanguages.LanguagesUniqueID equals lanuages.UniqueID
                               where lnkWebFunctionsLanguages.WebFunctionUniqueID == WebFunctionID && lanuages.UniqueID == Language
                               select lnkWebFunctionsLanguages.Description).FirstOrDefault();
            return description;
        }


        /// <summary>
        /// 根据用户表的UniqueID 获取到菜单的操作权限
        /// </summary>
        /// <param name="UserUniqueID"></param>
        /// <returns></returns>
        public List<WebFunctionOperationModel> GetWebFunctionsByUserUnqiueID(string UserUniqueID)
        {
            var list = (from lnkRolesWebFunctions in _lnkRolesWebFunctionsRepository.GetAsQueryable()
                        join roles in _rolesRepository.GetAsQueryable() on lnkRolesWebFunctions.RolesUniqueID equals roles.UniqueID
                        join lnkUsersRoles in _lnkUsersRolesRepository.GetAsQueryable() on roles.UniqueID equals lnkUsersRoles.RolesUniqueID
                        join webFunctions in _webFunctionsRepository.GetAsQueryable() on lnkRolesWebFunctions.WebFunctionUniqueID equals webFunctions.ID
                        join operations in _operationsRepository.GetAsQueryable() on lnkRolesWebFunctions.OperationsUniqueID equals operations.UniqueID
                        where lnkUsersRoles.UsersUniqueID == UserUniqueID
                        select new  WebFunctionOperationModel
                        {   
                            WebFunctionID = webFunctions.ID,
                            Area = webFunctions.Area,
                            Controller = webFunctions.Controller,
                            Action = webFunctions.Controller,
                            OperationID = operations.ID,
                            ParentID = webFunctions.ParentID
                        }).ToList();
            return list;
        }



    }
}
