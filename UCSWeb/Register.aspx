<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UCSWeb.Register" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>注册</title>
    <link rel="stylesheet" href="css/reset.css" />
    <link rel="stylesheet" href="css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script type="text/javascript" src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="Scripts/Validform_v5.3.1.js"></script>
    <script src="Scripts/layer/layer.js"></script>
    <script src="Scripts/jquery.cookie.js"></script>
    <!--[if IE]>
			<script src="Scripts/html5.js"></script>
		<![endif]-->
    <style type="text/css">
        /*.sortable {
            width: 300px;
        }*/

        .sortable li {
            height: 44px;
            margin-bottom: 12px;
        }

            .sortable li.ui-state-default {
                background: none;
                border: none;
            }
            .Validform_checktip{color:#fff;}
             .Validform_wrong{color:red;}
        .Validform_right{color:#91c954;}
    </style>
</head>
<body>
    <input type="hidden" id="hidPreUrl" runat="server" />
    <div class="login_wraps">
        <div class="w1200">
            <div class="register_wrap">
                <div class="register_nav">
                    <span class="active">新用户注册</span>
                    <span>已有用户激活</span>
                </div>
                <div class="register">
                    <div class="register_none">
                        <form id="newform" name="newform" method="post">
                            <input type="hidden" id="hidGuid" />
                            <ul id="sortable1" class="sortable">
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-name"></i>
                                        <input type="text" placeholder="请输入姓名" class="input1" datatype="*" nullmsg="请输入真实姓名！" id="New_Name" />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-card"></i>
                                        <input type="text" placeholder="请输入身份证号" class="input1" datatype="*17-18" nullmsg="请输入身份证号！" id="New_IDCard" name="New_IDCard" ajaxurl="/Handler/Valida.ashx?PageName=/UserManage/UserInfo.ashx&func=CheckUserValida" />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-user"></i>
                                        <input type="text" placeholder="请输入登录名" class="input1" datatype="*2-35" nullmsg="请输入登录名！" id="New_LoginName" name="New_LoginName" ajaxurl="/Handler/Valida.ashx?PageName=/UserManage/UserInfo.ashx&func=CheckUserValida" />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-password"></i>
                                        <input type="password" placeholder="请输入密码" class="input1" datatype="*6-15" errormsg="密码范围在6~15位之间！" id="New_Password" name="New_Password" />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-confirm"></i>
                                        <input type="password" placeholder="请再次输入密码" class="input1" datatype="*" recheck="New_Password" errormsg="您两次输入的用户密码不一致！" id="New_RepPassword" />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-group"></i>
                                        <input type="text" placeholder="请输入组织代号" class="input1" datatype="*" nullmsg="请输入组织代号！" id="New_OrgCode" name="New_OrgCode" ajaxurl="/Handler/Valida.ashx?PageName=/UserManage/UserInfo.ashx&func=CheckUserValida" />
                                    </div>
                                </li>
                            </ul>

                            <div class="row">
                                <input type="submit" name="" id="btnNewReg" value="注册>" class="btn" />
                            </div>
                        </form>
                        <div class="row" style="width: 300px;">
                            <a href="/Login_unity.aspx" class="resi">&lt;已有账号,返回登录 </a>
                        </div>
                    </div>
                    <div class="register_none none">
                        <form id="haveform" name="haveform" method="post">
                            <ul id="sortable2" class="sortable">
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-name"></i>
                                        <input type="text" placeholder="请输入姓名" class="input1" datatype="*" nullmsg="请输入真实姓名！" id="Have_Name" />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-card"></i>
                                        <input type="text" placeholder="请输入身份证号" class="input1" datatype="*" nullmsg="请输入身份证号！" id="Have_IDCard" />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-user"></i>
                                        <input type="text" placeholder="请输入登录名" class="input1" datatype="*2-35" nullmsg="请输入登录名！" id="Have_LoginName" />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-password"></i>
                                        <input type="password" placeholder="请输入密码" class="input1" datatype="*6-15" errormsg="密码范围在6~15位之间！" id="Have_Password" name="Have_Password" />
                                    </div>
                                </li>
                                <li class="ui-state-default">
                                    <div class="row">
                                        <i class="iconfont icon-confirm"></i>
                                        <input type="password" placeholder="请再次输入密码" class="input1" datatype="*" recheck="Have_Password" errormsg="您两次输入的用户密码不一致！" id="Have_RepPassword" />
                                    </div>
                                </li>
                            </ul>

                            <div class="row">
                                <input type="submit" name="" id="btnHaveReg" value="激活>" class="btn" />
                            </div>
                        </form>
                         <div class="row" style="width: 300px;">
                            <a href="/Login_unity.aspx" class="resi">&lt;已有账号,返回登录 </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="footer—wrap"></div>
    <script>
        var isCheck = false;
        $(function () {
            $('.register_nav span').on('click', function () {
                var n = $(this).index();
                $(this).addClass('active').siblings().removeClass('active');
                $('.register .register_none').eq(n).show().siblings().hide();
            })
            var valiNewForm = $("#newform").Validform({
                btnSubmit: "#btnNewReg",
                tiptype: 3,
                showAllError: true,
                ajaxPost: true,
                beforeSubmit: function (curform) {
                    //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                    //这里明确return false的话表单将不会提交;	
                    //saveData();
                    saveNewData();
                }
            })
            var valiHaveForm = $("#haveform").Validform({
                btnSubmit: "#btnHaveReg",
                tiptype: 3,
                showAllError: true,
                ajaxPost: true,
                beforeSubmit: function (curform) {
                    //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                    //这里明确return false的话表单将不会提交;	
                    //saveData();
                    saveHaveData();
                }
            })

            $("#Have_Name,#Have_IDCard").on("keyup", function () {
                var idc = $("#Have_IDCard").val();
                var name = $("#Have_Name").val();
                if (idc == "" || idc.length == 0 || name == "" || name.length == 0) {
                    return;
                }
                checkUserInfo(idc, name);
            })
        })

        function saveHaveData() {
            if (isCheck) {
                $.ajax({
                    url: "Common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/UserManage/UserInfo.ashx",
                        func: "UpdateUserByUniqueNo",
                        UniqueNo: $("#hidGuid").val(),
                        LoginName: $("#Have_LoginName").val(),
                        Password: $("#Have_Password").val(),
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            var str = json.result.retData;
                            if (str != null) {
                                var items = JSON.parse(str);
                                if (items != null && items.data != null && items.data.length > 0) {
                                    var item = items.data[0];
                                    $.cookie('LoginCookie_Author', JSON.stringify(item), { expires: 7, path: '/', secure: false });
                                    if ($("#hidPreUrl").val() != "" && ($("#hidPreUrl").val().toLocaleLowerCase().indexOf("Login_unity.aspx") < -1 || $("#hidPreUrl").val().toLocaleLowerCase().indexOf("register.aspx") < -1)) window.location = $("#hidPreUrl").val();
                                    else window.location = "/Index.aspx";
                                    return true;
                                }
                            }
                            layer.msg("注册失败！");
                            return false;
                        } else {
                            layer.msg("系统异常！请联系管理员。");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {

                    }
                });
            } else {
                layer.msg("系统未找到当前用户信息！");
            }
        }

        function saveNewData() {
            $.ajax({
                url: "Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    func: "Register",
                    Name: $("#New_Name").val(),
                    IDCard: $("#New_IDCard").val(),
                    LoginName: $("#New_LoginName").val(),
                    Password: $("#New_Password").val(),
                    OrgCode: $("#New_OrgCode").val()
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        layer.msg("注册成功！");
                        layer.load(1, {
                            shade: [0.7, '#393D49'], //0.1透明度的白色背景
                        });
                        loginUser($("#New_LoginName").val(), $("#New_Password").val());
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            });
        }

        function loginUser(loginName, passWord) {
            $.ajax({
                url: "Common.ashx",
                async: false,
                type: "Post",
                dataType: "json",
                data: { "PageName": "/UserManage/UserInfo.ashx", "Func": "Login", "loginName": loginName, "passWord": passWord },
                success: OnSuccessLogin,
                error: OnErrorLogin

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
                        $.cookie('LoginCookie_Author', JSON.stringify(item[0]), { expires: 7, path: '/', secure: false });
                        if ($("#hidPreUrl").val() != "" && ($("#hidPreUrl").val().toLocaleLowerCase().indexOf("Login_unity.aspx") < -1 || $("#hidPreUrl").val().toLocaleLowerCase().indexOf("register.aspx") < -1)) window.location = $("#hidPreUrl").val();
                        else window.location = "/Index.aspx";
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


        function checkUserInfo(idc, name) {
            $.ajax({
                url: "Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    func: "ValidUserByIDCardAndName",
                    IDCard: idc,
                    Name: name
                },
                success: function (json) {
                    $("#hidGuid").val("");
                    isCheck = false;
                    if (json.result.errNum == 0) {
                        var str = json.result.retData;
                        if (str != null) {
                            var items = JSON.parse(str);
                            if (items != null && items.data != null && items.data.length > 0) {
                                var item = items.data[0];
                                $("#Have_LoginName").val(item.LoginName);
                                //$("#Have_Password").val(item.Password);
                                //$("#Have_RepPassword").val(item.Password);
                                isCheck = true;
                                $("#hidGuid").val(item.UniqueNo);
                            }
                        }
                    } else if (json.result.errNum == 333) {
                        layer.msg("该用户已激活！");
                        $("#Have_LoginName").val("");
                        $("#Have_Password").val("");
                        $("#Have_RepPassword").val("");
                    } else {
                        $("#Have_LoginName").val("");
                        $("#Have_Password").val("");
                        $("#Have_RepPassword").val("");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            });
        }

    </script>
    <script type="text/javascript">
        $(function () {
            $('.footer—wrap').load('CommonPage/footer.html');
            $("#sortable1").sortable({
                placeholder: "ui-state-highlight"
            });
            $("#sortable1").disableSelection();
            $("#sortable2").sortable({
                placeholder: "ui-state-highlight"
            });
            $("#sortable2").disableSelection();
        });


    </script>
</body>
</html>
