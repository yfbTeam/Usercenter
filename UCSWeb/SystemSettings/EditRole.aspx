<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRole.aspx.cs" Inherits="UCSWeb.SystemSettings.EditRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title></title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
		<script src="Scripts/html5.js"></script>
	<![endif]-->
    <script src="../Scripts/layer/layer.js"></script>
    <script type="text/javascript" src="../Scripts/Common.js"></script>
    <link href="../Scripts/jquery-treetable-3.1.0-0/stylesheets/jquery.treetable.css" rel="stylesheet" />
    <link href="../Scripts/jquery-treetable-3.1.0-0/stylesheets/jquery.treetable.theme.default.css" rel="stylesheet" />
    <script src="../Scripts/jquery-treetable-3.1.0-0/vendor/jquery-ui.js"></script>
    <script src="../Scripts/jquery-treetable-3.1.0-0/javascripts/src/jquery.treetable.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>

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

            table.treetable tbody tr td:nth-child(2) {
                text-align: left;
                padding-left: 50px;
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
<body style="background: #F8FCFF;">
    <form id="form1" runat="server">
        <input type="hidden" id="HLoginUID" runat="server" />
        <div class="p20">
            <div class="dialog_wrap">
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">角色名称:</label>
                    <div class="row_content">
                        <input type="text" id="txt_Name" class="text" />
                    </div>
                </div>
                <div class="row_dia clearfix">
                    <label for="" class="row_label">角色权限:</label>
                    <div class="row_content">
                        <div class="ul_wrap">
                            <table id="table_tree">
                                <thead>
                                    <tr>
                                        <th class="w5">
                                            <input type="checkbox" name="ck_tball" id="ck_tball" onclick="CheckMenuBtnAll(this);" /></th>
                                        <th class="w45">导航名称</th>
                                        <th class="w50">权限分配</th>
                                    </tr>
                                </thead>
                                <tbody id="tb_list"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="btn_wrap">
                <input type="button" name="" id="" value="确定" class="btns insert" onclick="SaveItem();" />
                <input type="button" name="" id="" value="取消" class="btns cancel" onclick="javascript: parent.CloseIFrameWindow();" />
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            var itemid = UrlDate.itemid;
            if (itemid != 0) {
                if (UrlDate.flag != undefined && UrlDate.flag == 1) {
                    $(".btn_wrap").hide();
                }
                $("#txt_Name").val(decodeURIComponent(UrlDate.roname));
                GetMenuInfo(itemid);
            } else {
                GetMenuInfo("");
            }
        })
        //保存信息
        function SaveItem() {
            var name = $("#txt_Name").val().trim();
            if (!name.length) { layer.msg("请填写角色名称！"); return; }
            var checkedtr = $("input[type='checkbox'][name='ck_menusub']:checked");
            if (checkedtr.length == 0) { layer.msg('请勾选菜单！'); return; }
            var menuArray = [];
            $(checkedtr).each(function (i, n) {
                menuArray.push(n.value);
                var checkedbtn = $("input[type='checkbox'][name='ck_btnsub_" + n.value + "']:checked");
                $(checkedbtn).each(function (j, btn) {
                    menuArray.push(btn.value);
                });
            });
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/RoleHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddRole" : "EditRole",
                    ItemId: UrlDate.itemid,
                    Name: name,
                    Menuids: menuArray.join(","),
                    LoginUID: $("#HLoginUID").val(),
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该角色名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '新建角色成功!' : '修改角色成功!');
                        parent.getData(1, 10);
                        parent.CloseIFrameWindow();
                    } else {
                        layer.msg(result.errMsg);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("操作失败！");
                }
            });
        }
        var menu_list = [];
        function GetMenuInfo(roleid) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/MenuHandler.ashx",
                    Func: "GetMenuData",
                    RoleId: roleid,
                    IsMenu: 0,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var html = '';
                        menu_list = json.result.retData;
                        BindMenu(0);
                        $("#table_tree").treetable({ column: 1, expandable: true });
                        $('table.treetable tbody tr').find('.setings').click(function () {
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
                        if (UrlDate.flag != undefined && UrlDate.flag == 1) {
                            $("input[type='checkbox']").removeAttr("onclick");
                            $("input[type='checkbox']").click(function () {
                                return false;
                            });
                        }
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
        function BindMenu(parentid) {
            for (var menu in menu_list) {
                var curmenu = menu_list[menu];
                if (curmenu.Pid == parentid) {
                    var mtr = '<tr data-tt-id=' + curmenu.Id + ' data-tt-parent-id=' + (curmenu.Pid == 0 ? null : curmenu.Pid) + '><td class="w5"><input type="checkbox" name="ck_menusub" ' + (curmenu.ischeck == 0 ? '' : ' checked = "checked" ') + ' value="' + curmenu.Id + '" parvalue="' + curmenu.Pid + '" onclick="CheckTwoSub(this);"/></td><td>' + curmenu.Name + '</td><td>' + SplitBtnField(curmenu.ButtonField, curmenu.Id) + '</td></tr>';
                    $('#tb_list').append(mtr);
                    if (curmenu.ChildCount > 0) {
                        BindMenu(curmenu.Id);
                    }
                }
            }
        }
        function SplitBtnField(field, pid) {
            var btnstr = '';
            if (field != undefined && field.length) {
                var btnArray = field.split(',');
                for (var btn in btnArray) {
                    var curbtn = btnArray[btn].split('|');
                    btnstr += '<div class="showhide fl"><input type="checkbox" name="ck_btnsub_' + pid + '" ' + (curbtn[0] == 0 ? '' : ' checked = "checked" ') + ' value="' + curbtn[1] + '" onclick="CheckThreeSub(this);"/><label for="">' + curbtn[2] + '</label></div>';
                }
            }
            return btnstr;
        }
        //全选或全不选
        function CheckMenuBtnAll(obj, subname) {
            subname = arguments[1] || "ck_menusub";
            var flag = obj.checked;//获取全选按钮的状态 
            $("input[type=checkbox][name=" + subname + "]").each(function () {
                this.checked = flag;//选中或者取消选中 
                if (subname == "ck_menusub") {
                    $("input[type=checkbox][name=ck_btnsub_" + this.value + "]").each(function () {
                        this.checked = flag;//选中或者取消选中 
                    });
                }
            });
        }
        //二级反选
        function CheckTwoSub(obj, parname) {
            parname = arguments[1] || "ck_tball";
            var subname = obj.name;
            CheckMenuBtnAll(obj, "ck_btnsub_" + obj.value);
            TreeParCheck(obj);
            TreeChildCheck(obj);
            var flag = obj.checked;//获取当前按钮的状态 
            if (!flag) {
                $("input[type=checkbox][name=" + parname + "]")[0].checked = false;
                return;
            }
            var chsub = $("input[type='checkbox'][name='" + subname + "']").length; //获取subcheck的个数  
            var checkedsub = $("input[type='checkbox'][name='" + subname + "']:checked").length; //获取选中的subcheck的个数  
            if (checkedsub == chsub) {
                CheckReverseAll();
            }
        }
        //三级反选
        function CheckThreeSub(obj) {
            var subname = obj.name;
            var parid = subname.replace('ck_btnsub_', '');
            var flag = obj.checked;//获取当前按钮的状态 
            if (!flag) {
                $("input[type=checkbox][name=ck_tball]")[0].checked = false;
            }
            var chsub = $("input[type='checkbox'][name='" + subname + "']").length; //获取subname的个数  
            var checkedsub = $("input[type='checkbox'][name='" + subname + "']:checked").length; //获取选中的subname的个数  
            var twosub = $("input[type=checkbox][value=" + parid + "]")[0];
            if (checkedsub == 0) {
                twosub.checked = false;
            } else {
                twosub.checked = true;
                if (checkedsub == chsub) {
                    CheckTwoSub(twosub);
                }
            }
        }
        //父节点
        function TreeParCheck(obj) {
            $parentobj = $("input[type=checkbox][value=" + $(obj).attr('parvalue') + "]");
            if ($parentobj.length) {
                var checklen = $("input[type=checkbox][parvalue=" + $(obj).attr('parvalue') + "]:checked").length;
                if (checklen == 0) {
                    $parentobj[0].checked = false;
                } else {
                    $parentobj[0].checked = true;
                }
                TreeParCheck($parentobj[0]);
            }
        }
        //子节点
        function TreeChildCheck(obj) {
            $childs = $("input[type=checkbox][parvalue=" + obj.value + "]");
            if ($childs.length) {
                $childs.each(function () {
                    this.checked = obj.checked;
                    CheckMenuBtnAll(this, "ck_btnsub_" + this.value);
                    TreeChildCheck(this);
                });
            }
        }
        function CheckReverseAll() {  //反选ck_tball            
            var isckall = false;
            var cktwo = $("input[type='checkbox'][name='ck_menusub']").length;
            var ckchecktwo = $("input[type='checkbox'][name='ck_menusub']:checked").length;
            if (ckchecktwo == cktwo) {
                var ckthree = $("input[type='checkbox'][name^=ck_btnsub_]").length;
                var ckcheckthree = $("input[type='checkbox'][name^=ck_btnsub_]:checked").length;
                if (ckcheckthree == ckthree) {
                    isckall = true;
                }
            }
            $("input[type=checkbox][name=ck_tball]")[0].checked = isckall;
        }
    </script>
</body>
</html>
