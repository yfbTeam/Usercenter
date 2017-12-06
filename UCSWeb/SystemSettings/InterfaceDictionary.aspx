<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterfaceDictionary.aspx.cs" Inherits="UCSWeb.SystemSettings.InterfaceDictionary" %>

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
            <td>
                <input type="checkbox" name="ck_trsub" value="${Id}" onclick="CheckSub(this);"/></td>
            <td>${pageIndex()}</td>
            <td>${Name}</td>
            <td>${Description}</td>
            <td>{{if IsDelete==0}}<span class="colorgreen">启用</span>{{else}}<span class="colorred">禁用</span>{{/if}}</td>
            <td>${DateTimeConvert(LastOperationTime,'yyyy-MM-dd HH:mm:ss')}</td>
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
                            <a href="javascript:;" class="active">接口字典</a>
                        </div>
                    </div>
                    <div class="toolsbar clearfix mt10">
                        <div class="tool_left fl">
                            <span btncls="icon-plus" style="display:none;" onclick="javascript: OpenIFrameWindow('新建接口','EditInterface.aspx?itemid=0&btncls=icon-plus', '400px', '240px');">新建</span>
                            <span btncls="icon-edit" style="display:none;" onclick="EditItem();">修改</span>
                            <span btncls="icon-enable" style="display:none;" onclick="EnableItem(0);">启用</span>
                            <span btncls="icon-disable" style="display:none;" onclick="EnableItem(1);">禁用</span>
                        </div>
                        <div class="tool_right fr clearfix">
                            <div class="fl mr10 pr search">
                                <input type="text" id="txt_Name" placeholder="请输入接口关键字" onblur="SearchCondition(0);"/>
                                <i class="iconfont" onclick="SearchCondition(0);">&#xe604;</i>
                            </div>
                            <%--<div class="fl mr10">
                                <label>分类:</label>
                                <select class="select">
                                    <option value="">全部</option>
                                </select>
                            </div>--%>
                            <div class="fl">
                                <label>状态:</label>
                                <select class="select" id="sel_IsDelete" onchange="SearchCondition(0);">
                                    <option value="">全部</option>
                                    <option value="0">启用</option>
                                    <option value="1">禁用</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="table_wrap mt10">
                        <table>
                            <thead>
                                <tr>
                                    <th>
                                        <input type="checkbox" name="ck_tball" id="ck_tball" onclick="CheckAll(this);"/></th>
                                    <th class="number">序号</th>   
                                    <th onclick="SearchCondition(1);" style="cursor:pointer;">接口<i id="i_interorder" class="iconfont colorgreen up" style="font-size: 8px;">&#xe62b;</i></th>
                                    <th>描述</th>
                                    <th>状态</th>
                                    <th>最后操作时间</th>
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
        var sername = $("#txt_Name").val().trim();
        var isDelete = $("#sel_IsDelete").val();
        var orderby = "";
        function SearchCondition(type) {
            sername = $("#txt_Name").val().trim();
            isDelete = $("#sel_IsDelete").val();
            if (type == 1) {
                var $interorder = $("#i_interorder");
                if ($interorder.hasClass("up")) {
                    orderby = "Name desc ";
                    $interorder.removeClass("up").addClass("down");
                } else {
                    orderby = "Name ";
                    $interorder.removeClass("down").addClass("up");
                }
            }           
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
                    PageName: "/SystemSettings/InterfaceHandler.ashx",
                    Func: "GetInterfaceDataPage",
                    Name: sername,
                    IsDelete: isDelete,
                    OrderBy:orderby,
                    PageIndex: startIndex,
                    pageSize: pageSize,
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
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
                        $("#tb_list").html("<tr><td colspan='6'>暂无接口！</td></tr>");
                        $("#pageBar").hide();
                    }
                },
                error: function (errMsg) {
                    $("#tb_list").html("<tr><td colspan='6'>暂无接口！</td></tr>");
                }
            });
        }
        function EditItem() {
            var checkedtr = $("input[type='checkbox'][name='ck_trsub']:checked");
            if (checkedtr.length == 0) { layer.msg('请选择要修改的行！'); return; }
            if (checkedtr.length > 1) { layer.msg('请选择一行！'); return; }
            var itemid = checkedtr[0].value;
            OpenIFrameWindow('修改接口', 'EditInterface.aspx?itemid=' + itemid + "&btncls=icon-edit", '400px', '240px');
        }
        function EnableItem(type) {
            var wmsg = type == 0 ? '启用' : '禁用';
            var checkedtr = $("input[type='checkbox'][name='ck_trsub']:checked");
            if (checkedtr.length == 0) { layer.msg('请选择要' + wmsg + '的行！'); return; }
            var idArray = [];
            $(checkedtr).each(function (i, n) {
                idArray.push(n.value);
            });
            $.ajax({
                url: "../common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/InterfaceHandler.ashx",
                    func: "EnableInterface",
                    IsEnable: type,
                    IDs: idArray.join(","),
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        getData(1, 18);
                        layer.msg(wmsg+'成功！')
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
