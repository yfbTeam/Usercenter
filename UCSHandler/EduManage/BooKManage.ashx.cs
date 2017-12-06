using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCSBLL;
using UCSModel;
using UCSUtility;
using System.Collections;
using System.Web.Script.Serialization;

namespace UCSHandler.EduManage
{
    /// <summary>
    /// BooKManage 的摘要说明
    /// </summary>
    public class BooKManage : IHttpHandler
    {

        Sys_LogInfoService log = new Sys_LogInfoService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        BLLCommon com = new BLLCommon();
        string result = "";


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["func"];
            string accountNo = context.Request["SysAccountNo"];
            string loginname = HttpContext.Current.Request["loginname"] ?? "";
            string logType = context.Request["logType"] ?? "2";
            string uniqueNo = HttpContext.Current.Request["uniqueNo"] ?? "";
            try
            {
                if (jsonModel.errNum == 0)
                {
                    switch (func)
                    {
                        //获取专业信息
                        case "GetMajorInfo":
                            GetMajorInfo(context);
                            break;
                        //获取学科信息
                        case "GetSubJect":
                            GetSubJect(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", "", "获取学科信息");
                            break;
                        //获取版本信息
                        case "GetBookVersion":
                            GetBookVersion(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "获取版本信息");
                            break;
                        //获取版本信息
                        case "AddBookVersion":
                            AddBookVersion(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "获取版本信息");
                            break;
                        //获取版本信息
                        case "DelBookVersion":
                            DelBookVersion(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "删除版本信息");
                            break;
                        //获取目录信息
                        case "GetBookCatalog":
                            GetBookCatalog(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "获取目录信息");
                            break;
                        //添加目录信息
                        case "AddCatalog":
                            AddCatalog(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "添加目录信息");
                            break;
                        //添加目录信息
                        case "EditCatalog":
                            EditCatalog(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "添加目录信息");
                            break;
                        //删除目录信息
                        case "DelCatalog":
                            DelCatalog(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "删除目录信息");
                            break;
                        //获取目录信息
                        case "GetBook":
                            GetBook(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "获取目录信息");
                            break;
                        //获取目录信息
                        case "AddBook":
                            AddBook(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "获取目录信息");
                            break;
                        //获取所有信息
                        case "GetPSTVData":
                            GetPSTVData(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "删除班级信息");
                            break;

                        default:
                            jsonModel = new JsonModel()
                            {
                                errNum = 5,
                                errMsg = "没有此方法",
                                retData = ""
                            };
                            break;
                    }
                }
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
            if (result.Length == 0)
            {
                result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            }
            context.Response.Write(result);
            context.Response.End();
        }
        #region 获取教材信息
        private void AddBook(HttpContext context)
        {
            Edu_BookService MajorBll = new Edu_BookService();
            try
            {
                Edu_Book entity = new Edu_Book();
                entity.MajorID = Convert.ToInt32(context.Request["MajorID"]);
                entity.Name = context.Request["Name"].SafeToString();
                entity.VersionID = Convert.ToInt32(context.Request["VersionID"]);
                entity.SubID = Convert.ToInt32(context.Request["SubID"]);
                jsonModel = MajorBll.Add(entity);

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
        }
        private void GetBook(HttpContext context)
        {
            Edu_BookService MajorBll = new Edu_BookService();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("Name", context.Request["Name"].SafeToString());

                bool Ispage = true;
                if (context.Request["Ispage"].SafeToString().Length > 0)
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }
               
                jsonModel = MajorBll.GetPage(ht, Ispage);

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
        }
        #endregion

        #region 获取专业信息
        private void GetMajorInfo(HttpContext context)
        {
            string where = "";
            Edu_MajorInfoService MajorBll = new Edu_MajorInfoService();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TableName", "Edu_MajorInfo");
                bool Ispage = true;
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());

                if (context.Request["Ispage"].SafeToString().Length > 0)
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }
                if (context.Request["Name"].SafeToString().Length > 0)
                {
                    where += " and Name like '%" + context.Request["Name"].SafeToString() + "%'";
                }
                jsonModel = MajorBll.GetPage(ht, Ispage,where);

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
        }
        #endregion

        #region 获取学科信息
        private void GetSubJect(HttpContext context)
        {
            Edu_SubJectService MajorBll = new Edu_SubJectService();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("Name", context.Request["Name"].SafeToString());

                bool Ispage = true;
                if (context.Request["Ispage"].SafeToString().Length > 0)
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }

