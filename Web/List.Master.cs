using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class List : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{

            //管理员增加一个新类别时，让缓存中的数据失效。
            if (HttpRuntime.Cache["category"] == null)
            {
              
                BLL.CategoryManager bll = new BLL.CategoryManager();
                List<Model.Category> list = bll.GetModelList("");
                HttpRuntime.Cache["category"]=list;//将数据放入缓存。、
                
            }
            foreach (Model.Category model in HttpRuntime.Cache["category"] as List<Model.Category>)
                {
                    TreeNode node = new TreeNode();
                    node.Text = model.Name;
                    //node.NavigateUrl = "/BookList.aspx?categoryId="+model.Id;
                    node.NavigateUrl = "/BookList_"+model.Id+".aspx";
                    this.tvCategory.Nodes.Add(node);
                }
           // }
        }
    }
}