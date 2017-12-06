using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UCSBLL;
using UCSModel;
using UCSUtility;
namespace UCSHandler
{
    /// <summary>
    /// UserSkin 的摘要说明
    /// </summary>
    public class UserSkinHander : IHttpHandler
    {
        UserSkimLogService bll = new UserSkimLogService();
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

                        //新增用户浏览记录
                        case "AddSkim":
                            AddSkim(context);
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
        private void GetData(HttpContext context)
        {
            try
            {
                string where = "";
                Hashtable ht = new Hashtable();
                ht.Add("WebSite", context.Request["WebSite"].SafeToString());
                ht.Add("MinLong", context.Request["MinLong"].SafeToString());
                ht.Add("MaxLong", context.Request["MaxLong"].SafeToString());
                ht.Add("MinTime", context.Request["MinTime"].SafeToString());
                ht.Add("MaxTime", context.Request["MaxTime"].SafeToString());
                ht.Add("ToUrl", context.Request["ToUrl"].SafeToString());
                ht.Add("UserName", context.Request["UserName"].SafeToString());
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());

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

        private void AddSkim(HttpContext context)
        {
            UserSkimLog Uskin = new UserSkimLog();
            Uskin.UniqueNo = context.Request["UniqueNo"].SafeToString();
            Uskin.UserName = context.Request["LoginName"].SafeToString();
            Uskin.ToUrl = context.Request["ToUrl"].SafeToString();
            Uskin.FromUrl = context.Request["FromUrl"].SafeToString();
            Uskin.SkinLong = int.Parse(context.Request["SkinLong"]);
            Uskin.WebSite = context.Request["WebSite"].SafeToString();
            jsonModel = bll.Add(Uskin);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}