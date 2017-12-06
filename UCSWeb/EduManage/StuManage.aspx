<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StuManage.aspx.cs" Inherits="UCSWeb.EduManage.StuManage" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>学员设置</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="../Scripts/html5.js"></script>
		<![endif]-->
    <link href="../Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <link href="../Scripts/zTree/css/Common.css" rel="stylesheet" />
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/PageBar.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.core-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.excheck-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.exedit-3.5.js"></script>
    <script src="EduManage.js"></script>
    <style>
        .student_manage_left {
            width: 725px;
        }

        .menu_wrapa {
            width: 208px;
            background: #fafdff;
            overflow: auto;
            border: 1px solid #DDECFA;
        }
    </style>
    <style>
        .menu_wrap .tool_left span {
            margin-top: 8px;
            margin-right: 0;
            margin-left: 8px;
            padding: 5px 8px;
        }
    </style>
    <script id="trUser" type="text/x-jquery-tmpl">
        <tr>
            <td>
                <input type="checkbox" name="Check_box" id="subcheck" value="${Id}" />
            </td>
            <td>${Name}<div class="setings"><i class="iconfont">&#xe6bd;</i></div>
                <div class="setting_none">
                    <a href="#" onclick="UpdatePwd(${Id})">修改密码</a>
                    <a href="#" onclick="ViewUser(${Id})">查看属性</a>
                    <a href="#" onclick="EditUser(${Id})">修改属性</a>
                </div>
            </td>
            <td>{{if Sex==0}}女
                {{else}}男
                {{/if}} </td>
            <td>${LoginName}</td>
            <td>${IDCard}</td>
            <td>{{if AuthenType==2}}<span class="colorgreen">激活</span>
                {{else}}{{if AuthenType==1}}<span class="colororange">未激活</span>
                {{else}}<span class="colorred">新用户注册</span>
            {{/if}}{{/if}} 
               
        </tr>
    </script>
