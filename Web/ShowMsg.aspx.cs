using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class ShowMsg : System.Web.UI.Page
    {
        protected string msg = string.Empty;
        protected string txt = string.Empty;
        protected string url = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["msg"]))
            {
                msg = Request.QueryString["msg"];
            }
            else
            {
                msg = "暂无信息!!!!";
            }
            if (!string.IsNullOrEmpty(Request.QueryString["txt"]))
            {
                txt = Request.QueryString["txt"];
            }
            else
            {
                txt="商品页面";
            }
            if (!string.IsNullOrEmpty(Request.QueryString["url"]))
            {
                url = Request.QueryString["url"];
            }
            else
            {
                url = "/BookList.aspx";
            }
        }
    }
}