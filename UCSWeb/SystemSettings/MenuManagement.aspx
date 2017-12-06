<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuManagement.aspx.cs" Inherits="UCSWeb.SystemSettings.MenuManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>菜单管理</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <link href="../Scripts/jquery-treetable-3.1.0-0/stylesheets/jquery.treetable.css" rel="stylesheet" />
    <link href="../Scripts/jquery-treetable-3.1.0-0/stylesheets/jquery.treetable.theme.default.css" rel="stylesheet" />
    <script src="../Scripts/jquery-treetable-3.1.0-0/vendor/jquery-ui.js"></script>
    <script src="../Scripts/jquery-treetable-3.1.0-0/javascripts/src/jquery.treetable.js"></script>
    <!--[if IE]>
		<script src="../Scripts/html5.js"></script>
	<![endif]-->
    <style>
        #table_tree thead tr th {
            height: 40px;
            background: #eef5fd;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #d0ebff;
            font-size: 14px;
            color: #777;
        }

        table.treetable tbody tr td {
            height: 32px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #d0ebff;
            font-size: 14px;
            color: #888;
            position: relative;
        }

            table.treetable tbody tr td:first-child {
                text-align: left;
                padding-left: 70px;
            }

            table.treetable tbody tr td .setings {
                position: absolute;
                top: -18px;
                right: -18px;
                transform: rotate(225deg);
                border: 18px solid #91c954;
                width: 0px;
                height: 0px;
                border-color: #91c954 transparent transparent transparent;
                z-index: 9999;
                cursor: pointer;
            }

                table.treetable tbody tr td .setings .iconfont {
                    color: #fff;
                    font-size: 13px;
                    position: absolute;
                    bottom: 5px;
                    left: -7px;
                }

            table.treetable tbody tr td .setting_none {
                display: none;
                z-index: 999;
                position: absolute;
                right: -80px;
                top: 0;
                width: 78px;
                border: 1px solid #DEEFCB;
            }

                table.treetable tbody tr td .setting_none a {
                    display: block;
                    text-align: center;
                    width: 100%;
                    height: 29px;
                    border-bottom: 1px solid #DEEFCB;
                    background: #fff;
                    line-height: 29px;
                    color: #777777;
                    font-size: 14px;
                }

                    table.treetable tbody tr td .setting_none a:hover {
                        background: #91c954;
                        color: #fff;
                    }
    </style>
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
                            <a href="javascript:;" class="active">菜单管理</a>
                        </div>
                    </div>
                    <div class="mt10" id="div_table"></div>
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
            GetMenuInfo();
        });
        var menu_list = [];
        function GetMenuInfo() {
            $("#div_table").html('<table id="table_tree"><thead><tr><th style="width: 45%">导航名称</th><th style="width: 55%;">URL</th></tr></thead><tbody id="tb_list"><tr data-tt-id="0" data-tt-parent-id="null"><td>全部<div btncls="icon-setting" style="display:none;" class="setings" onclick="AddMenu(0,0);"><i class="iconfont" style="transform:rotate(-45deg)">&#xe606;</i></div></td><td></td></tr></tbody></table>');
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/MenuHandler.ashx",
                    Func: "GetMenuData",
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var html = '';
                        menu_list = json.result.retData;
                        BindMenu(0);
                        $("#table_tree").treetable({ expandable: true });
                        SetPageButton("<%=UserInfo.UniqueNo%>", "<%=UserInfo.LoginName%>", 'div')
                        $('table.treetable tbody tr').find('.setings').click(function () {
                            if ($(this).next().is(':hidden')) {
                                $('table.treetable tbody tr').find('.setting_none').hide();
                                $(this).next().show();
                                $(this).next().mouseleave(function () {
                                    $(this).hide();
                                });
                            } else {
                                $(this).next().hide();
                            }
                        })
                    }
                    else {
                        $("#tb_list").html("<tr><td colspan='2' style='text-align:center;'>暂无菜单！</td></tr>");
                        $("#table_tree").treetable({ expandable: true });
                    }
                },
                error: function (errMsg) {
                    $("#tb_list").html("<tr><td colspan='2' style='text-align:center;'>暂无菜单！</td></tr>");
                    $("#table_tree").treetable({ expandable: true });
                }
            });
        }
        function BindMenu(parentid) {
            for (var menu in menu_list) {
                var curmenu = menu_list[menu];
                if (curmenu.Pid == parentid) {
                    var mtr = '<tr data-tt-id=' + curmenu.Id + ' data-tt-parent-id=' + (curmenu.Pid == 0 ? null : curmenu.Pid) + '><td>' + curmenu.Name + '<div btncls="icon-setting" style="display:none;" class="setings"><i class="iconfont">&#xe6bd;</i></div><div class="setting_none">' + (curmenu.IsMenu.toString().toUpperCase() == "TRUE" ? '' : '<a href="javascript:;" onclick="AddMenu(0,' + curmenu.Id + ');">添加</a>') + '<a href="javascript:;" onclick="EditMenu(' + curmenu.Id + ',' + curmenu.Pid + ');">编辑</a><a href="javascript:;" onclick="DeleteMenu(' + curmenu.Id + ');">删除</a></div></td><td>' + curmenu.Url + '</td></tr>';
                    $('#tb_list').append(mtr);
                    if (curmenu.ChildCount > 0) {
                        BindMenu(curmenu.Id);
                    }
                }
            }
        }
        function AddMenu(itemid, pid) {
            OpenIFrameWindow('新建菜单', 'EditMenu.aspx?itemid=' + itemid + "&pid=" + pid, '480px', '430px');
        }
        function EditMenu(itemid, pid) {
            OpenIFrameWindow('修改菜单', 'EditMenu.aspx?itemid=' + itemid + "&pid=" + pid, '480px', '430px');
        }
        function DeleteMenu(menuid) {
            layer.confirm('确定要删除该菜单吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/SystemSettings/MenuHandler.ashx",
                        func: "DeleteMenuInfo",
                        ItemId: menuid,
	                    SysAccountNo: SysAccountNo,
                        LoginName:"<%=UserInfo.LoginName%>"
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            GetMenuInfo();
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
            }, function () { });
        }
    </script>
</body>
</html>
