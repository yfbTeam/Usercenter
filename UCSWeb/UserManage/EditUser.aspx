<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="UCSWeb.UserManage.EditUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <%-- <link href="../css/repository.css" rel="stylesheet" />
    <link href="../css/onlinetest.css" rel="stylesheet" />--%>
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <link href="../Scripts/layer/skin/layer.css" rel="stylesheet" />
    <script src="../Scripts/layer/layer.js"></script>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script src="../Scripts/Uploadyfy/uploadify/jquery.uploadify-3.1.min.js"></script>
    <link href="/Scripts/Uploadyfy/uploadify/uploadify.css" rel="stylesheet" />
    <script src="../Scripts/Validform_v5.3.1.js?v=1.05"></script>
    <style>
        .course_form_img {
            width: 270px;
            height: 114px;
            border: 1px solid #C7DFF7;
            overflow: hidden;
            position: relative;
            top: 10px;
        }

            .course_form_img img {
                width: 100%;
            }

            .course_form_img .uploadify-button {
                font-size: 14px;
                color: #fff;
                border: none;
                background: #19c857;
            }

        .change_picture {
            position: absolute;
            right: 0;
            top: 0;
        }

        .radio {
            line-height: 30px;
            vertical-align: middle;
            font-size: 14px;
            color: #666;
        }
    </style>
