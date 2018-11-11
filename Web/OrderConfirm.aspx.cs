using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace BookShop.Web
{
    public partial class OrderConfirm :Common.CheckSession
    {
        protected Model.User currUser = null;
        protected string strHtml = string.Empty;
        protected decimal totleMoney = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            currUser=(Model.User)Session["user"];
            if (!IsPostBack)
            {
                BindShop();
            }
            else
            {
                GoPay();//开始支付
            }
        }
        protected void GoPay()
        {
            //1:下订单。
            int userId=currUser.Id;
            string number=DateTime.Now.ToString("yyyyMMddHHmmssfff")+userId;//订单号。
            string address=string.Format("收件人:{0},电话:{1},地址:{2},邮编:{3}",Request.Form["txtName"],Request.Form["txtPhone"],Request.Form["txtAddress"],Request.Form["txtPostCode"]);
            BLL.OrdersManager bll = new BLL.OrdersManager();
            decimal totalMoney=bll.CreateOrders(userId, number, address);//下订单获取该订单的总价
            //2:把数据发送给支付宝
            Pay.AliPay pay = new Pay.AliPay("图书", "图书商城", number, totalMoney);
         Response.Redirect(pay.GotoPay());
        }

/// <summary>
/// 将放在购物车中的商品展示一下
/// </summary>
        protected void BindShop()
        {
            BLL.CartManager bll = new BLL.CartManager();
           List<Model.Cart>list=bll.GetModelList(currUser.Id);//根据用户编号找用户的商品
           if (list.Count == 0)
           {
               Response.Redirect("/ShowMsg.aspx?msg=" + Server.UrlEncode("请选择商品!") + "&txt=" + Server.UrlEncode("返回商品页面") + "&url=/BookList.aspx");
           }
           else
           {
               StringBuilder sb = new StringBuilder();
               foreach (Model.Cart model in list)
               {
                  sb.Append("<tr class=\"align_Center\">");
                sb.Append("<td style=\"PADDING-BOTTOM: 5px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 5px\">"+model.Book.Id+"</td>");
                 sb.Append(" <td class=align_Left><a onmouseover=\"\" onmouseout=\"\" onclick=\"\" href='<%#Eval(\"Book.Id\",\"book.aspx?id={0}\") %>' target=\"_blank\" >"+model.Book.Title+"</a></td>");
                  sb.Append("<td><span class=\"price\">￥"+model.Book.UnitPrice+"</span></td>");
                  sb.Append("<td>"+model.Count+"</td>   </tr>");
                   totleMoney=totleMoney+(model.Count*model.Book.UnitPrice);
               }
               strHtml = sb.ToString();
           }
        }
    }
}