<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewClass.aspx.cs" Inherits="UCSWeb.EduManage.NewClass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建班级</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link href="../css/choosen/chosen.css" rel="stylesheet" />
    <link href="../css/choosen/prism.css" rel="stylesheet" />
    <link href="../css/choosen/style.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="EduManage.js"></script>
    <script src="../Scripts/choosen/chosen.jquery.js"></script>
    <script src="../Scripts/choosen/prism.js"></script>

    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <style>
        .radio {
            line-height: 30px;
            vertical-align: middle;
            font-size: 14px;
            color: #666;
            margin-bottom: 10px;
        }

        .chosen-select {
            width: 194px;
        }
    </style>
</head>
<body style="background: #F8FCFF;">
    <input type="hidden" id="OldSectionID" />
    <input type="hidden" id="OldGradeID" />
    <form id="form1" runat="server">
        <div class="p20">
            <div class="dialog_wrap">
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">当前学期:</label>
                    <div class="row_content">
                        <select class="text" id="StudyTerm" onchange="GetGrade()">
                        </select>
                    </div>
                </div>
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">当前年级:</label>
                    <div class="row_content">
                        <select class="text" id="SelGrade">
                        </select>
                    </div>
                </div>
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">班级名称:</label>
                    <div class="row_content">
                        <input type="text" name="name" id="ClassName" class="text" placeholder="班级名称" />
                    </div>
                </div>
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">班级班号:</label>
                    <div class="row_content">
                        <input type="text" name="name" id="ClassNO" class="text" placeholder="班级班号" />
                    </div>
                </div>
                <div class="row_dia clearfix">
                    <label for="" class="row_label fl">班主任:</label>
                    <div class="row_content">

                        <select class="chosen-select" data-placeholder="班主任" id="HeadteacherNO">
                        </select>

                        <%--<input type="text" name="name" id="HeadteacherNO" class="text" placeholder="班主任" />--%>
                    </div>
                </div>
                <div class="row_dia clearfix" style="color: red" id="Message">
                    注:未分配学生的班级直接修改属性，已有学生分配的班级修改年级和学期会执行升级操作，将产生历史数据，请谨慎操作
                </div>
            </div>
            <div class="btn_wrap">
                <input type="button" name="" id="" value="确定" class="btns insert" onclick="EditClass();" />
                <input type="button" name="" id="" value="取消" class="btns cancel" onclick="javascript: parent.CloseIFrameWindow();" />
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();

        $(function () {
            GetTerm();
            GetGrade();
            GetAllTeacher();

            var ID = UrlDate.ID;
            if (ID == undefined) {
                $("#Message").hide();
                //$("#RegisterOrg").val(UrlDate.RegisterOrg);
            }
            else {
                GetClassByID(ID);
                $("#Message").show();
            }
            var config = {
                '.chosen-select': {},
                '.chosen-select-deselect': { allow_single_deselect: true },
                '.chosen-select-no-single': { disable_search_threshold: 10 },
                '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                '.chosen-select-width': { width: "95%" }
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }
        })


        function GetAllTeacher() {
            $("#HeadteacherNO").html("<option value=''>==请选择==</option>");
            var option = "";
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    Func: "GetData",
                    Ispage: "false",
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    IsStu: "false"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            option += "<option value='" + this.UniqueNo + "'>" + this.Name + "</option>";

                        })
                        $("#HeadteacherNO").append(option);

                    }
                    else {
                        layer.msg(errMsg);
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        function GetClassByID(ID) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/EduManage/ClassHandler.ashx",
                    Func: "GetData",
                    ID: ID,
                    Ispage: "false",
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    AcademicId: UrlDate.TermID
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#StudyTerm").val(this.SectionID);
                            $("#OldSectionID").val(this.SectionID);
                            GetGrade();
                            $("#SelGrade").val(this.GID);
                            $("#OldGradeID").val(this.GID);
                            $("#ClassName").val(this.ClassName);
                            $("#ClassNO").val(this.ClassNO);
                            $("#HeadteacherNO").val(this.HeadteacherNO);

                        })
                    }
                    else {
                        layer.msg(errMsg);
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }

        function EditClass() {
            var GradeId = $("#SelGrade").val();
            var AcademicId = $("#StudyTerm").val();
            var ClassName = $("#ClassName").val();
            var ClassNO = $("#ClassNO").val();
            var HeadteacherNO = $("#HeadteacherNO").val();
            var OldSectionID = $("#OldSectionID").val();
            var OldGradeID = $("#OldGradeID").val();

            var ID = "";
            var funcName = "AddClass";
            if (UrlDate.ID != undefined) {
                ID = UrlDate.ID;
                funcName = "EditClass"
            }
            if (!ClassNO.length || !ClassName.length) {
                layer.msg("请填写完整信息！");
            }
            else {
                $.ajax({
                    url: "../common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        "PageName": "/EduManage/ClassHandler.ashx",
                        func: funcName, GradeId: GradeId, AcademicId: AcademicId, ClassName: ClassName, ClassNO: ClassNO
                        , SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>",
                        HeadteacherNO: HeadteacherNO, ID: ID, OldSectionID: OldSectionID, OldGradeID: OldGradeID
                    },
                    success: function (json) {
                        var result = json.result;
                        if (result.errNum == 0) {
                            parent.layer.msg('操作成功!');
                            parent.getClassData(1, 10);
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
