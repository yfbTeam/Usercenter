<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysAccountNoSetting.aspx.cs" Inherits="UCSWeb.InterfaceManagement.SysAccountNoSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8" />
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
		<title>接口管理</title>
		<link rel="stylesheet" href="../css/reset.css" />
		<link rel="stylesheet" href="../css/iconfont.css" />
		<link rel="stylesheet" type="text/css" href="../css/style.css"/>
		<script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
		<!--[if IE]>
			<script src="../Scripts/html5.js"></script>
		<![endif]-->
        <script type="text/javascript" src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
        <script type="text/javascript" src="../Scripts/jquery.tmpl.js"></script>
        <script id="tr_interface" type="text/x-jquery-tmpl">
            <tr>
                <td style="width: 5%;">
                    <input type="checkbox" name="ck_trsub" value="${Id}" {{if InterfaceId!=0}}checked="checked"{{/if}} onclick="CheckSub(this);" />
                </td>
                <td>${Name}</td>
                <td>${Description}</td>
            </tr>
        </script>
    <script id="tr_entity" type="text/x-jquery-tmpl">
        <tr>
            <td style="width: 5%;">
                <input type="checkbox" name="ck_trsub_entity" value="${EntityName}" {{if EntityRelId!=0}}checked="checked"{{/if}} onclick="CheckSub(this, 'ck_tball_entity');"/>
            </td>
            <td>${EntityName}</td>
            <td style="padding:0px 25px;">${Rel_FieldsChina}
				<div class="setings" onclick="OpenIFrameWindow('可访问字段设置','FiledSettings.aspx?accountno=${AccountNo}&etname=${EntityName}','820px','460px')">
                    <i class="iconfont">&#xe6bd;</i>
                </div>
            </td>
        </tr>
    </script>
    <script type="text/javascript">
        $(function () {
            $('.footer—wrap').load('../CommonPage/footer.html');
            $('.header_wrap').load('../CommonPage/header.aspx');
        });
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="header_wrap" style="height:61px;"></div>
		<div id="main">
			<div class="w1200 clearfix wrap pr">
				<div class="api_wrap">
					<div class="content_wrap">
						<div class="title_nav clearfix" style="width:1158px;position:fixed;top:61px;padding-top:20px;z-index:999;background: #fff;">
							<div class="title_nav_left fl">
								<a href="javascript:;" class="active">系统账户</a>
							</div>
							<div class="title_nav_right fr">
								<a id="a_log" href="/SystemSettings/SystemLogList.aspx">查看详细日志</a>
							</div>
						</div>
						<div class="api_box clearfix pr" style="margin-top: 42px;">
							<div class="api_left fl">
								<div class="toolsbar clearfix mt10 mb10 ml10">
									<div class="tool_left fl">
										<span btncls="icon-plus" style="display:none;" onclick="OpenIFrameWindow('新建系统账户','EditSysAccountNo.aspx?itemid=0&btncls=icon-plus','410px','240px');">新建</span>
										<span btncls="icon-edit" style="display:none;" onclick="EditItem();">修改</span>
										<span btncls="icon-disable" style="display:none;" id="span_isenable" onclick="EnableItem(this.innerText);">禁用</span>
									</div>
								</div>
								<div class="api_user">
									<div id="div_sysaccount" class="user_wrap"></div>
								</div>
							</div>
							<div class="api_right fr pr" style="margin-top:-10px;">
                                <div class="apisub_right">
								    <div class="title_nav clearfix ml10 mr10" style="position:fixed;z-index: 999;width:921px;background: #fff;padding-top:20px;top:113px;">
									    <div class="title_nav_left fl" id="div_tabs">
										    <a href="javascript:;" class="active">接口权限</a>
										    <a href="javascript:;">实体权限</a>
									    </div>
								    </div>
								    <div class="table_box pl10 pr10" style="padding-top:42px;padding-bottom:50px;">
									    <div class="table_wrap mt10 ">
                                            <table>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 5%;">
                                                            <input type="checkbox" name="ck_tball" id="ck_tball" onclick="CheckAll(this);" /></th>
                                                        <th>接口</th>
                                                        <th>描述</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tb_interface"></tbody>
                                            </table>
									    </div>
									    <div class="table_wrap mt10 none">
                                            <table>
                                                <thead>
                                                    <tr>
                                                        <th style="width: 5%;">
                                                            <input type="checkbox" name="ck_tball_entity"  id="ck_tball_entity" onclick="CheckAll(this, 'ck_trsub_entity');"/></th>
                                                        <th>实体</th>
                                                        <th>可访问字段</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tb_entity"></tbody>
                                            </table>
									    </div>
								    </div>
                                </div>
								<div class="btn_fixed">
									<div class="btn_wrap">
										<input type="button" name="" id="" value="确定" class="btns insert" onclick="SaveRelationData();"/>
									</div>
								</div>
							</div>
						</div>
					</div>	
				</div>
			</div>
		</div>
		<div class="footer—wrap" style="height:40px;"></div>
		<script type="text/javascript">
		    (function init() {
		        var height = $(window).height() - 101;
		        function a() {
		            $('.api_left').height(height - 62);
		            $('.api_wrap').css('minHeight', height);
		            $('.user_wrap').height(height - 110);
		        }
		        a();
		        $(window).resize(function () {
		            a();
		        })
		    })();
		    $(function () {
		        GetLogUrl();
		        $('.title_nav_left a').click(function () {
		            $(this).addClass('active').siblings().removeClass('active');
		            var n = $(this).index();
		            $('.table_box .table_wrap').eq(n).show().siblings().hide();
		        })
		        $('.table_wrap table tbody tr').find('.setings').click(function () {
		            if ($(this).next().is(':hidden')) {
		                $('.table_wrap table tbody tr').find('.setting_none').hide();
		                $(this).next().show();
		                $(this).next().mouseleave(function () {
		                    $(this).hide();
		                });
		            } else {
		                $(this).next().hide();
		            }
		        });
		        GetSysAccontData();
		    });
		    //获取系统账号
		    function GetSysAccontData() {
		        $.ajax({
		            url: "../common.ashx",
		            type: "post",
		            async: false,
		            dataType: "json",
		            data: {
		                PageName: "/InterfaceManagement/SysAccountNoHandler.ashx",
		                Func: "GetSystemInfoDataPage",
		                ispage: false,
		                SysAccountNo: SysAccountNo,
		                LoginName: "<%=UserInfo.LoginName%>"
		            },
		            success: function (json) {
		                if (json.result.errNum.toString() == "0") {
		                    $("#div_sysaccount").html('');
		                    var rtnObj = json.result.retData;
		                    $(rtnObj).each(function (i, n) {
		                        var hlclass = n.IsDelete.toString() == "1" ? "red" : "";
		                        if (i == 0) {
		                            hlclass += " selected";
		                            $("#span_isenable").html(n.IsDelete.toString() == "1" ? "启用" : "禁用");
		                            GetRelationData(n.AccountNo, "GetInterfaceByAccountNo", "tb_interface", "tr_interface", "暂无接口");
		                            GetRelationData(n.AccountNo, "GetEntityByAccountNo", "tb_entity", "tr_entity", "暂无实体");
		                        }
		                        $("#div_sysaccount").append("<h1 id='sysacc_" + n.Id + "' isdelete='" + n.IsDelete + "' class='" + hlclass + "' accountno='" + n.AccountNo + "'>" + cutstr(n.Name + "(" + n.AccountNo + ")", 20) + "</h1>");
		                    });
		                    $('.user_wrap h1').click(function () {
		                        $(this).addClass('selected').siblings().removeClass('selected');
		                        var isdelete = $(this).attr('isdelete'), accountno = $(this).attr('accountno');
		                        $("#span_isenable").html(isdelete == "1" ? "启用" : "禁用");
		                        GetRelationData(accountno, "GetInterfaceByAccountNo", "tb_interface", "tr_interface", "暂无接口");
		                        GetRelationData(accountno, "GetEntityByAccountNo", "tb_entity", "tr_entity", "暂无实体");
		                    });
		                }
		                else {
		                    $("#div_sysaccount").html("<h1>暂无系统账户!</h1>");
		                }
		            },
		            error: function (errMsg) {
		                $("#div_sysaccount").html("<h1>暂无系统账户!</h1>");
		            }
		        });
            }
            function EditItem() {
                var accoNo = $('#div_sysaccount').find("h1.selected");
                if (accoNo.length == 0) { layer.msg('请选择要修改的系统账户！'); return; }
                OpenIFrameWindow('修改系统账户', 'EditSysAccountNo.aspx?itemid=' + accoNo[0].id.replace("sysacc_", "") +"&btncls=icon-edit", '410px', '240px');
            }
            function EnableItem(wmsg) {
                var accoNo = $('#div_sysaccount').find("h1.selected");
                if (accoNo.length == 0) { layer.msg('请选择要' + wmsg + '的系统账户！'); return; }
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/InterfaceManagement/SysAccountNoHandler.ashx",
                        func: "EnableSystemInfo",
                        IsEnable: wmsg == "启用" ? "0" : "1",
                        IDs: accoNo[0].id.replace("sysacc_", ""),
                        SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>"
		            },
		            success: function (json) {
		                if (json.result.errNum == 0) {
		                    GetSysAccontData();
		                    layer.msg(wmsg + '成功！')
		                }
		                else {
		                    layer.msg(json.result.errMsg);
		                }
		            },
		            error: function (ErroMsg) {
		                layer.msg(ErroMsg);
		            }
		        });
            }
            //获取接口/实体信息
            function GetRelationData(accountno, func, tbid, trid, msg) {
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/InterfaceManagement/SysAccountNoHandler.ashx",
                        Func: func,
                        AccountNo: accountno,
                        SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>"
		            },
		            success: function (json) {
		                if (json.result.errNum.toString() == "0") {
		                    $("#" + tbid).html('');
		                    var rtnObj = json.result.retData;
		                    $("#" + trid).tmpl(rtnObj).appendTo("#" + tbid);
		                }
		                else {
		                    $("#" + tbid).html("<tr><td colspan='5'>" + msg + "！</td></tr>");
		                }
		            },
		            error: function (errMsg) {
		                $("#" + tbid).html("<tr><td colspan='5'>" + msg + "！</td></tr>");
		            }
		        });
            }
            //保存配置信息
            function SaveRelationData() {
                var accountNo = $("#" + $('#div_sysaccount').find("h1.selected")[0].id).attr('accountno');
                var tabindex = $("#div_tabs").find("a.active").index();
                if (tabindex == 0) {
                    SetInterfacePermission(accountNo);
                } else {
                    SetEntityPermission(accountNo);
                }
            }
            function SetInterfacePermission(accountNo) {
                var checkedtr = $("input[type='checkbox'][name='ck_trsub']:checked");
                if (checkedtr.length == 0) { layer.msg('请勾选接口！'); return; }
                var idArray = [];
                $(checkedtr).each(function (i, n) {
                    idArray.push(n.value);
                });
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/InterfaceManagement/SysAccountNoHandler.ashx",
                        Func: "SetInterfacePermission",
                        AccountNo: accountNo,
                        InteridStr: idArray.join(","),
                        SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>"
		            },
		            success: function (json) {
		                var result = json.result;
		                if (result.errNum == 0) {
		                    layer.msg('接口权限配置成功!');
		                } else {
		                    layer.msg(result.errMsg);
		                }
		            },
		            error: function (XMLHttpRequest, textStatus, errorThrown) {
		                layer.msg("操作失败！");
		            }
		        });
            }
            function SetEntityPermission(accountNo) {
                var checkedtr = $("input[type='checkbox'][name='ck_trsub_entity']:checked");
                if (checkedtr.length == 0) { layer.msg('请勾选实体！'); return; }
                var enameArray = [];
                $(checkedtr).each(function (i, n) {
                    enameArray.push(n.value);
                });
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/InterfaceManagement/SysAccountNoHandler.ashx",
                        Func: "SetEntityPermission",
                        AccountNo: accountNo,
                        EntityNameStr: enameArray.join(","),
                        SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>"
		            },
		            success: function (json) {
		                var result = json.result;
		                if (result.errNum == 0) {
		                    layer.msg('实体权限配置成功!');
		                    GetRelationData(accountNo, "GetEntityByAccountNo", "tb_entity", "tr_entity", "暂无实体");
		                } else {
		                    layer.msg(result.errMsg);
		                }
		            },
		            error: function (XMLHttpRequest, textStatus, errorThrown) {
		                layer.msg("操作失败！");
		            }
		        });
            }
            function GetLogUrl() {
                var serurl = '/SystemSettings/SystemLogList.aspx';
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/SystemSettings/MenuHandler.ashx",
                        Func: "GetParentMenuByUrl",
                        Url: serurl,
                        SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>"
		            },
		            success: function (json) {
		                var result = json.result;
		                if (result.errNum == 0) {
		                    $("#a_log").attr("href", serurl + "?p=" + result.retData[0].Id);
		                }
		            }
		        });
            }
	    </script>
    </form>
</body>
</html>
