using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UCSWeb.Organiz
{
    /// <summary>
    /// UploadImage 的摘要说明
    /// </summary>
    public class UploadImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (!string.IsNullOrEmpty(action))
            {
                UploadFileForImg(context, action);
            }
        }

        public void UploadFileForImg(HttpContext context, string action)
        {
            string newFileName = string.Empty;
            HttpFileCollection files = context.Request.Files;
            if (files == null || files.Count == 0)
                context.Response.Write("{ result :false, desc : '附件不能为空！' }");
            //1.获取文件信息
            var fileToUpload = files[0];
            if (fileToUpload.ContentLength > 299999999)
                context.Response.Write("( result :false, desc: '文件过大！' }");
            if (!string.IsNullOrEmpty(fileToUpload.FileName))
            {
                string path = string.Empty;
                switch (action)
                {
                   
                    case "UploadImgForAdvertContent":
                        path = System.Configuration.ConfigurationManager.AppSettings["AdvertImgContentPath"];
                        break;
                  
                    default:
                        break;
                }
                string serverPath = context.Server.MapPath("~" + path);
                //2.判断文件目录是否存在
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                switch (action)
                {
                    case "UploadImgForAdvertContent":
                        newFileName = "AdvertContent" + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetFileName(fileToUpload.FileName);
                        break;
                   
                    default:
                        break;
                }

                string filePath = Path.Combine(serverPath, newFileName);
                string saveUrl = string.Empty;
                try
                {
                    fileToUpload.SaveAs(filePath);
                    //saveUrl = System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + path + "/" + newFileName;
                    saveUrl = path + "/" + newFileName;
                    switch (action)
                    {
                        case "UploadImgForAdvertContent":
                            context.Response.Write("{\"error\":0,\"url\":\"" + saveUrl + "\"}");
                            break;
                    }
                    //context.Response.Write("{ error : 0, name : '" + fileToUpload.FileName + "', url : '" + HttpUtility.UrlEncode(saveUrl) + "' }");
                }
                catch (Exception ex)
                {
                    context.Response.Write("System Error");
                }
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