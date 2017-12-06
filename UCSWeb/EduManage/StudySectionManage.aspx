<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudySectionManage.aspx.cs" Inherits="UCSWeb.EduManage.StudySectionManage" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>学期设置</title>
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
    <script id="tr_Section" type="text/x-jquery-tmpl">
        <tr>
            <td width="5%">
                <input type="checkbox" name="Check_box" id="" value="${Id}" />
            </td>
            <td>${Academic}</td>
            <td>${PeriodName}</td>
            <td>{{if IsDelete==1}}
                <span class="colorred">禁用</span>
                {{else}}
                <span class="colorgreen">启用</span>
                {{/if}}</td>
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
                            <a href="javascript:;" class="active">学期设置</a>
                        </div>
                    </div>
                    <div class="toolsbar clearfix mt10">
                        <div class="tool_left fl">
                            <span btncls="icon-plus" style="display: none;" onclick="OpenIFrameWindow('新建学期','SectionEdit.aspx?btncls=icon-plus','580px','400px')">新建</span>
                            <span btncls="icon-edit" style="display: none;" onclick="EditSection()">修改</span>
                            <span btncls="icon-del" style="display: none;" onclick="DelSection()">删除</span>
                            <span btncls="icon-copy" style="display: none;" onclick="CopySection()">复制学期</span>
                        </div>
                        <div class="tool_right fr clearfix">
                            <div class="fl mr10 pr search">
                                <input type="text" name="" id="" value="" placeholder="请输入关键字" />
                                <i class="iconfont">&#xe604;</i>
                            </div>
                            <div class="fl">
                                <lable>状态:</lable>
                                <select class="select" id="Status" onchange="getSectionData(1,10)">
                                    <option value="">全部</option>
                                    <option value="1">禁用</option>
                                    <option value="0">启用</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="table_wrap mt10">
                        <table>
                            <thead>
                                <tr>
                                    <th width="5%">
                                        <input type="checkbox" name="" id="" value="" /></th>
                                    <th>学期名称</th>
                                    <th>启用学段</th>
                                    <th>状态</th>
                                </tr>
                            </thead>
                            <tbody id="tb_Section"></tbody>
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

            getSectionData(1, 10);
        });
        function getSectionData(startIndex, pageSize) {
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/EduManage/StudySection.ashx",
                    Func: "GetData",
                    PageIndex: startIndex,
                    pageSize: pageSize,
                    Status: $("#Status").val(),
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#tb_Section").html('');
                        $("#tr_Section").tmpl(json.result.retData.PagedData).appendTo("#tb_Section");
                        if (json.result.retData.RowCount < pageSize) {
                            $("#pageBar").hide();
                        } else {
                            $("#pageBar").show();
                            makePageBar(getSectionData, document.getElementById("pageBar"), json.result.retData.PageIndex, json.result.retData.PageCount, pageSize, json.result.retData.RowCount);
                        }

                    }
                    else {
                        $("#tb_Section").html("<tr><td colspan='5'>暂无学期信息！</td></tr>");
                        $("#pageBar").hide();
                    }
                    NewCheckAll($('.table_wrap input[type=checkbox]'));

                },
                error: function (errMsg) {
                    $("#tb_Section").html("<tr><td colspan='5'>暂无学期信息！</td></tr>");
                }

            });
        }
        function CopySection() {
            var ids = "";
            var i = 0;
            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids = this.value;
                    i++;
                }
            });
            if (ids == "") {
                layer.msg("请选择要复制的学期");
            }
            else {
                if (i > 1) {
                    layer.msg("只能选择一行");
                }
                else {
                    OpenIFrameWindow('复制学期', 'CopyTerm.aspx?ID=' + ids, '580px', '340px')
                }
            }
        }
        //删除学期
        function DelSection() {
            if (confirm("确定要删除吗？")) {
                var ids = "";

                $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                    if (this.checked) {
                        ids += this.value + ",";
                    }
                });
                if (ids == "") {
                    layer.msg("请选择数据行");
                }
                else {
                    $.ajax({
                        url: "../common.ashx",
                        type: "post",
                        async: false,
                        dataType: "json",
                        data: {
                            PageName: "/EduManage/StudySection.ashx",
                            Func: "DelSection",
                            ID: ids,
                            SysAccountNo: SysAccountNo,
                            LoginName: "<%=UserInfo.LoginName%>"
                        },
                        success: function (json) {
                            if (json.result.errNum.toString() == "0") {
                                layer.msg("删除成功");
                                getSectionData(1, 10);
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

        function EditSection() {
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
                    OpenIFrameWindow('修改学期', 'SectionEdit.aspx?ID=' + ids + "&btncls=icon-edit", '580px', '400px')
                }
            }
        }
    </script>
</body>
</html>