                jsonModel = MajorBll.GetPage(ht, Ispage);

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
        }
        #endregion

        #region 获取版本信息
        private void GetBookVersion(HttpContext context)
        {
            Edu_BookVersionService BookBll = new Edu_BookVersionService();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TableName", "Edu_BookVersion");
                bool Ispage = true;
                if (context.Request["Ispage"].SafeToString().Length > 0)
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }

                jsonModel = BookBll.GetPage(ht, Ispage);

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
        }
        private void DelBookVersion(HttpContext context)
        {
            Edu_BookVersionService BookBll = new Edu_BookVersionService();
            try
            {
                jsonModel = BookBll.Delete(Convert.ToInt32(context.Request["ID"]));

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
        }
        private void AddBookVersion(HttpContext context)
        {
            Edu_BookVersionService BookBll = new Edu_BookVersionService();
            Edu_BookVersion entity = new Edu_BookVersion();
            try
            {
                string Name = context.Request["Name"].SafeToString();

                jsonModel = BookBll.GetEntityListByField("Name", Name);
                if (jsonModel.errNum != 0)
                {
                    entity.Name = Name;
                    jsonModel = BookBll.Add(entity);
                }
                else
                {
                    jsonModel = new JsonModel
                    {
                        errNum = 999,
                        errMsg = "名称重复",
                        retData = ""
                    };

                }

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
        }
        #endregion

        #region 获取目录信息

        private void EditCatalog(HttpContext context)
        {
            Edu_BookCatalogService CatalogBll = new Edu_BookCatalogService();

            try
            {
                Edu_BookCatalog entity = new Edu_BookCatalog();
                string ID = "";
                if (!string.IsNullOrWhiteSpace(context.Request["Id"]))
                {
                    ID = context.Request["Id"].ToString();
                }
                jsonModel = CatalogBll.GetEntityById(Convert.ToInt32(ID));


                entity = (Edu_BookCatalog)(jsonModel.retData);

                entity.Name = context.Request["Name"].SafeToString();


                jsonModel = CatalogBll.Update(entity);

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
        }
        private void AddCatalog(HttpContext context)
        {
            Edu_BookCatalogService CatalogBll = new Edu_BookCatalogService();
            try
            {
                Edu_BookCatalog entity = new Edu_BookCatalog();
                entity.BookID = Convert.ToInt32(context.Request["BookID"]);
                entity.Name = context.Request["Name"].SafeToString();
                entity.Pid = Convert.ToInt32(context.Request["Pid"]);
                jsonModel = CatalogBll.Add(entity);

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
        }
        private void DelCatalog(HttpContext context)
        {
            Edu_BookCatalogService CatalogBll = new Edu_BookCatalogService();
            try
            {
                jsonModel = CatalogBll.Delete(Convert.ToInt32(context.Request["ID"]));

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
        }
        private void GetBookCatalog(HttpContext context)
        {
            Edu_BookCatalogService CatalogBll = new Edu_BookCatalogService();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("BookID", context.Request["BookID"].SafeToString());
                ht.Add("Pid", context.Request["Pid"].SafeToString());
                ht.Add("Name", context.Request["Name"].SafeToString());
                bool Ispage = true;
                if (context.Request["Ispage"].SafeToString().Length > 0)
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }

                jsonModel = CatalogBll.GetPage(ht, Ispage);

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
        }
        #endregion

        #region 获得学段、科目、教材、教材版本
        /// <summary>
        /// 获得学段、科目、教材、教材版本
        /// </summary>
        /// <param name="context"></param>
        public void GetPSTVData(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();

                ht.Add("func", "GetPSTVData");

                //年级、科目
                ht["Columns"] = "a.Id,b.Id as GradeID,b.Name,c.Id as SubjectID,c.Name as SubjectName";
                ht["TableName"] = "Edu_Major_Sub_Rel a inner join Edu_MajorInfo b on a.MajorID=b.Id inner join Edu_SubJect c on a.SubID=c.Id";
                result = "{";
                //result += ",";
                JsonModel GradeOfSubject = com.GetData_NoVerification(ht);
                result += "\"GradeOfSubject\":" + jss.Serialize(GradeOfSubject);
                //教材版本
                ht["Columns"] = "*";
                ht["TableName"] = "Edu_BookVersion";
                ht["Where"] = "";
                JsonModel TextbookVersion = com.GetData_NoVerification(ht);
                result += ",";
                result += "\"TextbookVersion\":" + jss.Serialize(TextbookVersion);
                //教材
                ht["Columns"] = "a.*,b.Name as VersionName";
                ht["TableName"] = "Edu_Book a left join Edu_BookVersion b on a.VersionID=b.Id";
                ht["Where"] = "";
                JsonModel Textbook = com.GetData_NoVerification(ht);
                result += ",";
                result += "\"Textbook\":" + jss.Serialize(Textbook);
                result += "}";
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
                return;
            }
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