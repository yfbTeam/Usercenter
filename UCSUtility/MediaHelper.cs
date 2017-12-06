using System.Text;

namespace UCSUtility
{
    /// <summary>
    /// 播放器帮助类
    /// </summary>
    public class MediaHelper
    {
        /// <summary>
        /// 播放器
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>生成的html</returns>
        public static string Player(string url, int width, int height)
        {
            string strTmp = url.ToLower();
            if (strTmp.EndsWith(".wmv") || strTmp.EndsWith(".mp3") || strTmp.EndsWith(".wma") || strTmp.EndsWith(".avi") ||
                strTmp.EndsWith(".asf") || strTmp.EndsWith(".mpg"))
            {
                return Wmv(url, width, height);
            }
            if (strTmp.EndsWith(".mp3"))
            {
                return Mp3(url, width, height);
            }
            if (strTmp.EndsWith(".swf"))
            {
                return Swf(url, width, height);
            }
            if (strTmp.EndsWith(".rm"))
            {
                return Rm(url, width, height);
            }
            return "数据错误";
        }

        /// <summary>
        /// wmv格式文件播放
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>生成的html</returns>
        private static string Wmv(string url, int width, int height)
        {
            var sb = new StringBuilder();

            sb.Append("<object   id=\"tt90play\"   style=\"WIDTH:   " + width + "px;height:" + height + "px\"   \n");
            sb.Append(
                "classid=\"CLSID:6BF52A52-394A-11D3-B153-00C04F79FAA6\"   type=application/x-oleobject   standby=\"Loading   Windows   Media   Player   components...\"");
            sb.Append("codebase=\"downloads/mediaplayer9.0_cn.exe\"   VIEWASTEXT>\n");
            sb.Append("<param   name=\"URL\"   value='" + url + "'>\n");
            sb.Append("<param   name=\"controls\"   value=\"ControlPanel,StatusBa\">");
            sb.Append("<param   name=\"hidden\"   value=\"1\">");
            sb.Append("<param   name=\"ShowControls\"   VALUE=\"1\">");
            sb.Append("<param   name=\"rate\"   value=\"1\">\n");
            sb.Append("<param   name=\"balance\"   value=\"0\">\n");
            sb.Append("<param   name=\"currentPosition\"   value=\"-1\">\n");
            sb.Append("<param   name=\"defaultFrame\"   value=\"\">\n");
            sb.Append("<param   name=\"playCount\"   value=\"100\">\n");
            sb.Append("<param   name=\"autoStart\"   value=\"-1\">\n");
            sb.Append("<param   name=\"currentMarker\"   value=\"0\">\n");
            sb.Append("<param   name=\"invokeURLs\"   value=\"-1\">\n");
            sb.Append("<param   name=\"baseURL\"   value=\"\">\n");
            sb.Append("<param   name=\"volume\"   value=\"85\">\n");
            sb.Append("<param   name=\"mute\"   value=\"0\">\n");
            sb.Append("<param   name=\"uiMode\"   value=\"mini\">\n");
            sb.Append("<param   name=\"stretchToFit\"   value=\"0\">\n");
            sb.Append("<param   name=\"windowlessVideo\"   value=\"0\">\n");
            sb.Append("<param   name=\"enabled\"   value=\"-1\">\n");
            sb.Append("<param   name=\"enableContextMenu\"   value=\"false\">\n");
            sb.Append("<param   name=\"fullScreen\"   value=\"0\">\n");
            sb.Append("<param   name=\"SAMIStyle\"   value=\"\">\n");
            sb.Append("<param   name=\"SAMILang\"   value=\"\">\n");
            sb.Append("<param   name=\"SAMIFilename\"   value=\"\">\n");
            sb.Append("<param   name=\"captioningID\"   value=\"\">\n");
            sb.Append("</object><br>\n");

            return sb.ToString();
        }

