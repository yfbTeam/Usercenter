<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FiledSettings.aspx.cs" Inherits="UCSWeb.InterfaceManagement.FiledSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>接口管理</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
    <script src="../Scripts/html5.js"></script>
    <![endif]-->
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
</head>
<body style="background: #F8FCFF;">
    <form id="form1" runat="server">
        <div class="p20">
            <div class="dialog_wrap">
                <div class="row_dia clearfix" style="height: 30px;">
                    <label for="" class="row_label fl">已选字段:</label>
                </div>
                <div class="filed_content clearfix" id="div_hasFields"></div>
                <div class="row_dia clearfix" style="height: 30px;">
                    <label for="" class="row_label">可选字段:</label>
                </div>
                <div class="filed_content clearfix" id="div_allfields"></div>
            </div>
            <div class="btn_wrap">
                <input type="button" name="" id="" value="确定" class="btns insert" onclick="EditEntityRel();"/>
                <input type="button" name="" id="" value="取消" class="btns cancel" onclick="javascript: parent.CloseIFrameWindow();" />
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            GetEntityByAccName();
        });
        //获取实体信息
        function GetEntityByAccName() {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/InterfaceManagement/SysAccountNoHandler.ashx",
                    Func: "GetEntityByAccountNo",
                    AccountNo: UrlDate.accountno,
                    EntityName: UrlDate.etname,
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_hasFields").html("");
                        $("#div_allfields").html("");
                        var rtnObj = json.result.retData;
                        var hasChina = rtnObj[0].Rel_FieldsChina, hasEng = rtnObj[0].Rel_FieldsEng;
                        var hasChinaArray = [], hasEngArray = [], allChinaArray = [], allEngArray = [];
                        if (hasEng.length) {
                            hasChinaArray = hasChina.split(",");
                            hasEngArray = hasEng.split(",");
                            for (var i = 0; i < hasEngArray.length; i++) {
                                $("#div_hasFields").append("<div class='fl fileded' hasrng='" + hasEngArray[i] + "'><span>" + hasChinaArray[i] + "</span><div class='del' onclick=\"DelField(this,'" + hasEngArray[i] + "');\">x</div></div>");
                            }
                        }                        
                        var allChina = rtnObj[0].FieldsChina, allEng = rtnObj[0].FieldsEng;                        
                        if (allEng.length) {
                            allChinaArray = allChina.split(",");
                            allEngArray = allEng.split(",");
                            for (var j = 0; j < allEngArray.length; j++) {
                                var displaycss = '';
                                if ($.inArray(allEngArray[j], hasEngArray) != -1) { displaycss = ' style="display:none;"';}
                                $("#div_allfields").append("<div class='filed_select fl' " + displaycss + " alleng='" + allEngArray[j] + "' onclick=\"AddField(this);\"><span>" + allChinaArray[j] + "</span><div class='del'>+</div>");
                            }
                            $('#div_allfields>div').hover(function () {
                                $(this).find('.del').show();
                            }, function () {
                                $(this).find('.del').hide();
                            })
                        }                        
                    }                    
                }
            });
        }
        function AddField(fobj) {
            var $field = $(fobj);
            $("#div_hasFields").append("<div class='fl fileded' hasrng='" + $field.attr("alleng") + "'><span>" + $field.find('span').html() +"</span><div class='del' onclick=\"DelField(this,'" + $field.attr("alleng") + "');\">x</div></div>");
            $field.hide();
        }
        function DelField(delobj,fieldeng) {
            $("#div_allfields div[alleng='" + fieldeng + "']").show();
            $(delobj).parent().remove();;
        }
        function EditEntityRel() {
            var selfields = $("#div_hasFields div.fileded");
            if (selfields.length == 0) { layer.msg('请选择字段！'); return; }
            var engArray=[];
            $(selfields).each(function (i, n) {
                engArray.push($(n).attr("hasrng"));
            });
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/InterfaceManagement/SysAccountNoHandler.ashx",
                    Func: "EditEntityRel",
                    AccountNo:UrlDate.accountno,
                    EntityName:UrlDate.etname,
                    FieldsEng:engArray.join(","),
	                SysAccountNo: SysAccountNo,
                    LoginName:"<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == 0) {
                        parent.layer.msg('字段设置成功!');
                        parent.GetRelationData(UrlDate.accountno, "GetEntityByAccountNo", "tb_entity", "tr_entity", "暂无实体");
                        parent.CloseIFrameWindow();
                    } else {
                        layer.msg(result.errMsg);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("操作失败！");
                }
            });
        }
    </script>
</body>
</html>
