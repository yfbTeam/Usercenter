using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UCSBLL;
using UCSModel;
using UCSUtility;

namespace UCSHandler.SystemSettings
{
    /// <summary>
    /// InterfaceHandler 的摘要说明
    /// </summary>
    public class InterfaceHandler : IHttpHandler
    {
        Sys_InterfaceService bll = new Sys_InterfaceService();
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
                        case "GetInterfaceDataPage":
                            GetInterfaceDataPage(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Interface", "", "获取数据库实体的分页数据");
                            break;
                        case "GetInterfaceById":
                            GetInterfaceById(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Interface", "", "根据Id获取接口详情");
                            break;
                        case "AddInterface":
                            AddInterface(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Interface", "", "添加接口");
                            break;
                        case "EditInterface":
                            EditInterface(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Interface", " id=" + context.Request["interid"], "修改接口");
                            break;
                        case "EnableInterface":
                            EnableInterface(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Interface", " id=" + context.Request["enableid"], "启用/禁用接口");
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

        #region 获取接口表的分页数据
        private void GetInterfaceDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("IsDelete", context.Request["IsDelete"] ?? "");
                ht.Add("OrderBy", context.Request["OrderBy"] ?? "");                
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

        #region 根据Id获取接口详情
        private void GetInterfaceById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加接口
        private void AddInterface(HttpContext context)
        {
            string name = context.Request["Name"];
            jsonModel = bll.IsNameExists(name);
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
                    Sys_Interface inter = new Sys_Interface();
                    inter.Name = name;
                    inter.Description= context.Request["Description"];
                    inter.CreateUID = context.Request["LoginUID"];
                    inter.CreateTime = DateTime.Now;
                    jsonModel = bll.Add(inter);
                }
            }
        }
        #endregion        

        #region 修改接口
        private void EditInterface(HttpContext context)
        {
            int interid = Convert.ToInt32(context.Request["ItemId"]);
            string name = context.Request["Name"];
            jsonModel = bll.IsNameExists(name, interid);
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
                    jsonModel = bll.GetEntityById(interid);
                    if (jsonModel.errNum == 0)
                    {
                        Sys_Interface inter = jsonModel.retData as Sys_Interface;
                        inter.Id = interid;
                        inter.Name = name;
                        inter.Description = context.Request["Description"];
                        inter.EditUID = context.Request["LoginUID"];
                        inter.EditTime = DateTime.Now;
                        jsonModel = bll.Update(inter);
                    }
                }
            }
        }
        #endregion

        #region 启用/禁用接口
        private void EnableInterface(HttpContext context)
        {
            int isenable = Convert.ToInt32(context.Request["IsEnable"]);
            string[] ids =context.Request["IDs"].Split(',');
            jsonModel = bll.DeleteBatchFalse(isenable, ids);            
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