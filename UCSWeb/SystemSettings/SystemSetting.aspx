<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemSetting.aspx.cs" Inherits="UCSWeb.SystemSettings.SystemSetting" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>系统设置</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="../Scripts/html5.js"></script>
		<![endif]-->
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.js"></script>
    <script type="text/javascript" src="../Scripts/PageBar.js"></script>
    <%--接口--%>
    <script id="li_interface" type="text/x-jquery-tmpl">
        <li>
            <a href="javascript:void(0);" class="clearfix">
                <em class="fl">${Name}</em>
                <span class="fr">${VisitCount}次</span>
            </a>
        </li>
    </script>
    <%--实体--%>
    <script id="li_entity" type="text/x-jquery-tmpl">
        <li>
            <a href="javascript:void(0);" class="clearfix">
                <em class="fl">${EntityName}</em>
                <span class="fr">${EntityChina}</span>
            </a>
        </li>
    </script>
    <%--角色--%>
    <script id="li_role" type="text/x-jquery-tmpl">
        <li>
            <a href="javascript:void(0);" class="clearfix">
                <em class="fl">${Name}</em>
                <span class="fr">${UserCount}人</span>
            </a>
        </li>
    </script>
    <%--日志--%>
    <script id="li_log" type="text/x-jquery-tmpl">
        <li>
            <a href="javascript:void(0);" class="clearfix">
                <em class="fl">${OperationMsg}</em>
                <span class="fr">${DateTimeConvert(CreateTime,'yyyy-MM-dd HH:mm:ss')}</span>
            </a>
        </li>
    </script>
</head>
<body>
    <!--header-->
    <div class="header_wrap" style="height: 61px;"></div>
    <div id="main">
        <div class="w1200 clearfix wrap pr">
            <div class="menu_wrap fl leftmenuclass"></div>
            <div class="content fr">
                <div class="content_wrap">
                    <div class="title_nav clearfix">
                        <div class="title_nav_left fl">
                            <a href="javascript:;" class="active">系统设置</a>
                        </div>
                        <div class="title_nav_right fr">
                            <span>接口<em id="emhead_interface">0</em>个，实体<em id="emhead_entity">0</em>个，角色<em id="emhead_role">0</em>个，系统日志<em id="emhead_log">0</em>条
                            </span>
                        </div>
                    </div>
                    <div class="system_modult">
                        <div class="modult">
                            <h1>接口（<em id="em_interface">0</em>个）<a id="a_interface" href="/SystemSettings/InterfaceDictionary.aspx" class="fr"><i class="iconfont">&#xe61a;</i></a></h1>
                            <ul id="ul_interface"></ul>
                        </div>
                        <div class="modult">
                            <h1>实体（<em id="em_entity">0</em>个）<a id="a_entity" href="/SystemSettings/EntityDictionary.aspx" class="fr"><i class="iconfont">&#xe61a;</i></a></h1>
                            <ul id="ul_entity"></ul>
                        </div>
                        <div class="modult">
                            <h1>角色（<em id="em_role">0</em>个）<a id="a_role" href="/SystemSettings/RoleSettings.aspx" class="fr"><i class="iconfont">&#xe61a;</i></a></h1>
                            <ul id="ul_role"></ul>
                        </div>
                        <div class="modult">
                            <h1>日志（<em id="em_log">0</em>）<a id="a_log" href="/SystemSettings/SystemLogList.aspx" class="fr"><i class="iconfont">&#xe61a;</i></a></h1>
                            <ul id="ul_log"></ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer—wrap" style="height: 40px;"></div>
    <script type="text/javascript">
        (function init() {
            var height = $(window).height() - 101;
            $('.menu_wrap').height(height);
            $(window).resize(function () {
                $('.menu_wrap').height(height);
            })
            $('.wrap').css('minHeight', height);
            $('.content').css('minHeight', height);
        })();
        $(function () {
            $('.footer—wrap').load('../CommonPage/footer.html');
            $('.header_wrap').load('../CommonPage/header.aspx', function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    var parm = $('#hid_leftmenu').val();
                    $("#a_interface").attr("href", "../SystemSettings/InterfaceDictionary.aspx?p=" + parm);
                    $("#a_entity").attr("href", "../SystemSettings/EntityDictionary.aspx?p=" + parm);
                    $("#a_role").attr("href", "../SystemSettings/RoleSettings.aspx?p=" + parm);
                    $("#a_log").attr("href", "../SystemSettings/SystemLogList.aspx?p=" + parm);
                }
            });
            getData(1, 12, "SystemSettings/InterfaceHandler.ashx", "GetInterfaceDataPage", "ul_interface", "li_interface", "emhead_interface", "em_interface", "暂无接口");
            getData(1, 12, "SystemSettings/EntityHandler.ashx", "GetEntityDataPage", "ul_entity", "li_entity", "emhead_entity", "em_entity", "暂无实体");
            getData(1, 12, "SystemSettings/RoleHandler.ashx", "GetRoleDataPage", "ul_role", "li_role", "emhead_role", "em_role", "暂无角色");
            getData(1, 12, "SystemSettings/LogInfoHandler.ashx", "GetLogInfoDataPage", "ul_log", "li_log", "emhead_log", "em_log", "暂无日志");
        });
        function getData(startIndex, pageSize, pageName, func, ulid, liid, em_headid, emid, msg) {
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: pageName,
                    Func: func,
                    PageIndex: startIndex,
                    pageSize: pageSize,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
			        },
			        success: function (json) {
			            if (json.result.errNum.toString() == "0") {
			                $("#" + ulid).html('');
			                var rtnObj = json.result.retData;
			                $("#" + em_headid).html(rtnObj.RowCount);
			                $("#" + emid).html(rtnObj.RowCount);
			                $("#" + liid).tmpl(rtnObj.PagedData).appendTo("#" + ulid);
			            }
			            else {
			                $("#" + em_headid).html(0);
			                $("#" + emid).html(0);
			                $("#" + ulid).html('<li><a href="javascript:void(0);" class="clearfix"><em class="fl">' + msg + '</em><span class="fr"></span></a></li>');
			            }
			        },
			        error: function (errMsg) {
			            $("#" + ulid).html('<li><a href="javascript:void(0);" class="clearfix"><em class="fl">' + msg + '</em><span class="fr"></span></a></li>');
			        }
			    });
            }
    </script>
</body>
</html>

