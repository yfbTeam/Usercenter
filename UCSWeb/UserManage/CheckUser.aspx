<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckUser.aspx.cs" Inherits="UCSWeb.UserManage.CheckUser" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改密码</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script src="../Scripts/layer/layer.js"></script>

</head>
<body style="background: #F8FCFF;">
    <div class="p20">
        <div class="dialog_wrap">
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">审核意见:</label>
                <div class="row_content">
                    <input type="text" class="text" id="CheckMsg" value="" />
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="">是否通过：</label>

                <input type="radio" name="AuthenType" id="" value="2" checked="checked" />
                <label for="">是</label>
                <input type="radio" name="AuthenType" id="" value="3" />
                <label for="">否</label>

            </div>

        </div>
        <div class="btn_wrap">
            <input type="button" name="" id="" value="确定" class="btns insert" onclick="SaveCheck()" />
            <input type="button" name="" id="" value="取消" class="btns cancel" onclick="parent.CloseIFrameWindow()" />
        </div>
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate()
        function SaveCheck() {
            var AuthenType = $("input[name='AuthenType']:checked").val();

            $.ajax({
                url: "../common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    func: "CheckUser",
                    ID: UrlDate.ID,
                    AuthenType: AuthenType,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    CheckMsg: $("#CheckMsg").val()
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        parent.layer.msg('操作完成!');
                        parent.getData();
                        parent.CloseIFrameWindow();
                    } else {
                        layerMsg(json.result.errMsg);
                    }
                },
                error: function (errMsg) {
                    layerMsg(errMsg);
                }
            });
        }
    </script>
</body>
</html>
