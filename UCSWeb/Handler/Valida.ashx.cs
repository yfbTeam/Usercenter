using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCSUtility;

namespace UCSWeb.Handler
{
    /// <summary>
    /// Valida 的摘要说明
    /// </summary>
    public class Valida : IHttpHandler
    {
        string result = "";
        public void ProcessRequest(HttpContext context)
        {
            string urlHeader = ConfigHelper.GetConfigString("HttpService.ucc").ToString();
            string url = context.Request.Url.ToString();
            int index = url.IndexOf("PageName");
            url = urlHeader + url.Substring(index + 9, url.Length - index - 9).Replace("ashx&", "ashx?");
            string parms = context.Request.Form.ToString();
            if (!string.IsNullOrWhiteSpace(parms)) url += "&" + parms;
            GetResult(url);
            context.Response.Write(result);
            context.Response.End();
        }
        private void GetResult(string url)
        {
            result = NetHelper.RequestGetUrl(url);
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