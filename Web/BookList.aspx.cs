using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class BookList : System.Web.UI.Page
    {
        protected string htmlCurrentPage = String.Empty;//当前页码
        protected string btnOrderBy = "价格↑";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                BindRepeater(1);
            }

            //上一页（只接受用户单击的submit的value值）
            if (!string.IsNullOrEmpty(Request.Form["htmlBtnPre"]))
            {
                int currentPage = Convert.ToInt32(Request.Form["currentPage"]);
                BindRepeater(--currentPage);
            }
            //下一页
            if (!string.IsNullOrEmpty(Request.Form["htmlBtnNext"]))
            {
                int currentPage = Convert.ToInt32(Request.Form["currentPage"]);
                BindRepeater(++currentPage);
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
            BindRepeater(1);//排序完成以后回到第一页。

        }

        protected void BindRepeater(int pageIndex)
        {
            int categoryId=0;
            //接收类别编号
            if(!int.TryParse(Request.QueryString["categoryId"],out categoryId))
            {
                categoryId=0;
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

            htmlCurrentPage = pageIndex.ToString();

            string orderby = string.Empty;
            if (ViewState["orderby"] != null)//获取了排序的方式
            {
               
                orderby = ViewState["orderby"].ToString();
            }
            this.bookListRepeater.DataSource = bll.GetPageData(pageIndex, 10, categoryId,orderby);
            this.bookListRepeater.DataBind();
            this.lblCurretnPage.Text = pageIndex.ToString();
            this.lblPageCount.Text = pageCount.ToString();
      
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

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPre_Click(object sender, EventArgs e)
        {
           // int currentPage = Convert.ToInt32(this.lblCurretnPage.Text);
            int currentPage = Convert.ToInt32(Request.Form["currentPage"]);
            BindRepeater(--currentPage);
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNext_Click(object sender, EventArgs e)
        {
           // int currentPage = Convert.ToInt32(this.lblCurretnPage.Text);
            int currentPage = Convert.ToInt32(Request.Form["currentPage"]);
            BindRepeater(++currentPage);
        }
        
        /// <summary>
        /// 确定静态文件所在的文件夹
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        protected string GetDir(object time)
        {
            DateTime d = Convert.ToDateTime(time);
            return "/StaticPage/" + d.Year + "/" + d.Month + "/" + d.Day + "/";
        }

    }
}