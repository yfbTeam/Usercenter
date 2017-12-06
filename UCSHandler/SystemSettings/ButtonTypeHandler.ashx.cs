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
    /// ButtonTypeHandler 的摘要说明
    /// </summary>
    public class ButtonTypeHandler : IHttpHandler
    {
        Sys_ButtonTypeService bll = new Sys_ButtonTypeService();
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
            string uniqueNo = context.Request["UniqueNo"];
            string logType = context.Request["logType"] ?? "2";
            string result = string.Empty;
            try
            {
                if (accountNo != ConfigHelper.GetConfigString("SysAccountNo.ucc"))
                {
                    jsonModel = bll_com.IsHasInterAuth(accountNo, func);
                    log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ButtonType", "", "判断是否有访问接口的权限");
                }
                else { logType = "1"; }
                if (jsonModel.errNum == 0)
                {
                    switch (func)
                    {
                        case "GetButtonTypeDataPage":
                            GetButtonTypeDataPage(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_ButtonType", "", "获取按钮类型表的分页数据");
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

        #region 获取按钮类型表的分页数据
        private void GetButtonTypeDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Pid", context.Request["Pid"] ?? "0");
                ht.Add("Value", context.Request["Value"] ?? "");
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}