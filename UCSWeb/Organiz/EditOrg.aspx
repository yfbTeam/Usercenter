<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditOrg.aspx.cs" Inherits="UCSWeb.Organiz.EditOrg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />

    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <link href="/Scripts/layer/skin/layer.css" rel="stylesheet" />
    <link href="/Scripts/zTree/css/Common.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script src="../Scripts/menu_top.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.core-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.excheck-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.exedit-3.5.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script src="../Scripts/KindUeditor/kindeditor.js"></script>
    <script src="../Scripts/KindUeditor/plugins/code/prettify.js"></script>
    <script src="../Scripts/KindUeditor/lang/zh_CN.js"></script>
    <script src="../Scripts/Uploadyfy/uploadify/jquery.uploadify-3.1.min.js"></script>
    <link href="/Scripts/Uploadyfy/uploadify/uploadify.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery.tmpl.js"></script>
    <script type="text/javascript" src="../Scripts/PageBar.js"></script>
</head>
<body style="background: #F8FCFF;">
    <form id="registerform" name="registerform" class="registerform" runat="server">
        <input type="hidden" id="CreateUID" />
        <input type="hidden" id="RegisterOrg" value="1000" />
        <div class="p20">
            <div class="dialog_wrap">
                <div class="clearfix" style="height: 42px; overflow: hidden;">
                    <div class="row_dia clearfix fl">
                        <label for="" class="row_label fl">部门负责人:</label>
                        <div class="row_content">
                            <input type="text" placeholder="机构法人" class="text" id="LegalUID1" />
                        </div>
                    </div>
                    <div class="row_dia clearfix fr">
                        <label for="" class="row_label fl">部门编号:</label>
                        <div class="row_content">
                            <input type="text" placeholder="组织机构号" class="text" id="OrganNo" />
                        </div>
                    </div>
                </div>
                <div class="clearfix" style="height: 42px; overflow: hidden;">
                    <div class="row_dia clearfix fl">
                        <label for="" class="row_label fl">成立时间:</label>
                        <div class="row_content">
                            <input type="text" placeholder="成立时间" class="text Wdate" id="EstabLish1" onclick="javascript: WdatePicker({ dateFmt: 'yyyy-MM-dd' });" />
                        </div>
                    </div>
                    <div class="row_dia clearfix fr">
                        <label for="" class="row_label fl">部门人数:</label>
                        <div class="row_content">
                            <input type="text" placeholder="公司人数" class="text" id="UserCount1" />
                        </div>
                    </div>
                </div>
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">部门简介:</label>
                    <div class="row_content">
                        <textarea id="editor_id" name="content" style="width: 100%; height: 180px;"></textarea>
                    </div>
                </div>
            </div>
            <div class="btn_wrap">
                <input type="button" name="" id="" value="确定" class="btns insert" onclick="EditOrgDetail()" />
                <input type="button" name="" id="" value="取消" class="btns cancel" onclick="Reset()" />
            </div>
        </div>
    </form>

    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var Introduceeditor;
        $(function () {
            //富文本编辑器
            KindEditor.ready(function (K) {
                Introduceeditor = K.create('#editor_id', {
                    uploadJson: 'UploadImage.ashx?action=UploadImgForAdvertContent',
                    allowFileManager: false,//true时显示浏览服务器图片功能。
                    allowImageRemote: false,//网络图片
                    resizeType: 0,
                    items: [
                    'source', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', "strikethrough",
                'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                'insertunorderedlist', '|', 'undo', 'redo', '|', 'emoticons', 'image', 'link'],
                    afterFocus: function () {
                        self.edit = edit = this; var strIndex = self.edit.html().indexOf("请添加你的描述..."); if (strIndex != -1) { self.edit.html(self.edit.html().replace("请添加你的描述...", "")); }
                    },
                    //失去焦点事件
                    afterBlur: function () { this.sync(); self.edit = edit = this; if (self.edit.isEmpty()) { self.edit.html("请添加你的描述..."); } },
                    afterUpload: function (data) {
                        if (data.result) {
                            //data.url 处理
                        } else {

                        }
                    },
                    afterError: function (str) {
                        //alert('error: ' + str);
                    },

                });
                GetOrgByID(UrlDate.ID);
            });
        })
        function Reset() {
            parent.CloseIFrameWindow();
        }
        var UrlDate = new GetUrlDate();
        function EditOrgDetail() {
            var LegalUID = $("#LegalUID1").val();
            var UserCount = $("#UserCount1").val();
            var Introduce = Introduceeditor.html();
            var EstabLish = $("#EstabLish1").val();
            var OrganNo = $("#OrganNo").val();
            if (!OrganNo.length) {
                layer.msg("组织机构号不能为空");
                $("#OrganNo").focus();
            }
            else {
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/Organiz/Organiz.ashx",
                        Func: "EditOrgDetail",
                        SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>",
                        ID: UrlDate.ID, LegalUID: LegalUID, UserCount: UserCount, Introduce: Introduce, EstabLish: EstabLish, OrganNo: OrganNo
                    },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            layer.msg("修改成功！");
                            parent.CloseIFrameWindow();
                        }
                        else {
                            layer.msg(json.result.errMsg);
                        }
                    },
                    error: function (errMsg) {
                        layer.msg(errMsg);
                    }
                })
            }
        }
        function GetOrgByID(ID) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/Organiz/Organiz.ashx",
                    Func: "GetData",
                    ID: ID,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    Ispage: false
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#LegalUID1").val(this.LegalUID);
                            $("#EstabLish1").val(DateTimeConvert(this.EstabLish, "yyyy-MM-dd"));
                            $("#OrganNo").val(this.OrganNo);
                            $("#UserCount1").val(this.UserCount);
                            if (Introduceeditor != undefined) {
                                Introduceeditor.html(this.Introduce);
                            }
                        })
                    }
                },
                error: function (errMsg) {
                }
            });
        }
    </script>
</body>
</html>
