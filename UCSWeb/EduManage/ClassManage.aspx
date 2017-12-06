<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassManage.aspx.cs" Inherits="UCSWeb.EduManage.ClassManage" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>班级设置</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="EduManage.js"></script>
    <!--[if IE]>
			<script src="../Scripts/html5.js"></script>
		<![endif]-->
    <script type="text/javascript" src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.js"></script>
    <script type="text/javascript" src="../Scripts/PageBar.js"></script>
    <script id="tr_Class" type="text/x-jquery-tmpl">
        <tr>
            <td style="width: 5%;">
                <input type="checkbox" name="Check_box" id="" value="${Id}" />
            </td>
            <td>${Academic}</td>
            <td>${GradeName} </td>
            <td>${ClassName}</td>
            <td>${TeaName}</td>
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
                            <a href="javascript:;" class="active">班级设置</a>
                        </div>
                    </div>
                    <div class="toolsbar clearfix mt10">
                        <div class="tool_left fl">
                            <span btncls="icon-plus" style="display:none;" onclick="OpenIFrameWindow('新建班级','NewClass.aspx?btncls=icon-plus','400px','370px')">新建</span>
                            <span btncls="icon-edit" style="display:none;" onclick="EditClass()">修改</span>
                            <span btncls="icon-del" style="display:none;" onclick="DelClass()">删除</span>
                        </div>
                        <div class="tool_right fr clearfix">
                            <div class="fl mr10 pr search">
                                <input type="text" name="" id="Name" value="" placeholder="请输入关键字" />
                                <i class="iconfont" onclick="getClassData(1,10)">&#xe604;</i>
                            </div>
                            <div class="fl">
                                <lable>学期:</lable>
                                <select class="select" id="StudyTerm" onchange="getClassData(1,10)">
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="table_wrap mt10">
                        <table>
                            <thead>
                                <tr>
                                    <th>
                                        <input type="checkbox" name="" id="" value="" /></th>
                                    <th>学期名称</th>
                                    <th>年级名称</th>
                                    <th>班级名称</th>
                                    <th>班主任</th>
                                </tr>
                            </thead>
                            <tbody id="tb_Class"></tbody>
                        </table>

                    </div>
                    <!--分页-->
                    <div class="page" id="pageBar"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer—wrap" style="height: 40px;"></div>
    <script type="text/javascript">

        (function init() {
            var height = $(window).height() - 141;
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
            GetTerm();
            getClassData(1, 10);

        });

        function getClassData(startIndex, pageSize) {
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "../common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/EduManage/ClassHandler.ashx",
                    Func: "GetData",
                    PageIndex: startIndex,
                    pageSize: pageSize,
                    AcademicId: $("#StudyTerm").val(),
                    ClassName: $("#Name").val(),
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#tb_Class").html('');
                        $("#tr_Class").tmpl(json.result.retData.PagedData).appendTo("#tb_Class");
                        if (json.result.retData.RowCount < pageSize) {
                            $("#pageBar").hide();
                        } else {
                            $("#pageBar").show();
                            makePageBar(getClassData, document.getElementById("pageBar"), json.result.retData.PageIndex, json.result.retData.PageCount, pageSize, json.result.retData.RowCount);
                        }
                        NewCheckAll($('.table_wrap input[type=checkbox]'));

                    }
                    else {
                        $("#tb_Class").html("<tr><td colspan='5'>暂无班级信息！</td></tr>");
                        $("#pageBar").hide();
                    }
                },
                error: function (errMsg) {
                    $("#tb_Class").html("<tr><td colspan='5'>暂无班级信息！</td></tr>");
                }
            });
        }
        function EditClass() {
            var ids = "";
            var i = 0;
            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids = this.value;
                    i++;
                }
            });
            if (ids == "") {
                layer.msg("请选择数据行");
            }
            else {
                if (i > 1) {
                    layer.msg("只能选择一行");
                }
                else {
                    OpenIFrameWindow('修改班级', 'NewClass.aspx?ID=' + ids + "&TermID=" + $("#StudyTerm").val() + "&btncls=icon-edit", '400px', '410px')
                }
            }
        }
        function DelClass(ID) {
            if (confirm("确定要删除吗？")) {
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
                        async: false,
                        dataType: "json",
                        data: {
                            PageName: "/EduManage/ClassHandler.ashx",
                            Func: "DelClass",
                            ID: ids,
                            SysAccountNo: SysAccountNo,
                            LoginName: "<%=UserInfo.LoginName%>"

                        },
                        success: function (json) {
                            if (json.result.errNum.toString() == "0") {
                                layer.msg("删除成功");
                                getClassData(1, 10);
                            }
                            else {
                                layer.msg(json.result.errMsg);
                            }
                        },
                        error: function (errMsg) {
                            layer.msg(errMsg);
                        }
                    });
                }
            }
        }
    </script>
</body>
</html>
