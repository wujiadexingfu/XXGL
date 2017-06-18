using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.IDAL;
using XXGL.Base.IService;
using XXGL.Base.Models.Authenticated;

namespace XXGL.Base.Service
{
    public class WebFunctionsService : IWebFunctionsService
    {
        public static IWebFunctionsRepository _webFunctionsRepository { get; set; }

        public static ILnkWebFunctionsLanguagesRepository _lnkWebFunctionsLanguagesRepository { get; set; }

        public static ILanguagesRepository _languagesRepository { get; set; }

        public WebFunctionsService(IWebFunctionsRepository webFunctionsRepository, ILnkWebFunctionsLanguagesRepository lnkWebFunctionsLanguagesRepository, ILanguagesRepository languagesRepository)
        {
            _webFunctionsRepository = webFunctionsRepository;
            _lnkWebFunctionsLanguagesRepository = lnkWebFunctionsLanguagesRepository;
            _languagesRepository = languagesRepository;

        }


        /// <summary>
        /// 根据WebFunctionID和语言获取菜单的基本信息
        /// </summary>
        /// <param name="WebFunctionID"></param>
        /// <param name="Language"></param>
        /// <returns></returns>
        public   FunctionItem GetFunctionItem(string WebFunctionID,string Language)
        {

            var m = (from webFunctions in _webFunctionsRepository.GetAsQueryable()
                     join lnkWebFunctionsLanguages in _lnkWebFunctionsLanguagesRepository.GetAsQueryable() on webFunctions.ID equals lnkWebFunctionsLanguages.WebFunctionUniqueID
                     join lanuages in _languagesRepository.GetAsQueryable() on lnkWebFunctionsLanguages.LanguagesUniqueID equals lanuages.UniqueID
                     where webFunctions.ID == WebFunctionID && lanuages.UniqueID == Language
                     select new FunctionItem()
                     {
                         ID = webFunctions.ID,
                         Area = webFunctions.Area,
                         Controller = webFunctions.Controller,
                         Action = webFunctions.Action,
                         Icon = webFunctions.Icon,
                         Seq = webFunctions.Seq,
                     }).AsQueryable();
            var functionItem = (from webFunctions in _webFunctionsRepository.GetAsQueryable()
                                join lnkWebFunctionsLanguages in _lnkWebFunctionsLanguagesRepository.GetAsQueryable() on webFunctions.ID equals lnkWebFunctionsLanguages.WebFunctionUniqueID
                                join lanuages in _languagesRepository.GetAsQueryable() on lnkWebFunctionsLanguages.LanguagesUniqueID equals lanuages.UniqueID
                                where webFunctions.ID == WebFunctionID && lanuages.UniqueID == Language
                                select new FunctionItem()
                                {
                                    ID = webFunctions.ID,
                                    Area = webFunctions.Area,
                                    Controller = webFunctions.Controller,
                                    Action = webFunctions.Action,
                                    Icon = webFunctions.Icon,
                                    Seq = webFunctions.Seq,
                                }).FirstOrDefault();
            return functionItem;
        }


    }
}
