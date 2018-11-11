using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.member
{
    public partial class ULogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                checkLogin();
            }
            else
            {
                //校验COOKIE的值
                checkCookie();
            }
           
        }

        /// <summary>
        /// 校验COOKIE
        /// </summary>
        protected void checkCookie()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string txtName = Request.Cookies["cp1"].Value;
                BLL.UserManager bll = new BLL.UserManager();
                Model.User model=bll.GetModel(txtName);//根据从Cookie中取出的用户名找用户.
                if (model != null)
                {
                    //将数据库中的密码取出来采用相同的加密方式加密以后与COOKIE中的密码比较.
                    string txtPass = model.LoginPwd;
                    string encPass = Entry(txtPass);
                    if (encPass == Request.Cookies["cp2"].Value)
                    {
                        Session["user"] = model;
                        GotoPage("登录成功!");
                    }
                    else
                    {
                        //密码被修改了.
                        Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
                    }
                }
            }
        }

        protected void checkLogin()
        {
            string txtName = Request.Form["txtName"];
            string txtPass=Request.Form["txtPass"];
            BLL.UserManager bll = new BLL.UserManager();
            string msg = string.Empty;
            Model.User model = null;
           bool b= bll.CheckUserInfo(txtName,txtPass,out msg,out model);//校验用户
           if (b)
           {
               Session["user"] = model;
               //判断用户是否选中了复选框
               if (!string.IsNullOrEmpty(Request.Form["checkMe"]))
               {
                   //Cookie的创建
                   HttpCookie cookie1 = new HttpCookie("cp1", txtName);
                   HttpCookie cookie2 = new HttpCookie("cp2",Entry(txtPass));
                   cookie1.Expires = DateTime.Now.AddDays(3);
                   cookie2.Expires = DateTime.Now.AddDays(3);
                   Response.Cookies.Add(cookie1);
                   Response.Cookies.Add(cookie2);

               }

               //获取用户所访问页面的URL
               GotoPage(msg);
           }
        }
        /// <summary>
        /// 对密码进行加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string Entry(string pwd)
        {
            return Common.WebCommon.CreateMd5String(Common.WebCommon.CreateMd5String(pwd));
        }
        /// <summary>
        /// 页面跳转
        /// </summary>
        private void GotoPage(string msg)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["returnUrl"]))
            {
                Response.Redirect(Request.QueryString["returnUrl"]);
            }
            else
            {

                Response.Redirect("/ShowMsg.aspx?msg=" + Server.UrlEncode(msg) + "&txt=" + Server.UrlEncode("首页") + "&url=/Default.aspx");
            }
        }

      
    }
}