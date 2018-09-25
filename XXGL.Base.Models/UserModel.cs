using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Base.Models
{
   public  class UserModel
    {
        /// <summary> 
        ///   唯一编码
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
        ///   组织
        /// </summary> 
        public string OrganizationUniqueID { get; set; }

        /// <summary> 
        ///   
        /// </summary> 
        public string Photo { get; set; }

        /// <summary> 
        ///   照片
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
        ///   上次登录时间
        /// </summary> 
        public DateTime? LastLoginTime { get; set; }

        /// <summary> 
        ///   有效期开始日期
        /// </summary> 
        public DateTime? StartExpiryDate { get; set; }

        /// <summary> 
        ///   有效期结束日期
        /// </summary> 
        public DateTime? EndExpiryDate { get; set; }

        /// <summary> 
        ///   是否允许登录
        /// </summary> 
        public bool IsLogin { get; set; }
    }
}
