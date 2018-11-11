using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace BookShop.Web.Pay
{
    /// <summary>
    /// GetPay 的摘要说明
    /// </summary>
    public class GetPay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string out_trade_no = context.Request.QueryString["out_trade_no"];
            string returncode = context.Request.QueryString["returncode"];
            string total_fee = context.Request.QueryString["total_fee"];
            string sign = context.Request.QueryString["sign"];
            string key=ConfigurationManager.AppSettings["key"];
            if (!string.IsNullOrEmpty(out_trade_no) && !string.IsNullOrEmpty(returncode) && !string.IsNullOrEmpty(total_fee) && !string.IsNullOrEmpty(sign))
            {
                string mySign = Common.WebCommon.CreateMd5String(out_trade_no + returncode + total_fee + key);
                if (sign == mySign)
                {
                    if (returncode == "ok")//支付成功
                    {
                        BLL.OrdersManager bll = new BLL.OrdersManager();
                        //修改订单状态.,
                        Model.Orders model=bll.GetModel(out_trade_no);
                        model.State = 1;
                        bll.Update(model);
                        context.Response.Redirect("/OrderDetail.aspx?orderId=" + out_trade_no);
                    }
                    else
                    {
                       //支付失败了
                        
                    }
                }
                else
                {
                    //数据被篡改了

                }

            }
            else//参数为空
            {

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