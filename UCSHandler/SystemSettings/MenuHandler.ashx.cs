using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using UCSBLL;
using UCSModel;
using UCSUtility;

namespace UCSHandler.SystemSettings
{
    /// <summary>
    /// MenuHandler 的摘要说明
    /// </summary>
    public class MenuHandler : IHttpHandler
    {
        Sys_MenuInfoService bll = new Sys_MenuInfoService();
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
            string uniqueNo = context.Request["uniqueNo"] ?? "";
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
                        case "GetMenuData":
                            GetMenuData(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_MenuInfo", "", "获取菜单数据");
                            break;
                        case "GetNavigationMenu":
                            GetNavigationMenu(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_MenuInfo", "", "获取系统导航菜单");
                            break;
                        case "GetParentMenuByUrl":
                            GetParentMenuByUrl(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_MenuInfo", "", "获取父级菜单");
                            break;
                        case "GetMenuByPidAndUniqueNo":
                            GetMenuByPidAndUniqueNo(uniqueNo);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_MenuInfo", "", "根据pid和用户唯一号查找菜单");
                            break;
                        case "GetMenuById":
                            GetMenuById(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_MenuInfo", " id=" + context.Request["ItemId"], "根据Id获取菜单详情");
                            break;
                        case "AddMenuInfo":
                            AddMenuInfo(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_MenuInfo", "", "新建菜单");
                            break;
                        case "EditMenuInfo":
                            EditMenuInfo(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_MenuInfo", " id=" + context.Request["ItemId"], "修改菜单");
                            break;
                        case "DeleteMenuInfo":
                            DeleteMenuInfo(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_MenuInfo", " id=" + context.Request["ItemId"], "删除菜单");
                            break;
                        case "GetSubButtonByUrl":
                            GetSubButtonByUrl(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_MenuInfo", " Url=" + context.Request["Url"], "根据Url获取页面按钮");
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
        #region 获取菜单数据
        private void GetMenuData(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                if (context.Request["RoleId"]!=null)
                {
                    ht.Add("RoleId", context.Request["RoleId"]);
                }                
                ht.Add("IsMenu", context.Request["IsMenu"] ?? "");
                ht.Add("IsShow", context.Request["IsShow"] ?? "");
                jsonModel = bll.GetPage(ht, false);
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

        #region 获取系统导航菜单
        private void GetNavigationMenu(HttpContext context)
        {
            try
            {
                bool isAllLeaf = false;
                if (!string.IsNullOrEmpty(context.Request["IsAllLeaf"]))
                {
                    isAllLeaf = Convert.ToBoolean(context.Request["IsAllLeaf"]);
                }
                jsonModel = bll.GetNavigationMenu(context.Request["UniqueNo"], context.Request["Pid"] ?? "0", isAllLeaf);                
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

        #region 获取父级菜单
        private void GetParentMenuByUrl(HttpContext context)
        {
            try
            {
                jsonModel = bll.GetParentMenuByUrl(context.Request["Url"]);
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

        #region 根据pid和用户唯一号查找菜单
        private void GetMenuByPidAndUniqueNo(string uniqueNo)
        {
            try
            {
                string pid = HttpContext.Current.Request["pid"] ?? "";
                jsonModel = bll.GetMenuByPidAndUniqueNo(uniqueNo, pid);
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

        #region 根据Id获取菜单详情
        private void GetMenuById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 新建菜单
        private void AddMenuInfo(HttpContext context)
        {
            string name = context.Request["Name"];
            Sys_MenuInfo menu = new Sys_MenuInfo();
            menu.Id = 0;
            menu.Name = name;
            menu.Pid = Convert.ToInt32(context.Request["pid"]);
            menu.Url = context.Request["url"];
            menu.Description = context.Request["description"];
            menu.IsMenu = context.Request["ismenu"] == "0" ? false : true;
            menu.IsShow = Convert.ToByte(context.Request["isshow"]);
            menu.MenuCode= context.Request["MenuCode"];
            jsonModel = bll.EditMenu(menu);
        }
        #endregion        

        #region 修改菜单
        private void EditMenuInfo(HttpContext context)
        {
            int menuid = Convert.ToInt32(context.Request["ItemId"]);
            string name = context.Request["Name"];
            Sys_MenuInfo menu =new Sys_MenuInfo();
            menu.Id = menuid;
            menu.Name = name;
            menu.Pid = Convert.ToInt32(context.Request["pid"]);
            menu.Url = context.Request["url"];
            menu.Description = context.Request["description"];
            menu.IsMenu = context.Request["ismenu"] == "0" ? false : true;
            menu.IsShow = Convert.ToByte(context.Request["isshow"]);
            menu.MenuCode = context.Request["MenuCode"];
            jsonModel = bll.EditMenu(menu);
        }
        #endregion

        #region 删除菜单
        private void DeleteMenuInfo(HttpContext context)
        {
            int menuid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.DeleteMenuInfo(menuid);
        }
        #endregion

        #region 根据url查找子级button 
        private void GetSubButtonByUrl(HttpContext context)
        {
            try
            {
                jsonModel = bll.GetSubButtonByUrl(context.Request["Url"], context.Request["UniqueNo"], context.Request["MenuCode"]??"");
                jsonModel.errMsg = context.Request["UniqueNo"].SafeToString() == ConfigHelper.GetConfigString("SuperAdmin") ? "1" : "0";
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