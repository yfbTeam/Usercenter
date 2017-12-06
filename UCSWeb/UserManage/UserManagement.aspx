<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="UCSWeb.UserManage.UserManagement" %>

<!DOCTYPE html>

<html>
<head>
    <meta http-equiv="pragma" content="no-cache" />
    <meta charset="utf-8" />
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>用户管理</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link href="../Scripts/layer/skin/layer.css" rel="stylesheet" />
    <link href="../Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <link href="../Scripts/zTree/css/Common.css" rel="stylesheet" />

    <script src="../Scripts/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/menu_top.js"></script>

    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/PageBar.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.core-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.excheck-3.5.js"></script>
    <script src="../Scripts/zTree/js/jquery.ztree.exedit-3.5.js"></script>
    <style>
        .menu_wrap .tool_left {
            position: fixed;
            width: 298px;
            background: #FAFDFF;
            padding-bottom: 8px;
            z-index: 999;
        }

        .ztree {
            padding-top: 45px;
        }

        .menu_wrap .tool_left span {
            margin-top: 8px;
            margin-right: 0;
            margin-left: 8px;
            padding: 5px 8px;
        }

        .menu_wrap .tool_left span {
            margin-top: 8px;
            margin-right: 0;
            margin-left: 8px;
            padding: 5px 8px;
        }

        /*组织机构信息编辑*/
        #editmes .row_dia .row_label {
            width: 100px;
        }

        #editmes .row_dia .row_content {
            padding-left: 100px;
        }

            #editmes .row_dia .row_content .text {
                border: 1px solid #A9CFF3;
            }

        .upload_imga {
            width: 100px;
        }

            .upload_imga img {
                max-width: 100px;
                height: auto;
            }

            .upload_imga .uploadify-button {
                font-size: 16px;
                border: none;
                background: #5493D7;
                color: #fff;
            }

        .btn_s {
            height: 50px;
            background: #F8FBFE;
            border: 1px solid #E3EFF9;
        }

        #Add {
            position: relative;
        }

            #Add .addDom_none {
                position: absolute;
                min-width: 106px;
                top: 25px;
                left: 0px;
                border-radius: 2px;
                border: 1px solid #87BAEF;
                border-bottom: none;
                display: none;
            }


                #Add .addDom_none a {
                    display: block;
                    width: 100%;
                    height: 28px;
                    background: #fff;
                    color: #87BAEF;
                    border-bottom: 1px solid #87BAEF;
                    text-align: center;
                    line-height: 28px;
                }

                    #Add .addDom_none a:hover {
                        background: #AAD0F4;
                        color: #fff;
                    }
    </style>
    <!--[if IE]>
			<script src="Scripts/html5.js"></script>
		<![endif]-->
    <script type="text/x-jquery-tmpl" id="tr_User">
        <tr>
            <td>
                <input type="checkbox" name="Check_box" id="subcheck" value="${Id}" />
            </td>
            <td>${pageIndex()}</td>
            <td style="padding: 0px 25px;">${Name}
                <div class="setings"><i class="iconfont">&#xe6bd;</i></div>
                <div class="setting_none">
                    <a href="#" onclick="UpdatePwd('${LoginName}')">修改密码</a>
                    <a href="#" onclick="ViewUser(${Id})">查看属性</a>
                    <a href="#" onclick="EditUser(${Id})">修改属性</a>
                    <a href="#" onclick="ResetPwd(${Id})" btncls="icon-resetpwd" style="display: none;">重置密码</a>
                </div>
            </td>
            <td>${LoginName}</td>
            <td>{{if Sex==0}}女
                {{else}}男
                {{/if}}</td>
            <%--<td>${IDCard}</td>--%>
            <td>${OrgName}</td>
            <td>{{if AuthenType==2}}<span class="colorgreen">激活</span>
                {{else}}{{if AuthenType==1}}<span class="colororange">未激活</span>
                {{else}}{{if AuthenType==3}}<span class="colorred">审核失败</span>
                {{else}}<span class="colorred">新用户注册</span>
                {{/if}}{{/if}}{{/if}}             
            </td>
            <td>{{if IsEnable==1}}<span class="colorgreen">启用</span>
                {{else}}<span class="colorred">禁用</span>
                {{/if}}  </td>
        </tr>
    </script>
