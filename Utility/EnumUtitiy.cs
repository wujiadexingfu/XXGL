using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************

** 类名称：  EnumUtitiy

** 描述：********

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-09-18 20:39:00

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.42000      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace Utility
{


        public enum UserExpiryDateStatus
        {
            /// <summary>
            /// 未生效
            /// </summary>
            Ineffective ,
            /// <summary>
             /// 过期
            /// </summary>
            Expire ,

            /// <summary>
             /// 正常
            /// </summary>
            Normal 

        }

}
