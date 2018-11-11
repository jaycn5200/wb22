using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace BookShop.Web.member
{
    public partial class pageData : System.Web.UI.UserControl
    {

        protected string strHtml = string.Empty;
        private int currentPageIndex;//当前页

        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set { currentPageIndex = value; }
        }

        private int currentPageCount;//总页数

        public int CurrentPageCount
        {
            get { return currentPageCount; }
            set { currentPageCount = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (currentPageCount == 1)//总页数等于1
            {
                return;//只有一页没有必要出现页码。
            }
            int categoryId = 0;

            if (!int.TryParse(Request.QueryString["categoryId"], out categoryId))
            {
                categoryId = 0;
            }
            int start = currentPageIndex - 5;//起始位置。
            if (start < 1)
            {
                start = 1;
            }
            int end = start + 9;//终止位置.
            if (end > currentPageCount)
            {
                end = currentPageCount;
                start = currentPageCount - 9>0?currentPageCount-9:1;//该起始位置。(考虑到总页数小于9的情况)

            }
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (i == currentPageIndex)
                {
                    sb.Append("&nbsp;" + i + "&nbsp;");
                }
                else
                {
                    sb.Append(string.Format("<a href='?pageIndex={0}&categoryId={1}'>&nbsp;[{0}]&nbsp;</a>", i,categoryId));
                }
            }
            strHtml = sb.ToString();

        }
    }
}