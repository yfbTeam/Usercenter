using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UCSWeb.SystemSettings
{
    public partial class EditRole : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.UserInfo != null)
            {
                this.HLoginUID.Value = this.UserInfo.UniqueNo;
            }
        }
    }
}