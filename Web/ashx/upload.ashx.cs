using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace BookShop.Web.ashx
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file =context.Request.Files["Filedata"];//接收文件.
            string fileName = Path.GetFileName(file.FileName);//获取文件名。
            string fileExt = Path.GetExtension(fileName);//获取文件类型.
            if (fileExt == ".jpg")
            {

                //根据上传的文件的日期，创建文件夹.
                string dir = "/UploadImage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                Directory.CreateDirectory(Path.GetDirectoryName(context.Server.MapPath(dir)));//创建文件.
                string fullDir = dir + Common.WebCommon.GetStreamMD5(file.InputStream)+fileExt;//构建了一个完整的文件的存放路径。根据文件的MD5值作为文件的名称。
                file.SaveAs(context.Server.MapPath(fullDir));
                context.Response.Write("ok:"+fullDir);

                //file.SaveAs(context.Server.MapPath("/UploadImage/"+fileName));
                //context.Response.Write("ok:/UploadImage/"+fileName);
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