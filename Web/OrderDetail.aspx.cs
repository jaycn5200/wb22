using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected Model.Orders oneOrder = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string orderId = Request.QueryString["orderId"];
            if (!string.IsNullOrEmpty(orderId))
            {
                BLL.OrdersManager bll = new BLL.OrdersManager();
                oneOrder=bll.GetModel(orderId);
                BLL.OrderBookManager orderBll = new BLL.OrderBookManager();
                this.rptDetails .DataSource= orderBll.GetModelListByOrderId(orderId);//根据订单号找出属于该订单的商品.
                this.rptDetails.DataBind();
            }
        }
        /// <summary>
        /// 判断订单的状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string GetState(int state)
        {
            if (state == 0)
            {
                return "未付款，未发货!!";
            }
            else if (state == 1)
            {
                return "已付款，未发货!!";
            }
            else if (state == 2)
            {
                return "已发货";
            }
            else
            {
                return "签收";
            }

        }
    }
}