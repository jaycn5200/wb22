using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using BookShop.Model;
using BookShop.DAL;
using System.Text;
using System.IO;
using System.Web;
namespace BookShop.BLL
{
	/// <summary>
	/// 业务逻辑类BooksManager 的摘要说明。
	/// </summary>
	public class BookManager
	{
        PublisherServices publisherServices = new PublisherServices();
        CategoryServices categoryServices = new CategoryServices();
		private readonly BookShop.DAL.BookServices dal=new BookShop.DAL.BookServices();
		public BookManager()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(BookShop.Model.Book model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(BookShop.Model.Book model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public BookShop.Model.Book GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public BookShop.Model.Book GetModelByCache(int Id)
		{
			
			string CacheKey = "BooksModel-" + Id;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (BookShop.Model.Book)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<BookShop.Model.Book> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<BookShop.Model.Book> DataTableToList(DataTable dt)
		{
			List<BookShop.Model.Book> modelList = new List<BookShop.Model.Book>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BookShop.Model.Book model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new BookShop.Model.Book();
					if(dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					model.Title=dt.Rows[n]["Title"].ToString();
					model.Author=dt.Rows[n]["Author"].ToString();
					if(dt.Rows[n]["PublisherId"].ToString()!="")
					{
						int PublisherId=int.Parse(dt.Rows[n]["PublisherId"].ToString());
                        model.Publisher = publisherServices.GetModel(PublisherId);
					}
					if(dt.Rows[n]["PublishDate"].ToString()!="")
					{
						model.PublishDate=DateTime.Parse(dt.Rows[n]["PublishDate"].ToString());
					}
					model.ISBN=dt.Rows[n]["ISBN"].ToString();
					if(dt.Rows[n]["WordsCount"].ToString()!="")
					{
						model.WordsCount=int.Parse(dt.Rows[n]["WordsCount"].ToString());
					}
					if(dt.Rows[n]["UnitPrice"].ToString()!="")
					{
						model.UnitPrice=decimal.Parse(dt.Rows[n]["UnitPrice"].ToString());
					}
					model.ContentDescription=dt.Rows[n]["ContentDescription"].ToString();
					model.AurhorDescription=dt.Rows[n]["AurhorDescription"].ToString();
					model.EditorComment=dt.Rows[n]["EditorComment"].ToString();
					model.TOC=dt.Rows[n]["TOC"].ToString();
					if(dt.Rows[n]["CategoryId"].ToString()!="")
					{
						 int CategoryId=int.Parse(dt.Rows[n]["CategoryId"].ToString());
                         model.Category = categoryServices.GetModel(CategoryId);
					}
					if(dt.Rows[n]["Clicks"].ToString()!="")
					{
						model.Clicks=int.Parse(dt.Rows[n]["Clicks"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		//-------------------------------------------------------------
        /// <summary>
        /// 获取指定类别下的总的页数
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <returns></returns>
        public int GetPageCount(int categoryId,int pageSize)
        {
            int recordCount=dal.GetRecordCount(categoryId);
            int pageCount =Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        /// <summary>
        /// 获取分页的数据
        /// </summary>
        /// <param name="pageIndex">当前的页码</param>
        /// <param name="pageSize"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Model.Book> GetPageData(int pageIndex,int pageSize,int categoryId)
        {
            return GetPageData(pageIndex, pageSize, categoryId, "");
        }

        public List<Model.Book> GetPageData(int pageIndex, int pageSize, int categoryId,string orderby)
        {
            int start = (pageIndex - 1) * pageSize + 1;//计算开始位置
            int end = pageIndex * pageSize;//终止位置
            DataSet ds = dal.GetPageData(start, end, categoryId,orderby);
            return DataTableToList(ds.Tables[0]);

        }

        /// <summary>
        /// 将指定书的内容生成静态页
        /// </summary>
        /// <param name="bookId"></param>
        public void CreateStaticPage(int bookId)
        {
           Model.Book model= dal.GetModel(bookId);
           if (model != null)
           {
               //StringBuilder sb = new StringBuilder();
               //sb.Append("<html><head><title>"+model.Title+"</title></head>");
               //sb.Append("<body>");
               //sb.Append("<table>");
               //sb.Append("<tr><td>书名:</td><td>"+model.Title+"</td></tr>");
               //sb.Append("<tr><td>作者:</td><td>" + model.Author + "</td></tr>");
               //sb.Append("<tr><td>价格:</td><td>" + model.UnitPrice.ToString("0.00") + "</td></tr>");
               //sb.Append("<tr><td>封面:</td><td><img src='/Images/BookCovers/"+model.ISBN+ ".jpg'/></td></tr>");
               //sb.Append("<tr><td>内容:</td><td>" + model.ContentDescription + "</td></tr>");
               //sb.Append("</table>");
               //sb.Append("</body></html>");
               //模板  
               string templapate = File.ReadAllText(HttpContext.Current.Server.MapPath("/Template/BookTemplate.html"));
               string html = templapate.Replace("$title", model.Title).Replace("$body", model.ContentDescription).Replace("$bookId",model.Id.ToString());

               string dir = HttpContext.Current.Server.MapPath("/StaticPage/" + model.PublishDate.Year + "/" + model.PublishDate.Month + "/" + model.PublishDate.Day + "/");
               Directory.CreateDirectory(Path.GetDirectoryName(dir));//创建文件夹
               File.WriteAllText(dir+model.Id+".html",html, Encoding.UTF8);//将内容写到文件
               //File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath("/StaticPage/" + model.Id + ".html"), sb.ToString(), Encoding.UTF8);
           }
        }

		#endregion  成员方法
	}
}

