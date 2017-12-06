<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInterface.aspx.cs" Inherits="UCSWeb.SystemSettings.EditInterface" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改接口</title>
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
</head>
<body style="background: #F8FCFF;">
    <input type="hidden" id="HLoginUID" runat="server"/>
    <div class="p20">
        <div class="dialog_wrap">
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">名称:</label>
                <div class="row_content">
                    <input type="text" class="text" id="txt_Name" value="" placeholder="请输入接口名称"/>
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">描述:</label>
                <div class="row_content">
                    <textarea rows="3" class="text" cols="" id="txt_Description" placeholder="请输入接口描述"></textarea>
                </div>
            </div>            
        </div>
        <div class="btn_wrap">
            <input type="button" name="" id="" value="确定" class="btns insert" onclick="SaveItem();" />
            <input type="button" name="" id="" value="取消" class="btns cancel" onclick="javascript: parent.CloseIFrameWindow();"/>
        </div>
    </div>
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
            var description = $("#txt_Description").val().trim();
            if (!name.length) {layer.msg("请填写接口名称！");return;}
            if (!description.length) {layer.msg("请填写接口描述！");return;}
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/InterfaceHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddInterface" : "EditInterface",
                    ItemId: UrlDate.itemid,
                    Name: name,
                    Description: description,
                    LoginUID: $("#HLoginUID").val(),
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该接口名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '新建接口成功!' : '修改接口成功!');
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
        //绑定数据
        function GetItemById(itemid) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/InterfaceHandler.ashx",
                    Func: "GetInterfaceById",
                    ItemId: itemid,
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#txt_Name").val(model.Name);
                        $("#txt_Description").val(model.Description);
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
