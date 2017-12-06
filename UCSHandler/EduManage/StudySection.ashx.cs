using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UCSBLL;
using UCSModel;
using UCSUtility;

namespace UCSHandler.EduManage
{
    /// <summary>
    /// StudySection 的摘要说明
    /// </summary>
    public class StudySection : IHttpHandler
    {
        Sys_StudySectionService bll = new Sys_StudySectionService();
        Sys_LogInfoService log = new Sys_LogInfoService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        BLLCommon bll_com = new BLLCommon();

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
                        //获取数据信息
                        case "GetData":
                            GetData(context);
                            break;
                        //添加学年学期
                        case "AddSection":
                            AddSection(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_StudySection", "", "新建学年学期");
                            break;
                        //编辑学年学期
                        case "EditSection":
                            EditSection(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_StudySection", " id=" + context.Request["ID"], "编辑学年学期");
                            break;
                        //编辑学年学期
                        case "DelSection":
                            DelSection(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_StudySection", " id=" + context.Request["ID"], "删除学年学期");
                            break;
                        //编辑学年学期
                        case "CopySection":
                            CopySection(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_StudySection", " ", "复制学年学期");
                            break;
                        case "GetSectionStudentData":
                            GetSectionStudentData(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_StudySection", " ", "获取每学年学期学生人数");
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
        /// 复制学期
        /// </summary>
        /// <param name="context"></param>
        private void CopySection(HttpContext context)
        {
            Sys_StudySection org = new Sys_StudySection();
            org.Academic = context.Request["Academic"].SafeToString();
            org.StartDate = Convert.ToDateTime(context.Request["StartDate"]);
            org.EndDate = Convert.ToDateTime(context.Request["EndDate"]);
            org.Semester = context.Request["Semester"].SafeToString();
            org.Id = Convert.ToInt32(context.Request["OldSectionID"]);
            org.IsDelete = Convert.ToByte(context.Request["IsDelete"]);
            int isUp = Convert.ToInt32(context.Request["IsUp"]);
            jsonModel = bll.CopySection(org, isUp);
        }
        #region 获取学年学期信息
        private void GetData(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("ID", context.Request["ID"].SafeToString());
                ht.Add("Status", context.Request["Status"].SafeToString());
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

        #region 新建学年学期
        private void AddSection(HttpContext context)
        {
            Sys_StudySection org = new Sys_StudySection();
            org.Academic = context.Request["Academic"].SafeToString();
            org.StartDate = Convert.ToDateTime(context.Request["StartDate"]);
            org.EndDate = Convert.ToDateTime(context.Request["EndDate"]);
            org.Semester = context.Request["Semester"].SafeToString();
            org.PeriodIDs = context.Request["PeriodIDs"].SafeToString().TrimEnd(',');
            org.CreateUID = context.Request["CreateUID"].SafeToString();
            org.IsDelete = Convert.ToByte(context.Request["IsDelete"]);
            string Small = context.Request["Small"].SafeToString();
            string SmallL = context.Request["SmallL"].SafeToString();
            string Center = context.Request["Center"].SafeToString();
            string CenterL = context.Request["CenterL"].SafeToString();

            string High = context.Request["High"].SafeToString();
            string HighL = context.Request["HighL"].SafeToString();
            jsonModel = bll.AddSection(org, Small, SmallL, Center, CenterL, High, HighL);
        }
        #endregion

        #region 编辑学年学期
        private void EditSection(HttpContext context)
        {

            int OrgId = Convert.ToInt32(HttpContext.Current.Request["ID"]);

            jsonModel = bll.GetEntityById(OrgId);
            if (jsonModel.errNum == 0)
            {
                Sys_StudySection org = (Sys_StudySection)jsonModel.retData;
                org.Academic = context.Request["Academic"].SafeToString();
                org.StartDate = Convert.ToDateTime(context.Request["StartDate"]);
                org.EndDate = Convert.ToDateTime(context.Request["EndDate"]);
                org.Semester = context.Request["Semester"].SafeToString();
                org.IsDelete = Convert.ToByte(context.Request["IsDelete"]);
                jsonModel = bll.Update(org);
            }
        }
        #endregion

        #region 删除学期信息
        private void DelSection(HttpContext context)
        {
            string ID = HttpContext.Current.Request["ID"];
            string[] arry = ID.Split(',');
            for (int i = 0; i < arry.Length; i++)
            {
                if (arry[i] != "")
                {
                    jsonModel = bll.DelSection(arry[i]);
                }
            }
        }
        #endregion

        #region 获取每学年学期学生人数
        private void GetSectionStudentData(HttpContext context)
        {
            try
            {                
                jsonModel = bll.GetSectionStudentData();
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}