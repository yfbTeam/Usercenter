<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_unity.aspx.cs" Inherits="UCSWeb.Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>登录</title>
    <link rel="stylesheet" href="css/reset.css" />
    <link rel="stylesheet" href="css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="Scripts/Validform_v5.3.1.js"></script>
    <script src="Scripts/layer/layer.js"></script>
    <script src="Scripts/md5.js"></script>
    <script src="Scripts/jquery.cookie.js"></script>
    <script src="Scripts/Common.js?V=7.0"></script>
    <!--[if IE]>
			<script src="../Scripts/html5.js"></script>
		<![endif]-->
    <style type="text/css">
        .sortable li {
            height: 44px;
            margin-bottom: 12px;
        }

            .sortable li.ui-state-default {
                background: none;
                border: none;
            }

        #Validform_msg {
            display: none;
        }

        .Validform_checktip {
            display: block;
            line-height: 25px;
            font-size: 15px;
            color: #fff;
            text-indent: 45px;
        }

        .Validform_wrong {
            color: red;
        }

        .Validform_right {
            color: #91c954;
        }
    </style>
</head>
<body>
    <input type="hidden" id="hidPreUrl" runat="server" />
    <div class="login_wraps">
        <div class="login_center clearfix">
            <div class="two_code fl">
                <img src="images/ma.jpg" alt="" />
            </div>
            <div class="login_wrap fr">
                <form id="loginform" name="loginform" runat="server">
                    <ul id="sortable1" class="sortable">
                        <li class="ui-state-default">
                            <div class="row">
                                <i class="iconfont icon-user"></i>
                                <input type="text" placeholder="请输入登录名" class="input1" id="txt_loginName" name="txt_loginName" datatype="*" nullmsg="请输入登录名！" />
                            </div>
                        </li>
                        <li class="ui-state-default">
                            <div class="row">
                                <i class="iconfont icon-password"></i>
                                <input type="password" placeholder="请输入密码" class="input1" id="txt_passWord" datatype="*" nullmsg="请输入密码" />
                            </div>
                        </li>
                        <li class="ui-state-default">
                            <div class="row">
                                <i class="iconfont icon-code"></i>
                                <input type="hidden" id="hidCode" name="hidCode" />
                                <input type="text" placeholder="请输入验证码" class="input2" name="inpCode" id="inpCode" datatype="iCode" nullmsg="请输入验证码！" errormsg="验证码输入错误！" />
                                <div class="code" id="checkCode" onclick="createCode()" style="cursor: pointer;"></div>
                            </div>
                        </li>
                    </ul>

                    <div class="row">
                        <input type="checkbox" name="" id="rem_paddword" value="" /><label for="rem_paddword">记住密码</label>
                        <a href="javascript:void(0);" class="fr forget_paddword">忘记密码？</a>
                    </div>
                    <div class="row">
                        <input type="button" name="BtnLogin" id="BtnLogin" value="登录>" class="btn" />
                    </div>
                    <div class="row">
                        <a href="/Register.aspx" class="resi">还没有账号？立即注册 ></a>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <div class="footer—wrap"></div>
    <script type="text/javascript">
        $(function () {
            $('.footer—wrap').load('CommonPage/footer.html');
            $("#sortable1").sortable({
                placeholder: "ui-state-highlight"
            });
            $("#sortable1").disableSelection();
            //var loadIndex = layer.load(1, {
            //    shade: [0.8, '#393D49'], //0.1透明度的白色背景
            //});
            createCode();
            GetSysToken();
            //加载验证码

            //回车提交事件
            $("body").keydown(function () {
                if (event.keyCode == "13") {//keyCode=13是回车键
                    $("#BtnLogin").click();
                }
            });

            var valiNewForm = $("#loginform").Validform({
                datatype: {
                    "iCode": function (gets, obj, curform, regxp) {
                        /*参数gets是获取到的表单元素值，
                          obj为当前表单元素，
                          curform为当前验证的表单，
                          regxp为内置的一些正则表达式的引用。*/
                        var reg1 = regxp["*"];

                        var hidcode = curform.find("#hidCode");
                        if (reg1.test(gets)) { if (hidcode.val().toUpperCase() == gets.toUpperCase()) { return true; } }


                        return false;
                    }
                },
                ajaxPost: true,
                btnSubmit: "#BtnLogin",
                tiptype: 3,
                showAllError: false,
                beforeSubmit: function (curform) {
                    //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                    //这里明确return false的话表单将不会提交;	
                    Login();
                }
            })
        });
        function GetSysToken() {
            //var userInfo = "{\"Id\":7,\"UniqueNo\":\"啊发生\",\"UserType\":3,\"Name\":\"唐\",\"Nickname\":\"唐\",\"Sex\":1,\"Phone\":\"\",\"Birthday\":\"2016-09-29\",\"LoginName\":\"tang\",\"IDCard\":\"140481199805263255\",\"HeadPic\":\"\",\"RegisterOrg\":\"1001\",\"AuthenType\":0,\"Address\":\"\",\"Remarks\":\"\",\"CreateUID\":\"\",\"CreateTime\":\"2016-09-29 11:12:47\",\"EditUID\":null,\"EditTime\":null,\"IsEnable\":1,\"IsDelete\":0}";
            //$.cookie('TokenID', "e90bd89c594744c0b15d916a22b8ae92", { path: '/', secure: false });
            //$.cookie('LoginCookie_Author', userInfo, { path: '/', secure: false });
            if ($.cookie('TokenID') != null && $.cookie('TokenID') != "null" && $.cookie('TokenID') != "")
                GetUserInfoByToken($.cookie('TokenID'));
            else if ($.cookie('LoginCookie_Author') != null && $.cookie('LoginCookie_Author') != "null" && $.cookie('LoginCookie_Author') != "") {
                layer.closeAll('loading');
                var item = JSON.parse($.cookie('LoginCookie_Author'));
                if (item.LoginName != "") $("#txt_loginName").val(item.LoginName);
                if ($.cookie('RememberCookie_Cube') != null && $.cookie('RememberCookie_Cube') != "null" && $.cookie('RememberCookie_Cube') != "") {
                    if (item.Password != "") $("#txt_passWord").val($.cookie('RememberCookie_Cube'));
                    $("#rem_paddword").prop("checked", true);
                }
            }
            else
                layer.closeAll('loading');
        }


        var code; //在全局 定义验证码
        function createCode() {
            code = "";
            var codeLength = 4;//验证码的长度
            var checkCode = document.getElementById("checkCode");
            checkCode.innerHTML = "";
            var selectChar = new Array(1, 2, 3, 4, 5, 6, 7, 8, 9, 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');

            for (var i = 0; i < codeLength; i++) {
                var charIndex = Math.floor(Math.random() * 60);
                code += selectChar[charIndex];
            }
            if (code.length != codeLength) {
                createCode();
            }
            checkCode.innerHTML = code;
            $("#hidCode").val(code);
            //$("#inpCode").val(code);
        }

        function Login() {
            var loginName = $("#txt_loginName").val();
            var passWord = $("#txt_passWord").val();
            layer.load(1, {
                shade: [0.8, '#393D49'], //0.1透明度的白色背景
            });

            /******************************统一认证登录*************************************/

            var postData = { Func: "Login", userName: loginName, password: hex_md5(passWord), returnUrl: window.location.href };
            $.ajax({
                type: "Post",
                url: '<%=TokenPath%>',
                data: postData,
                dataType: "jsonp",
                jsonp: "jsoncallback",
                success: function (returnVal) {
                    var result = returnVal.result;
                    GetUserInfoByToken(result);

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.closeAll('loading');
                    //console.log(errorThrown);
                }
            });
           
        }
        var ismenupower = 0;
        function GetUserInfoByToken(tokenID) {

            if (tokenID != "") {
                var postData = { Func: "GetUserInfoByToken", tokenID: tokenID, returnUrl: window.location.href };
                $.ajax({
                    type: "Post",
                    url: '<%=TokenPath%>',
                    data: postData,
                    dataType: "jsonp",
                    jsonp: "jsoncallback",
                    success: function (returnVal) {
                        var flg = returnVal.result;
                        if (flg != null) {
                            if (flg.errNum == 0) {
                                if (flg.retData.IsEnable=="0") {
                                    layer.msg("用户已被禁用");
                                }
                                else if (flg.retData.IsDelete == "1") {
                                    layer.msg("用户不存在");
                                }
                                else {
                                    var item = flg.retData;
                                    $.ajax({
                                        url: "Common.ashx",
                                        type: "post",
                                        async: false,
                                        dataType: "json",
                                        data: { PageName: "/SystemSettings/MenuHandler.ashx", Func: "GetNavigationMenu", UniqueNo: item.UniqueNo, Pid: 0, SysAccountNo: SysAccountNo, LoginName: item.LoginName },
                                        success: function (json) {
                                            if (json.result.errNum.toString() == "0") {
                                                ismenupower = 1;
                                            }
                                        }
                                    });
                                    if (ismenupower == 0) {
                                        layer.closeAll('loading');
                                        layer.msg('您没有权限！');
                                        return;
                                    }
                                    AddLoginLog(item.LoginName);
                                    $.cookie('TokenID', tokenID, { path: '/', secure: false });
                                    if (item.CreateTime != null) item.CreateTime = DateTimeConvert(item.CreateTime);
                                    if (item.EditTime != null) item.EditTime = DateTimeConvert(item.EditTime);
                                    if (item.Birthday != null) item.Birthday = DateTimeConvert(item.Birthday, true);
                                    $.cookie('LoginCookie_Author', JSON.stringify(item), { path: '/', secure: false });
                                    if ($("#rem_paddword").is(":checked")) $.cookie('RememberCookie_Cube', $("#txt_passWord").val(), { path: '/', secure: false });
                                    if ($("#hidPreUrl").val() != "" && $("#hidPreUrl").val() != 'http://117.106.85.17:8080//DeskManage/Index.aspx' && ($("#hidPreUrl").val().toLocaleLowerCase().indexOf("Login_unity.aspx") == -1 || $("#hidPreUrl").val().toLocaleLowerCase().indexOf("register.aspx") == -1)) {
                                        window.location = $("#hidPreUrl").val();
                                    }
                                    else window.location = "Index.aspx";
                                }
                            } else {
                                layer.msg(flg.errMsg);
                            }
                        }
                        layer.closeAll('loading');
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        layer.closeAll('loading');
                        //console.log(errorThrown);
                    }
                });
            }
        }
        function AddLoginLog(loginname) {
            $.ajax({
                url: "Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/LogInfoHandler.ashx",
                    Func: "AddLoginLog",
                    SysAccountNo: SysAccountNo,
                    LoginName: loginname
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                    }
                }
            });
        }


        function OnSuccessLogin(json) {
            layer.closeAll('loading');
            var cookie = json.result;
            if (cookie.errNum == "0") {
                var str = cookie.retData[0];
                if (str != "" && str.length > 0) {
                    var items = JSON.parse(cookie.retData[0]);
                    if (items != null && items.data.length > 0) {
                        var item = items.data;
                        $.cookie('LoginCookie_Author', encodeURIComponent(JSON.stringify(item[0])), { path: '/', secure: false });
                        if ($("#rem_paddword").is(":checked")) $.cookie('RememberCookie_Cube', $("#txt_passWord").val(), { path: '/', secure: false });
                        if ($("#hidPreUrl").val() != "" && ($("#hidPreUrl").val().toLocaleLowerCase().indexOf("Login_unity.aspx") == -1 || $("#hidPreUrl").val().toLocaleLowerCase().indexOf("register.aspx") == -1)) window.location = $("#hidPreUrl").val();
                        else window.location = "Index.aspx";
                        return;
                    }
                }
                layer.msg("用户名或密码错误！");

            } else if (cookie.errNum == "333") {
                layer.msg("帐号已被禁用请联系管理员！");
            } else if (cookie.errNum == "444") {
                layer.msg("帐号已被删除请重新注册！");
            } else if (cookie.errNum == "999") {
                layer.msg("用户名或密码错误！");
            }
            else {
                layer.msg(json.result.errMsg + "！");
            }
        }
        function OnErrorLogin(XMLHttpRequest, textStatus, errorThrown) {
            layer.msg("登录名或密码错误！" + errorThrown);
        }


    </script>
</body>
</html>
