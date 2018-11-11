using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BookShop.Web.Common
{
    public class WebCommon
    {
        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static String GetStreamMD5(Stream stream)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;
            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            arrbytHashValue = oMD5Hasher.ComputeHash(stream); //计算指定Stream 对象的哈希值
            //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            strHashData = System.BitConverter.ToString(arrbytHashValue);
            //替换-
            strHashData = strHashData.Replace("-", "");
            strResult = strHashData;
            return strResult;
        }
        //记录跳转的页面
        public static void GoPage()
        {
            HttpContext.Current.Response.Redirect("/member/Login.aspx?returnUrl="+HttpContext.Current.Request.Url.ToString());
        }
        /// <summary>
        /// 对字符串进行MD5运算
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CreateMd5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[]buffer=System.Text.Encoding.UTF8.GetBytes(str);
            byte[]md5Buffer=md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in md5Buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 对时间差进行处理（DateTime类型相减得到的结果类型是TimeSpan）
        /// 3天5小时20分钟   time.TotalHours:   3*24+5+20/60
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeSpan(TimeSpan time)
        {
            if (time.TotalDays >= 365)
            {
               return  Math.Floor(time.TotalDays / 365) + "年前";
            }
            else if (time.TotalDays >= 30)
            {
                return Math.Floor(time.TotalDays/30)+"月前";

            }
            else if (time.TotalHours >= 24)
            {
                return Math.Floor(time.TotalDays) + "天前";
            }
            else if (time.TotalHours >=1)
            {
                return Math.Floor(time.TotalHours) + "小时前";
            }
            else if (time.TotalMinutes >=1)
            {
                return Math.Floor(time.TotalMinutes) + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }

    }
}