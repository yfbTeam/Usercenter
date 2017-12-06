<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgChange.aspx.cs" Inherits="UCSWeb.UserManage.OrgChange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="pragma" content="no-cache" />
    <title>修改组织机构</title>
    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script src="../Scripts/PageBar.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.core-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.excheck-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.exedit-3.5.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script type="text/javascript">
        var setting = {
            view: {
                selectedMulti: false
            },
            data: {
                keep: {
                    parent: true,
                    leaf: true
                },
                simpleData: {
                    enable: true
                }
            },
            callback: {
                onClick: onClick
            }
        };

        var log, className = "dark";
        function beforeClick(treeId, treeNode, clickFlag) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeClick ]&nbsp;&nbsp;" + treeNode.name);
            return (treeNode.click != false);
        }
        function onClick(event, treeId, treeNode, clickFlag) {
            $("#OrgNo").val(treeNode.org);
        }
        function showLog(str) {
            if (!log) log = $("#log");
            log.append("<li class='" + className + "'>" + str + "</li>");
            if (log.children("li").length > 8) {
                log.get(0).removeChild(log.children("li")[0]);
            }
        }
        function getTime() {
            var now = new Date(),
            h = now.getHours(),
            m = now.getMinutes(),
            s = now.getSeconds();
            return (h + ":" + m + ":" + s);
        }
        function treeNode() {

        }
        var UrlDate = new GetUrlDate();
        
        $(function () {
            $.ajax({
                type: "get",
                url: "../common.ashx",
                dataType: "json",
                data: {
                    "PageName": "/Organiz/Organiz.ashx", "Func": "GetOrgMenu",
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                                        ID: UrlDate.ID
                },
                success: function (returnVal) {
                    $.fn.zTree.init($("#treeDemo"), setting, $.parseJSON(returnVal.result.retData));
                    var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
                    treeObj.expandAll(true);
                },
                error: function (errMsg) {
                   layer.msg('数据加载失败！');
                }
            });
        });
        function treeClick() {
            this.close();
            parent.EditOrg($("#OrgNo").val());
        }
      
    </script>


</head>
<body>

    <form id="form1" runat="server">
        <div class="content_wrap">
            <input id="OrgNo" type="hidden" />

            <div class="zTreeDemoBackground left">
                <ul id="treeDemo" class="ztree"></ul>
                <input type="button" value="确定" onclick='treeClick()' style="background: rgb(84, 147, 215); margin: 10px; border-radius: 3px; width: 100px; height: 34px; text-align: center; color: white;" />
            </div>

        </div>

    </form>
</body>
</html>

