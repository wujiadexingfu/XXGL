﻿using Autofac;
using DbCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.DAL;
using XXGL.Base.IDAL;
using XXGL.Base.IService;
using XXGL.Base.Service;
using XXGL.Interface;
using XXGL.Log.Log4Net;
using XXGL.Log.Nlog;


namespace AutoFacRegisterType
{
  public   class AutoFac
  {
      public static void Register(ContainerBuilder builder)
      {
          builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
          builder.RegisterType<NlogUtility>().As<ILogger>(); //利用Nlog作为日志记录方式
          //builder.RegisterType<Log4NetUtilty>().As<ILogger>();  //利用Log4net作为日志记录方式




          builder.RegisterType<UsersRepository>().As<IUsersRepository>();
          builder.RegisterType<UsersService>().As<IUsersService>();

          builder.RegisterType<RolesRepository>().As<IRolesRepository>();
          builder.RegisterType<RolesService>().As<IRolesService>();

          builder.RegisterType<LanguagesRepository>().As<ILanguagesRepository>();
          builder.RegisterType<LanguagesService>().As<ILanguagesService>();

          builder.RegisterType<LnkOperationsLanguagesRepository>().As<ILnkOperationsLanguagesRepository>();
          builder.RegisterType<LnkOperationsLanguagesService>().As<ILnkOperationsLanguagesService>();

          builder.RegisterType<LnkRolesWebFunctionsRepository>().As<ILnkRolesWebFunctionsRepository>();
          builder.RegisterType<LnkRolesWebFunctionsService>().As<ILnkRolesWebFunctionsService>();

          builder.RegisterType<LnkUsersRolesRepository>().As<ILnkUsersRolesRepository>();
          builder.RegisterType<LnkUsersRolesService>().As<ILnkUsersRolesService>();

          builder.RegisterType<LnkWebFunctionsLanguagesRepository>().As<ILnkWebFunctionsLanguagesRepository>();
          builder.RegisterType<LnkWebFunctionsLanguagesService>().As<ILnkWebFunctionsLanguagesService>();

          
          builder.RegisterType<OperationsRepository>().As<IOperationsRepository>();
          builder.RegisterType<OperationsService>().As<IOperationsService>();

          builder.RegisterType<WebFunctionsRepository>().As<IWebFunctionsRepository>();
          builder.RegisterType<WebFunctionsService>().As<IWebFunctionsService>();

          builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>();
          builder.RegisterType<OrganizationService>().As<IOrganizationService>();

          
      }
    }
}
