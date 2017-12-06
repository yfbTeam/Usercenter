using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UCSBLL;
using UCSModel;
using UCSUtility;
using System.Collections;
using System.IO;
using System.Data;

namespace UCSHandler.UserManage
{
    /// <summary>
    /// UserInfo 的摘要说明
    /// </summary>
    public class UserInfo : IHttpHandler
    {

        Sys_UserInfoService bll = new Sys_UserInfoService();
        Sys_LogInfoService log = new Sys_LogInfoService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        BLLCommon bll_com = new BLLCommon();
        Org_MechanismService bll_ms = new Org_MechanismService();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["func"];
            string accountNo = context.Request["SysAccountNo"];
            string loginname = context.Request["LoginName"] ?? "";
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
                        case "GetDataToDataTable":
                            GetDataToDataTable(context);
                            return;
                            break;
                        //获取数据信息
                        case "GetData":
                            GetData(context);
                            break;
                        //导入用户
                        case "ImprtUser":
                            ImprtUser(context);
                            break;
                        //添加用户信息
                        case "AddUser":
                            AddUser(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_UserInfo", "", "添加用户信息");
                            break;
                        //编辑用户信息
                        case "EditUser":
                            EditUser(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_UserInfo", " id=" + context.Request["ID"], "编辑用户信息");
                            break;
                        case "ResetUser":
                            ResetUser(context);
                            break;
                        //编辑用户信息
                        case "DelUser":
                            DelUser(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_UserInfo", " id=" + context.Request["ID"], "删除用户信息");
                            break;

                        //编辑用户信息
                        case "EnableUser":
                            EnableUser(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_UserInfo", " id=" + context.Request["ID"], "启用、禁用用户信息");
                            break;
                        case "UpdatePwd":
                            UpdatePwd(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_UserInfo", " LoginName=" + context.Request["LoginName"], "修改登录密码");
                            break;
                        case "UpdatePwdByLoginName":
                            UpdatePwdByLoginName(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_UserInfo", " LoginName=" + context.Request["LoginName"], "修改登录密码");
                            break;
                        case "EditOrg":
                            EditOrg(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "Sys_UserInfo", " LoginName=" + context.Request["LoginName"], "修改组织机构");
                            break;
                        case "ValidUserByIDCardAndName":
                            ValidUserByIDCardAndName(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "UserInfo", context.Request["IDCard"], "用户激活");
                            break;
                        case "UpdateUserByUniqueNo":
                            UpdateUserByUniqueNo(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "UserInfo", context.Request["UniqueNo"], "根据唯一标识修改用户信息");
                            break;
                        case "Register":
                            Register(context);
                            log.WriteLog(accountNo, loginname, func, logType, "", "UserInfo", context.Request["IDCard"], "查找用户（根据身份证）");
                            break;
                        case "CheckUserValida":
                            CheckUserValida(context);
                            return;
                            break;
                        case "Login":
                            Login(context);
                            log.WriteLog(accountNo, loginname, func, "0", "", "UserInfo", "LoginName=" + context.Request["loginName"], "用户登录");
                            break;
                        case "CheckUser":
                            CheckUser(context);
                            break;
                        case "CensusUser":
                            CensusUser(context);
                            break;
                        case "CensusActiveUser":
                            CensusActiveUser(context);
                            break;
                        case "CRM_Login":
                            CRM_Login(context);
                            return;
                            break;
                        case "GetUserByRegisterOrg":
                            GetUserByRegisterOrg(context);
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

        #region 统计活跃用户数
        /// <summary>
        /// 统计活跃用户数
        /// </summary>
        /// <param name="context"></param>
        private void CensusActiveUser(HttpContext context)
        {
            jsonModel = bll.censusActiveUser();

        }
        #endregion

        #region 统计不同组织机构的用户数
        /// <summary>
        /// 统计不同组织机构的用户数
        /// </summary>
        /// <param name="context"></param>
        private void CensusUser(HttpContext context)
        {
            string RegisterOrg = context.Request["RegisterOrg"].SafeToString();
            string AuthenType = context.Request["AuthenType"].SafeToString();
            jsonModel = bll.censusUser(RegisterOrg, AuthenType);

        }
        #endregion
        private void GetDataToDataTable(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TableName", " Sys_UserInfo");
                DataTable dt = bll.GetData(ht, false);
                context.Response.Write(dt);
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "",
                    retData = dt
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
                context.Response.Write("");
            }

        }

        #region 获取用户信息
        private void GetData(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("Name", context.Request["Name"].SafeToString());
                ht.Add("Key", context.Request["Key"].SafeToString());
                ht.Add("Status", context.Request["Status"].SafeToString());
                ht.Add("OrgNo", context.Request["OrgNo"].SafeToString());
                ht.Add("ID", context.Request["ID"].SafeToString());
                ht.Add("IsStu", context.Request["IsStu"].SafeToString());
                ht.Add("AcademicId", context.Request["AcademicId"].SafeToString());
                ht.Add("AuthenType", context.Request["AuthenType"].SafeToString());
                ht.Add("UniqueNo", context.Request["UniqueNo"].SafeToString());
                ht.Add("JoinNoConn", context.Request["JoinNoConn"].SafeToString());
                ht.Add("KaNo", context.Request["KaNo"].SafeToString());
                ht.Add("Phone", context.Request["Phone"].SafeToString());
                ht.Add("IDCard", context.Request["IDCard"].SafeToString());
                ht.Add("LoginName", context.Request["GetLoginName"].SafeToString());
                ht.Add("TeaNo", context.Request["TeaNo"].SafeToString());
                ht.Add("NoFeedBack", context.Request["NoFeedBack"].SafeToString());
                ht.Add("HeadteacherNO", context.Request["HeadteacherNO"].SafeToString());
                
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

        #region 新建用户信息
        private void AddUser(HttpContext context)
        {
            Sys_UserInfo org = new Sys_UserInfo();
            org.IDCard = context.Request["IDCard"].SafeToString();
            org.LoginName = context.Request["LoginName"].SafeToString();
            org.RegisterOrg = context.Request["RegisterOrg"].SafeToString();
            org.Password = EncryptHelper.Md5By32("123456");

            org.Name = context.Request["Name"].SafeToString();
            org.Sex = Convert.ToByte(context.Request["Sex"]);
            org.Address = context.Request["Address"].SafeToString();
            org.Phone = context.Request["Phone"].SafeToString();
            org.Nickname = context.Request["Nickname"].SafeToString();
            org.UserType = Convert.ToByte(context.Request["UserType"]);
            org.Birthday = Convert.ToDateTime(context.Request["Birthday"]);
            org.Remarks = context.Request["Remarks"].SafeToString();
            org.HeadPic = context.Request["HeadPic"].SafeToString();
            org.AuthenType = Convert.ToByte(context.Request["AuthenType"]);
            org.CreateUID = context.Request["CreateUID"].SafeToString();
            org.IsDelete = 0;
            org.Email = context.Request["Emai"].SafeToString();

            org.RegisterOrg = context.Request["RegisterOrg"].SafeToString();
            jsonModel = bll.AddUserInfo(org);
        }
        #endregion

        #region 用户审核
        /// <summary>
        /// 用户审核
        /// </summary>
        /// <param name="context"></param>
        private void CheckUser(HttpContext context)
        {
            int Id = Convert.ToInt32(context.Request["ID"]);

            jsonModel = bll.GetEntityById(Id);
            if (jsonModel.errNum == 0)
            {
                Sys_UserInfo org = (Sys_UserInfo)jsonModel.retData;
                org.AuthenType = Convert.ToByte(context.Request["AuthenType"]);
                org.CheckMsg = context.Request["CheckMsg"].SafeToString();

                jsonModel = bll.Update(org);
            }
        }
        #endregion

        #region 编辑用户基本信息
        private void EditUser(HttpContext context)
        {
            int Id = Convert.ToInt32(context.Request["ID"]);

            jsonModel = bll.GetEntityById(Id);
            if (jsonModel.errNum == 0)
            {
                Sys_UserInfo org = (Sys_UserInfo)jsonModel.retData;
                string UserType = context.Request["UserType"].SafeToString();
                if (UserType.Length > 0)
                {
                    org.UserType = Convert.ToByte(UserType);
                }
                org.Id = Id;
                org.Name = context.Request["Name"].SafeToString();
                org.Nickname = context.Request["Nickname"].SafeToString();
                string Sex = context.Request["Sex"].SafeToString();
                if (Sex.Length > 0)
                {
                    org.Sex = Convert.ToByte(Sex);
                }
                string Birthday = context.Request["Birthday"].SafeToString();
                if (Birthday.Length > 0)
                {
                    org.Birthday = Convert.ToDateTime(Birthday);
                }
                org.Phone = context.Request["Phone"].SafeToString();
                org.LoginName = context.Request["LoginName"].SafeToString();
                org.IDCard = context.Request["IDCard"].SafeToString();
                org.Address = context.Request["Address"].SafeToString();
                org.Remarks = context.Request["Remarks"].SafeToString();
                org.HeadPic = context.Request["HeadPic"].SafeToString();
                org.Email = context.Request["Email"].SafeToString();
                jsonModel = bll.EditUserInfo(org);
            }
        }
        #endregion
        #region 编辑用户基本信息
        private void ResetUser(HttpContext context)
        {
            int Id = Convert.ToInt32(context.Request["ID"]);

            jsonModel = bll.GetEntityById(Id);
            if (jsonModel.errNum == 0)
            {
                Sys_UserInfo org = (Sys_UserInfo)jsonModel.retData;
                org.Id = Id;
                org.Password = "e10adc3949ba59abbe56e057f20f883e";
                org.IsFirstLogin = 0;
                jsonModel = bll.EditUserInfo(org);
            }
        }
        #endregion

        #region
        private void EditOrg(HttpContext context)
        {
            string[] idArry = context.Request["ID"].Split(',');
            for (int i = 0; i < idArry.Length; i++)
            {
                if (idArry[i].Length > 0)
                {
                    int Id = Convert.ToInt32(idArry[i]);

                    jsonModel = bll.GetEntityById(Id);
                    if (jsonModel.errNum == 0)
                    {
                        Sys_UserInfo org = (Sys_UserInfo)jsonModel.retData;
                        org.RegisterOrg = context.Request["OrgNo"].SafeToString();

                        jsonModel = bll.Update(org);
                    }
                }
            }
        }
        #endregion

        #region 删除用户
        private void DelUser(HttpContext context)
        {
            string[] idArry = context.Request["ID"].Split(',');

            jsonModel = bll.DeleteBatch(idArry);
            //for (int i = 0; i < idArry.Length; i++)
            //{
            //    if (idArry[i].Length > 0)
            //    {
            //        int Id = Convert.ToInt32(idArry[i]);

            //        jsonModel = bll.GetEntityById(Id);
            //        if (jsonModel.errNum == 0)
            //        {
            //            Sys_UserInfo org = (Sys_UserInfo)jsonModel.retData;
            //            org.IsEnable = Convert.ToByte(context.Request["IsEnable"]);
            //            jsonModel = bll.Update(org);
            //        }
            //    }
            //}

        }
        #endregion
        #region 启用\禁用用户
        private void EnableUser(HttpContext context)
        {
            string[] idArry = context.Request["ID"].Split(',');
            for (int i = 0; i < idArry.Length; i++)
            {
                if (idArry[i].Length > 0)
                {
                    int Id = Convert.ToInt32(idArry[i]);

                    jsonModel = bll.GetEntityById(Id);
                    if (jsonModel.errNum == 0)
                    {
                        Sys_UserInfo org = (Sys_UserInfo)jsonModel.retData;
                        org.IsEnable = Convert.ToByte(context.Request["IsEnable"]);
                        jsonModel = bll.Update(org);
                    }
                }
            }

        }
        #endregion

        #region 导入用户信息
        /// <summary>
        /// 导入用户信息
        /// </summary>
        /// <param name="context"></param>
        private void ImprtUser(HttpContext context)
        {
            try
            {
                string UploadPath = context.Server.MapPath("/UploadFile");
                string ImageName = "TeacherInfo.xlsx";
                HttpFileCollection files = context.Request.Files;
                if (files == null || files.Count == 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 1,
                        errMsg = "没有文件！",
                        retData = ""
                    };
                    return;
                }
                //1.获取文件信息
                var fileToUpload = files[0];
                //判断文件目录是否存在
                if (!Directory.Exists(UploadPath))
                {
                    Directory.CreateDirectory(UploadPath);
                }
                string FilePath = UploadPath + "\\" + ImageName;
                fileToUpload.SaveAs(FilePath);
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "上传成功",
                    retData = FilePath
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
                return;
            }
        }
        #endregion

        #region 修改密码
        private void UpdatePwd(HttpContext context)
        {
            //int Id = Convert.ToInt32(context.Request["ID"]);

            //jsonModel = bll.GetEntityById(Id);

            //if (jsonModel.errNum == 0)
            //{
            string LoginName = context.Request["UserName"].SafeToString();
            string OldPwd = context.Request["OldPwd"].SafeToString();
            string NewPwd = context.Request["NewPwd"].SafeToString();
            jsonModel = bll.UpdatePwd(LoginName, EncryptHelper.Md5By32(OldPwd), EncryptHelper.Md5By32(NewPwd));
            //}
        }
        #endregion

        private void UpdatePwdByLoginName(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            ht.Add("LoginName", context.Request["LoginName"].SafeToString());

            jsonModel = bll.GetPage(ht, false);
            string ID = (jsonModel.retData as List<Dictionary<string, object>>)[0]["Id"].ToString();
            Sys_UserInfo user = (Sys_UserInfo)bll.GetEntityById(int.Parse(ID)).retData;

            if (jsonModel.errNum == 0)
            {
                user.Password = EncryptHelper.Md5By32(context.Request["Password"].SafeToString());
                jsonModel = bll.EditUserInfo(user);
            }
        }
        #region 用户注册
        /// <summary>
        /// 查找用户（根据身份证和姓名）
        /// </summary>
        /// <param name="context"></param>
        private void ValidUserByIDCardAndName(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            //ht.Add("TableName", "Sys_UserInfo");
            string where = string.Empty;
            //if (!string.IsNullOrWhiteSpace(context.Request["Name"])) where += " and Name='" + context.Request["Name"] + "'";
            //if (!string.IsNullOrWhiteSpace(context.Request["IDCard"])) where += " and IDCard='" + context.Request["IDCard"] + "'";
            ht.Add("IsStu", "false");
            if (!string.IsNullOrWhiteSpace(context.Request["Name"])) ht.Add("equalsName", context.Request["Name"]);
            if (!string.IsNullOrWhiteSpace(context.Request["IDCard"])) ht.Add("IDCard", context.Request["IDCard"]);
            DataTable dt = bll.GetData(ht, false, where);
            List<string> list = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["AuthenType"]) == (int)AuthenType.用户未激活)
                {
                    list.Add(bll_com.DataTableToJson(dt));
                    jsonModel.retData = list;
                }
                else
                {
                    jsonModel.errNum = 333;
                    jsonModel.errMsg = "no";
                }

            }
            else
            {
                jsonModel.errNum = 999;
                jsonModel.errMsg = "null";
            }

        }

        /// <summary>
        /// 激活用户
        /// </summary>
        /// <param name="context"></param>
        private void UpdateUserByUniqueNo(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrWhiteSpace(context.Request["UniqueNo"])) ht.Add("UniqueNo", context.Request["UniqueNo"]);
            if (!string.IsNullOrWhiteSpace(context.Request["LoginName"])) ht.Add("LoginName", context.Request["LoginName"]);
            if (!string.IsNullOrWhiteSpace(context.Request["Password"])) ht.Add("Password", EncryptHelper.Md5By32(context.Request["Password"]));
            if (ht.Count < 3)
            {
                jsonModel.errNum = -1;
                jsonModel.errMsg = "error";
            }
            else
            {
                jsonModel = bll.GetUserInfoByUniqueNo(ht);
            }
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="context"></param>
        private void Register(HttpContext context)
        {
            Sys_UserInfo org = new Sys_UserInfo();
            org.UniqueNo = Guid.NewGuid().ToString();
            org.UserType = Convert.ToByte(context.Request["UserType"]);
            org.Name = context.Request["Name"].SafeToString();
            org.IDCard = context.Request["IDCard"].SafeToString();
            org.LoginName = context.Request["LoginName"].SafeToString();
            org.Password = EncryptHelper.Md5By32(context.Request["Password"]).SafeToString();
            org.RegisterOrg = context.Request["OrgCode"].SafeToString();
            org.UserType = Convert.ToByte(((int)enumUserType.学生));
            org.CreateTime = DateTime.Now;
            org.IsDelete = Convert.ToByte(((int)SysStatus.正常));
            org.AuthenType = (int)AuthenType.新用户注册;
            org.IsEnable = (int)isEnable.启用;
            jsonModel = bll.Add(org);
        }

        /// <summary>
        /// 检查用户身份证或登录名是否存在
        /// </summary>
        /// <param name="context"></param>
        private void CheckUserValida(HttpContext context)
        {
            string param = context.Request["param"];
            string name = context.Request["name"];
            Hashtable ht = new Hashtable();
            string where = string.Empty;
            string strinfo = string.Empty;
            string strinfo2 = string.Empty;
            string status = "n";
            string status2 = "y";
            DataTable dt = null;
            JsonModel jm = null;
            switch (name)
            {
                case "New_IDCard":
                    //ht.Add("TableName", "Sys_UserInfo");
                    //where += " and IDCard='" + param + "'";
                    ht.Add("IDCard", param);
                    ht.Add("IsStu", "false");
                    jm = bll.GetEntityListByField("IDCard", param);
                    if (jm.errNum == 0)
                        strinfo = "身份证号已存在！";
                    else
                        strinfo2 = "验证通过";
                    break;
                case "New_LoginName":
                    //ht.Add("TableName", "Sys_UserInfo");
                    //where += " and LoginName='" + param + "'";
                    ht.Add("LoginName", param);
                    ht.Add("IsStu", "false");
                    jm = bll.GetEntityListByField("LoginName", param);
                    if (jm.errNum == 0)
                        strinfo = "登录名已存在！";
                    else
                        strinfo2 = "验证通过";
                    //dt = bll.GetData(ht, false, where);
                    break;
                case "txt_loginName":
                    //ht.Add("TableName", "Sys_UserInfo");
                    //where += " and LoginName='" + param + "'";
                    ht.Add("LoginName", param);
                    ht.Add("IsStu", "false");
                    jm = bll.GetEntityListByField("LoginName", param);
                    if (jm.errNum == 0)
                    {
                        strinfo = "";
                        status = "y";
                    }
                    else
                    {
                        strinfo2 = "登录名不存在！";
                        status2 = "n";
                    }
                    break;
                case "New_OrgCode":
                    ht.Add("TableName", "Org_Mechanism");
                    where += " and OrganNo='" + param + "'";
                    dt = bll_ms.GetData(ht, false, where);
                    strinfo = "验证通过";
                    strinfo2 = "组织代号不存在！";
                    status = "y";
                    status2 = "n";
                    //dt = bll.GetData(ht, false, where);
                    break;
            }
            if (jm != null)
            {
                if (jm.errNum == 0) context.Response.Write("{\"info\":\"" + strinfo + "\",\"status\":\"" + status + "\"}");
                else context.Response.Write("{\"info\":\"" + strinfo2 + "\",\"status\":\"" + status2 + "\"}");
            }
            else if (dt != null)
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    context.Response.Write("{\"info\":\"" + strinfo + "\",\"status\":\"" + status + "\"}");
                }
                else
                {
                    context.Response.Write("{\"info\":\"" + strinfo2 + "\",\"status\":\"" + status2 + "\"}");
                }
            }

        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="context"></param>
        private void Login(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            ht.Add("IsStu", "false");
            //ht.Add("TableName", "Sys_UserInfo");
            string where = string.Empty;
            //if (!string.IsNullOrWhiteSpace(context.Request["LoginName"])) where += " and LoginName='" + context.Request["LoginName"].ToString() + "'";
            //if (!string.IsNullOrWhiteSpace(context.Request["Password"])) where += " and [Password]='" + EncryptHelper.Md5By32(context.Request["Password"].ToString()) + "'"; ;
            if (!string.IsNullOrWhiteSpace(context.Request["LoginName"])) ht.Add("LoginName", context.Request["LoginName"].ToString());
            if (!string.IsNullOrWhiteSpace(context.Request["Password"])) ht.Add("Password", EncryptHelper.Md5By32(context.Request["Password"].ToString()));
            DataTable dt = bll.IsPwdTure(context.Request["LoginName"].ToString(), EncryptHelper.Md5By32(context.Request["Password"].ToString()));
            //DataTable dt = bll.GetData(ht, false, where);
            List<string> list = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["IsEnable"]) == (int)isEnable.禁用)
                {
                    jsonModel.errMsg = "enable";
                    jsonModel.errNum = 333;
                }
                else if (Convert.ToInt32(dt.Rows[0]["IsDelete"]) == (int)SysStatus.删除)
                {
                    jsonModel.errMsg = "enable";
                    jsonModel.errNum = 444;
                }
                else
                {
                    jsonModel.errMsg = "success";
                    jsonModel.errNum = 0;
                    list.Add(bll_com.DataTableToJson(dt));
                    jsonModel.retData = list;
                }
            }
            else
            {
                jsonModel.errNum = 999;
                jsonModel.errMsg = "null";
            }
        }

        #endregion

        #region CRM_Login
        private void CRM_Login(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", HttpUtility.UrlDecode(context.Request["Name"].SafeToString()));
                ht.Add("Key", context.Request["Key"].SafeToString());
                ht.Add("Status", context.Request["Status"].SafeToString());
                ht.Add("OrgNo", context.Request["OrgNo"].SafeToString());
                ht.Add("IsStu", context.Request["IsStu"].SafeToString());
                ht.Add("Phone", context.Request["Phone"].SafeToString());
                ht.Add("IDCard", context.Request["IDCard"].SafeToString());
                CRM_JsonModel crm_jsonModel = new CRM_JsonModel() { errNum = 0, errMsg = "success", retData = "" };
                crm_jsonModel = bll.CRM_Login(ht);
                context.Response.Write("{\"result\":" + jss.Serialize(crm_jsonModel) + "}");
                context.Response.End();
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

        #region 获取部门成员
        private void GetUserByRegisterOrg(HttpContext context)
        {
            try
            {
                jsonModel = bll.GetUserByRegisterOrg(context.Request["RegisterOrg"].SafeToString());
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