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
    /// ClassHandler 的摘要说明
    /// </summary>
    public class ClassHandler : IHttpHandler
    {

        Sys_ClassInfoService bll = new Sys_ClassInfoService();
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
                        //添加班级信息
                        case "AddClass":
                            AddClass(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", "", "添加班级信息");
                            break;
                        //编辑班级信息
                        case "EditClass":
                            EditClass(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ClassInfo", " id=" + context.Request["ID"], "编辑班级信息");
                            break;
                        //删除年级信息
                        case "DelClass":
                            DelClass(context);
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
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();
        }
        #region 获取班级信息
        private void GetData(HttpContext context)
        {
            try
            {
                string where = "";
                Hashtable ht = new Hashtable();
                ht.Add("AcademicId", context.Request["AcademicId"].SafeToString());
                ht.Add("ClassName", context.Request["ClassName"].SafeToString());
                ht.Add("ID", context.Request["ID"].SafeToString());
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("HeadteacherNO", context.Request["HeadteacherNO"].SafeToString());
                bool Ispage = true;
                if (context.Request["Ispage"].SafeToString().Length > 0)
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }

                jsonModel = bll.GetPage(ht, Ispage, where);

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
        private void AddClass(HttpContext context)
        {
            Sys_ClassInfo org = new Sys_ClassInfo();
            int AcademicId = Convert.ToInt32(context.Request["AcademicId"].SafeToString());
            org.ClassName = context.Request["ClassName"].SafeToString();
            org.ClassNO = context.Request["ClassNO"];
            org.CreateUID = context.Request["CreateUID"].SafeToString();
            org.HeadteacherNO = context.Request["HeadteacherNO"].SafeToString();
            org.MonitorNO = context.Request["MonitorNO"].SafeToString();
            int GradeId = Convert.ToInt32(context.Request["GradeId"]);
            jsonModel = bll.AddClass(org, GradeId, AcademicId);
        }
        #endregion

        #region 编辑班级信息
        private void EditClass(HttpContext context)
        {
            int ClassId = Convert.ToInt32(HttpContext.Current.Request["ID"]);
            Sys_ClassInfo org = new Sys_ClassInfo();
            org.Id = ClassId;
            int AcademicId = Convert.ToInt32(context.Request["AcademicId"].SafeToString());
            org.ClassName = context.Request["ClassName"].SafeToString();
            org.ClassNO = context.Request["ClassNO"];
            org.HeadteacherNO = context.Request["HeadteacherNO[]"].SafeToString();
            int GradeId = Convert.ToInt32(context.Request["GradeId"]);
            int OldSectionID = context.Request["OldSectionID"].SafeToString() == "" ? 0 : Convert.ToInt32(context.Request["OldSectionID"]);
            int OldGradeID = context.Request["OldGradeID"].SafeToString() == "" ? 0 : Convert.ToInt32(context.Request["OldGradeID"]);
            jsonModel = bll.EditClass(org, OldSectionID, OldGradeID,AcademicId,GradeId);

        }
        #endregion

        #region 删除班级信息
        private void DelClass(HttpContext context)
        {
            string[] ClassIds = HttpContext.Current.Request["ID"].Split(',');
            for (int i = 0; i < ClassIds.Length; i++)
            {
                if (ClassIds[i].Length > 0)
                {
                    jsonModel = bll.DelClass(ClassIds[i]);
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