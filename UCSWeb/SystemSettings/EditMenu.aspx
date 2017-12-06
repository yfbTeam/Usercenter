<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditMenu.aspx.cs" Inherits="UCSWeb.SystemSettings.EditMenu" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改菜单</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script type="text/javascript" src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
     <style>           
         .radio{line-height:30px;vertical-align:middle;font-size:14px;color:#666;}
    </style>
</head>
<body style="background: #F8FCFF;">
     <div class="p20">
        <div class="dialog_wrap">
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">菜单名称:</label>
                <div class="row_content">
                    <input type="text" class="text" id="txt_Name" value="" placeholder="请输入菜单名称"/>
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">菜单URL:</label>
                <div class="row_content">
                    <input type="text" class="text" id="txt_Url" value="" placeholder="请输入菜单URL"/>
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">菜单/按钮:</label>
                <div class="row_content">
                    <div class="radio">						        
                        <label for=""><input type="radio" name="radio_IsMenu" value="0" checked="checked" />菜单</label>                                
                        <label for=""><input type="radio" name="radio_IsMenu" value="1"/>按钮</label>
                    </div>
                </div>
            </div>
            <div id="div_BtnType" class="row_dia clearfix" style="display:none;">
                <label for="" class="row_label fl">按钮类型:</label>
                <div class="row_content">
                    <div class="radio">						        
                      <select id="sel_BtnType" class="text"></select>
                    </div>
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">菜单显示:</label>
                <div class="row_content">
                    <div class="radio">		
                        <label for=""><input type="radio" name="radio_IsShow" value="3" checked="checked"/>都显示</label>			                                                              
                        <label for=""><input type="radio" name="radio_IsShow" value="1"/>显示导航</label>
                        <label for=""><input type="radio" name="radio_IsShow" value="2"/>显示权限列表</label>
                        <label for=""><input type="radio" name="radio_IsShow" value="0"/>不显示</label>                         
                    </div>
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label">菜单描述:</label>
                <div class="row_content">
                    <textarea rows="4" class="text" id="txt_Description" placeholder="请输入菜单描述" style="height:50px;"></textarea>
                </div>
            </div>
        </div>
        <div class="btn_wrap">
            <input type="button" name="" id="" value="确定" class="btns insert" onclick="SaveItem();" />
            <input type="button" name="" id="" value="取消" class="btns cancel" onclick="javascript: parent.CloseIFrameWindow();" />
        </div>
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            var itemid = UrlDate.itemid;
            if (itemid != 0) {
                GetItemById(itemid);
            } else {
                BindBtnType('');
            }
            $("input[name='radio_IsMenu']").click(function () {
                var $div_BtnType = $("#div_BtnType");
                if (this.value == "0") {
                    $div_BtnType.hide();
                    $("input[name='radio_IsShow'][value=3]").attr("checked", true);
                } else {
                    $div_BtnType.show();
                    $("input[name='radio_IsShow'][value=2]").attr("checked", true);
                }
            });
        })
        //保存信息
        function SaveItem() {
            var name = $("#txt_Name").val().trim();
            var url = $("#txt_Url").val().trim();
            var description = $("#txt_Description").val().trim();
            var ismenu = $("input[name='radio_IsMenu']:checked").val();
            var isshow = $("input[name='radio_IsShow']:checked").val();
            if (!name.length) { layer.msg("请填写菜单名称！"); return; }
            var menucode = ismenu == 1 ? $("#sel_BtnType").val(): '';
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/MenuHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddMenuInfo" : "EditMenuInfo",
                    ItemId: UrlDate.itemid,                   
                    pid: UrlDate.pid,
                    Name: name,
                    url: url,
                    description: description,
                    ismenu: ismenu,
                    isshow: isshow,
                    MenuCode:menucode,
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg(ismenu == 1 ? "该按钮类型已存在!" : "该菜单名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '新建菜单成功!' : '修改菜单成功!');
                        parent.GetMenuInfo();
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
        //绑定数据
        function GetItemById(itemid) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/MenuHandler.ashx",
                    Func: "GetMenuById",
                    ItemId: itemid,
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        var ismenu = model.IsMenu.toString().toUpperCase() == "TRUE" ? 1 : 0;
                        $("#txt_Name").val(model.Name);
                        $("#txt_Url").val(model.Url);
                        $("#txt_Description").val(model.Description);
                        $("input[name='radio_IsMenu'][value=" + ismenu + "]").attr("checked", true);
                        $("input[name='radio_IsShow'][value=" + model.IsShow + "]").attr("checked", true);
                        BindBtnType(ismenu == 1 ? model.MenuCode : '');
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
        function BindBtnType(ekey) {
            var $sel_BtnType = $("#sel_BtnType");
            $sel_BtnType.html('');
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/ButtonTypeHandler.ashx",
                    Func: "GetButtonTypeDataPage",
                    Ispage: false,
                    SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"                    
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var rtnData = json.result.retData;
                        $(rtnData).each(function () {
                            $sel_BtnType.append("<option value='" + this.Key + "'>" + this.Value + "(" + this.Key + ")</option>");
                        });
                        if (ekey.length) {
                            $sel_BtnType.val(ekey);
                            $("#div_BtnType").show();
                        }
                        else {
                            if ($("input[name='radio_IsMenu']:checked").val()=="1") {
                                $("#txt_Name").val(rtnData[0].Value);
                            }                               
                        }
                        $sel_BtnType.change(function () {
                            var seltext=$(this).children('option:selected').text();
                            $("#txt_Name").val(seltext.replace("(" + this.value + ")", ''));
                        });
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
    </script>
</body>
</html>
