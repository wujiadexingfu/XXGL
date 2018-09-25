using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XXGL.Interface;


/********************************************************************************

** 类名称：  Log4NetUtilty

** 描述：Log4Net的文件

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-25 20:37:44

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Log.Log4Net
{
    public  class Log4NetUtilty 
    {

        private static  Type type = MethodBase.GetCurrentMethod().DeclaringType;
        ILog log = LogManager.GetLogger(type);

        public void Trace(string message)
        {
            log.Info(message);
        }

        public void Debug(string message)
        {
            log.Debug(message);
        }

        public void Info(string message)
        {
            log.Info(message);
        }

        public void Warn(string message)
        {
            log.Warn(message);
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public void Fatal(string message)
        {
            log.Fatal(message);
        }
    }
}
