<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleSettings.aspx.cs" Inherits="UCSWeb.SystemSettings.RoleSettings" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>角色管理</title>
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
            <td>
                <input type="checkbox" name="ck_trsub" value="${Id}" roname="${Name}" onclick="CheckSub(this);" /></td>
            <td>${pageIndex()}</td>
            <%--<td>R010001</td>--%>
            <td>${Name}（共${UserCount}人） </td>
            <td>${UserNames}</td>
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
                            <a href="javascript:;" class="active">角色管理</a>
                        </div>
                    </div>
                    <div class="toolsbar clearfix mt10">
                        <div class="tool_left fl">
                            <span btncls="icon-plus" style="display:none;" onclick="OpenIFrameWindow('新建角色','EditRole.aspx?itemid=0&btncls=icon-plus','850px','585px');">新建</span>
                            <span btncls="icon-show" style="display:none;" onclick="LookItem();">查看</span>
                            <span btncls="icon-edit" style="display:none;" onclick="EditItem();">修改</span>
                            <span btncls="icon-membereditor" style="display:none;" onclick="MemberEdit();">成员编辑</span>
                        </div>
                    </div>
                    <div class="table_wrap mt10">
                        <table>
                            <thead>
                                <tr>
                                    <th>
                                        <input type="checkbox" name="ck_tball" id="ck_tball" onclick="CheckAll(this);"></th>
                                    <th class="number" width="40">序号</th>
                                    <%-- <th>编号</th>--%>
                                    <th width="300">名称</th>
                                    <th>成员</th>
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
        function getData(startIndex, pageSize) {
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/RoleHandler.ashx",
                    Func: "GetRoleDataPage",
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
			                $("#tb_list").html("<tr><td colspan='5'>暂无角色！</td></tr>");
			                $("#pageBar").hide();
			            }
			        },
			        error: function (errMsg) {
			            $("#tb_list").html("<tr><td colspan='5'>暂无角色！</td></tr>");
			        }
			    });
            }
            function LookItem() {
                var checkedtr = $("input[type='checkbox'][name='ck_trsub']:checked");
                if (checkedtr.length == 0) { layer.msg('请选择要查看的行！'); return; }
                if (checkedtr.length > 1) { layer.msg('请选择一行！'); return; }
                OpenIFrameWindow('查看角色', 'EditRole.aspx?itemid=' + checkedtr[0].value + "&roname=" + encodeURIComponent($(checkedtr[0]).attr("roname")) + "&flag=1" + "&btncls=icon-show", '850px', '585px');
            }
            function EditItem() {
                var checkedtr = $("input[type='checkbox'][name='ck_trsub']:checked");
                if (checkedtr.length == 0) { layer.msg('请选择要修改的行！'); return; }
                if (checkedtr.length > 1) { layer.msg('请选择一行！'); return; }
                OpenIFrameWindow('修改角色', 'EditRole.aspx?itemid=' + checkedtr[0].value + "&roname=" + encodeURIComponent($(checkedtr[0]).attr("roname")) + "&btncls=icon-edit", '850px', '585px');
            }
            function MemberEdit() {
                var checkedtr = $("input[type='checkbox'][name='ck_trsub']:checked");
                if (checkedtr.length == 0) { layer.msg('请选择要编辑成员的行！'); return; }
                if (checkedtr.length > 1) { layer.msg('请选择一行！'); return; }
                OpenIFrameWindowCall('成员编辑', 'EditMember.aspx?itemid=' + checkedtr[0].value + "&roname=" + encodeURIComponent($(checkedtr[0]).attr("roname")), '1000px', '560px')
            }
    </script>
</body>
</html>
