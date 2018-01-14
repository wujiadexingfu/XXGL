using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace XXGL.Base.Models.CommonModel
{
    /// <summary>
    /// 该类用于数据分页
    /// </summary>
    public abstract class BasePageModel
    {
        /// <summary>
        /// /当前页码
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 每一页显示的数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 需要排序的列
        /// </summary>
        public string SortParam { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string SortOrder { get; set; }


        public BasePageModel()
        {
            PageNo = 1;  //设置默认第一页
            var   defaultPageSize = 15; //设置默认一夜显示15条数据
            int.TryParse(Config.PageSize, out defaultPageSize);  //从xml文件中读取分页信息转换

            this.PageSize = defaultPageSize;
            this.SortOrder = "ASC";

        }
    }
}