        /// <summary>
        /// Wma格式文件播放
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>生成的html</returns>
        private static string Wma(string url, int width, int height)
        {
            var sb = new StringBuilder();

            sb.Append(
                "<object   id=\"WMPlay\"   classid=\"clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95\"   style=\"Z-INDEX:   101;   LEFT:   40px;   WIDTH:   240px;   POSITION:   absolute;   TOP:   32px;   HEIGHT:   248px\"   >");
            sb.Append("<param   name=\"Filename\"   value=\"" + url + "\">");
            sb.Append("<param   name=\"PlayCount\"   value=\"1\">");
            sb.Append("<param   name=\"AutoStart\"   value=\"0\">");
            sb.Append("<param   name=\"ClickToPlay\"   value=\"1\">");
            //sb.Append("<param   name=\"DisplaySize\"   value=\"0\">"); 
            sb.Append("<param   name=\"ShowControls\" value=\"0\">"); //<!--是否显示控制,比如播放,停止,暂停-->
            sb.Append("<param   name=\"EnableFullScreen   Controls\"   value=\"1\">");
            sb.Append("<param   name=\"ShowAudio   Controls\"   value=\"1\">");
            sb.Append("<param   name=\"EnableContext   Menu\"   value=\"1\">");
            sb.Append("<param   name=\"ShowDisplay\"   value=\"1\">");
            sb.Append("</object>");
            return sb.ToString();
        }

        /// <summary>
        /// Avi格式文件播放
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>生成的html</returns>
        private static string Avi(string url, int width, int height)
        {
            var sb = new StringBuilder();

            sb.Append(
                "<object   id=\"WMPlay\"   width=\"400\"   height=\"200\"   border=\"0\"   classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\">");
            sb.Append("<param   name=\"ShowDisplay\"   value=\"0\">");
            sb.Append("<param   name=\"ShowControls\"   value=\"1\">");
            sb.Append("<param   name=\"AutoStart\"   value=\"1\">");
            sb.Append("<param   name=\"AutoRewind\"   value=\"0\">");
            sb.Append("<param   name=\"PlayCount\"   value=\"0\">");
            sb.Append("<param   name=\"Appearance   value=\"0   value=\"\"\">");
            sb.Append("<param   name=\"BorderStyle   value=\"0   value=\"\"\">");
            sb.Append("<param   name=\"MovieWindowHeight\"   value=\"240\">");
            sb.Append("<param   name=\"MovieWindowWidth\"   value=\"320\">");
            sb.Append("<param   name=\"FileName\"   value=\"" + url + "\">");
            sb.Append("</object>");

            return sb.ToString();
        }

        /// <summary>
        /// Mpg格式文件播放
        /// </summary>        
        /// <param name="url">url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>生成的html</returns>
        private static string Mpg(string url, int width, int height)
        {
            var sb = new StringBuilder();

            sb.Append(
                "<object   classid=\"clsid:05589FA1-C356-11CE-BF01-00AA0055595A\"   id=\"WMPlay\"   width=\"239\"   height=\"250\">");
            sb.Append("<param   name=\"Filename\"   value=\"" + url + "\"   valuetype=\"ref\">");
            sb.Append("<param   name=\"Appearance\"   value=\"0\">");
            sb.Append("<param   name=\"AutoStart\"   value=\"-1\">");
            sb.Append("<param   name=\"AllowChangeDisplayMode\"   value=\"-1\">");
            sb.Append("<param   name=\"AllowHideDisplay\"   value=\"0\">");
            sb.Append("<param   name=\"AllowHideControls\"   value=\"-1\">");
            sb.Append("<param   name=\"AutoRewind\"   value=\"-1\">");
            sb.Append("<param   name=\"Balance\"   value=\"0\">");
            sb.Append("<param   name=\"CurrentPosition\"   value=\"0\">");
            sb.Append("<param   name=\"DisplayBackColor\"   value=\"0\">");
            sb.Append("<param   name=\"DisplayForeColor\"   value=\"16777215\">");
            sb.Append("<param   name=\"DisplayMode\"   value=\"0\">");
            sb.Append("<param   name=\"Enabled\"   value=\"-1\">");
            sb.Append("<param   name=\"EnableContextMenu\"   value=\"-1\">");
            sb.Append("<param   name=\"EnablePositionControls\"   value=\"-1\">");
            sb.Append("<param   name=\"EnableSelectionControls\"   value=\"0\">");
            sb.Append("<param   name=\"EnableTracker\"   value=\"-1\">");
            sb.Append("<param   name=\"FullScreenMode\"   value=\"0\">");
            sb.Append("<param   name=\"MovieWindowSize\"   value=\"0\">");
            sb.Append("<param   name=\"PlayCount\"   value=\"1\">");
            sb.Append("<param   name=\"Rate\"   value=\"1\">");
            sb.Append("<param   name=\"SelectionStart\"   value=\"-1\">");
            sb.Append("<param   name=\"SelectionEnd\"   value=\"-1\">");
            sb.Append("<param   name=\"ShowControls\"   value=\"-1\">");
            sb.Append("<param   name=\"ShowDisplay\"   value=\"-1\">");
            sb.Append("<param   name=\"ShowPositionControls\"   value=\"0\">");
            sb.Append("<param   name=\"ShowTracker\"   value=\"-1\">");
            sb.Append("<param   name=\"Volume\"   value=\"-480\">");
            sb.Append("</object>");

            return sb.ToString();
        }

