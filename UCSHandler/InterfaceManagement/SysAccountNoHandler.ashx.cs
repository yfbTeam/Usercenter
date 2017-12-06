using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UCSBLL;
using UCSModel;
using UCSUtility;

namespace UCSHandler.InterfaceManagement
{
    /// <summary>
    /// SysAccountNoHandler 的摘要说明
    /// </summary>
    public class SysAccountNoHandler : IHttpHandler
    {
        Sys_SystemInfoService bll = new Sys_SystemInfoService();
        Sys_LogInfoService log = new Sys_LogInfoService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        BLLCommon bll_com = new BLLCommon();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["Func"];
            string accountNo = context.Request["SysAccountNo"];
            string loginname = context.Request["LoginName"] ?? "";
            string logType = context.Request["logType"] ?? "2";
            string result = string.Empty;
            try
            {
                if (accountNo != ConfigHelper.GetConfigString("SysAccountNo.ucc"))
                {
                    jsonModel = bll_com.IsHasInterAuth(accountNo, func);
                    log.WriteLog(accountNo, loginname, func, logType, "", "Sys_LogInfo", "", "判断是否有访问接口的权限");
                }
                else { logType = "1"; }
                if (jsonModel.errNum == 0)
                {
                    switch (func)
                    {
                        case "GetSystemInfoDataPage":
                            GetSystemInfoDataPage(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SystemInfo", "", "获取系统账号表的分页数据");
                            break;
                        case "GetSystemInfoById":
                            GetSystemInfoById(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SystemInfo", " id="+context.Request["ItemId"], "根据Id获取系统账号详情");
                            break;
                        case "AddSystemInfo":
                            AddSystemInfo(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SystemInfo", "", "添加系统账号");
                            break;
                        case "EditSystemInfo":
                            EditSystemInfo(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SystemInfo", " id=" + context.Request["sysid"], "修改系统账号");
                            break;
                        case "EnableSystemInfo":
                            EnableSystemInfo(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SystemInfo", " id=" + context.Request["enableid"], "启用/禁用系统账号");
                            break;
                        case "GetInterfaceByAccountNo":
                            GetInterfaceByAccountNo(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SysOfInter_Rel", " AccountNo=" + context.Request["AccountNo"], "获取系统账户的接口信息");
                            break;
                        case "SetInterfacePermission":
                            SetInterfacePermission(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SysOfInter_Rel", " AccountNo=" + context.Request["AccountNo"], "接口权限配置");
                            break;
                        case "GetEntityByAccountNo":
                            GetEntityByAccountNo(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SysOfEntity_Rel", " AccountNo=" + context.Request["AccountNo"], "获取系统账户的实体信息");
                            break;
                        case "SetEntityPermission":
                            SetEntityPermission(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SysOfEntity_Rel", " AccountNo=" + context.Request["AccountNo"], "实体权限配置");
                            break;
                        case "EditEntityRel":
                            EditEntityRel(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_SysOfEntity_Rel", " AccountNo=" + context.Request["AccountNo"], "实体字段配置");
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

        #region 获取系统账号表的分页数据
        private void GetSystemInfoDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("AccountNo", context.Request["AccountNo"] ?? "");
                ht.Add("TableName", "Sys_SystemInfo");
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = bll.GetPage(ht, ispage);
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

        #region 根据Id获取系统账号详情
        private void GetSystemInfoById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加系统账号
        private void AddSystemInfo(HttpContext context)
        {
            string name = context.Request["Name"], accountNo = context.Request["AccountNo"];
            jsonModel = bll.IsNameExists(accountNo, 0, "AccountNo");
            if (jsonModel.errNum == 0)
            {
                if (jsonModel.retData.ToString().ToLower() == "true")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = -1,
                        errMsg = "exist",
                        retData = ""
                    };
                }
                else
                {
                    Sys_SystemInfo sys = new Sys_SystemInfo();
                    sys.Name = name;
                    sys.AccountNo = accountNo;
                    sys.CreateUID = context.Request["LoginUID"];
                    sys.CreateTime = DateTime.Now;
                    jsonModel = bll.Add(sys);
                }
            }
        }
        #endregion        

        #region 修改系统账号
        private void EditSystemInfo(HttpContext context)
        {
            int sysid = Convert.ToInt32(context.Request["ItemId"]);
            string name = context.Request["Name"], accountNo = context.Request["AccountNo"];
            Sys_SystemInfo sys = new Sys_SystemInfo();
            sys.Id = sysid;
            sys.Name = name;
            sys.AccountNo = accountNo;
            sys.EditUID = context.Request["LoginUID"];
            jsonModel = bll.EditSystemInfo(sys);
        }
        #endregion

        #region 启用/禁用系统账号
        private void EnableSystemInfo(HttpContext context)
        {
            int isenable = Convert.ToInt32(context.Request["IsEnable"]);
            string[] ids = context.Request["IDs"].Split(',');
            jsonModel = bll.DeleteBatchFalse(isenable, ids);
        }
        #endregion

        #region 获取系统账户的接口信息
        private void GetInterfaceByAccountNo(HttpContext context)
        {
            string accountNo = context.Request["AccountNo"];
            jsonModel = bll.GetInterfaceByAccountNo(accountNo);
        }
        #endregion

        #region 接口权限配置
        private void SetInterfacePermission(HttpContext context)
        {
            try
            {
                string accountNo = context.Request["AccountNo"];
                string interidStr = context.Request["InteridStr"];
                jsonModel = bll.SetInterfacePermission(accountNo, interidStr);
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

        #region 获取系统账户的实体信息
        private void GetEntityByAccountNo(HttpContext context)
        {
            string accountNo = context.Request["AccountNo"];
            string entityName = context.Request["EntityName"]??"";
            jsonModel = bll.GetEntityByAccountNo(accountNo, entityName);
        }
        #endregion

        #region 实体权限配置
        private void SetEntityPermission(HttpContext context)
        {
            try
            {                
                Sys_SysOfEntity_Rel rel = new Sys_SysOfEntity_Rel();
                rel.AccountNo = context.Request["AccountNo"];
                rel.EntityName = context.Request["EntityNameStr"];                
                jsonModel = bll.SetEntityPermission(rel);
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

        #region 实体字段配置
        private void EditEntityRel(HttpContext context)
        {
            try
            {
                Sys_SysOfEntity_Rel rel = new Sys_SysOfEntity_Rel();
                rel.AccountNo = context.Request["AccountNo"];
                rel.EntityName = context.Request["EntityName"];               
                rel.FieldsEng = context.Request["FieldsEng"];
                jsonModel = bll.EditEntityRel(rel);
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