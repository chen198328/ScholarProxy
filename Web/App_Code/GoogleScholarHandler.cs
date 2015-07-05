using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleScholarWeb
{
    /// <summary>
    /// GoogleScholarHandler 的摘要说明
    /// </summary>
    public class GoogleScholarHandler : IHttpAsyncHandler
    {
        public GoogleScholarHandler()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            throw new NotImplementedException();
        }

        void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
        {

        }

        bool IHttpHandler.IsReusable
        {
            get { return true; }
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {

        }
    }
}