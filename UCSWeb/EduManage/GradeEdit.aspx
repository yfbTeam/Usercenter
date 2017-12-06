<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeEdit.aspx.cs" Inherits="UCSWeb.EduManage.GradeEdit" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改年级</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>


</head>
<body style="background: #F8FCFF;">
    <div class="p20">
        <div class="dialog_wrap">
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">当前学期:</label>
                <div class="row_content">
                    <select class="select" id="StudyTerm" disabled="disabled" style="width:196px;">
                    </select>
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">年级名称:</label>
                <div class="row_content">
                    <input type="text" class="text" id="GradeName" value="" disabled="disabled" />
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label">负责人:</label>
                <div class="row_content">
                    <input type="text" class="text" id="Leader" value="" />
                </div>
            </div>
        </div>
        <div class="btn_wrap">
            <input type="button" name="" id="" value="确定" class="btns insert" onclick="UpdateGrade()" />
            <input type="button" name="" id="" value="取消" class="btns cancel" onclick="javascript: parent.CloseIFrameWindow();" />
        </div>
    </div>
    <script type="text/javascript">
        function GetTerm() {
            var option = "";

            $.ajax({
                url: "../common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/EduManage/StudySection.ashx", Func: "GetData", SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    "Ispage": "false"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            option += "<option value='" + this.Id + "'>" + this.Academic + "</option>";
                        })
                    }
                    else {
                        layer.msg(json.result.errMsg);
                    }
                    $("#StudyTerm").append(option);
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });



        }
    </script>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            GetTerm();
            GetGradeByID(UrlDate.ID);
        })

        function GetGradeByID(ID) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/EduManage/GradeHandler.ashx",
                    Func: "GetData",
                    ID: ID,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    Ispage: "false"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#GradeName").val(this.GradeName);
                            $("#Leader").val(this.Leader);
                            $("#AcademicId").val(this.AcademicId);
                        })
                    }
                },
                error: function (errMsg) {
                }
            });
        }
       

        function UpdateGrade()
        {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/EduManage/GradeHandler.ashx",
                    Func: "EditGrade",
                    ID: UrlDate.ID,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    "Leader": $("#Leader").val()
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == 0) {
                        parent.layerMsg('操作成功!');
                        parent.getGradeData(1, 10);
                        parent.CloseIFrameWindow();
                    }
                    else {
                        parent.layerMsg(result.errMsg);
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