</head>
<body style="background: #F8FCFF;">
    <form id="registerform" name="registerform" class="registerform" runat="server">
        <input type="hidden" id="CreateUID" />
        <input type="hidden" id="RegisterOrg" value="1000" />
        <div class="p20">
            <div class="dialog_wrap">
                <div class="fl" style="height: 126px;">
                    <div class="row_dia clearfix">
                        <label for="" class="row_label fl">姓名:</label>
                        <div class="row_content">
                            <input type="text" placeholder="姓名" class="text" id="Name" />
                        </div>
                    </div>
                    <div class="row_dia clearfix">
                        <label for="" class="row_label fl">昵称:</label>
                        <div class="row_content">
                            <input type="text" placeholder="昵称" class="text" id="Nickname" />
                        </div>
                    </div>
                    <div class="row_dia clearfix">
                        <label for="" class="row_label fl">用户账号:</label>
                        <div class="row_content">
                            <input type="text" placeholder="用户账号" class="text" id="LoginName" />
                        </div>
                    </div>
                </div>
                <div class="fr">
                    <div class="course_form_img">
                        <img id="img_Pic" alt="" src="" />
                        <div class="change_picture">
                            <input type="file" id="uploadify" name="uploadify" />
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
                <div class="clearfix" style="height: 42px; overflow: hidden;">
                    
                    <div class="row_dia clearfix fl">
                        <label for="" class="row_label fl">出生日期:</label>
                        <div class="row_content">
                            <input type="text" placeholder="出生日期" class="text" id="Birthday" onclick="WdatePicker();" />
                        </div>
                    </div>
                </div>
                <div class="clearfix" style="height: 42px; overflow: hidden;">
                    <div class="row_dia clearfix fl">
                        <label for="" class="row_label fl">联系电话:</label>
                        <div class="row_content">
                            <input type="text" placeholder="联系电话" class="text" id="Phone" datatype="m" errormsg="手机号格式错误"//>
                        </div>
                    </div>
                    <div class="row_dia clearfix fr">
                        <label for="" class="row_label fl">认证类型:</label>
                        <div class="row_content">
                            <select id="AuthenType" class="text">
                                <option value="0">新用户注册</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="clearfix" style="height: 42px; overflow: hidden;">
                    <div class="row_dia clearfix fl">
                        <label for="" class="row_label fl">用户性别:</label>
                        <div class="row_content">
                            <div class="radio">
                                <input type="radio" name="Sex" id="" value="1" />
                                <label for="">男</label>
                                <input type="radio" name="Sex" id="" value="0" checked="checked" />
                                <label for="">女</label>
                            </div>
                        </div>
                    </div>
                    <div class="row_dia clearfix fr">
                        <label for="" class="row_label fl">用户类型:</label>
                        <div class="row_content">
                            <select id="UserType" class="text">
                                <option value="0">==请选择==</option>
                                <option value="1">教师</option>
                                <option value="2">学生</option>
                                <option value="3">普通用户</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="row_dia clearfix fl">
                    <label for="" class="row_label fl">身份证号:</label>
                    <div class="row_content">
                        <input type="text" placeholder="身份证号" class="text" id="IDCard" datatype="IDCard" />
                    </div>
                </div>
                 <div class="row_dia clearfix fr">
                    <label for="" class="row_label fl">邮箱地址:</label>
                    <div class="row_content">
                        <input type="text" placeholder="邮箱地址" class="text" id="Email" datatype="e" />
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">用户住址:</label>
                    <div class="row_content">
                        <input type="text" placeholder="用户住址" class="text" class="text" id="Address" style="width: 490px;" />
                    </div>
                </div>
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">用户备注:</label>
                    <div class="row_content">
                        <input type="text" placeholder="备注" class="text" id="Remarks" style="width: 490px;" />
                    </div>
                </div>
            </div>
            <div class="btn_wrap">
                <input type="button" name="" id="btnCreate" value="确定" class="btns insert" />
            </div>
        </div>
    </form>
    <%--头像上传--%>
    <script type="text/javascript">
        $(function () {
            $("#uploadify").uploadify({
                'auto': true,                      //是否自动上传
                'swf': '../Scripts/Uploadyfy/uploadify/uploadify.swf',
                'uploader': 'Uploade.ashx',
                'formData': { Func: "UplodPhoto" }, //参数
                'fileTypeExts': '*.jpg;*.png;*.jpeg',
                'buttonText': '选择头像',//按钮文字
                // 'cancelimg': 'uploadify/uploadify-cancel.png',
                'width': 90,
                'height': 24,
                //最大文件数量'uploadLimit':
                'multi': false,//单选            
                'fileSizeLimit': '10MB',//最大文档限制
                'queueSizeLimit': 1,  //队列限制
                'removeCompleted': true, //上传完成自动清空
                'removeTimeout': 0, //清空时间间隔
                //'overrideEvents': ['onDialogClose', 'onUploadSuccess', 'onUploadError', 'onSelectError'],
                'onUploadSuccess': function (file, data, response) {
                    var json = $.parseJSON(data);
                    $("#img_Pic").attr("src", json.url);
                },

            });

            var valiForm = $(".registerform").Validform({
                btnSubmit: "#btnCreate",
                tiptype: 3,
                ajaxPost: true,
                showAllError: true,
                beforeSubmit: function (curform) {
                    //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                    //这里明确return false的话表单将不会提交;	
                    EditUser();
                }
            })
        });
    </script>
    <%--数据操作--%>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            var ID = UrlDate.ID;
            var Type = UrlDate.Type;
            var IsStu = UrlDate.IsStu;
            if (Type == "2") {
                $("#btnCreate").hide();
            }
            else {
                $("#btnCreate").show();
            }

            if (IsStu == "true") {
                $("#UserType").html('<option value="0">==请选择==</option><option value="2">学生</option>');
                $("#UserType").val("2");
            }
            else {
                $("#UserType").html('<option value="0">==请选择==</option><option value="1">老师</option><option value="3">普通用户</option>');
            }
            if (ID == undefined) {
            }
            else {
                GetUserByID(ID)
            }
        })
        function GetUserByID(ID) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    Func: "GetData",
                    ID: ID,
                    Ispage: "false",
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    IsStu: UrlDate.IsStu
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#UserType").val(this.UserType);
                            $("#Name").val(this.Name);
                            $("#Nickname").val(this.Nickname);
                            $("input[name='Sex'][value=" + this.Sex + "]").attr("checked", true);
                            $("#Birthday").val(this.Birthday);
                            $("#Phone").val(this.Phone);
                            $("#LoginName").val(this.LoginName);
                            $("#IDCard").val(this.IDCard);
                            $("#img_Pic").attr("src", this.HeadPic);
                            $("#AuthenType").val(this.AuthenType);
                            $("#Address").val(this.Address);
                            $("#Remarks").val(this.Remarks);
                            $("#RegisterOrg").val(this.RegisterOrg);
                            $("#Email").val(this.Email);

                        })
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

        function EditUser() {

            var UserType = $("#UserType").val();
            var Name = $("#Name").val();
            var Nickname = $("#Nickname").val();
            var Birthday = $("#Birthday").val();
            var Phone = $("#Phone").val();
            var LoginName = $("#LoginName").val();
            var IDCard = $("#IDCard").val();
            var HeadPic = $("#img_Pic").attr("src");
            var RegisterOrg = $("#RegisterOrg").val();
            if (UrlDate.RegisterOrg != "" && UrlDate.RegisterOrg != undefined) {
                RegisterOrg = UrlDate.RegisterOrg
            }
            var AuthenType = $("#AuthenType").val();
            var Address = $("#Address").val();
            var Remarks = $("#Remarks").val();
            var CreateUID = $("#CreateUID").val();
            var Sex = $("input[name='Sex']:checked").val();
            var ID = "";
            var Email = $("#Email").val();
            var funcName = "AddUser";
            if (UrlDate.ID != undefined) {
                ID = UrlDate.ID;
                funcName = "EditUser"
            }
            if (!Birthday.length) {
                $("#Birthday").focus();
                layer.msg("请填写完用出生日期！");
            }
            else if (!LoginName.length) {
                $("#LoginName").focus();
                layer.msg("请填写完用户账号！");
            }
            else if (UserType == "0") {
                $("#UserType").focus();
                layer.msg("请选择用户类型");
            }
            else {
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        "PageName": "/UserManage/UserInfo.ashx",
                        func: funcName, UserType: UserType, Name: Name, Nickname: Nickname
                        , Sex: Sex, Birthday: Birthday, Phone: Phone, LoginName: LoginName, IDCard: IDCard, ID: ID
                        , HeadPic: HeadPic, RegisterOrg: RegisterOrg, AuthenType: AuthenType, Address: Address, Remarks: Remarks, CreateUID: CreateUID, RegisterOrg: RegisterOrg, Email: Email
                    },
                    success: function (json) {
                        var result = json.result;
                        if (result.errNum == 0) {
                            parent.layer.msg('操作成功!');
                            parent.getData(1, 10);
                            parent.CloseIFrameWindow();
                        }
                        else {
                            layer.msg(result.errMsg);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        layer.msg("操作失败！");
                    }
                });
            }
        }
    </script>
</body>
</html>
