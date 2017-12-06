using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UCSWeb.CommonPage
{
    public partial class SystemMenu : BasePage
    {
        public string UniqueNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            UniqueNo = this.UserInfo.UniqueNo;
        }
    }
}