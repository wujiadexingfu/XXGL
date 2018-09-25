using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Interface;


/********************************************************************************

** 类名称：  NlogUtility

** 描述：Nlog的实现类

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-25 20:41:42

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Log.Nlog
{
   public  class NlogUtility
    {
        //reference http://www.cnblogs.com/fuchongjundream/p/3936431.html

        Logger log = LogManager.GetCurrentClassLogger();

        public   void Trace(string message)
        {
            log.Trace(message);
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
