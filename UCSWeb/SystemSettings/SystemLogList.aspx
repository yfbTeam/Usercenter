<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemLogList.aspx.cs" Inherits="UCSWeb.SystemSettings.SystemLogList" %>

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
    <script type="text/javascript" src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.js"></script>
    <script type="text/javascript" src="../Scripts/PageBar.js"></script>
    <script id="tr_list" type="text/x-jquery-tmpl">
        <tr>
            <td>${pageIndex()}</td>
            <td>${DateTimeConvert(CreateTime,'yyyy-MM-dd HH:mm:ss')}</td>
            <td>${LoginName}</td>
            <td>${ShowLogType(LogType)}</td>
            <td>${OperationMsg}</td>
        </tr>
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
                            <a href="javascript:;" class="active">系统日志</a>
                        </div>
                    </div>
                    <div class="toolsbar clearfix mt10">
                        <div class="tool_right fr clearfix">
                            <div class="fl mr10 pr search">
                                <input type="text" id="txt_OperationMsg" placeholder="请输入操作关键字" onblur="SearchCondition();" />
                                <i class="iconfont" onclick="SearchCondition();">&#xe604;</i>
                            </div>
                            <div class="fl">
                                <label>类别:</label>
                                <select class="select" id="sel_LogType" onchange="SearchCondition();">
                                    <option value="" selected="selected">全部</option>
                                    <option value="0">登录日志</option>
                                    <option value="1">本地日志</option>
                                    <option value="2">接口日志</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="table_wrap mt10">
                        <table>
                            <thead>
                                <tr>
                                    <th class="number">序号</th>
                                    <th>日期</th>
                                    <th>账号</th>
                                    <th>类别</th>
                                    <th>操作</th>
                                </tr>
                            </thead>
                            <tbody id="tb_list"></tbody>
                        </table>
                        <!--分页-->
                        <div class="page" id="pageBar"></div>
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
            $('.header_wrap').load('../CommonPage/header.aspx');
            getData(1, 18);
        });
        var sername = $("#txt_OperationMsg").val().trim();
        var serLogType = $("#sel_LogType").val();
        function SearchCondition() {
            sername = $("#txt_OperationMsg").val().trim();
            serLogType = $("#sel_LogType").val();
            getData(1, 18);
        }
        function getData(startIndex, pageSize) {
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/LogInfoHandler.ashx",
                    Func: "GetLogInfoDataPage",
                    OperationMsg: sername,
                    SerLogType: serLogType,
                    PageIndex: startIndex,
                    pageSize: pageSize,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
	                },
	                success: function (json) {
	                    if (json.result.errNum.toString() == "0") {
	                        $("#tb_list").html('');
	                        var rtnObj = json.result.retData;
	                        $("#tr_list").tmpl(rtnObj.PagedData).appendTo("#tb_list");
	                        $("#pageBar").show();
	                        makePageBar(getData, document.getElementById("pageBar"), rtnObj.PageIndex, rtnObj.PageCount, pageSize, rtnObj.RowCount);
	                    }
	                    else {
	                        $("#tb_list").html("<tr><td colspan='5'>暂无系统日志！</td></tr>");
	                        $("#pageBar").hide();
	                    }
	                },
	                error: function (errMsg) {
	                    $("#tb_list").html("<tr><td colspan='5'>暂无系统日志！</td></tr>");
	                }
	            });
            }
    </script>
</body>
</html>
