//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DbEntity.MSSQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sys_Organization
    {
        public string UniqueID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string ParentUniqueID { get; set; }
        public string ManagerUniqueID { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string ModifyUser { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public Nullable<int> Seq { get; set; }
    }
}