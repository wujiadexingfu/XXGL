using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/********************************************************************************

** 类名称：  RequestResult

** 描述：返回结果类

** 作者： zhangyang

** Version: 1.0

** 创建时间：2018-01-09 20:54:05

** 最后修改人：（无）

** 最后修改时间：（无）

** CLR版本：4.0.30319.36388      

** 版权所有 (C) :zhangyang

*********************************************************************************/

namespace XXGL.Base.Models
{
   public class RequestResult
    {
       /// <summary>
       /// 是否正常
       /// </summary>
       public bool IsSuccess { get; set; }

       /// <summary>
       /// 返回结果，一般用于IsSuccess为true
       /// </summary>
       public Object Result { get; set; }


       public string Message { get; set; }

       /// <summary>
       /// 数据正确，无需返回数据时使用
       /// </summary>
       public void Success()
       {
           this.IsSuccess = true;
           this.Message = null;
           this.Result = null;
       }

       /// <summary>
       /// 数据正确，需返回数据时使用
       /// </summary>
       /// <param name="result"></param>
       public void RetunSuccessMessage(string message)
       {
           this.IsSuccess=true;
           this.Result =null ;
           this.Message = message;
       }

       /// <summary>
       /// 数据正确，需返回数据时使用
       /// </summary>
       /// <param name="message"></param>

       public void RetunSuccessMessage(string message,object result)
       {
           this.IsSuccess = true;
           this.Result = result;
           this.Message = message;
       }


       /// <summary>
       /// 错误
       /// </summary>
       public void Failed()
       {
           this.IsSuccess = false;
           this.Message = null;
           this.Result = null;
       }

       /// <summary>
       /// 一个错误信息
       /// </summary>
       /// <param name="message"></param>
       public void ReturnFailMessage(string  message)
       {
           this.IsSuccess=false;
           this.Message = message;
           this.Result = null;
     
       }

       public void ReturnFailMessage(string  message,string result)
       {
           this.IsSuccess = false;
           this.Message = message;
           this.Result = result;
       }


      
    }
}
