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

namespace UCSHandler.Organiz
{
    /// <summary>
    /// Organiz 的摘要说明
    /// </summary>
    public class Organiz : IHttpHandler
    {

        Org_MechanismService bll = new Org_MechanismService();
        Sys_LogInfoService log = new Sys_LogInfoService();
        StringBuilder orgJson = new StringBuilder();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        BLLCommon bll_com = new BLLCommon();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["func"];
            string accountNo = context.Request["accountNo"];
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
                        case "GetOrgMenu":
                            string retdata = "[" + GetOrgMenu() + "]";
                            jsonModel = new JsonModel()
                            {
                                errNum = 0,
                                errMsg = "success",
                                retData = retdata
                            };
                            break;
                        //添加组织架构
                        case "AddOrg":
                            AddOrg(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Org_Mechanism", " id=" + context.Request["Pid"], "新建组织架构");
                            break;
                        //编辑组织架构
                        case "EditOrgDetail":
                            EditOrgDetail(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Org_Mechanism", " id=" + context.Request["ID"], "编辑组织架构");
                            break;
                        //编辑组织架构
                        case "EditOrder":
                            EditOrder(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Org_Mechanism", " id=" + context.Request["ID"], "编辑组织架构排序");
                            break;
                        //编辑组织架构
                        case "DelMenu":
                            DelMenu(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Org_Mechanism", " id=" + context.Request["ID"], "删除组织架构");
                            break;
                        case "GetData":
                            GetData(context);
                            break;
                        case "CensusOrg":
                            CensusOrg(context);
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

        #region 统计不同类型的组织机构个数
        /// <summary>
        /// 统计不同类型的组织机构个数
        /// </summary>
        /// <param name="context"></param>
        private void CensusOrg(HttpContext context)
        {
            string OrgType = context.Request["OrgType"].SafeToString();
            jsonModel = bll.censusOrg(OrgType);

        }
        #endregion

        #region 根据ID获取组织架构信息
        private void GetData(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TableName", "Org_Mechanism");

                bool Ispage = true;
                if (context.Request["Ispage"].SafeToString().Length > 0)
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }
                string where = "";
                if (!string.IsNullOrWhiteSpace(context.Request["ID"]))
                    where += " and ID=" + context.Request["ID"].SafeToString();
                if (!string.IsNullOrWhiteSpace(context.Request["HeadteacherNO"]))
                    where += " and LegalUID ='" + context.Request["HeadteacherNO"].SafeToString()+"'";

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
        #region 获取组织架构菜单
        private string GetOrgMenu(string pid = "0")
        {
            try
            {
                int num = 1;
                DataTable dt = bll.GetOrgMenu(pid);
                int dtcount = dt.Rows.Count;
                if (dtcount > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        if (num < dtcount)
                        {
                            orgJson.Append("{\"id\":" + row["Id"].ToString() + ", \"pId\": " + row["Pid"].ToString()
                                             + ", \"name\":\"" + row["Name"].ToString() + "\", \"org\":\"" + row["OrganNo"].ToString() + "\"},");
                            num++;
                        }
                        else
                        {
                            orgJson.Append("{\"id\":" + row["Id"].ToString() + ", \"pId\": " + row["Pid"].ToString()
                                                + ", \"name\":\"" + row["Name"].ToString() + "\", \"org\":\"" + row["OrganNo"].ToString() + "\"}");
                            num++;
                        }
                        //     orgJson.Append("{\"id\":" + row["Id"].ToString() + ", \"pid\": " + row["Pid"].ToString()
                        //+ ", \"name\":\"" + row["Name"].ToString() + "\", \"org\":\"" + row["OrganNo"].ToString() + "\",\"children\":[");
                        //     GetOrgMenu(row["Id"].ToString());
                        //     string endStr = (i + 1) == dtcount ? "]}" : "]},";
                        //orgJson.Append(endStr);
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
        #endregion

        #region 新建组织架构
        private void AddOrg(HttpContext context)
        {
            try
            {
                Org_Mechanism org = new Org_Mechanism();
                org.Name = context.Request["Name"].SafeToString();
                org.Pid = 0;
                if (context.Request["Pid"].SafeToString().Length > 0)
                {
                    org.Pid = Convert.ToInt32(context.Request["Pid"]);
                }
                org.OrganType = int.Parse(context.Request["OrganType"]);
                jsonModel = bll.AddOrg(org);
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
            }

        }
        #endregion

        #region 编辑组织架构
        private void EditOrgDetail(HttpContext context)
        {
            int OrgId = Convert.ToInt32(HttpContext.Current.Request["ID"]);

            jsonModel = bll.GetEntityById(OrgId);
            if (jsonModel.errNum == 0)
            {
                Org_Mechanism org = (Org_Mechanism)jsonModel.retData;
                org.Id = OrgId;
                string Name = context.Request["Name"].SafeToString();
                if (Name.Length > 0)
                {
                    org.Name = context.Request["Name"].SafeToString();
                }
                string OrganNo = context.Request["OrganNo"].SafeToString();
                if (OrganNo.Length > 0)
                {
                    org.OrganNo = context.Request["OrganNo"].SafeToString();
                }

                string ImageInfo = context.Request["ImageInfo"].SafeToString();
                if (ImageInfo.Length > 0)
                {
                    org.ImageInfo = ImageInfo;
                }
                string Introduce = context.Request["Introduce"].SafeToString();
                if (Introduce.Length > 0)
                {
                    org.Introduce = Introduce;
                }
                if (context.Request["EstabLish"].SafeToString().Length > 0)
                {
                    org.EstabLish = Convert.ToDateTime(context.Request["EstabLish"]);
                }
                string LegalUID = context.Request["LegalUID"].SafeToString();
                if (LegalUID.Length > 0)
                {
                    org.LegalUID = LegalUID;
                }
                if (context.Request["IsDelete"].SafeToString().Length > 0)
                {
                    org.IsDelete = 0;
                }
                if (context.Request["UserCount"].SafeToString().Length > 0)
                {
                    org.UserCount = Convert.ToInt32(context.Request["UserCount"]);
                }
                jsonModel = bll.EditOrg(org);
            }
        }
        #endregion

        #region 编辑组织架构排序
        private void EditOrder(HttpContext context)
        {
            int OrgId = Convert.ToInt32(HttpContext.Current.Request["ID"]);
            string Type = context.Request["OrderType"].SafeToString();
            jsonModel = bll.EditOrgOrder(OrgId, Type);
        }
        #endregion
        #region 删除组织架构
        private void DelMenu(HttpContext context)
        {
            int OrgId = Convert.ToInt32(HttpContext.Current.Request["ID"]);

            jsonModel = bll.DeleteOrg(OrgId);

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