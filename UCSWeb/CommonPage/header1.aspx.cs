using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UCSWeb.CommonPage
{
    public partial class header1 : BasePage
    {

        public string uName;
        public string headPic;
        public string UniqueNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.UserInfo != null)
            {
                uName = this.UserInfo.Name;
                headPic = this.UserInfo.HeadPic;
                UniqueNo = this.UserInfo.UniqueNo;
            }

        }
    }
}