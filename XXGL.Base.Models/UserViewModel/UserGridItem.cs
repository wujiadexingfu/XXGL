using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace XXGL.Base.Models.UserViewModel
{
   public class UserGridItem
    {
        /// <summary> 
        ///   Guid
        /// </summary> 
        public string UniqueID { get; set; }

        /// <summary> 
        ///   编号
        /// </summary> 
        public string ID { get; set; }

        /// <summary> 
        ///   名字
        /// </summary> 
        public string Name { get; set; }

        /// <summary> 
        ///   密码
        /// </summary> 
        public string PassWord { get; set; }

        /// <summary> 
        ///   组织的GUID
        /// </summary> 
        public string OrganizationUniqueID { get; set; }

        /// <summary> 
        ///   照片
        /// </summary> 
        public string Photo { get; set; }

        /// <summary> 
        ///   邮件地址
        /// </summary> 
        public string Email { get; set; }

        /// <summary> 
        ///   出生日期
        /// </summary> 
        public DateTime? BirthDay { get; set; }

        /// <summary> 
        ///   职称
        /// </summary> 
        public string Title { get; set; }

        /// <summary> 
        ///   电话
        /// </summary> 
        public string MobilePhone { get; set; }

        /// <summary> 
        ///   状态
        /// </summary> 
        public bool State { get; set; }

        /// <summary> 
        ///   创建时间
        /// </summary> 
        public DateTime? CreateTime { get; set; }

        /// <summary> 
        ///   创建人员
        /// </summary> 
        public string CreateUser { get; set; }

        /// <summary> 
        ///   修改人员
        /// </summary> 
        public string ModifyUser { get; set; }

        /// <summary> 
        ///   修改时间
        /// </summary> 
        public DateTime? ModifyTime { get; set; }

        /// <summary> 
        ///   其他
        /// </summary> 
        public string Other { get; set; }

        /// <summary> 
        ///   语言
        /// </summary> 
        public string Lanuage { get; set; }

        /// <summary> 
        ///   上次登录时间
        /// </summary> 
        public DateTime? LastLoginTime { get; set; }

        public bool IsLogin { get; set; }
       /// <summary>
       /// 是否生效和过期
       /// </summary>
        public string  ExpiryDateStatus { get; set; }

       /// <summary>
       /// 颜用户有效期的颜色
       /// </summary>
        public  string ExpiryDateStatusColor { get; set; }
    }
}