</head>
<body>
    <!--header-->
    <div class="header_wrap" style="height: 61px;"></div>
    <input type="hidden" id="HOrgNo" />
    <input type="hidden" id="HOrgID" value="0" />
    <input type="hidden" id="HOrganType" />
    <input type="hidden" id="PPID" />


    <div id="main">
        <div class="w1200 clearfix wrap pr">
            <div class="menu_wrap fl" style="width: 298px;">
                <div class="tool_left clearfix">
                    <span btncls="icon-plus2" style="display: none;" id="Add">
                        <em>添加 <i class="iconfont">&#xe6d4;</i></em>
                        <div class="addDom_none">
                            <a href="javascript:;" onclick="addHoverDom(3)">添加部门</a>
                            <a href="javascript:;" onclick="addHoverDom(2)">添加子组织机构</a>
                        </div>
                    </span>
                    <span btncls="icon-edit2" style="display: none;" id="Edit" onclick="EditName()">编辑</span>
                    <span btncls="icon-del2" style="display: none;" id="Del" onclick="DelOrg()">删除</span>
                    <span btncls="icon-moveup" style="display: none;" id="up" onclick="EditOrder('up')">上移</span>
                    <span btncls="icon-movedown" style="display: none;" id="down" onclick="EditOrder('down')">下移</span>
                </div>
                <ul class="ztree" id="treeMenu"></ul>
            </div>
            <div class="content fr" style="width: 900px;">
                <div class="content_wrap">
                    <div class="title_nav clearfix">
                        <div class="title_nav_left fl">
                            <a href="javascript:;" class="active" id="OrganName"></a>
                        </div>
                        <div class="fl oran bgblue" id="OrganType">
                            企业
                        </div>
                        <a style="background: rgb(85, 161, 232); padding: 3px 7px; border-radius: 3px; margin-top: 5px; color: rgb(255, 255, 255); font-size: 12px; margin-left: 5px; position: absolute;" onclick="editMes();" href="javascript:;">编辑组织架构<i class="iconfont"></i></a>
                        <div class="title_nav_right fr">
                            <span style="color: #91c954;" class="mr20 fl" id="UseUserNum"></span>
                            <span style="color: #ef805b;" class="mr20 fl" id="ActiveUserNum"></span>
                            <span class="fl" id="TotolNum"></span>
                        </div>
                    </div>
                    <div class="toolsbar clearfix mt10">
                        <div class="tool_left fl">
                            <span btncls="icon-plus" style="display: none;" class="NewItem">新建</span>
                            <span btncls="icon-edit" style="display: none;" onclick="OpenEditOrg()">修改组织机构</span>
                            <span btncls="icon-enable" style="display: none;" onclick="EnableUser('1')">启用</span>
                            <span btncls="icon-disable" style="display: none;" onclick="EnableUser('0')">禁用</span>
                            <span btncls="icon-import" style="display: none;" class="insert_user">导入</span>
                        </div>
                        <div class="tool_right fr clearfix">
                            <div class="fl mr10 pr search">
                                <input type="text" name="" id="Name" value="" placeholder="请输入关键字" />
                                <i class="iconfont" onclick="getData(1,18)">&#xe604;</i>
                            </div>
                            <div class="fl">
                                <lable>状态:</lable>
                                <select class="select" id="Status" onchange="getData(1,18)">
                                    <option value="">全部</option>
                                    <option value="1" selected="selected">启用</option>
                                    <option value="0">禁用</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="table_wrap mt10">
                        <table>
                            <thead>
                                <tr>
                                    <th>
                                        <input type="checkbox" name="" id="checkAll" value="" /></th>
                                    <th>编号<i class="iconfont colorgreen" style="font-size: 8px; transform: rotate(180deg); display: inline-block;">&#xe62b;</i></th>
                                    <th>姓名</th>
                                    <th>账号</th>
                                    <th>姓别</th>
                                    <%--<th>身份证号</th>--%>
                                    <th>组织机构</th>
                                    <th>激活状态</th>
                                    <th>启用状态</th>
                                </tr>
                            </thead>
                            <tbody id="tbUser">
                            </tbody>
                        </table>
                        <!--分页-->
                        <div class="page" id="pageBar"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer—wrap" style="height: 40px;"></div>
    <%--组织机构相关--%>
    <script type="text/javascript">
        function GetOrgByID(ID) {
            $('#editmes_success').show();
            $('#log_wrap').show();
            $('#editmes').hide();
            var oDate = new Date();
            var getTime = oDate.getTime();
            $.ajax({
                url: "../Common.ashx?time=" + getTime,
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

                            $("#OrganName").html(this.Name);
                            var Type = this.OrganType;
                            //0 学校；1企业；2子组织机构 ；3部门

                            switch (Type) {
                                case "0":
                                    $("#OrganType").html("学校");
                                    break;
                                case "1":
                                    $("#OrganType").html("企业");
                                    break;
                                case "2":
                                    $("#OrganType").html("组织机构");
                                    break;
                                case "3":
                                    $("#OrganType").html("部门");
                                    break;
                                default:
                                    $("#OrganType").html("学校");
                                    break;
                            }

                        })
                    }
                },
                error: function (errMsg) {
                }
            });
        }
        var zNodes = [];

        var setting = {
            view: {
                showLine: false,
                showIcon: false,
                selectedMulti: false,
                dblClickExpand: false,
                addDiyDom: addDiyDom,
                //addHoverDom: addHoverDom,
                //removeHoverDom: removeHoverDom,
                //selectedMulti: false,

            },
            edit: {
                //enable: true,
                editNameSelectAll: true,
                //showRemoveBtn: showRemoveBtn,
                //showRenameBtn: showRenameBtn
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
                beforeDrag: beforeDrag,
                beforeEditName: beforeEditName,
                beforeRemove: beforeRemove,
                beforeRename: beforeRename,
                onRemove: onRemove,
                //onRename: onRename,
                beforeClick: beforeClick,
                onClick: onClick
            }

        };
        function addDiyDom(treeId, treeNode) {
            var spaceWidth = 5;
            var switchObj = $("#" + treeNode.tId + "_switch"),
			icoObj = $("#" + treeNode.tId + "_ico");
            switchObj.remove();
            icoObj.before(switchObj);

            if (treeNode.level > 1) {
                var spaceStr = "<span style='display: inline-block;width:" + (spaceWidth * treeNode.level) + "px'></span>";
                switchObj.before(spaceStr);
            }
        }
        var log, className = "dark";
        function beforeClick(treeId, treeNode, clickFlag) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeClick ]&nbsp;&nbsp;" + treeNode.name);
            return (treeNode.click != false);
        }
        function beforeDrag(treeId, treeNodes) {
            return false;
        }
        function beforeEditName(treeId, treeNode) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeEditName ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name);
            var zTree = $.fn.zTree.getZTreeObj("treeMenu");
            zTree.selectNode(treeNode);
        }
        function beforeRemove(treeId, treeNode) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeRemove ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name);
            var zTree = $.fn.zTree.getZTreeObj("treeMenu");
            zTree.selectNode(treeNode);
            return confirm("确认删除 节点 -- " + treeNode.name + " 吗？");
        }

        function beforeRename(treeId, treeNode, newName, isCancel) {

            var newname = newName.trim();
            if (treeNode.id == "" && !newname.length) {
                var zTree = $.fn.zTree.getZTreeObj("treeMenu");
                zTree.removeNode(treeNode);
            }
            return newname.length > 0;
        }

        //编辑节点
        function onRename(event, treeId, treeNode, isCancel) {
            EditTree(treeNode.name, treeNode.id.toString(), '', '');
        }
        var newCount = 1;

        function removeHoverDom(treeId, treeNode) {
            $("#addBtn_" + treeNode.tId).unbind().remove();
        };
        function onRemove(e, treeId, treeNode) {
            DelMenu(treeNode.id);
        }
        function EditName() {
            var zTree = $.fn.zTree.getZTreeObj("treeMenu");
            var treeNode = zTree.getSelectedNodes()[0];
            if (treeNode != null) {
                //$(".tool_left").hide();
                //document.getElementById('treeMenu').style.marginTop = '0px';
                var treeId = treeNode.tId;
                beforeEditName(treeId, treeNode);
                $("#" + treeId).find("a").addClass("curSelectedNode_Edit");
                var Name = $("#" + treeId).find("a").children("#" + treeId + "_span").html();
                if ($("#" + treeId + "_input").length > 0) {
                    SavaChange(treeId, treeNode.id);
                }
                else {
                    $("#" + treeId).find("a").children("#" + treeId + "_span").html("<input class=\"rename\" id=\"" + treeId
                       + "_input\" type=\"text\" treenode_input=\"\" value=\"" + Name + "\" onblur=\"SavaChange('" + treeId + "'," + treeNode.id + ")\"></input>")
                    //onRename(null, treeId, treeNode, true);
                    $("#" + treeId + "_input").focus();
                }
            }
            else {
                layer.msg("请选择要编辑的节点");
            }
        }
        function SavaChange(treeId, id) {
            var Name = $("#" + treeId + "_input").val();
            $(".tool_left").show();
            EditTree(Name, id, '')
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
        var newCount = 1;
        //增加节点   
        function addHoverDom(OrganType) {
            var Pid = $("#HOrgID").val();
            var PPID = $("#PPID").val();
            if (((PPID == "0" || PPID == "") && OrganType == "2") || OrganType == "3") {
                $("#HOrganType").val(OrganType);
                var pId = $("#HOrgID").val();
                var zTree = $.fn.zTree.getZTreeObj("treeMenu");
                var treeNode = zTree.getSelectedNodes()[0];
                //var newNodes = zTree.addNodes(treeNode, { id: "", pId: pId, name: "new node" + (newCount++) });
                EditTree("new node" + (newCount++), "", pId);
                //添加到数据库中/
                //zTree.editName(newNodes[0]);
                return false;
            }
            else {
                if (OrganType == "2") {
                    if (PPID != "0" && PPID != "") {
                        layer.msg("只有根节点能添加子组织机构");
                    }
                }
            }
        };

        function onClick(event, treeId, treeNode, clickFlag) {
            if (treeNode.org == undefined || treeNode.org == "") {
                $("#HOrgNo").val("0");
            }
            else {
                $("#HOrgNo").val(treeNode.org);
            }
            $("#HOrgID").val(treeNode.id);
            $("#PPID").val(treeNode.pId);
            getData(1, 18);
            CensusActiveUser();
            UseUserNum();
            GetOrgByID(treeNode.id);
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
        function ZtreeNode(id, pId, name) {//定义ztree的节点类  
            this.id = id;
            this.pId = pId;
            this.name = name;
        }

        function EditTree(Name, id, pid) {
            var pId = "0";
            if ($("#HOrgID").val() != undefined && $("#HOrgID").val() != "") {
                pId = $("#HOrgID").val();
            }
            var func = "AddOrg";// "EditOrgDetail";
            if (id == "" || id == undefined) {
                func = "AddOrg"
            }
            else {
                func = "EditOrgDetail";
            }
            $.ajax({
                type: "Post",
                url: "../common.ashx",
                async: false,
                dataType: "json",
                data: {
                    "PageName": "Organiz/Organiz.ashx",
                    "func": func,
                    "Name": Name,
                    "ID": id,
                    "Pid": pId,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    "OrganType": $("#HOrganType").val()
                },
                success: function (json) {
                    if (json.result.errNum == "0") {
                        var treeObj = $.fn.zTree.getZTreeObj("treeMenu");//获取ztree对象  
                        var ZNode = treeObj.getNodeByParam("id", $("#HOrgID").val());

                        if (id == "")//添加 
                        {
                            layer.msg("添加成功");
                            if (navigator.userAgent.indexOf('Trident') > -1) {
                                var msg = json.result.retData;
                                id = msg.split('-')[0];
                                var childZNode = new ZtreeNode(id, $("#HOrgID").val(), Name);
                                treeObj.addNodes(ZNode, childZNode, true);
                                treeObj.selectNode(ZNode, true);//指定选中ID的节点  
                                treeObj.expandNode(ZNode, true, false);//指定选中ID节点展开 
                            }
                            else {
                                GetNave();
                            }
                        }
                        else //修改节点名称
                        {
                            GetNave();
                            layer.msg("修改成功");
                            ZNode.name = Name;
                            treeObj.updateNode(ZNode);

                        }
                        //location.reload();
                    }
                    else {
                        layer.msg(json.result.errMsg);
                        //GetNave();
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        function GetNave() {
            $.ajax({
                type: "get",
                url: "../common.ashx",
                dataType: "json",
                async: false,
                data: {
                    "PageName": "/Organiz/Organiz.ashx",
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    "Func": "GetOrgMenu",
                    cache: false,//不保存缓存
                },
                success: function (returnVal) {
                    if (returnVal.result.errNum == 0) {
                        var treeObj = $("#treeMenu");
                        $.fn.zTree.init(treeObj, setting, $.parseJSON(returnVal.result.retData));
                        zTree_Menu = $.fn.zTree.getZTreeObj("treeMenu");
                        var pid = $("#HOrgID").val();
                        var node;
                        if (pid == "0" || pid == undefined || pid == "") {
                            var nodes = zTree_Menu.getNodes();
                            node = nodes[0];
                            //pid = nodes[0].id
                            //zTree_Menu.selectNode(nodes[0]);
                            //$("#HOrgID").val(pid);
                            //getData(1, 10);
                            //zTree_Menu.expandNode(nodes[0], true, true, true);
                        }
                        else {
                            node = zTree_Menu.getNodeByParam("id", pid);
                        }
                        zTree_Menu.selectNode(node, true);//指定选中ID的节点  
                        zTree_Menu.expandNode(node, true, false);//指定选中ID节点展开 
                        $("#HOrgID").val(node.id);
                        $("#HOrgNo").val(node.org);
                        //$("#OrganName").html(node.Name);
                        GetOrgByID(node.id);
                        getData(1, 18);

                        GetOrgByID(pid);
                        //获取用户信息
                        getData(1, 18);
                        CensusActiveUser();
                        UseUserNum();
                    }
                    //else {
                    //    window.location.href = "NoOrganize.aspx";
                    //}

                },
                error: function (errMsg) {
                    layer.msg('数据加载失败！');
                }
            });
        };

        $(function () {
            GetNave();
        })
        function DelOrg() {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/Organiz/Organiz.ashx",
                    Func: "DelMenu",
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    ID: $("#HOrgID").val(),
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("删除成功");
                        if (navigator.userAgent.indexOf('Trident') > -1) {
                            //删除树节点
                            var zTree_Menu = $.fn.zTree.getZTreeObj("treeMenu");
                            var pid = $("#HOrgID").val();
                            node = zTree_Menu.getNodeByParam("id", pid);
                            zTree_Menu.removeNode(node);
                            //选中第一个节点
                            var PPID = $("#PPID").val();
                            node = zTree_Menu.getNodeByParam("id", PPID);
                            if (node == null || node == undefined) {
                                var nodes = zTree_Menu.getNodes();
                                node = nodes[0];
                                PPID = node.id;
                            }
                            $("#HOrgID").val(PPID);
                            zTree_Menu.selectNode(node, true);//指定选中ID的节点  
                            zTree_Menu.expandNode(node, true, false);//指定选中ID节点展开 
                            GetOrgByID(PPID);
                        }
                        else {
                            $("#HOrgID").val($("#PPID").val());
                            GetNave();
                        }
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
        //上移、下移
        function EditOrder(OrderType) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/Organiz/Organiz.ashx",
                    Func: "EditOrder",
                    ID: $("#HOrgID").val(),
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    OrderType: OrderType
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("操作成功");
                        GetNave();
                        //location.reload();
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
    </script>
    <%--页面加载事件--%>
    <script type="text/javascript">

        (function init() {
            var height = $(window).height() - 101;
            $('.menu_wrap').height(height);
            $(window).resize(function () {
                $('.menu_wrap').height(height);
            })
            $('.wrap').css('minHeight', height);
            $('.content').css('minHeight', height);
        })();
        $(function () {            //加载公共页面
            $('.footer—wrap').load('../CommonPage/footer.html');
            $('.header_wrap').load('../CommonPage/header.aspx');
            $('.menu li').hover(function () {
                $(this).children('h1').addClass('active');
                $(this).find('.edit_wrapa').show();
            }, function () {
                $(this).find('h1').removeClass('active');
                $(this).find('.edit_wrapa').hide();
                $(this).find('.edit_wrap').hide();
            })
            $('.menu li .iconfont_edit').click(function () {
                $(this).next('.edit_wrap').show();
            })
            $('.edit_wrap .edit_one').hover(function () {
                $(this).find('.edit_two').show();
            }, function () {
                $(this).find('.edit_two').hide();
            })
            $('#Add').hover(function () {
                $(this).find('.addDom_none').slideDown();
            }, function () {
                $(this).find('.addDom_none').stop().slideUp();
            })
        })
        function SeetingShow() {
            $('.table_wrap table tbody tr').find('.setings').click(function () {
                if ($(this).next().is(':hidden')) {
                    $('.table_wrap table tbody tr').find('.setting_none').hide();
                    $(this).next().show();
                    $(this).next().mouseleave(function () {
                        $(this).hide();
                    });
                } else {
                    $(this).next().hide();
                }
            })
        }
        function editMes() {
            OpenIFrameWindow("编辑组织架构", "../Organiz/EditOrg.aspx?ID=" + $("#HOrgID").val(), "800px", "460px");
        }
    </script>
    <%--用户相关事件--%>
    <script type="text/javascript">
        function CensusActiveUser() {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    func: "CensusActiveUser",
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",

                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        $("#ActiveUserNum").html("活跃人数" + json.result.retData + "人");

                    }
                    else {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function (ErroMsg) {
                    layer.msg(ErroMsg);
                }
            })
        }

        function UseUserNum() {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    func: "CensusUser",
                    AuthenType: 2,
                    RegisterOrg: $("#HOrgNo").val(),
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        $("#UseUserNum").html("已激活" + json.result.retData + "人");

                    }
                    else {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function (ErroMsg) {
                    layer.msg(ErroMsg);
                }
            })
        }
        //获取用户信息
        function getData(startIndex, pageSize) {
            $("#checkAll").attr('checked', false);
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    Func: "GetData",
                    PageIndex: startIndex,
                    pageSize: pageSize,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    Key: $("#Name").val(),
                    Status: $("#Status").val(),
                    OrgNo: $("#HOrgNo").val(),
                    IsStu: "false",
                    AuthenType: 2
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#tbUser").html('');
                        $("#tr_User").tmpl(json.result.retData.PagedData).appendTo("#tbUser");
                        $("#TotolNum").html("共" + json.result.retData.RowCount + "人");
                        if (json.result.retData.RowCount < pageSize) {
                            $("#pageBar").hide();
                        }
                        else {
                            makePageBar(getData, document.getElementById("pageBar"), json.result.retData.PageIndex, json.result.retData.PageCount, pageSize, json.result.retData.RowCount);
                            $("#pageBar").show();
                        }
                        SetPageButton("<%=UserInfo.UniqueNo%>".trim(), "<%=UserInfo.LoginName%>".trim(), 'a')
                    }
                    else {
                        $("#tbUser").html("<tr><td colspan='100'>暂无用户信息！</td></tr>");
                        $("#pageBar").hide();
                    }
                    SeetingShow();
                    NewCheckAll($('.table_wrap input[type=checkbox]'));

                },
                error: function (errMsg) {
                    $("#tb_Class").html("<tr><td colspan='5'>暂无用户信息！</td></tr>");
                }
            });
        }
        //用户导入
        $(document).on('click', '.insert_user', function (event) {
            OpenIFrameWindow("导入用户", "ImportUser.aspx?OrgNo=" + $("#HOrgNo").val(), "560px", "395px");
        });

        //新增用户
        $(document).on('click', '.NewItem', function (event) {
            OpenIFrameWindow("新增用户", "EditUser.aspx?RegisterOrg=" + $("#HOrgNo").val(), "660px", "540px");
        });
        //修改用户
        function EditUser(ID) {
            OpenIFrameWindow("用户修改", "EditUser.aspx?ID=" + ID + "&IsStu=false&Type=1", "660px", "540px")
        }
        //查看
        function ViewUser(ID) {
            OpenIFrameWindow("用户查看", "EditUser.aspx?ID=" + ID + "&IsStu=false&Type=2", "660px", "540px")
        }
        function ResetPwd(ID) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    func: "ResetUser",
                    ID: ID,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        layer.msg("密码重置成功,新密码:123456");
                    }
                    else {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function (ErroMsg) {
                    layer.msg(ErroMsg);
                }
            })
        }
        //启用、禁用用户
        function EnableUser(Status) {
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
                    dataType: "json",
                    data: {
                        PageName: "/UserManage/UserInfo.ashx",
                        func: "EnableUser",
                        IsEnable: Status,
                        ID: ids,
                        SysAccountNo: SysAccountNo,
                        LoginName: "<%=UserInfo.LoginName%>",
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            getData(1, 18);
                        }
                        else {
                            layer.msg(json.result.errMsg);
                        }
                    },
                    error: function (ErroMsg) {
                        layer.msg(ErroMsg);
                    }
                })
            }
        }

        //修改密码
        function UpdatePwd(LoginName) {
            OpenIFrameWindow("修改密码", "UpdatePwd.aspx?LoginName=" + LoginName, "400px", "300px")
        }
        //修改组织架构
        function OpenEditOrg() {
            var ids = "";

            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids += this.value + ",";
                }
            });
            if (ids == "") {
                layer.msg("请选择数据行");
            }
            else {
                OpenIFrameWindow("修改组织架构", "OrgChange.aspx", "250px", "380px")
            }
        }
        function EditOrg(OrgNo) {
            if (OrgNo == "" || OrgNo == undefined) {
                layer.msg("未选择组织机构或所选组织机构缺少组织机构号！");
            }
            else {
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
                        dataType: "json",
                        data: {
                            PageName: "/UserManage/UserInfo.ashx",
                            func: "EditOrg",
                            OrgNo: OrgNo,
                            ID: ids,
                            SysAccountNo: SysAccountNo,
                            LoginName: "<%=UserInfo.LoginName%>"
                        },
                        success: function (json) {
                            if (json.result.errNum == 0) {
                                layer.msg("修改成功");
                                getData(1, 18);
                                CloseIFrameWindow();
                            }
                            else {
                                layer.msg(json.result.errMsg);
                            }
                        },
                        error: function (ErroMsg) {
                            layer.msg(ErroMsg);
                        }
                    })
                }
            }
        }
    </script>
    <%--组织机构相关--%>
    <%--<script type="text/javascript">
        var zNodes = [];

        var setting = {
            view: {
                showLine: false,
                showIcon: false,
                selectedMulti: false,
                dblClickExpand: false,
                addDiyDom: addDiyDom
            },
            edit: {
                editNameSelectAll: true,
            },
            data: {
                //keep: {
                //    parent: true,
                //    leaf: true
                //},
                simpleData: {
                    enable: true
                }
            },
            callback: {
                beforeDrag: beforeDrag,
                beforeEditName: beforeEditName,
                beforeRemove: beforeRemove,
                beforeRename: beforeRename,
                onRemove: onRemove,
                //onRename: onRename,
                beforeClick: beforeClick,
                onClick: onClick
            }
        };

        var log, className = "dark";
        function beforeClick(treeId, treeNode, clickFlag) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeClick ]&nbsp;&nbsp;" + treeNode.name);
            return (treeNode.click != false);
        }
        function addDiyDom(treeId, treeNode) {
            var spaceWidth = 5;
            var switchObj = $("#" + treeNode.tId + "_switch"),
			icoObj = $("#" + treeNode.tId + "_ico");
            switchObj.remove();
            icoObj.before(switchObj);

            if (treeNode.level > 1) {
                var spaceStr = "<span style='display: inline-block;width:" + (spaceWidth * treeNode.level) + "px'></span>";
                switchObj.before(spaceStr);
            }
        }
        function onClick(event, treeId, treeNode, clickFlag) {
            $("#HOrgNo").val(treeNode.org);
            getData(1, 10);
            CensusActiveUser();
            UseUserNum();
            GetOrgByID(treeNode.id);
        }
        function GetOrgByID(ID) {
            $('#editmes_success').show();
            $('#log_wrap').show();
            $('#editmes').hide();
            var oDate = new Date();
            var getTime = oDate.getTime();
            $.ajax({
                url: "../common.ashx?time=" + getTime,
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/Organiz/Organiz.ashx",
                    Func: "GetData",
                    ID: ID,
                    Ispage: false
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {

                            $("#OrganName").html(this.Name);
                            var Type = this.OrganType;
                            //0 学校；1企业；2子组织机构 ；3部门

                            switch (Type) {
                                case "0":
                                    $("#OrganType").html("学校");
                                    break;
                                case "1":
                                    $("#OrganType").html("企业");
                                    break;
                                case "2":
                                    $("#OrganType").html("组织机构");
                                    break;
                                case "3":
                                    $("#OrganType").html("部门");
                                    break;
                                default:
                                    $("#OrganType").html("学校");
                                    break;
                            }

                        })
                    }
                },
                error: function (errMsg) {
                }
            });
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

        function beforeDrag(treeId, treeNodes) {
            return false;
        }
        function beforeEditName(treeId, treeNode) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeEditName ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name);
            var zTree = $.fn.zTree.getZTreeObj("treeMenu");
            zTree.selectNode(treeNode);
        }
        function beforeRemove(treeId, treeNode) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeRemove ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name);
            var zTree = $.fn.zTree.getZTreeObj("treeMenu");
            zTree.selectNode(treeNode);
            return confirm("确认删除 节点 -- " + treeNode.name + " 吗？");
        }

        function beforeRename(treeId, treeNode, newName, isCancel) {

            var newname = newName.trim();
            if (treeNode.id == "" && !newname.length) {
                var zTree = $.fn.zTree.getZTreeObj("treeMenu");
                zTree.removeNode(treeNode);
            }
            return newname.length > 0;
        }

        //编辑节点
        function onRename(event, treeId, treeNode, isCancel) {
            EditTree(treeNode.name, treeNode.id.toString(), '', '');
        }
        var newCount = 1;

        function removeHoverDom(treeId, treeNode) {
            $("#addBtn_" + treeNode.tId).unbind().remove();
        };
        function onRemove(e, treeId, treeNode) {
            DelMenu(treeNode.id);
        }
        function EditName() {
            var zTree = $.fn.zTree.getZTreeObj("treeMenu");
            var treeNode = zTree.getSelectedNodes()[0];
            if (treeNode != null) {
                $(".tool_left").hide();
                var treeId = treeNode.tId;
                beforeEditName(treeId, treeNode);
                $("#" + treeId).find("a").addClass("curSelectedNode_Edit");
                var Name = $("#" + treeId).find("a").children("#" + treeId + "_span").html();
                $("#" + treeId).find("a").children("#" + treeId + "_span").html("<input class=\"rename\" id=\"" + treeId
                    + "_input\" type=\"text\" treenode_input=\"\" value=\"" + Name + "\" onblur=\"SavaChange('" + treeId + "'," + treeNode.id + ")\"></input>")
                //onRename(null, treeId, treeNode, true);
                $("#" + treeId + "_input").focus();
            }
            else {
                layer.msg("请选择要编辑的节点");
            }
        }
        function SavaChange(treeId, id) {
            var Name = $("#" + treeId + "_input").val();
            $(".tool_left").show();
            EditTree(Name, id, '')
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
        var newCount = 1;
        //增加节点   
        function addHoverDom(OrganType) {
            var Pid = $("#HOrgID").val();
            if (Pid != "0" && OrganType == "2") {
                layer.msg("只有跟几点可以添加子组织机构");

            }
            else {
                $("#HOrganType").val(OrganType);
                var pId = $("#HOrgID").val();
                var zTree = $.fn.zTree.getZTreeObj("treeMenu");
                var treeNode = zTree.getSelectedNodes()[0];
                //var newNodes = zTree.addNodes(treeNode, { id: "", pId: pId, name: "new node" + (newCount++) });
                EditTree("new node" + (newCount++), "", pId);
                //添加到数据库中/
                //zTree.editName(newNodes[0]);
                return false;
            }
        };

        function onClick(event, treeId, treeNode, clickFlag) {
            $("#HOrgNo").val(treeNode.org);
            $("#HOrgID").val(treeNode.id);
            $('#editmes_success').show();
            $('#editmes').hide();
            GetOrgByID(treeNode.id);

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
        function EditTree(Name, id, pid) {
            var pId = "0";
            if ($("#HOrgID").val() != undefined && $("#HOrgID").val() != "") {
                pId = $("#HOrgID").val();
            }
            var func = "AddOrg";// "EditOrgDetail";
            if (id == "" || id == undefined) {
                func = "AddOrg"
            }
            else {
                func = "EditOrgDetail";
            }
            $.ajax({
                type: "Post",
                url: "../common.ashx",
                async: false,
                dataType: "json",
                data: {
                    "PageName": "Organiz/Organiz.ashx",
                    "func": func,
                    "Name": Name,
                    "ID": id,
                    "Pid": pId,
                    "OrganType": $("#HOrganType").val()
                },
                success: function (json) {
                    if (json.result.errNum == "0") {
                        GetNave();
                        //location.reload();
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
        function GetNave() {
            $.ajax({
                type: "get",
                url: "../common.ashx",
                dataType: "json",
                async: false,
                data: { "PageName": "/Organiz/Organiz.ashx", "Func": "GetOrgMenu" },
                success: function (returnVal) {
                    if (returnVal.result.errNum == 0) {
                        var treeObj = $("#treeMenu");
                        $.fn.zTree.init(treeObj, setting, $.parseJSON(returnVal.result.retData));
                        zTree_Menu = $.fn.zTree.getZTreeObj("treeMenu");
                        //zTree_Menu.expandAll(true);
                        var nodes = zTree_Menu.getNodes();
                        zTree_Menu.selectNode(nodes[0]);
                        GetOrgByID(nodes[0].id);
                        $("#HOrgID").val(nodes[0].id);
                    }
                    else {
                        window.location.href = "NoOrganize.aspx";
                    }

                },
                error: function (errMsg) {
                    layer.msg('数据加载失败！');
                }
            });
        };
        // var Introduceeditor;

        function EditOrgDetail() {

            var treeObj = $.fn.zTree.getZTreeObj("treeMenu");
            var nodes = treeObj.getSelectedNodes(true);
            var OrgID = nodes[0].id;
            var ImageInfo = $("#ImageInfo1").attr("src");
            var LegalUID = $("#LegalUID1").val();
            var UserCount = $("#UserCount1").val();
            //var Introduce = Introduceeditor.html();
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
                        ID: OrgID, ImageInfo: ImageInfo, LegalUID: LegalUID, UserCount: UserCount, Introduce: Introduce, EstabLish: EstabLish, OrganNo: OrganNo
                    },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            layer.msg("修改成功！");
                            GetOrgByID(OrgID);
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
       
        $(function () {
            GetNave();
        })
        function DelOrg() {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/Organiz/Organiz.ashx",
                    Func: "DelMenu",
                    ID: $("#HOrgID").val(),
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("删除成功");
                        GetNave();
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
        //上移、下移
        function EditOrder(OrderType) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/Organiz/Organiz.ashx",
                    Func: "EditOrder",
                    ID: $("#HOrgID").val(),
                    OrderType: OrderType
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("操作成功");
                        GetNave();
                        //location.reload();
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
        /*
        $(function () {
            $.ajax({
                type: "get",
                url: "../common.ashx",
                dataType: "json",
                data: { "PageName": "/Organiz/Organiz.ashx", "Func": "GetOrgMenu" },
                success: function (returnVal) {
                    var treeObj = $("#treeMenu");
                    $.fn.zTree.init(treeObj, setting, $.parseJSON(returnVal.result.retData));
                    zTree_Menu = $.fn.zTree.getZTreeObj("treeMenu");
                    zTree_Menu.expandAll(true);
                    var nodes = zTree_Menu.getNodes();
                    zTree_Menu.selectNode(nodes[0]);
                    $("#HOrgNo").val(nodes[0].org);
                    treeObj.hover(function () {
                        if (!treeObj.hasClass("showIcon")) {
                            treeObj.addClass("showIcon");
                        }
                    }, function () {
                        treeObj.removeClass("showIcon");
                    });

                    getData(1, 10);
                },
                error: function (errMsg) {
                    alert('数据加载失败！');
                }
            });
        });*/

    </script>--%>
</body>
</html>
