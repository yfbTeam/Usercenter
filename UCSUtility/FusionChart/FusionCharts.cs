﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UCSUtility.FusionChart
{
    public class FusionCharts
    {
        private static Hashtable __CONFIG__ = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
        private static bool __CONFIG__Initialized = false;

        #region RenderALL methods
        /// <summary>
        /// Generate html code for rendering chart
        /// This function assumes that you've already included the FusionCharts JavaScript class in your page
        /// </summary>
        /// <param name="chartSWF">SWF File Name (and Path) of the chart which you intend to plot</param>
        /// <param name="dataUrl">If you intend to use dataURL method for this chart, pass the URL as this parameter. Else, set it to "" (in case of dataXML method)</param>
        /// <param name="dataStr">If you intend to use dataXML method for this chart, pass the XML data as this parameter. Else, set it to "" (in case of dataURL method)</param>
        /// <param name="chartId">Id for the chart, using which it will be recognized in the HTML page. Each chart on the page needs to have a unique Id.</param>
        /// <param name="chartWidth">Intended width for the chart (in pixels)</param>
        /// <param name="chartHeight">Intended height for the chart (in pixels)</param>
        /// <param name="debugMode">Whether to start the chart in debug mode</param>
        /// <param name="registerWithJS">Whether to ask chart to register itself with JavaScript</param>
        /// <param name="allowTransparent">Whether allowTransparent chart (true / false)</param>
        /// <param name="bgColor">Back Ground Color</param>
        /// <param name="scaleMode">Set Scale Mode</param>
        /// <param name="language">Set SWF file Language</param>
        /// <returns>JavaScript + HTML code required to embed a chart</returns>
        private static string RenderChartALL(string chartSWF, string dataUrl, string dataStr, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool allowTransparent, string bgColor, string scaleMode, string language)
        {
            __INIT();

            // string dataprovider_js_code;
            SetConfiguration("debugMode", boolToNum(debugMode));
            SetConfiguration("registerWithJS", boolToNum(true));
            // setup debug mode js parameter
            int debugMode_js_param = boolToNum(debugMode);
            // setup register with js js parameter
            int registerWithJS_js_param = boolToNum(true);
            string dataFormat = GetConfiguration("dataFormat");
            dataFormat = (dataFormat == "" ? "xml" + (dataStr == "" ? "url" : "") : dataFormat + (dataStr == "" ? "url" : ""));

            if (GetConfiguration("renderAt") == "") SetConfiguration("renderAt", chartId + "Div");

            string wmode = GetConfiguration("wMode");
            if (wmode.Trim() == "" || wmode == null)
            {
                wmode = allowTransparent ? "transparent" : "opaque";
            }

            SetConfiguration("swfUrl", chartSWF);
            SetConfiguration("dataFormat", dataFormat);
            SetConfiguration("id", chartId);
            SetConfiguration("width", chartWidth);
            SetConfiguration("height", chartHeight);
            SetConfiguration("wMode", wmode);

            string dataSource = (dataStr == "" ? dataUrl : dataStr.Replace("\n\r", ""));
            string dataSourceJSON = "\"dataSource\" : " + (dataFormat == "json" ? dataSource : "\"" + dataSource + "\"");
            string chartConfigJSON = "{" + fc_encodeJSON(GetConfigurationGroup("params"), false) + "," + dataSourceJSON + "}";

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<!-- START Script Block for Chart {0} -->" + Environment.NewLine, chartId);
            builder.AppendFormat("<div id='{0}Div' >" + Environment.NewLine, chartId);
            builder.Append("Chart." + Environment.NewLine);
            builder.Append("</div>" + Environment.NewLine);
            builder.Append("<script type=\"text/javascript\">" + Environment.NewLine);
            builder.AppendFormat("var chart_{0} = new FusionCharts({1}).render();", chartId, chartConfigJSON);
            builder.Append("</script>" + Environment.NewLine);
            builder.AppendFormat("<!-- END Script Block for Chart {0} -->" + Environment.NewLine, chartId);

            // Re-Initializing...
            __fc__initialize__();

            return builder.ToString();


        }

        /// <summary>
        /// Renders the HTML code for the chart. This
        /// method does NOT embed the chart using JavaScript class. Instead, it uses
        /// direct HTML embedding. So, if you see the charts on IE 6 (or above), you'll
        /// see the "Click to activate..." message on the chart.
        /// </summary>
        /// <param name="chartSWF">SWF File Name (and Path) of the chart which you intend to plot</param>
        /// <param name="dataUrl">If you intend to use dataURL method for this chart, pass the URL as this parameter. Else, set it to "" (in case of dataXML method)</param>
        /// <param name="dataStr">If you intend to use dataXML method for this chart, pass the XML data as this parameter. Else, set it to "" (in case of dataURL method)</param>
        /// <param name="chartId">Id for the chart, using which it will be recognized in the HTML page. Each chart on the page needs to have a unique Id.</param>
        /// <param name="chartWidth">Intended width for the chart (in pixels)</param>
        /// <param name="chartHeight">Intended height for the chart (in pixels)</param>
        /// <param name="debugMode">Whether to start the chart in debug mode</param>
        /// <param name="registerWithJS">Whether to ask chart to register itself with JavaScript</param>
        /// <param name="allowTransparent">Whether allowTransparent chart (true / false)</param>
        /// <param name="bgColor">Back Ground Color</param>
        /// <param name="scaleMode">Set Scale Mode</param>
        /// <param name="language">Set SWF file Language</param>
        /// <returns></returns>
        private static string RenderChartHTMLALL(string chartSWF, string dataUrl, string dataStr, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool allowTransparent, string bgColor, string scaleMode, string language)
        {
            __INIT();

            string wmode = GetConfiguration("wMode");
            if (wmode.Trim() == "" || wmode == null)
            {
                wmode = allowTransparent ? "transparent" : "opaque";
            }

            SetConfiguration("movie", chartSWF);
            SetConfiguration("src", chartSWF);
            SetConfiguration("dataXML", dataStr);
            SetConfiguration("dataURL", dataUrl);
            SetConfiguration("width", chartWidth);
            SetConfiguration("height", chartHeight);
            SetConfiguration("chartWidth", chartWidth);
            SetConfiguration("chartHeight", chartHeight);
            SetConfiguration("DOMId", chartId);
            SetConfiguration("id", chartId);
            SetConfiguration("debugMode", boolToNum(debugMode));
            SetConfiguration("wMode", wmode);

            string strFlashVars = FC_Transform(GetConfigurationGroup("fvars"), "&{key}={value}", true);
            SetConfiguration("flashvars", strFlashVars);

            string strObjectNode = "<object " + FC_Transform(GetConfigurationGroup("object"), " {key}=\"{value}\"", true) + " >\n";
            string strObjectParamsNode = FC_Transform(GetConfigurationGroup("objparams"), "\t<param name=\"{key}\" value=\"{value}\">\n", true);
            string strEmbedNode = "<embed " + FC_Transform(GetConfigurationGroup("embed"), " {key}=\"{value}\"", true) + " />\n";


            StringBuilder htmlcodes = new StringBuilder();
            htmlcodes.AppendFormat("<!-- START Code Block for Chart {0} -->" + Environment.NewLine, chartId);
            htmlcodes.Append(strObjectNode + Environment.NewLine);
            htmlcodes.Append(strObjectParamsNode + Environment.NewLine);
            htmlcodes.Append(strEmbedNode + Environment.NewLine);
            htmlcodes.AppendFormat("</object>\n<!-- END Code Block for Chart {0} -->" + Environment.NewLine, chartId);

            // Re-Initializing...
            __fc__initialize__();

            return htmlcodes.ToString();
        }
        #endregion

        /// <summary>
        /// Generate html code for rendering chart
        /// This function assumes that you've already included the FusionCharts JavaScript class in your page
        /// </summary>
        /// <param name="chartSWF">SWF File Name (and Path) of the chart which you intend to plot</param>
        /// <param name="dataUrl">If you intend to use dataURL method for this chart, pass the URL as this parameter. Else, set it to "" (in case of dataXML method)</param>
        /// <param name="dataStr">If you intend to use dataXML method for this chart, pass the XML data as this parameter. Else, set it to "" (in case of dataURL method)</param>
        /// <param name="chartId">Id for the chart, using which it will be recognized in the HTML page. Each chart on the page needs to have a unique Id.</param>
        /// <param name="chartWidth">Intended width for the chart (in pixels)</param>
        /// <param name="chartHeight">Intended height for the chart (in pixels)</param>
        /// <param name="debugMode">Whether to start the chart in debug mode</param>
        /// <param name="registerWithJS">Whether to ask chart to register itself with JavaScript</param>
        /// <returns>JavaScript + HTML code required to embed a chart</returns>
        public static string RenderChart(string chartSWF, string dataUrl, string dataStr, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS)
        {
            return RenderChartALL(chartSWF, dataUrl, dataStr, chartId, chartWidth, chartHeight, debugMode, registerWithJS, false, "", "noScale", "EN");
        }

        /// <summary>
        /// Generate html code for rendering chart
        /// This function assumes that you've already included the FusionCharts JavaScript class in your page
        /// </summary>
        /// <param name="chartSWF">SWF File Name (and Path) of the chart which you intend to plot</param>
        /// <param name="dataUrl">If you intend to use dataURL method for this chart, pass the URL as this parameter. Else, set it to "" (in case of dataXML method)</param>
        /// <param name="dataStr">If you intend to use dataXML method for this chart, pass the XML data as this parameter. Else, set it to "" (in case of dataURL method)</param>
        /// <param name="chartId">Id for the chart, using which it will be recognized in the HTML page. Each chart on the page needs to have a unique Id.</param>
        /// <param name="chartWidth">Intended width for the chart (in pixels)</param>
        /// <param name="chartHeight">Intended height for the chart (in pixels)</param>
        /// <param name="debugMode">Whether to start the chart in debug mode</param>
        /// <param name="registerWithJS">Whether to ask chart to register itself with JavaScript</param>
        /// <param name="allowTransparent">Whether allowTransparent chart (true / false)</param>
        /// <returns>JavaScript + HTML code required to embed a chart</returns>
        public static string RenderChart(string chartSWF, string dataUrl, string dataStr, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool allowTransparent)
        {
            return RenderChartALL(chartSWF, dataUrl, dataStr, chartId, chartWidth, chartHeight, debugMode, registerWithJS, allowTransparent, "", "noScale", "EN");
        }

        /// <summary>
        /// Generate html code for rendering chart
        /// This function assumes that you've already included the FusionCharts JavaScript class in your page
        /// </summary>
        /// <param name="chartSWF">SWF File Name (and Path) of the chart which you intend to plot</param>
        /// <param name="dataUrl">If you intend to use dataURL method for this chart, pass the URL as this parameter. Else, set it to "" (in case of dataXML method)</param>
        /// <param name="dataStr">If you intend to use dataXML method for this chart, pass the XML data as this parameter. Else, set it to "" (in case of dataURL method)</param>
        /// <param name="chartId">Id for the chart, using which it will be recognized in the HTML page. Each chart on the page needs to have a unique Id.</param>
        /// <param name="chartWidth">Intended width for the chart (in pixels)</param>
        /// <param name="chartHeight">Intended height for the chart (in pixels)</param>
        /// <param name="debugMode">Whether to start the chart in debug mode</param>
        /// <param name="registerWithJS">Whether to ask chart to register itself with JavaScript</param>
        /// <param name="allowTransparent">Whether allowTransparent chart (true / false)</param>
        /// <param name="bgColor">Back Ground Color</param>
        /// <param name="scaleMode">Set Scale Mode</param>
        /// <param name="language">Set SWF file Language</param>
        /// <returns>JavaScript + HTML code required to embed a chart</returns>
        public static string RenderChart(string chartSWF, string dataUrl, string dataStr, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool allowTransparent, string bgColor, string scaleMode, string language)
        {
            return RenderChartALL(chartSWF, dataUrl, dataStr, chartId, chartWidth, chartHeight, debugMode, registerWithJS, allowTransparent, bgColor, scaleMode, language);
        }

        /// <summary>
        /// Renders the HTML code for the chart. This
        /// method does NOT embed the chart using JavaScript class. Instead, it uses
        /// direct HTML embedding. So, if you see the charts on IE 6 (or above), you'll
        /// see the "Click to activate..." message on the chart.
        /// </summary>
        /// <param name="chartSWF">SWF File Name (and Path) of the chart which you intend to plot</param>
        /// <param name="dataUrl">If you intend to use dataURL method for this chart, pass the URL as this parameter. Else, set it to "" (in case of dataXML method)</param>
        /// <param name="dataStr">If you intend to use dataXML method for this chart, pass the XML data as this parameter. Else, set it to "" (in case of dataURL method)</param>
        /// <param name="chartId">Id for the chart, using which it will be recognized in the HTML page. Each chart on the page needs to have a unique Id.</param>
        /// <param name="chartWidth">Intended width for the chart (in pixels)</param>
        /// <param name="chartHeight">Intended height for the chart (in pixels)</param>
        /// <param name="debugMode">Whether to start the chart in debug mode</param>
        /// <returns></returns>
        public static string RenderChartHTML(string chartSWF, string dataUrl, string dataStr, string chartId, string chartWidth, string chartHeight, bool debugMode)
        {
            return RenderChartHTMLALL(chartSWF, dataUrl, dataStr, chartId, chartWidth, chartHeight, debugMode, false, false, "", "noScale", "EN");
        }

        /// <summary>
        /// Renders the HTML code for the chart. This
        /// method does NOT embed the chart using JavaScript class. Instead, it uses
        /// direct HTML embedding. So, if you see the charts on IE 6 (or above), you'll
        /// see the "Click to activate..." message on the chart.
        /// </summary>
        /// <param name="chartSWF">SWF File Name (and Path) of the chart which you intend to plot</param>
        /// <param name="dataUrl">If you intend to use dataURL method for this chart, pass the URL as this parameter. Else, set it to "" (in case of dataXML method)</param>
        /// <param name="dataStr">If you intend to use dataXML method for this chart, pass the XML data as this parameter. Else, set it to "" (in case of dataURL method)</param>
        /// <param name="chartId">Id for the chart, using which it will be recognized in the HTML page. Each chart on the page needs to have a unique Id.</param>
        /// <param name="chartWidth">Intended width for the chart (in pixels)</param>
        /// <param name="chartHeight">Intended height for the chart (in pixels)</param>
        /// <param name="debugMode">Whether to start the chart in debug mode</param>
        /// <param name="registerWithJS">Whether to ask chart to register itself with JavaScript</param>
        /// <returns></returns>
        public static string RenderChartHTML(string chartSWF, string dataUrl, string dataStr, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS)
        {
            return RenderChartHTMLALL(chartSWF, dataUrl, dataStr, chartId, chartWidth, chartHeight, debugMode, registerWithJS, false, "", "noScale", "EN");
        }

        /// <summary>
        /// Renders the HTML code for the chart. This
        /// method does NOT embed the chart using JavaScript class. Instead, it uses
        /// direct HTML embedding. So, if you see the charts on IE 6 (or above), you'll
        /// see the "Click to activate..." message on the chart.
        /// </summary>
        /// <param name="chartSWF">SWF File Name (and Path) of the chart which you intend to plot</param>
        /// <param name="dataUrl">If you intend to use dataURL method for this chart, pass the URL as this parameter. Else, set it to "" (in case of dataXML method)</param>
        /// <param name="dataStr">If you intend to use dataXML method for this chart, pass the XML data as this parameter. Else, set it to "" (in case of dataURL method)</param>
        /// <param name="chartId">Id for the chart, using which it will be recognized in the HTML page. Each chart on the page needs to have a unique Id.</param>
        /// <param name="chartWidth">Intended width for the chart (in pixels)</param>
        /// <param name="chartHeight">Intended height for the chart (in pixels)</param>
        /// <param name="debugMode">Whether to start the chart in debug mode</param>
        /// <param name="registerWithJS">Whether to ask chart to register itself with JavaScript</param>
        /// <param name="allowTransparent">Whether allowTransparent chart (true / false)</param>
        /// <returns></returns>
        public static string RenderChartHTML(string chartSWF, string dataUrl, string dataStr, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool allowTransparent)
        {
            return RenderChartHTMLALL(chartSWF, dataUrl, dataStr, chartId, chartWidth, chartHeight, debugMode, registerWithJS, allowTransparent, "", "noScale", "EN");
        }

        /// <summary>
        /// Renders the HTML code for the chart. This
        /// method does NOT embed the chart using JavaScript class. Instead, it uses
        /// direct HTML embedding. So, if you see the charts on IE 6 (or above), you'll
        /// see the "Click to activate..." message on the chart.
        /// </summary>
        /// <param name="chartSWF">SWF File Name (and Path) of the chart which you intend to plot</param>
        /// <param name="dataUrl">If you intend to use dataURL method for this chart, pass the URL as this parameter. Else, set it to "" (in case of dataXML method)</param>
        /// <param name="dataStr">If you intend to use dataXML method for this chart, pass the XML data as this parameter. Else, set it to "" (in case of dataURL method)</param>
        /// <param name="chartId">Id for the chart, using which it will be recognized in the HTML page. Each chart on the page needs to have a unique Id.</param>
        /// <param name="chartWidth">Intended width for the chart (in pixels)</param>
        /// <param name="chartHeight">Intended height for the chart (in pixels)</param>
        /// <param name="debugMode">Whether to start the chart in debug mode</param>
        /// <param name="registerWithJS">Whether to ask chart to register itself with JavaScript</param>
        /// <param name="allowTransparent">Whether allowTransparent chart (true / false)</param>
        /// <param name="bgColor">Back Ground Color</param>
        /// <param name="scaleMode">Set Scale Mode</param>
        /// <param name="language">Set SWF file Language</param>
        /// <returns></returns>
        public static string RenderChartHTML(string chartSWF, string dataUrl, string dataStr, string chartId, string chartWidth, string chartHeight, bool debugMode, bool registerWithJS, bool allowTransparent, string bgColor, string scaleMode, string language)
        {
            return RenderChartHTMLALL(chartSWF, dataUrl, dataStr, chartId, chartWidth, chartHeight, debugMode, registerWithJS, allowTransparent, bgColor, scaleMode, language);
        }


        /// <summary>
        /// encodes the dataURL before it's served to FusionCharts
        /// If you have parameters in your dataURL, you'll necessarily need to encode it
        /// </summary>
        /// <param name="dataUrl">dataURL to be fed to chart</param>
        /// <param name="noCacheStr">Whether to add aditional string to URL to disable caching of data</param>
        /// <returns>Encoded dataURL, ready to be consumed by FusionCharts</returns>
        public static string EncodeDataURL(string dataUrl, bool noCacheStr)
        {

            string result = dataUrl;
            if (noCacheStr)
            {
                result += (dataUrl.IndexOf("?") != -1) ? "&" : "?";
                //Replace : in time with _, as FusionCharts cannot handle : in URLs
                result += "FCCurrTime=" + DateTime.Now.ToString().Replace(":", "_");
            }

            return System.Web.HttpUtility.UrlEncode(result);
        }

        /// <summary>
        /// Enables Print Manager for Mozilla browsers
        /// This function returns a small JavaScript snippet which can be added to ClientScript's RegisterClientScriptBlock method
        /// </summary>
        /// <example>ClientScript.RegisterClientScriptBlock(Page.GetType(), "", FusionCharts.enableFCPrintManager());</example>
        /// <returns>String with the JavaScript code</returns>
        public static string EnablePrintManager()
        {
            string strHTML = "<script type=\"text/javascript\"><!--\n if(FusionCharts && FusionCharts.printManager) FusionCharts.printManager.enabled(true);\n// --></script>";
            return (strHTML);
        }


        /// <summary>
        /// Enables Print Manager for Mozilla browsers
        /// </summary>
        /// <param name="CurrentPage">Current page reference</param>
        public static void EnablePrintManager(object CurrentPage)
        {
            System.Web.UI.Page HostPage;
            HostPage = (System.Web.UI.Page)CurrentPage;
            string strHTML = "<script type=\"text/javascript\"><!--\n if(FusionCharts && FusionCharts.printManager) FusionCharts.printManager.enabled(true);\n// --></script>";
            HostPage.ClientScript.RegisterClientScriptBlock(HostPage.GetType(), "", strHTML);
        }


        private static void __INIT()
        {
            if (__CONFIG__Initialized == false)
            {
                __fc__initialize__();
                __fc__initstatic__();
                __CONFIG__Initialized = true;
            }
        }

        /// <summary>
        /// Sets the dataformat to be provided to charts (json/xml)
        /// </summary>
        /// <param name="format">Data format. Default is 'xml'. Other format is 'json'</param>
        public static void SetDataFormat(string format)
        {
            __INIT();

            if (format.Trim().Length == 0)
            {
                format = "xml";
            }
            // Stores the dataformat in global configuration store
            SetConfiguration("dataFormat", format);
        }

        /// <summary>
        /// Sets renderer type (flash/javascript)
        /// </summary>
        /// <param name="renderer"> Name of the renderer. Default is 'flash'. Other possibility is 'javascript'</param>
        public static void SetRenderer(string renderer)
        {
            __INIT();

            if (renderer.Trim().Length == 0)
            {
                renderer = "flash";
            }
            // stores the renderer name in global configuration store
            SetConfiguration("renderer", renderer);
        }

        /// <summary>
        /// Explicitely sets window mode (window[detault]/transpatent/opaque)
        /// </summary>
        /// <param name="mode">Name of the mode. Default is 'window'. Other possibilities are 'transparent'/'opaque'</param>
        public static void SetWindowMode(string mode)
        {
            __INIT();
            SetConfiguration("wMode", mode);
        }

        /// <summary>
        /// FC_SetConfiguration sets various configurations of FusionCharts
        /// It takes configuration names as first parameter and its value a second parameter
        /// There are config groups which can contain common configuration names. All config names in all groups gets set with this value
        /// unless group is specified explicitly
        /// </summary>
        /// <param name="setting">Name of configuration</param>
        /// <param name="value">Value of configuration</param>
        public static void SetConfiguration(string setting, object value)
        {
            foreach (DictionaryEntry de in __CONFIG__)
            {
                if (((Hashtable)__CONFIG__[de.Key]).ContainsKey(setting))
                {
                    ((Hashtable)__CONFIG__[de.Key])[setting] = value;
                }
            }
        }

        #region Helper Private Methods
        private static string GetHTTP()
        {
            //Checks for protocol type.
            string isHTTPS = HttpContext.Current.Request.ServerVariables["HTTPS"];
            //Checks browser type.
            bool isMSIE = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].Contains("MSIE");
            //Protocol initially sets to http.
            string sHTTP = "http";
            if (isHTTPS.ToLower() == "on")
            {
                sHTTP = "https";
            }
            return sHTTP;
        }

        /// <summary>
        /// Transform the meaning of boolean value in integer value
        /// </summary>
        /// <param name="value">true/false value to be transformed</param>
        /// <returns>1 if the value is true, 0 if the value is false</returns>
        private static int boolToNum(bool value)
        {
            return value ? 1 : 0;
        }

        private static void SetCONSTANTConfiguration(string setting, object value)
        {
            ((Hashtable)__CONFIG__["constants"])[setting] = value;
        }

        private static string GetConfiguration(string setting)
        {
            foreach (DictionaryEntry de in __CONFIG__)
            {
                if (((Hashtable)__CONFIG__[de.Key]).ContainsKey(setting))
                {
                    return ((Hashtable)__CONFIG__[de.Key])[setting].ToString();
                }
            }
            return null;
        }

        private static Hashtable GetConfigurationGroup(string setting)
        {
            if (__CONFIG__.ContainsKey(setting))
            {
                return (Hashtable)__CONFIG__[setting];
            }
            return null;
        }

        private static string FC_Transform(Hashtable arr, string tFormat, bool ignoreBlankValues)
        {
            string converted = "";
            string Key = "", Value = "";
            foreach (DictionaryEntry ds in arr)
            {
                if (ignoreBlankValues == true && ds.Value.ToString().Trim() == "") continue;
                Key = ds.Key.ToString();
                Value = ds.Value.ToString();
                if (Key.ToLower().Equals("codebase"))
                {
                    Value = Value.Replace("http", GetHTTP());
                }
                string TFApplied = tFormat.Replace("{key}", Key);
                TFApplied = TFApplied.Replace("{value}", Value);
                converted = converted + TFApplied;
            }
            return converted;
        }

        private static string fc_encodeJSON(Hashtable json, bool enclosed)
        {
            string strjson = "";
            if (enclosed == true) strjson = "{";

            strjson = strjson + FC_Transform(json, "\"{key}\" : \"{value}\", ", true);
            strjson = strjson.Trim();

            if (strjson.EndsWith(",")) strjson = strjson.Remove(strjson.Length - 1);

            return strjson;
        }

        private static void __fc__initstatic__()
        {
            Hashtable constant = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
            constant["scriptbaseUri"] = "";
            __CONFIG__["constants"] = constant;
            constant = null;
        }

        private static void __fc__initialize__()
        {
            Hashtable param = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
            param["swfUrl"] = "";
            param["width"] = "";
            param["height"] = "";
            param["renderAt"] = "";
            param["renderer"] = "";
            param["dataSource"] = "";
            param["dataFormat"] = "";
            param["id"] = "";
            param["lang"] = "";
            param["debugMode"] = "";
            param["registerWithJS"] = "";
            param["detectFlashVersion"] = "";
            param["autoInstallRedirect"] = "";
            param["wMode"] = "";
            param["scaleMode"] = "";
            param["menu"] = "";
            param["bgColor"] = "";
            param["quality"] = "";

            __CONFIG__["params"] = param;

            Hashtable fvar = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
            fvar["dataURL"] = "";
            fvar["dataXML"] = "";
            fvar["chartWidth"] = "";
            fvar["chartHeight"] = "";
            fvar["DOMId"] = "";
            fvar["registerWithJS"] = "1";
            fvar["debugMode"] = "0";
            fvar["scaleMode"] = "noScale";
            fvar["lang"] = "EN";
            __CONFIG__["fvars"] = fvar;

            Hashtable oBject = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
            oBject["height"] = "";
            oBject["width"] = "";
            oBject["id"] = "";
            oBject["classid"] = "clsid:d27cdb6e-ae6d-11cf-96b8-444553540000";
            oBject["codebase"] = "http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0";
            __CONFIG__["object"] = oBject;

            Hashtable objparam = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
            objparam["movie"] = "";
            objparam["FlashVars"] = "";
            objparam["scaleMode"] = "noScale";
            objparam["wMode"] = "";
            objparam["bgColor"] = "";
            objparam["quality"] = "best";
            objparam["allowScriptAccess"] = "always";
            objparam["swLiveConnect"] = "";
            objparam["base"] = "";
            objparam["align"] = "";
            objparam["salign"] = "";
            objparam["scale"] = "";
            objparam["menu"] = "";
            __CONFIG__["objparams"] = objparam;

            Hashtable embeds = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
            embeds["height"] = "";
            embeds["width"] = "";
            embeds["id"] = "";
            embeds["src"] = "";
            embeds["flashvars"] = "";
            embeds["name"] = "";
            embeds["scaleMode"] = "noScale";
            embeds["wMode"] = "";
            embeds["bgColor"] = "";
            embeds["quality"] = "best";
            embeds["allowScriptAccess"] = "always";
            embeds["type"] = "application/x-shockwave-flash";
            embeds["pluginspage"] = "http://www.macromedia.com/go/getflashplayer";
            embeds["swLiveConnect"] = "";
            embeds["base"] = "";
            embeds["align"] = "";
            embeds["salign"] = "";
            embeds["scale"] = "";
            embeds["menu"] = "";
            __CONFIG__["embed"] = embeds;

        }
        #endregion
    }
}
