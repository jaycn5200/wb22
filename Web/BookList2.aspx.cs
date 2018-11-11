using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class BookList2 : System.Web.UI.Page
    {
      
        protected string btnOrderBy = "价格↑";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                BindRepeater();
            }

       
            //按照价格排序
            if (!string.IsNullOrEmpty(Request.Form["btnPriceOrderby"]))
            {
                OrderByPrice(Request.Form["btnPriceOrderby"]);
            }
            else//改变排序按钮上的文字
            {
                if (ViewState["orderby"] != null)
                {
                    if (ViewState["orderby"].ToString() == "UnitPrice asc")
                    {
                        btnOrderBy = "价格↓";
                    }
                }
            }
        }

        //价格排序
        protected void OrderByPrice(string txt)
        {
            if (txt == "价格↑")//升序
            {
                btnOrderBy = "价格↓";
               ViewState["orderby"]= "UnitPrice asc";
            }
            else//降序
            {
                btnOrderBy = "价格↑";
                ViewState["orderby"] = "UnitPrice desc";
            }
            BindRepeater();//排序完成以后回到第一页。

        }

        protected void BindRepeater()
        {
            int categoryId=0;
            int pageIndex = 1;
            //接收类别编号
            if(!int.TryParse(Request.QueryString["categoryId"],out categoryId))
            {
                categoryId=0;
            }
            if (!int.TryParse(Request.QueryString["pageIndex"], out pageIndex))
            {
                pageIndex = 1;
            }
            BLL.BookManager bll = new BLL.BookManager();
            int pageCount = bll.GetPageCount(categoryId, 10);//求出指定类别下的总的页数
            if (pageIndex < 1)//判断当前页的范围
            {
                pageIndex = 1;
            }
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }
            //给用户控件中的属性赋值
            this.pageData1.CurrentPageCount = pageCount;
            this.pageData1.CurrentPageIndex = pageIndex;

            string orderby = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["orderby"]))
            {
                orderby = Request.QueryString["orderby"];
                //在这里改变日期排序按钮上的文字。
            }
            if (ViewState["orderby"] != null)//获取了排序的方式
            {
                orderby = ViewState["orderby"].ToString();
            }
            this.bookListRepeater.DataSource = bll.GetPageData(pageIndex, 10, categoryId,orderby);
            this.bookListRepeater.DataBind();
        
      
        }
        /// <summary>
        /// 对字符进行截取
        /// </summary>
        /// <returns></returns>
        protected string CutString(string str,int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length) + "......";
            }
            else
            {
                return str;
            }

        }
        //出版日期排序
        protected void btnPublishDate_Click(object sender, EventArgs e)
        {
            string orderby = string.Empty;
            if (this.btnPublishDate.Text == "出版日期↑")
            {
                this.btnPublishDate.Text = "出版日期↓";
              //  ViewState["orderby"] = "PublishDate asc";
                orderby = "PublishDate asc";
            }
            else
            {
                this.btnPublishDate.Text = "出版日期↑";
              //  ViewState["orderby"] = "PublishDate desc";
                orderby = "PublishDate desc";
            }
            //BindRepeater();
            //构建一个新的URL.
          //  get
            string url = string.Empty;
            int categoryId=0;
            if (int.TryParse(Request.QueryString["categoryId"], out categoryId))
            {
                url = string.Format("/BookList2.aspx?orderby={0}&categoryId={1}", Server.UrlEncode(orderby), categoryId);
            }
            else
            {
                url = "/BookList2.aspx?orderby=" + Server.UrlEncode(orderby);
            }
            Response.Redirect(url);


        }

        
    }
}