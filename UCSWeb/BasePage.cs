using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using UCSBLL;
using UCSModel;
using UCSUtility;

namespace UCSWeb
{
    public class BasePage : System.Web.UI.Page
    {
        protected Sys_UserInfo UserInfo { get; set; }
        protected override void OnInit(EventArgs e)
        {
            string LoginPage = ConfigHelper.GetConfigString("LoginPage.ucc");
            try
            {
                //登陆页地址 从Web.config 读取
                string action = Request["action"];
                if (!string.IsNullOrEmpty(action) && action == "loginOut")   //退出登录
                {
                    Response.Cookies["LoginCookie_Author"].Expires = DateTime.Now.AddDays(-3);
                    Response.Cookies["ClassID"].Expires = DateTime.Now.AddDays(-3);
                    Response.Cookies["TokenID"].Expires = DateTime.Now.AddDays(-3);
                    //跳转登陆页面
                    Response.Redirect(LoginPage);
                }
                else
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    if (Request.Cookies["LoginCookie_Author"] != null && !string.IsNullOrWhiteSpace(Request.Cookies["LoginCookie_Author"].Value))
                    {
                        string loginCookie = System.Web.HttpUtility.UrlDecode(Request.Cookies["LoginCookie_Author"].Value);
                        UCSModel.Sys_UserInfo item = jss.Deserialize<UCSModel.Sys_UserInfo>(loginCookie);
                        UserInfo = item;
                        //SetPageButton();
                    }
                    else if (Request.Cookies["TokenID"] != null && !string.IsNullOrWhiteSpace(Request.Cookies["TokenID"].Value))
                    {
                        #region
                        var postData = "&Func=GetUserInfoByToken&tokenID=" + System.Web.HttpUtility.UrlDecode(Request.Cookies["TokenID"].Value) + "&returnUrl=" + Request.Url.ToString();
                        string result = NetHelper.RequestPostUrl(ConfigHelper.GetSettingString("TokenPath"), postData);

                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            int starIndex = result.IndexOf(":") + 1;
                            int endIndex = result.LastIndexOf("}");
                            result = result.Substring(starIndex, endIndex - starIndex);
                            UCSModel.JsonModel jsonModel = jss.Deserialize<UCSModel.JsonModel>(result);
                            if (jsonModel != null && jsonModel.retData != null && jsonModel.errNum == 0)
                            {
                                Dictionary<string, object> ht = jsonModel.retData as Dictionary<string, object>;
                                if (ht != null)
                                {
                                    Sys_User item = new Sys_User();
                                    System.Reflection.PropertyInfo[] properties = item.GetType().GetProperties();
                                    foreach (System.Reflection.PropertyInfo property in properties)
                                    {
                                        if (ht.ContainsKey(property.Name))
                                        {
                                            //object obj = property.GetValue(item, null);
                                            //object obj2 = property.GetValue(item, DBNull.Value);
                                            //property.SetValue(item, property.GetValue(item,null), null);
                                            if (property.PropertyType.Name.StartsWith("String"))
                                                property.SetValue(item, ht[property.Name].SafeToString(), null);
                                            if (property.PropertyType.Name.StartsWith("Int32"))
                                                if (string.IsNullOrWhiteSpace(Convert.ToString(ht[property.Name])))
                                                    property.SetValue(item, -999999, null);
                                                else
                                                    property.SetValue(item, Convert.ToInt32(ht[property.Name]), null);
                                            if (property.PropertyType.Name.StartsWith("DateTime"))
                                                if (string.IsNullOrWhiteSpace(Convert.ToString(ht[property.Name])))
                                                    property.SetValue(item, Convert.ToDateTime("1900-01-01"), null);
                                                else
                                                    property.SetValue(item, Convert.ToDateTime(ht[property.Name]), null);
                                            if (property.PropertyType.Name.StartsWith("Byte"))
                                                if (string.IsNullOrWhiteSpace(Convert.ToString(ht[property.Name])))
                                                    property.SetValue(item, Convert.ToByte(255), null);
                                                else
                                                    property.SetValue(item, Convert.ToByte(ht[property.Name]), null);
                                        }
                                    }

                                    List<Sys_User> list = new List<Sys_User>();
                                    list.Add(item);
                                    var items = from c in list.AsEnumerable()
                                                select new UCSModel.Sys_UserInfo()
                                                {
                                                    Id = c.Id,
                                                    Address = c.Address,
                                                    Birthday = c.Birthday,
                                                    UserType = c.UserType,
                                                    AuthenType = c.AuthenType,
                                                    CheckMsg = c.CheckMsg,
                                                    CreateTime = c.CreateTime,
                                                    CreateUID = c.CreateUID,
                                                    EditTime = c.EditTime,
                                                    EditUID = c.EditUID,
                                                    HeadPic = c.HeadPic,
                                                    IDCard = c.IDCard,
                                                    IsDelete = c.IsDelete,
                                                    IsEnable = c.IsEnable,
                                                    LoginName = c.LoginName,
                                                    Name = c.Name,
                                                    Nickname = c.Nickname,
                                                    Password = c.Password,
                                                    Phone = c.Phone,
                                                    RegisterOrg = c.RegisterOrg,
                                                    Remarks = c.Remarks,
                                                    Sex = c.Sex,
                                                    UniqueNo = c.UniqueNo
                                                };
                                    UserInfo = items.ToList().Find(o => o.Id != null);
                                    if (Convert.ToDateTime(UserInfo.Birthday).ToString("yyyy-MM-dd") == "1990-01-01")
                                    {
                                        UserInfo.Birthday = null;
                                    }
                                    if (Convert.ToDateTime(UserInfo.CreateTime).ToString("yyyy-MM-dd") == "1990-01-01")
                                    {
                                        UserInfo.EditTime = null;
                                    }
                                    if (Convert.ToDateTime(UserInfo.EditTime).ToString("yyyy-MM-dd") == "1990-01-01")
                                    {
                                        UserInfo.EditTime = null;
                                    }
                                    if (UserInfo.UserType == 255)
                                    {
                                        UserInfo.UserType = null;
                                    }
                                    if (UserInfo.AuthenType == 255)
                                    {
                                        UserInfo.AuthenType = 255;
                                    }
                                    if (UserInfo.IsDelete == 255)
                                    {
                                        UserInfo.IsDelete = null;
                                    }
                                    if (UserInfo.IsEnable == 255)
                                    {
                                        UserInfo.IsEnable = null;
                                    }
                                    if (UserInfo.Sex == 255)
                                    {
                                        UserInfo.Sex = null;
                                    }
                                    Response.Cookies["LoginCookie_Author"].Value = HttpUtility.UrlEncode(JsonHelper.SerializeObject(UserInfo));
                                    Response.Cookies["username"].Expires = DateTime.MaxValue;
                                }
                                //SetPageButton();
                                if (Request.Url.ToString().IndexOf("Login.aspx") > -1 || Request.Url.ToString().IndexOf("Resgister.aspx") > -1)
                                    Response.Write("<script language='javascript'>window.location='/Index.aspx'</script>");
                                // Response.Redirect("~/Index.aspx");
                                else
                                    Response.Write("<script language='javascript'>window.location='" + Request.Url.ToString() + "'</script>");
                                //Response.Redirect(Request.Url.ToString());                                
                            }
                            else
                            {
                                Response.Cookies["LoginCookie_Author"].Expires = DateTime.Now.AddDays(-3);
                                Response.Cookies["ClassID"].Expires = DateTime.Now.AddDays(-3);
                                Response.Cookies["TokenID"].Expires = DateTime.Now.AddDays(-3);
                                Response.Redirect(LoginPage);

                                // Response.Write("<script>window.location.href='/Login.aspx'</script>");
                                //Response.Redirect("~/Resgister.aspx");
                                //Response.Write("<script language='javascript'>window.location='/Resgister.aspx'</script>");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        Response.Cookies["LoginCookie_Author"].Expires = DateTime.Now.AddDays(-3);
                        Response.Cookies["ClassID"].Expires = DateTime.Now.AddDays(-3);
                        Response.Cookies["TokenID"].Expires = DateTime.Now.AddDays(-3);

                        //Response.Redirect("~/Login.aspx");
                        Response.Redirect(LoginPage);
                    }
                    // base.OnInit(e);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                //Response.Redirect("~/Login.aspx");
                Response.Redirect(LoginPage);
            }

        }
        private void SetPageButton()
        {
            try
            {
                string cururl = HttpContext.Current.Request.Url.AbsolutePath;
                if (cururl.IndexOf("CommonPage/header.aspx") != -1 || cururl.IndexOf("CommonPage/SystemMenu.aspx") != -1)
                {
                    return;
                }
                JsonModel pb_jsonModel = new Sys_MenuInfoService().GetSubButtonByUrl(cururl, UserInfo.UniqueNo);
                if (pb_jsonModel.errNum == 0)
                {
                    List<Dictionary<string, object>> pagelist = pb_jsonModel.retData as List<Dictionary<string, object>>;
                    var curpage = pagelist[0];
                    var btnfield = curpage["ButtonField"].SafeToString();
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "setbuttonScript", "<script>SetPageButton_Back('" + btnfield + "');</script>", true);
                }
                else
                {
                    Response.Redirect("/CommonPage/NoPower.html", false);
                    //Server.Transfer("/CommonPage/NoPower.html");                    
                }
            }
            catch (Exception ex) { throw; }
        }
    }

