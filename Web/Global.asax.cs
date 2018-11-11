using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text.RegularExpressions;

namespace BookShop.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 请求管道中的第一个事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
          string url =Request.AppRelativeCurrentExecutionFilePath;//~/BookList_26.aspx
            //判断用户请求的url是否是需要重写。
         Match match= Regex.Match(url, @"~/BookList_(\d+).aspx");
         if (match.Success)
         {
             string categoryId = match.Groups[1].Value;
             Context.RewritePath("/BookList.aspx?categoryId=" + categoryId);
         }


        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}