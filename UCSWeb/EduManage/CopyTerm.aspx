<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyTerm.aspx.cs" Inherits="UCSWeb.EduManage.CopyTerm" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建学期</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
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
    </style>
</head>
<body style="background: #F8FCFF;">
    <div class="p20">
        <div class="dialog_wrap">
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">学期名称:</label>
                <div class="row_content">
                    <input type="text" class="text" id="Academic" value="" placeholder="学期名称" />
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">起止时间:</label>
                <div class="row_content">
                    <input type="text" class="text Wdate" id="StartDate" value="" placeholder="开始时间" style="min-width: 105px;" onclick="javascript: WdatePicker({ maxDate: '#F{$dp.$D(\'EndDate\')}' });" />
                    <span style="display: inline-block; color: #777; text-align: center; line-height: 30px; width: 25px;">-</span>
                    <input type="text" class="text Wdate" style="min-width: 105px;" id="EndDate" value="" placeholder="结束时间" onclick="javascript: WdatePicker({ minDate: '#F{$dp.$D(\'StartDate\')}' });" />
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">是否升级:</label>
                <div class="row_content">
                    <div class="radio">
                         <input type="radio" name="IsUp" id="" value="1" checked="checked" />
                        <label for="">是</label>
                        <input type="radio" name="IsUp" id="" value="0" />
                        <label for="">否</label>
                       
                    </div>
                </div>
            </div>
            <div class="row_dia clearfix">
                <label for="" class="row_label fl">是否启用:</label>
                <div class="row_content">
                    <div class="radio">
                        <input type="radio" name="IsDelete" id="" value="0" checked="checked" />
                        <label for="">启用</label>
                        <input type="radio" name="IsDelete" id="" value="1" />
                        <label for="">禁用</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="btn_wrap">
            <input type="button" name="" id="" value="确定" class="btns insert" onclick="CopySection();" />
            <input type="button" name="" id="" value="取消" class="btns cancel" onclick="javascript: parent.CloseIFrameWindow();" />
        </div>
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();

        //保存信息
        function CopySection() {

            var Academic = $("#Academic").val();
            var Semester = $("#Academic").val();
            var StartDate = $("#StartDate").val();
            var EndDate = $("#EndDate").val();
            var IsDelete = $("input[name='IsDelete']:checked").val();
            var IsUp = $("input[name='IsUp']:checked").val();
            var OldSectionID = UrlDate.ID
            if (OldSectionID == "" || OldSectionID == undefined) {
                layer.msg("请选择要复制的学期");
            }
            else {
                if (!Academic.length || !Semester.length || !StartDate.length || !EndDate.length) {
                    layer.msg("请填写完整信息！");
                }
                else {
                    $.ajax({
                        url: "../common.ashx",
                        type: "post",
                        async: false,
                        dataType: "json",
                        data: {
                            "PageName": "/EduManage/StudySection.ashx",
                            func: "CopySection", Academic: Academic, Semester: Semester, StartDate: StartDate, EndDate: EndDate, IsDelete: IsDelete, IsUp: IsUp,
                            OldSectionID: OldSectionID, SysAccountNo: SysAccountNo, LoginName: "<%=UserInfo.LoginName%>"
                        },
                        success: function (json) {
                            var result = json.result;
                            if (result.errNum == 0) {
                                parent.layerMsg('操作成功!');
                                parent.getSectionData(1, 10);
                                parent.CloseIFrameWindow();
                            }
                            else {
                                layerMsg(result.errMsg);
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            layer.msg("操作失败！");
                        }
                    });
                }
            }
        }
        //绑定数据
        function GetSectionByID(ID) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/EduManage/StudySection.ashx",
                    Func: "GetData",
                    ID: ID,
                    Ispage: "false",
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#Academic").val(this.Academic);
                            $("#StartDate").val(DateTimeConvert(this.StartDate, "yyyy-MM-dd"));
                            $("#EndDate").val(DateTimeConvert(this.EndDate, "yyyy-MM-dd"));
                            $("input[name='IsDelete'][value=" + this.IsDelete + "]").attr("checked", true);
                            var arry = this.PeriodIDs.split(',');
                            for (var i = 0; i < arry.length; i++) {
                                $("input[type=checkbox][name=PeriodIDs]").each(function () {//查找每一个name为cb_sub的checkbox 
                                    if (this.value == arry[i]) {
                                        this.checked = true;
                                    }
                                });
                            }

                        })
                    }
                    else {
                        layerMsg(result.errMsg);
                    }
                },
                error: function (errMsg) {
                    layer.msg("操作失败！");
                }
            });
        }
    </script>
</body>
</html>

