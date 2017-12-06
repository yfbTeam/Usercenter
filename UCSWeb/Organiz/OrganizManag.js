var UrlDate = new GetUrlDate();
$(function () {
    getData(1, 10)
})
function GetOrgMenu() {
    //初始化序号 
    $.ajax({
        url: "../common.ashx",
        type: "post",
        async: false,
        dataType: "json",
        data: {
            PageName: "/Organiz/Organiz.ashx",
            Func: "GetOrgMenu",
        },
        success: function (json) {
            if (json.result.errNum.toString() == "0") {

            }
            else {

            }
        },
        error: function (errMsg) {
        }
    });
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
        },
        success: function (json) {
            if (json.result.errNum.toString() == "0") {
                $(json.result.retData).each(function () {
                    $("#AcademicId").val(this.AcademicId);
                    $("#ClassNO").val(this.ClassNO);
                    $("#ClassName").val(this.ClassName);
                    $("#HeadteacherNO").val(this.HeadteacherNO);
                    $("input[name='IsUse'][value=" + this.IsUse + "]").attr("checked", true);
                    $("input[name='IsUse'][value=" + this.IsUse + "]").attr("checked", true);
                    $("#MonitorNO").val(this.MonitorNO);
                    $("#GradeId").val(this.GradeId);
                })
            }
        },
        error: function (errMsg) {
        }
    });
}

function DelOrg() {
    $.ajax({
        url: "../common.ashx",
        type: "post",
        async: false,
        dataType: "json",
        data: {
            PageName: "/Organiz/Organiz.ashx",
            Func: "DelClass",
            ID: UrlDate.ID,
        },
        success: function (json) {
            if (json.result.errNum.toString() == "0") {
                AlertMsg("删除成功");
            }
            else {
                AlertMsg(json.result.errMsg);
            }
        },
        error: function (errMsg) {
            AlertMsg(errMsg);
        }
    });
}

//添加、修改基础信息
function EditOrg() {
    var Name = $("#Name").val();
    var Pid = $("#Pid").val().trim();
    var OrganType = $("#OrganType").val();
    if (UrlDate.ID != undefined) {
        ID = UrlDate.ID;
        funcName = "EditOrgDetail"
    }
    if (!Academic.length || !Semester.length) {
        layer.msg("请填写完整信息！");
    }
    else {
        $.ajax({
            url: "../common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "/Organiz/Organiz.ashx",
                func: funcName, Name: Name, Pid: Pid, OrganType: OrganType
            },
            success: function (json) {
                var result = json.result;
                if (result.errNum == 0) {
                    parent.AlertMsg('操作成功!');
                    parent.getData(1, 10);
                    parent.CloseWindow();
                }
                else {
                    parent.AlertMsg(result.errMsg);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });
    }
}
//修改详细信息
function EditOrgDetail() {
    var LegalUID = $("#LegalUID").val();
    var OrganNo = $("#OrganNo").val();
    var EstabLish = $("#EstabLish").val().trim();
    var ImageInfo = $("#ImageInfo").val();
    var Introduce = $("#Introduce").val();
    var ID = UrlDate.ID;

    if (!OrganNo.length) {
        layer.msg("请填写完整信息！");
    }
    else {
        $.ajax({
            url: "../common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "/Organiz/Organiz.ashx",
                func: "EditOrgDetail", LegalUID: LegalUID, OrganNo: OrganNo, EstabLish: EstabLish, ImageInfo: ImageInfo, Introduce: Introduce
            },
            success: function (json) {
                var result = json.result;
                if (result.errNum == 0) {
                    parent.AlertMsg('操作成功!');
                    parent.getData(1, 10);
                    parent.CloseWindow();
                }
                else {
                    parent.AlertMsg(result.errMsg);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });
    }
}
