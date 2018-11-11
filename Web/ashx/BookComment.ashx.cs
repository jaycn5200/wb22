using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// BookComment 的摘要说明
    /// </summary>
    public class BookComment : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request.Form["action"];
            if (action == "postComment")
            {
                BLL.BookCommentManager bll = new BLL.BookCommentManager();
                Model.BookComment model = new Model.BookComment();
                model.Msg=context.Request.Form["msg"];
                model.CreateDateTime = DateTime.Now;
                model.BookId=Convert.ToInt32(context.Request.Form["bookId"]);
                bll.Add(model);
                context.Response.Write("yes");
               
            }
            else if (action == "load")//加载评论内容
            {
                string bookId = context.Request.Form["bookId"];
                BLL.BookCommentManager bll = new BLL.BookCommentManager();
               List<Model.BookComment>list= bll.GetModelList(Convert.ToInt32(bookId));//根据书的编号找出书的评论。

                //根据ViewModel创建一个新的集合
               List<ViewModel> newList = new List<ViewModel>();
               foreach (Model.BookComment model in list)
               {
                   //将原有集合中存储的评论的内容放入新的集合中。
                   ViewModel viewModel = new ViewModel();
                   viewModel.Msg = model.Msg;
                   TimeSpan time=DateTime.Now-model.CreateDateTime;
                   viewModel.CreateDateTime =Common.WebCommon.GetTimeSpan(time) ;//获取时间差的字符串
                   newList.Add(viewModel);

               }

               System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
               context.Response.Write(js.Serialize(newList.ToArray()));//将评论的内容生成JSON返回。
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