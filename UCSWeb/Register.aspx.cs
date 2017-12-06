using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UCSWeb
{
    public partial class Register :System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Convert.ToString(Request.UrlReferrer))) hidPreUrl.Value = Request.UrlReferrer.ToString();
            }
        }
    }
}