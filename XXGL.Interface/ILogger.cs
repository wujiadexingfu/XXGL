using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXGL.Interface
{
   public  interface ILogger
    {
       /// <summary>
        /// Trace - 最常见的记录信息，一般用于普通输出
       /// </summary>
       /// <param name="message"></param>
        void   Trace (string message);

       /// <summary>
        /// Debug - 同样是记录信息，不过出现的频率要比Trace少一些，一般用来调试程序
       /// </summary>
       /// <param name="message"></param>
       void    Debug(string message); 

       /// <summary>
       /// Info - 信息类型的消息
       /// </summary>
       /// <param name="message"></param>
       void Info (string message);

       /// <summary>
       /// Warn - 警告信息，一般用于比较重要的场合
       /// </summary>
       /// <param name="message"></param>
      void   Warn (string message);

       /// <summary>
      /// Error - 错误信息
       /// </summary>
       /// <param name="message"></param>
      void Error(string message);

       /// <summary>
      /// Fatal - 致命异常信息。一般来讲，发生致命异常之后程序将无法继续执行。
       /// </summary>
       /// <param name="message"></param>
      void Fatal(string message);
    }
}