        /// <summary>
        /// rm格式文件播放
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>生成的html</returns>
        private static string Rm(string url, int width, int height)
        {
            var sb = new StringBuilder();
            sb.Append(
                "<OBJECT   ID=\"WMPlay\"   codebase=\"downloads/RealPlayer10-5GOLD_cn0302.exe\"   CLASSID=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\"   HEIGHT=" +
                height + "   WIDTH=" + width + ">");
            sb.Append("<param   name=\"SRC\"   value=\"" + url + "\">");
            //sb.Append("<param   name=\"_ExtentX\"   value=\"9313\">");   
            //sb.Append("<param   name=\"_ExtentY\"   value=\"7620\">");   
            sb.Append("<param   name=\"AUTOSTART\"   value=\"0\">");
            sb.Append("<param   name=\"SHUFFLE\"   value=\"0\">");
            sb.Append("<param   name=\"PREFETCH\"   value=\"0\">");
            sb.Append("<param   name=\"NOLABELS\"   value=\"0\">");
            sb.Append("<param   name=\"CONTROLS\"   value=\"ImageWindow,ControlPanel,statusbar\">");
            sb.Append("<param   name=\"CONSOLE\"   value=\"Clip1\">");
            sb.Append("<param   name=\"LOOP\"   value=\"0\">");
            sb.Append("<param   name=\"NUMLOOP\"   value=\"0\">");
            sb.Append("<param   name=\"CENTER\"   value=\"0\">");
            sb.Append("<param   name=\"MAINTAINASPECT\"   value=\"0\">");
            sb.Append("<param   name=\"BACKGROUNDCOLOR\"   value=\"#000000\">");
            //sb.Append("<embed   SRC   type=\"audio/x-pn-realaudio-plugin\"   CONSOLE=\"Clip1\"   CONTROLS=\"ImageWindow\"   HEIGHT=\"250\"   WIDTH=\"354\"   AUTOSTART=\"false\">");   
            sb.Append("</OBJECT>");

            return sb.ToString();
        }

