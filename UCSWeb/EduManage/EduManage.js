function GetTerm() {
    var option = "";

    $.ajax({
        url: "../common.ashx",//random" + Math.random(),//方法所在页面和方法名
        type: "post",
        async: false,
        dataType: "json",
        data: { PageName: "/EduManage/StudySection.ashx", Func: "GetData", "Ispage": "false" },
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

function GetGrade() {
    $("#SelGrade").html("");
    var option = "";

    $.ajax({
        url: "../common.ashx",//random" + Math.random(),//方法所在页面和方法名
        type: "post",
        async: false,
        dataType: "json",
        data: { PageName: "/EduManage/GradeHandler.ashx", Func: "GetData", "Ispage": "false", AcademicId: $("#StudyTerm").val() },
        success: function (json) {
            if (json.result.errNum.toString() == "0") {
                $(json.result.retData).each(function () {
                    option += "<option value='" + this.Id + "'>" + this.GradeName + "</option>";
                })
                $("#SelGrade").append(option);
            }
            else {
                $("#SelGrade").html("");
            }
        },
        error: function (errMsg) {
            layer.msg(errMsg);
        }
    });

}
function getGradeData(startIndex, pageSize) {
    //初始化序号 
    pageNum = (startIndex - 1) * pageSize + 1;
    $.ajax({
        url: "../common.ashx",
        type: "post",
        async: false,
        dataType: "json",
        data: {
            PageName: "/EduManage/GradeHandler.ashx",
            Func: "GetData",
            PageIndex: startIndex,
            pageSize: pageSize,
            AcademicId: $("#StudyTerm").val(),
            GradeName: $("#Name").val()
        },
        success: function (json) {
            if (json.result.errNum.toString() == "0") {
                $("#tb_Grade").html('');
                $("#tr_Grade").tmpl(json.result.retData.PagedData).appendTo("#tb_Grade");
                if (json.result.retData.RowCount < pageSize) {
                    $("#pageBar").hide();
                } else {
                    $("#pageBar").show();
                    makePageBar(getGradeData, document.getElementById("pageBar"), json.result.retData.PageIndex, json.result.retData.PageCount, pageSize, json.result.retData.RowCount);
                }
                NewCheckAll($('.table_wrap input[type=checkbox]'));
            }
            else {
                $("#tb_Grade").html("<tr><td colspan='5'>暂无年级信息！</td></tr>");
                $("#pageBar").hide();
            }
        },
        error: function (errMsg) {
            $("#tb_Grade").html("<tr><td colspan='5'>暂无年级信息！</td></tr>");
        }
    });
}
function DelGrade() {
    if (confirm("确定要删除吗？")) {
        var ids = "";

        $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
            if (this.checked) {
                ids += this.value + ",";
            }
        });
        if (ids == "") {
            layer.msg("请选择数据行");
        } else {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/EduManage/GradeHandler.ashx",
                    Func: "DelGrade",
                    ID: ids,
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("删除成功");
                        getGradeData(1, 10);
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
    }
    
}
function EditGrade() {
    var ids = "";
    var i = 0;
    $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
        if (this.checked) {
            ids = this.value;
            i++;
        }
    });
    if (ids == "") {
        layer.msg("请选择数据行");
    }
    else {
        if (i > 1) {
            layer.msg("只能选择一行");
        }
        else {
            OpenIFrameWindow('修改年级', 'GradeEdit.aspx?ID=' + ids, '380px', '300px')
        }
    }
}

