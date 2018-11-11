using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using BookShop.Model;
using BookShop.DAL;

namespace BookShop.BLL
{
   public partial class UserManager
    {
        /// <summary>
        /// 增加一条数据,部分类
        /// </summary>
        public int Add(BookShop.Model.User model,out string msg)
        {
            //判断用户名是否已经被占用。
            if (CheckUser(model.LoginId))//表示用户名已经被占用了
            {
                msg = "用户注册失败!!";
                return -1;
            }
            else
            {
                msg = "注册成功";
                return dal.Add(model);
            }

        }
       /// <summary>
       /// 根据用户名校验用户是否存在
       /// </summary>
       /// <param name="userName"></param>
       /// <returns></returns>
        public bool CheckUser(string userName)
        {
            Model.User model=dal.GetModel(userName);
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Model.User GetModel(string userName)
        {
            return dal.GetModel(userName);
        }

       /// <summary>
       /// 判读用户名和密码
       /// </summary>
       /// <param name="txtName"></param>
       /// <param name="txtPass"></param>
       /// <param name="msg"></param>
       /// <param name="model"></param>
       /// <returns></returns>
        public bool CheckUserInfo(string txtName, string txtPass, out string msg, out Model.User model)
        {
           model=dal.GetModel(txtName);//根据用户名找用户
           if (model != null)
           {
               if (model.UserState.Name == "正常")
               {
                   if (model.LoginPwd == txtPass)
                   {
                       msg = "登录成功!!";
                       return true;
                   }
                   else
                   {
                       msg = "用户名密码错误!!!";
                       return false;
                   }
               }
               else
               {
                   msg = "此用户已经被锁定了!!";
                   return false;
               }
           }
           else
           {
               msg = "此用户不存在!!";
               return false;
           }
        }
        
    }
}
