using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.member
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //表单校验
            if (IsPostBack)//
            {
                if (CheckCode())
                {
                    Model.User model = new Model.User();
                    model.Address=Request.Form["txtAddress"];
                    model.LoginId = Request.Form["txtName"];
                    model.LoginPwd = Request.Form["txtPass"];
                    model.Mail = Request.Form["txtEmail"];
                    model.Name = Request.Form["txtTrueName"];
                    model.Phone = Request.Form["txtPhone"];
                    model.UserState.Id = 1;
                    BLL.UserManager bll = new BLL.UserManager();
                    string msg = string.Empty;//返回的注册信息
                   int i= bll.Add(model,out msg);
                   if (i > 0)
                   {
                       //
                       Response.Redirect("/ShowMsg.aspx?msg="+Server.UrlEncode(msg)+"&txt="+Server.UrlEncode("首页")+"&url=/Default.aspx");
                   }

                }
            }
        }
        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <returns></returns>
        private bool CheckCode()
        {
            if (Session["vCode"] != null)
            {
                string txtCode = Request.Form["txtCode"];
                string sysCode = Session["vCode"].ToString();
                if (sysCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}