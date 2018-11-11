using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// Seo 的摘要说明
    /// </summary>
    public class Seo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (action == "a1")
            {
                context.Response.Write("今天天气很差!!");

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}