    [Serializable]
    public partial class Sys_User
    {

        /// <summary>
        ///主键 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///用户唯一值 
        /// </summary>
        public string UniqueNo { get; set; }
        /// <summary>
        ///用户类型 
        /// </summary>
        public Byte UserType { get; set; }
        /// <summary>
        ///姓名 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///昵称 
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        ///性别 
        /// </summary>
        public Byte Sex { get; set; }
        /// <summary>
        ///联系电话 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        ///出生日期 
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        ///用户账号 
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        ///密码 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        ///身份证件号 
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        ///头像 
        /// </summary>
        public string HeadPic { get; set; }
        /// <summary>
        ///注册的组织机构 
        /// </summary>
        public string RegisterOrg { get; set; }
        /// <summary>
        ///认证类型 
        /// </summary>
        public Byte AuthenType { get; set; }
        /// <summary>
        ///现住址 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        ///备注 
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        ///创建人 
        /// </summary>
        public string CreateUID { get; set; }
        /// <summary>
        ///创建时间 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///修改人 
        /// </summary>
        public string EditUID { get; set; }
        /// <summary>
        ///修改时间 
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        ///启用/禁用 
        /// </summary>
        public Byte IsEnable { get; set; }
        /// <summary>
        ///是否删除 
        /// </summary>
        public Byte IsDelete { get; set; }

        public string CheckMsg { get; set; }
    }

}