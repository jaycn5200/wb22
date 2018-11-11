using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.Common
{
    public class CheckSession:System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)//Init
        {
            if (Session["user"] == null)
            {
               // Response.Redirect("/Login.aspx");
                WebCommon.GoPage();
            }
            base.OnInit(e);
        }
    }
}