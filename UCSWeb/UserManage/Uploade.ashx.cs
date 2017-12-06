using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using UCSModel;
using UCSUtility;
using UCSBLL;
using System.Web.Script.Serialization;

namespace UCSWeb.UserManage
{
    /// <summary>
    /// Uploade 的摘要说明
    /// </summary>
    public class Uploade : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string FuncName = context.Request["Func"].SafeToString();
            string result = string.Empty;

            context.Response.ContentType = "text/plain";
            if (FuncName != null && FuncName != "")
            {
                try
                {
                    switch (FuncName)
                    {
                        case "UplodExcel"://导入用户excel
                            UplodExcel(context);
                            break;
                        case "ImportUser":
                            ImportUser(context);
                            break;
                        case "UplodPhoto"://上传用户头像
                            UplodPhoto(context);
                            break;
                        case "UplodComponyImg"://上传用户头像
                            UplodComponyImg(context);
                            break;
                    }
                }
                catch (Exception ex) { }

            }
        } 
        #region 上传公司封面
        /// <summary>
        /// 上传微课资源
        /// </summary>
        /// <param name="context"></param>
        private void UplodComponyImg(HttpContext context)
        {
            string result = "";
            JsonModel JsonModel = new JsonModel();
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string Fpath = ConfigHelper.GetConfigString("FileManageName") + "/ComImg/";
            FileHelper.CreateDirectory(context.Server.MapPath(Fpath));

            string ext = System.IO.Path.GetExtension(file.FileName);

            string fileName = Path.GetFileName(file.FileName);// DateTime.Now.Ticks + ext;

            string p = Fpath + "/" + fileName;

            string path = context.Server.MapPath(p);
            #region 处理文件同名问题
            if (FileHelper.IsExistFile(path))
            {
                int i = 0;
                while (true)
                {
                    i++;
                    if (!FileHelper.IsExistDirectory(context.Server.MapPath(Fpath + "/" + fileName.Split('.')[0] + "(" + i + ")" + "." + fileName.Split('.')[1])))
                    {
                        fileName = fileName.Split('.')[0] + "(" + i + ")" + "." + fileName.Split('.')[1];
                        p = Fpath + "/" + fileName;
                        path = context.Server.MapPath(p);

                        break;
                    }
                }
            }
            #endregion

            file.SaveAs(path);

            JsonModel = new JsonModel()
            {
                errNum = 0,
                errMsg = "",
                retData = p
            };
            result = "{\"error\":0,\"url\":\"" + p + "\"}";
            context.Response.Write(result);
            context.Response.End();


        }
        #endregion

        #region 上传资源
        /// <summary>
        /// 上传微课资源
        /// </summary>
        /// <param name="context"></param>
        private void UplodExcel(HttpContext context)
        {
            string result = "";
            JsonModel JsonModel = new JsonModel();
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string Fpath = ConfigHelper.GetConfigString("FileManageName") + "/Excel/";
            FileHelper.CreateDirectory(context.Server.MapPath(Fpath));

            string ext = System.IO.Path.GetExtension(file.FileName);

            string fileName = Path.GetFileName(file.FileName);// DateTime.Now.Ticks + ext;

            string p = Fpath + "/" + fileName;

            string path = context.Server.MapPath(p);
            #region 处理文件同名问题
            if (FileHelper.IsExistFile(path))
            {
                int i = 0;
                while (true)
                {
                    i++;
                    if (!FileHelper.IsExistDirectory(context.Server.MapPath(Fpath + "/" + fileName.Split('.')[0] + "(" + i + ")" + "." + fileName.Split('.')[1])))
                    {
                        fileName = fileName.Split('.')[0] + "(" + i + ")" + "." + fileName.Split('.')[1];
                        p = Fpath + "/" + fileName;
                        path = context.Server.MapPath(p);

                        break;
                    }
                }
            }
            #endregion

            file.SaveAs(path);

            JsonModel = new JsonModel()
            {
                errNum = 0,
                errMsg = "",
                retData = p
            };
            result = "{\"error\":0,\"url\":\"" + p + "\"}";
            context.Response.Write(result);
            context.Response.End();


        }
        #endregion

        #region 用户导入
        /// <summary>
        /// 用户导入
        /// </summary>
        /// <param name="context"></param>
        public void ImportUser(HttpContext context)
        {
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            string result = "";
            JsonModel jsonModel = new JsonModel();
            Sys_UserInfoService bll = new Sys_UserInfoService();
            string FilePath = context.Request["FilePath"].SafeToString();
            string OrgNo = context.Request["OrgNo"].SafeToString();
            try
            {
                jsonModel = bll.ImportUser(context.Server.MapPath(FilePath), OrgNo);
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                LogService.WriteErrorLog(ex.Message);
            }
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();
        }
        #endregion

        #region 上传资源
        /// <summary>
        /// 上传微课资源
        /// </summary>
        /// <param name="context"></param>
        private void UplodPhoto(HttpContext context)
        {
            string result = "";
            JsonModel JsonModel = new JsonModel();
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string Fpath = ConfigHelper.GetConfigString("FileManageName") + "/UserPhoto/";
            FileHelper.CreateDirectory(context.Server.MapPath(Fpath));

            string ext = System.IO.Path.GetExtension(file.FileName);

            string fileName = Path.GetFileName(file.FileName);// DateTime.Now.Ticks + ext;

            string p = Fpath + "/" + fileName;

            string path = context.Server.MapPath(p);
            #region 处理文件同名问题
            if (FileHelper.IsExistFile(path))
            {
                int i = 0;
                while (true)
                {
                    i++;
                    if (!FileHelper.IsExistDirectory(context.Server.MapPath(Fpath + "/" + fileName.Split('.')[0] + "(" + i + ")" + "." + fileName.Split('.')[1])))
                    {
                        fileName = fileName.Split('.')[0] + "(" + i + ")" + "." + fileName.Split('.')[1];
                        p = Fpath + "/" + fileName;
                        path = context.Server.MapPath(p);

                        break;
                    }
                }
            }
            #endregion

            file.SaveAs(path);

            JsonModel = new JsonModel()
            {
                errNum = 0,
                errMsg = "",
                retData = p
            };
            result = "{\"error\":0,\"url\":\"" + p + "\"}";
            context.Response.Write(result);
            context.Response.End();


        }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}