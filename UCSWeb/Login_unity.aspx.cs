using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCSUtility;

namespace UCSWeb
{
    public partial class Login : System.Web.UI.Page
    {
        public string TokenPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            TokenPath = ConfigHelper.GetSettingString("TokenPath");
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.UrlReferrer))) hidPreUrl.Value = Request.UrlReferrer.ToString();
            }
        }
    }
}