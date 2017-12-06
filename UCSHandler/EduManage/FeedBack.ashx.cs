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
    /// FeedBack 的摘要说明
    /// </summary>
    public class FeedBack : IHttpHandler
    {

        FeedBack_StuListService bll = new FeedBack_StuListService();
        Sys_LogInfoService log = new Sys_LogInfoService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["func"];
            string result = string.Empty;
            try
            {
                if (jsonModel.errNum == 0)
                {
                    switch (func)
                    {
                        //获取数据信息
                        case "Add":
                            Add(context);
                            break;
                        //添加班级信息
                        case "Edit":
                            Edit(context);
                            break;
                        //编辑班级信息
                        case "Del":
                            Del(context);
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


        #region 新建
        private void Add(HttpContext context)
        {
            FeedBack_StuList org = new FeedBack_StuList();
            org.StuNo = context.Request["StuNo"].SafeToString();

            jsonModel = bll.Add(org);
        }
        #endregion

        #region 编辑
        private void Edit(HttpContext context)
        {
            FeedBack_StuList entity = (FeedBack_StuList)bll.GetEntityListByField("StuNo", context.Request["StuNo"]).retData;
            entity.Status = Convert.ToInt32(context.Request["Status"]);
            jsonModel = bll.Update(entity);
        }
        #endregion

        #region 删除
        private void Del(HttpContext context)
        {
            FeedBack_StuList entity = (FeedBack_StuList)bll.GetEntityListByField("StuNo", context.Request["StuNo"]).retData;
            int id = Convert.ToInt32(entity.ID);
            jsonModel = bll.Delete(id);
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