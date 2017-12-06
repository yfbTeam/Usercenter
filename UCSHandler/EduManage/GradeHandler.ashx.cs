using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UCSBLL;
using UCSModel;
using UCSUtility;
using System.Collections;
using System.Data;
using System.Text;

namespace UCSHandler.EduManage
{
    /// <summary>
    /// GradeHandler 的摘要说明
    /// </summary>
    public class GradeHandler : IHttpHandler
    {
        Sys_GradeInfoService bll = new Sys_GradeInfoService();
        Sys_LogInfoService log = new Sys_LogInfoService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        BLLCommon bll_com = new BLLCommon();
        StringBuilder orgJson = new StringBuilder();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["func"];
            string accountNo = context.Request["SysAccountNo"];
            string loginname = HttpContext.Current.Request["loginname"] ?? "";
            string logType = context.Request["logType"] ?? "2";
            string uniqueNo = HttpContext.Current.Request["uniqueNo"] ?? "";
            string result = string.Empty;
            try
            {
                if (jsonModel.errNum == 0)
                {
                    switch (func)
                    {
                        case "GetGradClass":
                            string retdata = "[" + GetGradClass() + "]";
                            jsonModel = new JsonModel()
                            {
                                errNum = 0,
                                errMsg = "success",
                                retData = retdata
                            };
                            break;

                        //获取数据信息
                        case "GetData":
                            GetData(context);
                            break;
                        //添加年级信息
                        case "AddGrade":
                            AddGrade(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_GradeInfo", "", "新建年级信息");
                            break;
                        //编辑年级信息
                        case "EditGrade":
                            EditGrade(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_GradeInfo", " id=" + context.Request["ID"], "编辑年级信息");
                            break;
                        //编辑年级信息
                        case "DelGrade":
                            DelGrade(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_GradeInfo", " id=" + context.Request["ID"], "删除年级信息");
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
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();
        }
        /// <summary>
        /// 获取年级班级信息
        /// </summary>
        /// <param name="context"></param>
        private string GetGradClass(string pid = "0", string PPid = "0")
        {
            try
            {
                if (PPid == "0")
                {

                    string AcademicId = HttpContext.Current.Request["AcademicId"].SafeToString();
                    DataTable dt = bll.GetGradClass(pid, AcademicId);
                    int dtcount = dt.Rows.Count;
                    if (dtcount > 0)
                    {
                        for (int i = 0; i < dtcount; i++)
                        {
                            DataRow row = dt.Rows[i];
                            orgJson.Append("{\"id\":\"" + row["Id"].ToString() + "\", \"pid\": " + row["Pid"].ToString()
                       + ", \"name\":\"" + row["Name"].ToString() + "\",\"children\":[");
                            GetGradClass(row["Id"].ToString(), pid);
                            //getClassByGrade(AcademicId, row["Id"].ToString());
                            string endStr = (i + 1) == dtcount ? "]}" : "]},";
                            orgJson.Append(endStr);
                        }
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
            return orgJson.SafeToString();
        }
        private void getClassByGrade(string AcademicId, string GradeId)
        {
            DataTable dt = bll.GetGradClass(GradeId, AcademicId);
            int dtcount = dt.Rows.Count;

            for (int i = 0; i < dtcount; i++)
            {
                DataRow row = dt.Rows[i];
                orgJson.Append("{\"id\":" + row["Id"].ToString() + ", \"pid\": " + row["Pid"].ToString()
           + ", \"name\":\"" + row["Name"].ToString() + "\",\"children\":[");
                string endStr = (i + 1) == dtcount ? "]}" : "]},";
            }
        }
        #region 获取班级信息
        private void GetData(HttpContext context)
        {
            try
            {
                //string where = "";
                Hashtable ht = new Hashtable();
                ht.Add("AcademicId", context.Request["AcademicId"].SafeToString());
                ht.Add("ID", context.Request["ID"].SafeToString());
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("GradeName", context.Request["GradeName"].SafeToString());


                bool Ispage = true;
                if (context.Request["Ispage"].SafeToString().Length > 0)
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }
                jsonModel = bll.GetPage(ht, Ispage);

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

        #region 新建班级信息
        private void AddGrade(HttpContext context)
        {
            Sys_GradeInfo org = new Sys_GradeInfo();
            org.OrganNo = context.Request["OrganNo"].SafeToString();
            org.GradeName = context.Request["StartDate"].SafeToString();
            org.IsGraduate = Convert.ToByte(context.Request["IsGraduate"]);
            org.PeriodID = Convert.ToInt32(context.Request["Semester"]);
            org.CreateUID = context.Request["CreateUID"].SafeToString();
            org.OrganNo = context.Request["OrganNo"].SafeToString();
            jsonModel = bll.Add(org);
        }
        #endregion

        #region 编辑班级信息
        private void EditGrade(HttpContext context)
        {
            int OrgId = Convert.ToInt32(HttpContext.Current.Request["ID"]);

            jsonModel = bll.GetEntityById(OrgId);
            if (jsonModel.errNum == 0)
            {
                Sys_GradeInfo org = (Sys_GradeInfo)jsonModel.retData;

                org.Leader = context.Request["Leader"].SafeToString();
                jsonModel = bll.Update(org);
            }
        }
        #endregion

        #region 删除年级信息
        private void DelGrade(HttpContext context)
        {
            string[] GradeIds = HttpContext.Current.Request["ID"].SafeToString().TrimEnd(',').Split(',');
            for (int i = 0; i < GradeIds.Length; i++)
            {
                if (GradeIds[i].Length > 0)
                {
                    jsonModel = bll.DelGrade(GradeIds[i]);
                }
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