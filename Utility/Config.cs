using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Utility
{
    public class Config
    {
        /// <summary>
        ///注意设置xml文件的 复制到输出目录，否则程式找不到文件
        /// </summary>



        private static XElement Root
        {
            get
            {
                return XDocument.Load(Define.ConfigFile).Root;
            }
        }

        /// <summary>
        /// 获取每一页显示的数量
        /// </summary>
        /// <returns></returns>
        public  static  string PageSize
        {
            get
            {
               return Root.Element("PageConfig").Element("PageSize").Value;
            }
        }

        /// <summary>
        /// 显示的最大页码数量
        /// </summary>
        public static string MaxPageNumber
        {
            get
            {
                return Root.Element("PageConfig").Element("MaxPageNumber").Value;
            }
        }

        /// <summary>
        /// File节点
        /// </summary>
        private static XElement File 
        {
            get
            {
                return Root.Element("File");
            }
        }

        /// <summary>
        /// File的属性，虚拟路径对应的目录名称
        /// </summary>
        public static string VirtualFile
        {
            get 
            {
                return File.Attribute("VirtualPath").Value;
            }
        }

        /// <summary>
        /// Image节点
        /// </summary>
        private static XElement Image 
        {
            get 
            {
                return File.Element("Image");
            }
        }

        /// <summary>
        /// Image的属性，虚拟路径对应的目录名称
        /// </summary>
        public static string VirtualImage 
        {
            get 
            {
                return Image.Attribute("VirtualPath").Value;
            }
        }

        /// <summary>
        /// UserPhoto的物理路径
        /// </summary>
        public static string UserPhoto
        {
            get 
            {
                return Image.Element("UserPhoto").Value;
            }
        }

        /// <summary>
        /// UserPhtoto的属性，虚拟路径对应的目录名称
        /// </summary>
        public static string VirtualUserPhtoto
        {
            get 
            {
                return Image.Element("UserPhoto").Attribute("VirtualPath").Value;
            }
        }

        /// <summary>
        /// UserPhoto完整的虚拟路径地址
        /// </summary>
        public static string FullVirtualUserPhtoto
        {
            get
            {
                return System.IO.Path.Combine("~", VirtualFile, VirtualImage, VirtualUserPhtoto);
            }
        }





    }
}
