using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    //页面中展示的内容。
    public class ViewModel
    {
        public string CreateDateTime { get; set; }//评论时间
        public string Msg { get; set; }//评论的内容

        // MVC  Model  View  Control
    }
}