</head>
<body>
    <input type="hidden" id="HOrgNo" />
    <input type="hidden" id="Pid" />
    <!--header-->
    <div class="header_wrap" style="height: 61px;"></div>
    <div id="main">
        <div class="w1200 clearfix wrap pr">
            <div class="menu_wrap fl leftmenuclass"></div>
            <div class="content fr">
                <div class="content_wrap">
                    <div class="title_nav clearfix">
                        <div class="title_nav_left fl">
                            <a href="javascript:;" class="active">学员设置</a>
                        </div>
                    </div>
                    <div class="toolsbar clearfix mt10">
                        <div class="tool_left fl">
                            <span btncls="icon-plus" style="display:none;" class="NewItem">新建</span>
                            <span btncls="icon-import" style="display:none;" class="insert_user">导入</span>
                            <%--<span btncls="icon-del" style="display:none;" onclick="DelUser()">删除</span>--%>
                            <span btncls="icon-enable" style="display:none;" onclick="EnableUser('1')">启用</span>
                            <span btncls="icon-disable" style="display:none;" onclick="EnableUser('0')">禁用</span>
                        </div>
                        <div class="tool_right fr clearfix">
                            <div class="fl mr10 pr search">
                                <input type="text" name="" id="Name" value="" placeholder="请输入关键字" />
                                <i class="iconfont" onclick="getData(1,10)">&#xe604;</i>
                            </div>
                            <div class="fl">
                                <lable>状态:</lable>
                                <select class="select" id="Status" onchange="getData(1,10)">
                                    <option value="">全部</option>
                                    <option value="1">启用</option>
                                    <option value="0">禁用</option>
                                    <option value="2">未激活</option>
                                </select>
                                <lable>学期:</lable>
                                <select class="select" id="StudyTerm" onchange="BindGrade();getData(1,10);">
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="student_manage mt10 clearfix">
                        <div class="menu_wrapa fl">
                            <ul class="ztree" id="treeMenu"></ul>
                        </div>
                        <div class="fr student_manage_left fr">
                            <div class="table_wrap">
                                <table>
                                    <thead>
                                        <tr>
                                            <th>
                                                <input type="checkbox" name="" id="checkAll" value="" /></th>

                                            <th>姓名</th>
                                            <th>性别</th>
                                            <th>登陆名</th>
                                            <th>身份证号</th>
                                            <th>状态</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbUser"></tbody>
                                </table>
                                <!--分页-->
                                <div class="page" id="pageBar"></div>
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
            //$('.menu_wrapa').load('/CommonPage/menu.html');
            GetTerm();
            BindGrade();
        });
        function SeetingShow() {
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
            })
        }
        //获取用户信息
        function getData(startIndex, pageSize) {
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
                    Name: $("#Name").val(),
                    Status: $("#Status").val(),
                    IsStu: "true",
                    OrgNo: $("#HOrgNo").val(),
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    AcademicId: $("#StudyTerm").val()
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#tbUser").html('');
                        $("#trUser").tmpl(json.result.retData.PagedData).appendTo("#tbUser");
                        if (json.result.retData.RowCount < pageSize) {
                            $("#pageBar").hide();
                        } else {
                            makePageBar(getData, document.getElementById("pageBar"), json.result.retData.PageIndex, json.result.retData.PageCount, pageSize, json.result.retData.RowCount);

                            $("#pageBar").show();
                        }
                    }
                    else {
                        $("#tbUser").html("<tr><td colspan='100'>暂无用户信息！</td></tr>");
                        $("#pageBar").hide();
                    }
                    SeetingShow();
                    NewCheckAll($('.table_wrap input[type=checkbox]'));

                },
                error: function (errMsg) {
                    $("#tb_Class").html("<tr><td colspan='5'>暂无用户信息！</td></tr>");
                }
            });
        }
        //用户导入
        $(document).on('click', '.insert_user', function (event) {
            if ($("#Pid").val() == undefined || $("#Pid").val() == "") {
                layer.msg("请选择要添加用户的班级");
            }
            else {
                OpenIFrameWindow("导入用户", "../UserManage/ImportUser.aspx?OrgNo=" + $("#HOrgNo").val(), "560px", "355px")
            }
        });

        //新增用户
        $(document).on('click', '.NewItem', function (event) {
            if ($("#Pid").val() == undefined || $("#Pid").val() == "") {
                layer.msg("请选择要添加用户的班级");
            }
            else {
                OpenIFrameWindow("新增用户", "../UserManage/EditUser.aspx?RegisterOrg=" + $("#HOrgNo").val() + "&IsStu=true", "660px", "540px");
            }
        });

        function DelUser() {
            var ids = "";

            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids += this.value + ",";
                }
            });
            if (ids == "") {
                layer.msg("请选择数据行");
            } else {
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/UserManage/UserInfo.ashx",
                        func: "DelUser",
                        ID: ids
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            getData(1, 10);
                        }
                        else {
                            layer.msg(json.result.errMsg);
                        }
                    },
                    error: function (ErroMsg) {
                        layer.msg(ErroMsg);
                    }
                })
            }
        }
        //启用、禁用用户
        function EnableUser(Status) {
            var ids = "";

            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids += this.value + ",";
                }
            });
            if (ids == "") {
                layer.msg("请选择数据行");
            } else {
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/UserManage/UserInfo.ashx",
                        func: "EnableUser",
                        IsEnable: Status,
                        SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>",
                        ID: ids
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            getData(1, 10);
                        }
                        else {
                            layer.msg(json.result.errMsg);
                        }
                    },
                    error: function (ErroMsg) {
                        layer.msg(ErroMsg);
                    }
                })
            }
        }

        //修改用户
        function EditUser(ID) {
            //event.preventDefault();
            OpenIFrameWindow("用户修改", "../UserManage/EditUser.aspx?ID=" + ID + "&IsStu=true", "660px", "520px")
        }
        //查看
        function ViewUser(ID) {
            OpenIFrameWindow("用户查看", "../UserManage/EditUser.aspx?ID=" + ID + "&IsStu=true&Type=2", "660px", "540px")
        }
        //修改密码
        function UpdatePwd(ID) {
            OpenIFrameWindow("修改密码", "../UserManage/UpdatePwd.aspx", "400px", "300px")
        }
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

        var log, className = "dark";
        function beforeClick(treeId, treeNode, clickFlag) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeClick ]&nbsp;&nbsp;" + treeNode.name);
            return (treeNode.click != false);
        }
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
        function onClick(event, treeId, treeNode, clickFlag) {
            $("#HOrgNo").val(treeNode.id);
            $("#Pid").val(treeNode.pId);
            getData(1, 10);
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
        function BindGrade() {
            $.ajax({
                type: "get",
                url: "../common.ashx",
                dataType: "json",
                async: false,
                data: {
                    "PageName": "/EduManage/GradeHandler.ashx", "Func": "GetGradClass", SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    AcademicId: $("#StudyTerm").val()
                },
                success: function (returnVal) {
                    if (returnVal.result.errNum == "0") {

                        var treeObj = $("#treeMenu");
                        var jsonTree = $.parseJSON(returnVal.result.retData);
                        if (jsonTree.length > 0) {
                            $.fn.zTree.init(treeObj, setting, $.parseJSON(returnVal.result.retData));
                            zTree_Menu = $.fn.zTree.getZTreeObj("treeMenu");
                            zTree_Menu.expandAll(true);
                            var nodes = zTree_Menu.getNodes();
                            zTree_Menu.selectNode(nodes[0]);
                            $("#HOrgNo").val(nodes[0].id)
                            getData(1, 10);
                            treeObj.hover(function () {
                                if (!treeObj.hasClass("showIcon")) {
                                    treeObj.addClass("showIcon");
                                }
                            }, function () {
                                treeObj.removeClass("showIcon");
                            });
                        }

                    }
                    else {
                        $("#tbUser").html("<tr><td colspan='100'>暂无用户信息！</td></tr>");
                    }
                },
                error: function (errMsg) {
                    alert('数据加载失败！');
                }
            });
        };

    </script>
</body>
</html>
