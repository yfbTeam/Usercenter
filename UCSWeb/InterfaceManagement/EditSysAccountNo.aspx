<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSysAccountNo.aspx.cs" Inherits="UCSWeb.InterfaceManagement.EditSysAccountNo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="p20">
            <div class="dialog_wrap">
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">系统名称:</label>
                    <div class="row_content">
                        <input type="text" id="txt_Name" class="text" placeholder="请输入系统名称"/>
                    </div>
                </div>
                <div class="row_dia clearfix">
                    <label for="" class="row_label">账号名称:</label>
                    <div class="row_content">
                        <input type="text" id="txt_AccountNo" class="text" placeholder="请输入账号名称"/>
                    </div>
                </div>
            </div>
            <div class="btn_wrap">
                <input type="button" name="" id="" value="确定" class="btns insert" onclick="SaveItem();"/>
                <input type="button" name="" id="" value="取消" class="btns cancel" onclick="javascript: parent.CloseIFrameWindow();"/>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            var itemid = UrlDate.itemid;
            if (itemid != 0) {
                GetItemById(itemid);
            }
        })
        //保存信息
        function SaveItem() {
            var name = $("#txt_Name").val().trim();           
            var accountNo = $("#txt_AccountNo").val().trim();
            if (!name.length) {layer.msg("请填写系统名称！");return;}
            if (!accountNo.length) { layer.msg("请填写账号名称！"); return; }
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/InterfaceManagement/SysAccountNoHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddSystemInfo" : "EditSystemInfo",
                    ItemId: UrlDate.itemid,
                    Name: name,
                    AccountNo: accountNo,
                    LoginUID: '<%=LoginUID%>',
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该系统账号已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '新建系统账号成功!' : '修改系统账号成功!');
                        parent.GetSysAccontData();
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
                    PageName: "/InterfaceManagement/SysAccountNoHandler.ashx",
                    Func: "GetSystemInfoById",
                    ItemId: itemid,
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#txt_Name").val(model.Name);
                        $("#txt_AccountNo").val(model.AccountNo);
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
