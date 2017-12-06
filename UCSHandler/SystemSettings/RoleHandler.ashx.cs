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
    /// RoleHandler 的摘要说明
    /// </summary>
    public class RoleHandler : IHttpHandler
    {
        Sys_RoleService bll = new Sys_RoleService();
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
                        case "GetRoleDataPage":
                            GetRoleDataPage(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Role", "", "获取角色的分页数据");
                            break;
                        case "GetRoleById":
                            GetRoleById(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Role", "", "根据Id获取角色详情");
                            break;
                        case "GetRoleTreeData":
                            GetRoleTreeData(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Role", "", "获取角色树信息");
                            break;
                        case "GetRoleByUser":
                            GetRoleByUser(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_RoleOfUser", "", "获取某用户的角色信息");
                            break;
                        case "AddRole":
                            AddRole(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Role", "", "添加角色");
                            break;
                        case "EditRole":
                            EditRole(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Role", " id=" + context.Request["RoleId"], "修改角色");
                            break;
                        case "DeleteRole":
                            DeleteRole(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_Role", " id=" + context.Request["RoleId"], "删除角色");
                            break;
                        case "GetUserDataByRoleId":
                            GetUserDataByRoleId(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_RoleOfUser", "", "根据角色id获取角色下的用户");
                            break;                        
                        case "SetRoleMember":
                            SetRoleMember(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_RoleOfUser", " roleid=" + context.Request["RoleId"], "设置角色成员");
                            break;
                        case "DeleteUserRelation":
                            DeleteUserRelation(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_RoleOfUser", " roleid=" + context.Request["RoleId"], "将用户移出角色");
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
        #region 获取角色的分页数据
        private void GetRoleDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
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

        #region 根据Id获取角色详情
        private void GetRoleById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 获取角色树信息
        private void GetRoleTreeData(HttpContext context)
        {
            try
            {
                DataTable roledt = bll.GetAllRoleList();
                StringBuilder roleJson = new StringBuilder();
                if (roledt.Rows.Count > 0)
                {
                    for (int i = 0; i < roledt.Rows.Count; i++)
                    {
                        DataRow row = roledt.Rows[i];
                        roleJson.Append("{\"id\":" + row["Id"].ToString() + ", \"pId\": 0, \"name\":\"" + row["Name"].ToString() + "\"},");
                    }
                }
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = "[" + roleJson.ToString().TrimEnd(',') + "]"
                };
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

        #region 获取某用户的角色信息
        private void GetRoleByUser(HttpContext context)
        {
            try
            {
                string uniqueNo = context.Request["uniqueNo"];
                Hashtable ht = new Hashtable();
                ht.Add("UniqueNo", uniqueNo);
                jsonModel = bll.GetRoleByUser(ht);
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

        #region 添加角色
        private void AddRole(HttpContext context)
        {
            string name = context.Request["Name"];
            Sys_Role role = new Sys_Role();
            role.Id = 0;
            role.Name = name;
            role.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditRole(role,context.Request["Menuids"]);
        }
        #endregion        

        #region 修改角色
        private void EditRole(HttpContext context)
        {
            int roleid = Convert.ToInt32(context.Request["ItemId"]);
            string name = context.Request["Name"];
            Sys_Role role = new Sys_Role();
            role.Id = roleid;
            role.Name = name;
            role.EditUID = context.Request["LoginUID"];
            role.EditTime = DateTime.Now;
            jsonModel = bll.EditRole(role, context.Request["Menuids"]);
        }
        #endregion

        #region 删除角色
        private void DeleteRole(HttpContext context)
        {
            int roleid = Convert.ToInt32(context.Request["RoleId"]);
            jsonModel = bll.DeleteRole(roleid);
        }
        #endregion

        #region 根据角色id获取角色下的用户
        private void GetUserDataByRoleId(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("RoleId", context.Request["RoleId"]??"");
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }               
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = new Sys_RoleOfUserService().GetPage(ht,ispage);
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

        #region 设置角色成员
        private void SetRoleMember(HttpContext context)
        {
            try
            {
                string roleid = context.Request["RoleId"];
                string uniqueNoStr = context.Request["uniqueNoStr"];
                jsonModel = bll.SetRoleMember(roleid, uniqueNoStr);
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

        #region 将用户移出角色
        private void DeleteUserRelation(HttpContext context)
        {           
            string ids = context.Request["IDs"];
            jsonModel = bll.DeleteUserRelation(ids);
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