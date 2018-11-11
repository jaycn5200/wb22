using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using BookShop.Model;
using BookShop.DAL;


namespace BookShop.BLL
{
	/// <summary>
	/// ҵ���߼���UsersManager ��ժҪ˵����
	/// </summary>
    public partial class UserManager
	{
     
        UserStateServices userStateServices = new UserStateServices();
		private readonly BookShop.DAL.UserServices dal=new BookShop.DAL.UserServices();
		public UserManager()
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
		public int  Add(BookShop.Model.User model)
		{
          
                return dal.Add(model);
           
		}

       

		/// <summary>
		/// ����һ������
		/// </summary>
		public void Update(BookShop.Model.User model)
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
		public BookShop.Model.User GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

 

		/// <summary>
		/// �õ�һ������ʵ�壬�ӻ����С�
		/// </summary>
		public BookShop.Model.User GetModelByCache(int Id)
		{
			
			string CacheKey = "UsersModel-" + Id;
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
			return (BookShop.Model.User)objModel;
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
		public List<BookShop.Model.User> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// ��������б�
		/// </summary>
		public List<BookShop.Model.User> DataTableToList(DataTable dt)
		{
			List<BookShop.Model.User> modelList = new List<BookShop.Model.User>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				BookShop.Model.User model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new BookShop.Model.User();
					if(dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					model.LoginId=dt.Rows[n]["LoginId"].ToString();
					model.LoginPwd=dt.Rows[n]["LoginPwd"].ToString();
					model.Name=dt.Rows[n]["Name"].ToString();
					model.Address=dt.Rows[n]["Address"].ToString();
					model.Phone=dt.Rows[n]["Phone"].ToString();
					model.Mail=dt.Rows[n]["Mail"].ToString();
					
					if(dt.Rows[n]["UserStateId"].ToString()!="")
					{
					    int UserStateId=int.Parse(dt.Rows[n]["UserStateId"].ToString());
                        model.UserState = userStateServices.GetModel(UserStateId);
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



		#endregion  ��Ա����
	}
}
