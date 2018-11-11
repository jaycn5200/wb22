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
	/// ҵ���߼���BooksManager ��ժҪ˵����
	/// </summary>
	public class BookManager
	{
        PublisherServices publisherServices = new PublisherServices();
        CategoryServices categoryServices = new CategoryServices();
		private readonly BookShop.DAL.BookServices dal=new BookShop.DAL.BookServices();
		public BookManager()
		{}
		#region  ��Ա����

		/// <summary>
		/// �õ����ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// �Ƿ���ڸü�¼
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public int  Add(BookShop.Model.Book model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(BookShop.Model.Book model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// ɾ��һ������
		/// </summary>
		public void Delete(int Id)
		{
			
			dal.Delete(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ��
		/// </summary>
		public BookShop.Model.Book GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
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
		/// ��������б�
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<BookShop.Model.Book> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
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
		/// ��������б�
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		//-------------------------------------------------------------
        /// <summary>
        /// ��ȡָ������µ��ܵ�ҳ��
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageSize">ÿҳ��ʾ�ļ�¼��</param>
        /// <returns></returns>
        public int GetPageCount(int categoryId,int pageSize)
        {
            int recordCount=dal.GetRecordCount(categoryId);
            int pageCount =Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }
        /// <summary>
        /// ��ȡ��ҳ������
        /// </summary>
        /// <param name="pageIndex">��ǰ��ҳ��</param>
        /// <param name="pageSize"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Model.Book> GetPageData(int pageIndex,int pageSize,int categoryId)
        {
            return GetPageData(pageIndex, pageSize, categoryId, "");
        }

        public List<Model.Book> GetPageData(int pageIndex, int pageSize, int categoryId,string orderby)
        {
            int start = (pageIndex - 1) * pageSize + 1;//���㿪ʼλ��
            int end = pageIndex * pageSize;//��ֹλ��
            DataSet ds = dal.GetPageData(start, end, categoryId,orderby);
            return DataTableToList(ds.Tables[0]);

        }

        /// <summary>
        /// ��ָ������������ɾ�̬ҳ
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
               //sb.Append("<tr><td>����:</td><td>"+model.Title+"</td></tr>");
               //sb.Append("<tr><td>����:</td><td>" + model.Author + "</td></tr>");
               //sb.Append("<tr><td>�۸�:</td><td>" + model.UnitPrice.ToString("0.00") + "</td></tr>");
               //sb.Append("<tr><td>����:</td><td><img src='/Images/BookCovers/"+model.ISBN+ ".jpg'/></td></tr>");
               //sb.Append("<tr><td>����:</td><td>" + model.ContentDescription + "</td></tr>");
               //sb.Append("</table>");
               //sb.Append("</body></html>");
               //ģ��  
               string templapate = File.ReadAllText(HttpContext.Current.Server.MapPath("/Template/BookTemplate.html"));
               string html = templapate.Replace("$title", model.Title).Replace("$body", model.ContentDescription).Replace("$bookId",model.Id.ToString());

               string dir = HttpContext.Current.Server.MapPath("/StaticPage/" + model.PublishDate.Year + "/" + model.PublishDate.Month + "/" + model.PublishDate.Day + "/");
               Directory.CreateDirectory(Path.GetDirectoryName(dir));//�����ļ���
               File.WriteAllText(dir+model.Id+".html",html, Encoding.UTF8);//������д���ļ�
               //File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath("/StaticPage/" + model.Id + ".html"), sb.ToString(), Encoding.UTF8);
           }
        }

		#endregion  ��Ա����
	}
}

