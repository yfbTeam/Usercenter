<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SectionEdit.aspx.cs" Inherits="UCSWeb.EduManage.SectionEdit" %>

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
                <label for="" class="row_label fl">启用学段:</label>
                <div class="row_content">
                    <div class="radio">
                        <input id="Checkbox1" type="checkbox" value="1" name="PeriodIDs" /><label>小学</label>
                        <select class="text" style="margin-left: 10px; min-width: 80px;" id="Small">
                            <option value="5" selected="selected">五年制</option>
                            <option value="6">六年制</option>
                        </select>
                    </div>
                    <div class="radio">
                        <input id="Checkbox2" type="checkbox" value="2" name="PeriodIDs" /><label>初中</label>
                        <select class="text" style="margin-left: 10px; min-width: 80px;" id="Center">
                            <option value="3" selected="selected">三年制</option>
                        </select>
                    </div>
                    <div class="radio">
                        <input id="Checkbox3" type="checkbox" value="3" name="PeriodIDs" /><label>高中</label>
                        <select class="text" style="margin-left: 10px; min-width: 80px;" id="High">
                            <option value="3" selected="selected">三年制</option>
                        </select>
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
            <input type="button" name="" id="" value="确定" class="btns insert" onclick="EditSection();" />
            <input type="button" name="" id="" value="取消" class="btns cancel" onclick="javascript: parent.CloseIFrameWindow();" />
        </div>
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            var ID = UrlDate.ID;
            if (ID != 0 && ID != undefined) {
                GetSectionByID(ID);
                $("#Checkbox1").attr("disabled", "disabled");
                $("#Checkbox2").attr("disabled", "disabled")
                $("#Checkbox3").attr("disabled", "disabled")
                $("#Small").attr("disabled", "disabled");
                $("#Center").attr("disabled", "disabled")
                $("#High").attr("disabled", "disabled")
            }
            else {
                $("#Checkbox1").removeAttr("disabled");
                $("#Checkbox2").removeAttr("disabled")
                $("#Checkbox3").removeAttr("disabled")
                $("#Small").removeAttr("disabled");
                $("#Center").removeAttr("disabled")
                $("#High").removeAttr("disabled")
            }
        })
        //保存信息
        function EditSection() {
            var PeriodIDs = "";
            $("input[type=checkbox][name=PeriodIDs]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    PeriodIDs += this.value + ",";
                }
            });
            var Academic = $("#Academic").val();
            var Semester = $("#Academic").val();
            var StartDate = $("#StartDate").val();
            var EndDate = $("#EndDate").val();
            var IsDelete = $("input[name='IsDelete']:checked").val();

            var ID = "";
            var funcName = "AddSection";
            if (UrlDate.ID != undefined) {
                ID = UrlDate.ID;
                funcName = "EditSection"
            }
            var Small = "";
            var SmallL = 0;
            var Center = "";
            var CenterL = 0;
            var High = "";
            var HighL = 0;
            if (Checkbox1.checked) {
                Small = $("#Checkbox1").val();
                SmallL = $("#Small").val();
            }
            if (Checkbox2.checked) {
                Center = $("#Checkbox2").val();
                CenterL = $("#Center").val();
            }
            if (Checkbox3.checked) {
                High = $("#Checkbox3").val();
                HighL = $("#High").val();
            }
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
                        func: funcName, Academic: Academic, Semester: Semester, StartDate: StartDate, EndDate: EndDate, IsDelete: IsDelete, PeriodIDs: PeriodIDs,
                        Small: Small, SmallL: SmallL, Center: Center, CenterL: CenterL, High: High, HighL: HighL, ID: ID, SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>"
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
