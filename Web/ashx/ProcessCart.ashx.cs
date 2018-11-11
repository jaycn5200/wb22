using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ProcessCart 的摘要说明
    /// </summary>
    public class ProcessCart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
          
            string action = context.Request.Form["action"];
            if (action == "change")// 更新商品数量
            {
                  int count, pkId;
            if (!int.TryParse(context.Request.Form["count"], out count))
            {
                context.Response.Write("no");
                return;
            }
            if (!int.TryParse(context.Request.Form["pkId"], out pkId))
            {
                context.Response.Write("no");
                return;
            }
                BLL.CartManager bll = new BLL.CartManager();
                Model.Cart model=bll.GetModel(pkId);
                model.Count = count;
                bll.Update(model);
                context.Response.Write("yes");
            }
            else if (action == "del")
            {
                 int pkId=0;
                 if (!int.TryParse(context.Request.Form["pkId"], out pkId))
            {
                context.Response.Write("no");
                return;
            }
                BLL.CartManager bll = new BLL.CartManager();
                bll.Delete(pkId);
                context.Response.Write("yes");
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