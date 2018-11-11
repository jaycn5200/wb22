using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class Cart :Common.CheckSession
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                AddCart();//添加购物车。
                BindCart();


            }
        }
        /// <summary>
        /// 绑定商品
        /// </summary>
        protected void BindCart()
        {
            BLL.CartManager bll = new BLL.CartManager();
            List<Model.Cart>list=bll.GetModelList(((Model.User)Session["user"]).Id);
            this.CartRepeater.DataSource = list;
            this.CartRepeater.DataBind();
        }

        protected void AddCart()
        {
            int bookId = 0;
            if (int.TryParse(Request.QueryString["bookId"], out bookId))
            {
                //判断有没有这本书。
                BLL.BookManager bll = new BLL.BookManager();
                Model.Book bookModel=bll.GetModel(bookId);
                if (bookModel != null)
                {
                    //当前用户在购物车中是否已经添加了该书。如果有，更新数量，否则添加。
                    BLL.CartManager cartBll = new BLL.CartManager();
                    //根据用户的编号与书的编号一起查询.
                   Model.Cart modelCart= cartBll.GetModel(((Model.User)Session["user"]).Id, bookId);
                   if (modelCart != null)
                   {
                       modelCart.Count = modelCart.Count + 1;
                       cartBll.Update(modelCart);
                   }
                   else//添加了
                   {
                       Model.Cart cartModel = new Model.Cart();
                       cartModel.Count = 1;
                       cartModel.Book = bookModel;
                       cartModel.User = (Model.User)Session["user"];
                       cartBll.Add(cartModel);
                   }
                }
                else
                {
                    Response.Redirect("/ShowMsg.aspx?msg=" + Server.UrlEncode("此书不存在!") + "&txt=" + Server.UrlEncode("跳转到商品页!" + "&url=/BookList.aspx"));
                }
            }
        }
    }
}