<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMember.aspx.cs" Inherits="UCSWeb.SystemSettings.EditMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>成员编辑</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />

    <link rel="stylesheet" href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>

    <!--[if IE]>
    <script src="../Scripts/html5.js"></script>
    <![endif]-->
    <script type="text/javascript" src="../Scripts/layer/layer.js"></script>
    <script type="text/javascript" src="../Scripts/Common.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.js"></script>
    <script type="text/javascript" src="../Scripts/PageBar.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.core-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.excheck-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.exedit-3.5.js"></script>
    <script id="tr_list" type="text/x-jquery-tmpl">
        <tr>
            <td>
                <input type="checkbox" name="ck_trsub" value="${Id}" onclick="CheckSub(this);" />
            </td>
            <td>${Name}</td>
            <td>${LoginName}</td>
        </tr>
    </script>
    <script id="tr_listorg" type="text/x-jquery-tmpl">
        <tr>
            <td>
                <input type="checkbox" name="ck_trsub_org" value="${UniqueNo}" onclick="CheckSub(this, 'ck_tball_org');" />
            </td>
            <td>${Name}</td>
            <td>${LoginName}</td>
        </tr>
    </script>
</head>
<body style="background: #F8FCFF;">
    <form id="form1" runat="server">
        <input type="hidden" id="HOrgNo" />
        <div class="p20 clearfix">
            <div class="dialog_left fl">
                <div class="member_box">
                    <h1 class="member_name" id="h1_roname"></h1>
                    <div class="pl10 pr10 pb10">
                        <div class="toolsbar clearfix mt10">
                            <div class="tool_left fl">
                                <span onclick="DelItems();">删除</span>
                            </div>
                            <div class="tool_right fr clearfix">
                                <div class="fl pr search">
                                    <input type="text" id="txt_Name" placeholder="请输入关键字" onblur="SearchCondition();" />
                                    <i class="iconfont" onclick="SearchCondition();">&#xe604;</i>
                                </div>
                            </div>
                        </div>
                        <div class="table_wrap mt10">
                            <table>
                                <thead>
                                    <tr>
                                        <th>
                                            <input type="checkbox" name="ck_tball" id="ck_tball" onclick="CheckAll(this);" /></th>
                                        <th>用户名</th>
                                        <th>登录名</th>
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
            <div class="dialog_right fr">
                <div class="member_box clearfix">
                    <div class="menu_box fl">
                        <ul class="ztree" id="treeMenu"></ul>
                    </div>
                    <div class="member_right">
                        <div class="pl10 pr10 pb10">
                            <div class="toolsbar clearfix pt10">
                                <div class="tool_left fl">
                                    <span style="background: #9ad35b; color: #fff;" onclick="SetRoleMember();">确定</span>
                                </div>
                                <div class="tool_right fr clearfix">
                                    <div class="fl pr search">
                                        <input type="text" id="txt_NameOrg" value="" placeholder="请输入关键字" onblur="SearchCondition_org();" />
                                        <i class="iconfont" onclick="SearchCondition_org();">&#xe604;</i>
                                    </div>
                                </div>
                            </div>
                            <div class="table_wrap mt10">
                                <table>
                                    <thead>
                                        <tr>
                                            <th>
                                                <input type="checkbox" name="ck_tball" id="ck_tball_org" onclick="CheckAll(this, 'ck_trsub_org');" /></th>
                                            <th>用户名</th>
                                            <th>登录名</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tb_listorg"></tbody>
                                </table>
                                <div class="page" id="pageBar_org"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            //$('.menu_box').load('/CommonPage/menu.html');
            $("#h1_roname").html(decodeURIComponent(UrlDate.roname));
            getData(1, 10);
        });
        var sername = $("#txt_Name").val().trim();
        function SearchCondition() {
            sername = $("#txt_Name").val().trim();
            getData(1, 10);
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
                    PageName: "/SystemSettings/RoleHandler.ashx",
                    Func: "GetUserDataByRoleId",
                    Name: sername,
                    RoleId: UrlDate.itemid,
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
                        $("#tb_list").html("<tr><td colspan='5'>暂无成员！</td></tr>");
                        $("#pageBar").hide();
                    }
                },
                error: function (errMsg) {
                    $("#tb_list").html("<tr><td colspan='5'>暂无成员！</td></tr>");
                }
            });
        }
        function DelItems() {
            var checkedtr = $("input[type='checkbox'][name='ck_trsub']:checked");
            if (checkedtr.length == 0) { layer.msg('请选择要删除的行！'); return; }
            var idArray = [];
            $(checkedtr).each(function (i, n) {
                idArray.push(n.value);
            });
            $.ajax({
                url: "../common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/RoleHandler.ashx",
                    func: "DeleteUserRelation",
                    IDs: idArray.join(","),
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        getData(1, 10);
                        layer.msg('删除成功！')
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
    </script>
    <%--组织机构相关--%>
    <script type="text/javascript">
        //获取组织机构信息
        var setting = {
            view: {
                showLine: false,
                showIcon: false,
                selectedMulti: false,
                dblClickExpand: false,
                addDiyDom: addDiyDom
            },
            data: {
                keep: {
                    parent: true,
                    leaf: true
                },
                simpleData: {
                    enable: true
                }
            },
            callback: {
                onClick: onClick
            }
        };
        $(function () {
            $.ajax({
                type: "get",
                url: "../common.ashx",
                dataType: "json",
                data: { "PageName": "/Organiz/Organiz.ashx", "Func": "GetOrgMenu", SysAccountNo: SysAccountNo, LoginName: "<%=UserInfo.LoginName%>" },
                success: function (returnVal) {
                    $.fn.zTree.init($("#treeMenu"), setting, $.parseJSON(returnVal.result.retData));
                    var treeObj = $.fn.zTree.getZTreeObj("treeMenu");
                    treeObj.expandAll(true);
                },
                error: function (errMsg) {
                    alert('数据加载失败！');
                }
            });
            GetUserByOrg(1, 10);
        });
        var log, className = "dark";
        function addDiyDom(treeId, treeNode) {
            var spaceWidth = 5;
            var switchObj = $("#" + treeNode.tId + "_switch"),
			icoObj = $("#" + treeNode.tId + "_ico");
            switchObj.remove();
            icoObj.before(switchObj);

            if (treeNode.level > 1) {
                var spaceStr = "<span style='display: inline-block;width:" + (spaceWidth * treeNode.level) + "px'></span>";
                switchObj.before(spaceStr);
            }
        }
        function beforeClick(treeId, treeNode, clickFlag) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeClick ]&nbsp;&nbsp;" + treeNode.name);
            return (treeNode.click != false);
        }
        function onClick(event, treeId, treeNode, clickFlag) {
            $("#HOrgNo").val(treeNode.org);
            GetUserByOrg(1, 10);
        }
        function showLog(str) {
            if (!log) log = $("#log");
            log.append("<li class='" + className + "'>" + str + "</li>");
            if (log.children("li").length > 8) {
                log.get(0).removeChild(log.children("li")[0]);
            }
        }
        function getTime() {
            var now = new Date(),
            h = now.getHours(),
            m = now.getMinutes(),
            s = now.getSeconds();
            return (h + ":" + m + ":" + s);
        }
        function treeNode() {

        }
        var sername_org = $("#txt_NameOrg").val().trim();
        function SearchCondition_org() {
            sername_org = $("#txt_NameOrg").val().trim();
            GetUserByOrg(1, 10);
        }
        function GetUserByOrg(startIndex, pageSize) {
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    Func: "GetData",
                    PageIndex: startIndex,
                    pageSize: pageSize,
                    Name: sername_org,
                    Status: 1,
                    OrgNo: $("#HOrgNo").val(),
                    IsStu: "false",
                    AuthenType: 2,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#tb_listorg").html('');
                        var rtnObj = json.result.retData;
                        $("#tr_listorg").tmpl(rtnObj.PagedData).appendTo("#tb_listorg");
                        $("#pageBar_org").show();
                        makePageBar(GetUserByOrg, document.getElementById("pageBar_org"), rtnObj.PageIndex, rtnObj.PageCount, pageSize, rtnObj.RowCount);
                    }
                    else {
                        $("#tb_listorg").html("<tr><td colspan='5'>暂无用户信息！</td></tr>");
                        $("#pageBar_org").hide();
                    }
                },
                error: function (errMsg) {
                    $("#tb_listorg").html("<tr><td colspan='5'>暂无用户信息！</td></tr>");
                }
            });
        }
        function SetRoleMember() {
            var checkedtr = $("input[type='checkbox'][name='ck_trsub_org']:checked");
            if (checkedtr.length == 0) { layer.msg('请选择要添加的行！'); return; }
            var uniqueArray = [];
            $(checkedtr).each(function (i, n) {
                uniqueArray.push(n.value);
            });
            $.ajax({
                url: "../common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/RoleHandler.ashx",
                    func: "SetRoleMember",
                    RoleId: UrlDate.itemid,
                    uniqueNoStr: uniqueArray.join(","),
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        getData(1, 10);
                        layer.msg('添加成功！')
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
    </script>
</body>
</html>
