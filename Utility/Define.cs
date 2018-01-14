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



      
     
         public enum EnumOrganizationPermission
        {
            None,
            Visible,
            Queryable,
            Editable
        }


        

        


    }
}
