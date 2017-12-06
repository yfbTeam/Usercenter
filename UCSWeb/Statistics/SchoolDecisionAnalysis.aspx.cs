using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UCSWeb.Statistics
{
    public partial class SchoolDecisionAnalysis : BasePage
    {
        public string uName;
        public string headPic;
        public string UniqueNo;
        public string loginName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.UserInfo != null)
            {
                uName = this.UserInfo.Name;
                headPic = this.UserInfo.HeadPic;
                UniqueNo = this.UserInfo.UniqueNo;
                loginName = this.UserInfo.LoginName;
            }
        }
    }
}