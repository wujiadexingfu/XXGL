using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
   public  class Define
    {
       public const string Version = "V2017.06.17.001";
       public const string InitialPassword = "123456";   //初始密码
       public const string Log4NetFileName = "log4Net.config";


       /// <summary>
       /// xml文件的文件名
       /// </summary>
       private const string ConfigFileName = "Utility.Config.xml";

      /// <summary>
      /// xml文件的完整路径
      /// </summary>
       public static string ConfigFile  
       {
           get
           {
               String exePath = System.AppDomain.CurrentDomain.BaseDirectory;
               string fullFilePath = Path.Combine(exePath, ConfigFileName);
               if (File.Exists(fullFilePath))
               {
                   return fullFilePath;
               }
               else
               {
                   fullFilePath = Path.Combine(exePath, "bin", ConfigFileName);
                   if (File.Exists(fullFilePath))
                   {
                       return fullFilePath;
                   }
                   else
                   {
                       return ConfigFileName;
                   }

               }
           }
       }

       /// <summary>
       /// log4net的配置文件
       /// </summary>
       public static string Log4NetFile
       {
           get
           {
               String exePath = System.AppDomain.CurrentDomain.BaseDirectory;
               string fullFilePath = Path.Combine(exePath, Log4NetFileName);
               if (File.Exists(fullFilePath))
               {
                   return fullFilePath;
               }
               else
               {
                   fullFilePath = Path.Combine(exePath, "bin", Log4NetFileName);
                   if (File.Exists(fullFilePath))
                   {
                       return fullFilePath;
                   }
                   else
                   {
                       return Log4NetFileName;
                   }

               }
           }
       
       }


         public enum EnumOrganizationPermission
        {
            None,
            Visible,
            Queryable,
            Editable
        }




        

        


    }
}
