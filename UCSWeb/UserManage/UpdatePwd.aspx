<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePwd.aspx.cs" Inherits="UCSWeb.UserManage.UpdatePwd" %>

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
                <label for="" class="row_label fl">登陆账号:</label>
                <div class="row_content">
                    <input type="text" class="text" id="LoginName" runat="server" placeholder="请输入登陆账号" style="border: 0px" disabled="disabled" />
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">原密码:</label>
                <div class="row_content">
                    <input type="text" class="text" id="OldPwd" value="" placeholder="请输入原密码" />
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label">新密码:</label>
                <div class="row_content">
                    <input type="text" class="text" id="NewPwd" value="" placeholder="请输入新密码" />
                </div>
            </div>
        </div>
        <div class="btn_wrap">
            <input type="button" name="" id="" value="确定" class="btns insert" onclick="UpdatePwd()" />
            <input type="button" name="" id="" value="取消" class="btns cancel" onclick="Close()" />
        </div>
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();

        $(function () {
            $("#LoginName").val(UrlDate.LoginName);
        })
        function Close() {
            parent.CloseIFrameWindow();
        }

        function UpdatePwd() {
            var OldPwd = $("#OldPwd").val();
            var NewPwd = $("#NewPwd").val();
            if (!OldPwd.length || !NewPwd.length) {
                layer.msg("新密码和旧密码不能为空");
            } else {
                if (OldPwd.trim() == NewPwd.trim()) {
                    layer.msg("新密码和旧密码不能相同");
                }
                else {
                    $.ajax({
                        url: "../common.ashx",
                        type: "post",
                        dataType: "json",
                        data: {
                            PageName: "/UserManage/UserInfo.ashx",
                            func: "UpdatePwd",
                            SysAccountNo: SysAccountNo,
                            LoginName: "<%=UserInfo.LoginName%>",
                            UserName:$("#LoginName").val(),
                            OldPwd: $("#OldPwd").val(),
                            NewPwd: $("#NewPwd").val()
                        },
                        success: function (json) {
                            if (json.result.errNum == 0) {
                                parent.layerMsg("密码修改成功");
                                parent.CloseIFrameWindow();
                            } else {
                                parent.layerMsg(json.result.errMsg);
                            }
                        },
                        error: function (errMsg) {
                            parent.layerMsg(errMsg);
                        }
                    });
                }
            }

        }
    </script>
</body>
</html>
