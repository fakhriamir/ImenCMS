using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal
{
    public class HttpHeadersCleanup : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += PreSendRequestHeaders;
        }
        void PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
			HttpContext.Current.Response.Headers.Remove("Etag");
			HttpContext.Current.Response.Headers.Remove("X-Powered-By");
			HttpContext.Current.Response.Headers.Remove("P3P");
        }
        public void Dispose()
        { }
    }
}