        /// <summary>
        /// Swf格式文件播放
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>生成的html</returns>
        private static string Swf(string url, int width, int height)
        {
            var sb = new StringBuilder();


            sb.Append(
                "<OBJECT   codeBase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\"   ");
            sb.Append("   height=\"" + height + "\"   width=\"" + width + "\"   >");
            sb.Append("<PARAM   NAME=\"FlashVars\"   VALUE=\"\">");
            sb.Append("<PARAM   NAME=\"Movie\"   VALUE=\"" + url + "\">");
            sb.Append("<PARAM   NAME=\"Src\"   VALUE=\"" + url + "\">");
            sb.Append("<PARAM   NAME=\"WMode\"   VALUE=\"Window\">");
            sb.Append("<PARAM   NAME=\"Play\"   VALUE=\"-1\">");
            sb.Append("<PARAM   NAME=\"Loop\"   VALUE=\"-1\">");
            sb.Append("<PARAM   NAME=\"Quality\"   VALUE=\"High\">");
            sb.Append("<PARAM   NAME=\"SAlign\"   VALUE=\"\">");
            sb.Append("<PARAM   NAME=\"Menu\"   VALUE=\"0\">");
            sb.Append("<PARAM   NAME=\"Base\"   VALUE=\"\">");
            sb.Append("<PARAM   NAME=\"AllowScriptAccess\"   VALUE=\"always\">");
            sb.Append("<PARAM   NAME=\"Scale\"   VALUE=\"ShowAll\">");
            sb.Append("<PARAM   NAME=\"DeviceFont\"   VALUE=\"0\">");
            sb.Append("<PARAM   NAME=\"EmbedMovie\"   VALUE=\"0\">");
            sb.Append("<PARAM   NAME=\"BGColor\"   VALUE=\"\">");
            sb.Append("<PARAM   NAME=\"SWRemote\"   VALUE=\"\">");
            sb.Append("<PARAM   NAME=\"MovieData\"   VALUE=\"\">");
            sb.Append("<PARAM   NAME=\"SeamlessTabbing\"   VALUE=\"1\">");
            sb.Append("<embed   src=\"" + url + "\"   height=\"" + height + "\"   width=\"" + width +
                      "\"   quality=\"high\"   pluginspage=\"http://www.macromedia.com/go/getflashplayer\"type=\"application/x-shockwave-flash\"   menu=\"false\">");
            sb.Append("</embed>");
            sb.Append("</OBJECT>");

            return sb.ToString();
        }

        /// <summary>
        /// Mp3格式文件播放
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>生成的html</returns>
        private static string Mp3(string url, int width, int height)
        {
            var sb = new StringBuilder();
            sb.Append(
                "<object   classid=\"WMPlay\"   codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,5,715\"   type=\"application/x-oleobject\"   width=\"   +   width   +   \"   height=\"   +   height   +   \"   hspace=\"0\"   standby=\"Loading   Microsoft   Windows   Media   Player   components...\"   id=\"NSPlay\">");

            sb.Append("<param   name=\"AutoRewind\"   value=\"0\">");
            sb.Append("<param name=\"AudioStream\" value='-1'>");
            sb.Append("<param   name=\"FileName\"   value=\"   +   url   +   \">");
            sb.Append("<param   name=\"ShowControls\"   value=\"1\">");
            sb.Append("<param   name=\"ShowPositionControls\"   value=\"-1\">");
            sb.Append("<param   name=\"ShowAudioControls\"   value=\"1\">");
            sb.Append("<param   name=\"ShowTracker\"   value=\"-1\">");
            sb.Append("<param   name=\"ShowDisplay\"   value=\"-1\">");
            sb.Append("<param   name=\"ShowStatusBar\"   value=\"1\">");
            sb.Append("<param   name=\"ShowGotoBar\"   value=\"0\">");
            sb.Append("<param   name=\"ShowCaptioning\"   value=\"-1\">");
            sb.Append("<param   name=\"AutoStart\"   value=\"1\">");
            sb.Append("<param   name=\"Volume\"   value=\"-2500\">");
            sb.Append("<param   name=\"AnimationAtStart\"   value=\"-1\">");
            sb.Append("<param   name=\"TransparentAtStart\"   value=\"-1\">");
            sb.Append("<param   name=\"AllowChangeDisplaySize\"   value=\"-1\">");
            sb.Append("<param   name=\"AllowScan\"   value=\"-1\">");
            sb.Append("<param   name=\"EnableContextMenu\"   value=\"-1\">");
            sb.Append("<param   name=\"ClickToPlay\"   value=\"-1\">");

            sb.Append("</object>");
            return sb.ToString();
        }
    }
}