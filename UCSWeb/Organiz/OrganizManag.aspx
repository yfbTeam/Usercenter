<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrganizManag.aspx.cs" Inherits="UCSWeb.Organiz.OrganizManag" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>组织结构管理</title>
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

    <script id="tr_list" type="text/x-jquery-tmpl">
        <tr>
            <td>${OperationMsg}</td>
            <td>${DateTimeConvert(CreateTime)}</td>
        </tr>
    </script>
    <script type="text/javascript">
        $(function () {
            $('.footer—wrap').load('../CommonPage/footer.html');
            $('.header_wrap').load('../CommonPage/header.aspx');
        })
    </script>
    <style>
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
                z-index: 999;
            }

            #Add .addDom_none {
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
</head>
<body>
    <input type="hidden" id="HOrgNo" />
    <input type="hidden" id="HOrgID" value="0" />
    <div class="header_wrap" style="height: 61px;"></div>
    <input type="hidden" id="HOrganType" />
    <!--header-->

    <div id="main">
        <div class="w1200 clearfix wrap pr">
            <div class="menu_wrap fl" style="width: 298px;">
                <div class="tool_left clearfix">
                    <span btncls="icon-plus2" style="display:none;" id="Add">
                        <em>添加 <i class="iconfont">&#xe6d4;</i></em>
                        <div class="addDom_none">
                            <a href="javascript:;" onclick="addHoverDom(3)">添加部门</a>
                            <a href="javascript:;" onclick="addHoverDom(2)">添加子组织机构</a>
                        </div>
                    </span>
                    <span btncls="icon-edit2" style="display:none;" id="Edit" onclick="EditName()">编辑</span>
                    <span btncls="icon-del2" style="display:none;" id="Del" onclick="DelOrg()">删除</span>
                    <span btncls="icon-moveup" style="display:none;" id="up" onclick="EditOrder('up')">上移</span>
                    <span btncls="icon-movedown" style="display:none;" id="down" onclick="EditOrder('down')">下移</span>
                </div>
                <ul class="ztree" id="treeMenu"></ul>
            </div>
            <div class="content fr" style="width: 899px;">
                <div class="content_wrap">
                    <div class="nomes_wrap none" onclick="editMes()">
                        <img src="images/nogroupmes.jpg" alt="">
                        <p>等你好久了，快快完善组织机构信息吧，点击完善</p>
                    </div>
                    <div class="title_nav clearfix ">
                        <div class="title_nav_left fl">
                            <a href="javascript:;" class="active" id="Name"></a>
                        </div>
                        <div class="fl oran bgblue" id="OrganType">
                        </div>
                        <div class="title_nav_right fr">
                            <a href="javascript:;" onclick="editMes();"><i class="iconfont">&#xe61b;</i></a>
                        </div>
                    </div>
                    <div id="editmes_success">
                        <div class="article">
                            <h1>组织机构介绍</h1>
                            <p id="Introduce"></p>
                            <h2 class="clearfix"><span class="fl" id="LegalUID">机构法人：</span>
                                <span class="fl ml20" id="EstabLish">成立时间：</span><span class="fl ml20" id="OrganNo1">组织机构号：</span><span class="fr" id="UserCount">用户数：230人</span></h2>
                            <p>
                                <img src="images/company.jpg" alt="" id="ImageInfo" />
                            </p>
                        </div>
                    </div>
                    <div id="editmes" class="none">
                        <div class="row_dia clearfix">
                            <label for="" class="row_label fl">机构法人:</label>
                            <div class="row_content">
                                <input type="text" class="text" placeholder="机构法人" id="LegalUID1" />
                            </div>
                        </div>
                        <div class="row_dia clearfix">
                            <label for="" class="row_label fl">组织机构号:</label>
                            <div class="row_content">
                                <input type="text" class="text" placeholder="组织机构号" id="OrganNo" />
                            </div>
                        </div>
                        <div class="row_dia clearfix">
                            <label for="" class="row_label fl">成立时间:</label>
                            <div class="row_content">
                                <input type="text" class="text Wdate" id="EstabLish1" value="" placeholder="成立时间" onclick="javascript: WdatePicker({ dateFmt: 'yyyy-MM-dd' });">
                            </div>
                        </div>
                        <div class="row_dia clearfix">
                            <label for="" class="row_label fl">公司人数:</label>
                            <div class="row_content">
                                <input type="text" class="text" placeholder="公司人数" id="UserCount1" />
                            </div>
                        </div>
                        <div class="row_dia clearfix">
                            <label for="" class="row_label fl">组织机构简介:</label>
                            <div class="row_content">
                                <textarea id="editor_id" name="content" style="width: 100%; height: 180px;"></textarea>
                            </div>
                        </div>
                        <div class="row_dia clearfix">
                            <label for="" class="row_label fl">上传图片:</label>
                            <div class="row_content">
                                <img src="../images/company.jpg" id="ImageInfo1" />
                                <div class="upload_imga">
                                    <input type="file" name="" id="uploadify" multiple="multiple" />
                                </div>
                            </div>
                        </div>
                        <div class="btn_s">
                            <div class="btn_wrap">
                                <input type="button" name="" id="" value="确定" class="btns insert" onclick="EditOrgDetail()">
                                <input type="button" name="" id="" value="取消" class="btns cancel" onclick="Reset()">
                            </div>
                        </div>
                    </div>
                    <div id="log_wrap">
                        <div class="title_nav clearfix">
                            <div class="title_nav_left fl">
                                <a href="javascript:;" class="active">操作日志</a>
                            </div>
                            <div class="title_nav_right fr">
                                <span id="LogNum"></span>
                            </div>
                        </div>
                        <div class="table_wrap mt10">
                            <table>
                                <thead>
                                    <tr>
                                        <th>内容</th>
                                        <th>时间</th>
                                    </tr>
                                </thead>
                                <tbody id="tb_list">
                                </tbody>
                            </table>
                            <div class="page clearfix">
                                <div id="pageBar"></div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="footer—wrap" style="height: 40px;"></div>
    <script>
        function Reset() {
            $('#editmes').hide();
            if ($("#OrganNo1").html().length == 6) {
                $(".nomes_wrap").show();
            }
            else {
                var treeObj = $.fn.zTree.getZTreeObj("treeMenu");
                var nodes = treeObj.getSelectedNodes(true);
                var OrgID = nodes[0].id;

                GetOrgByID(OrgID);
                $('#editmes_success').show();
                $('#log_wrap').show();
            }
        }
        var Introduceeditor;
        $(function () {
            //上传
            $("#uploadify").uploadify({
                'auto': true,                      //是否自动上传
                'swf': '../Scripts/Uploadyfy/uploadify/uploadify.swf',
                'uploader': '/UserManage/Uploade.ashx',
                'formData': { Func: "UplodComponyImg" }, //参数
                'fileTypeExts': '*.png;*.jpg;*.jpeg;',
                'buttonText': '选择图片',//按钮文字
                // 'cancelimg': 'uploadify/uploadify-cancel.png',
                'width': 90,
                'height': 30,
                //最大文件数量'uploadLimit':
                'multi': false,//单选            
                'fileSizeLimit': '10MB',//最大文档限制
                'queueSizeLimit': 1,  //队列限制
                'removeCompleted': true, //上传完成自动清空
                'removeTimeout': 0, //清空时间间隔
                //'overrideEvents': ['onDialogClose', 'onUploadSuccess', 'onUploadError', 'onSelectError'],
                'onUploadSuccess': function (file, data, response) {
                    var json = $.parseJSON(data);
                    $("#ImageInfo1").attr("src", json.url);
                },

            });
            //富文本编辑器
            KindEditor.ready(function (K) {
                Introduceeditor = K.create('#editor_id', {
                    uploadJson: '../../Handler/UploadImage.ashx?action=UploadImgForAdvertContent',
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
                    }
                });
            });

        })
        //编辑
        function editMes() {
            $('#editmes_success').hide();
            $('#editmes').show();
            $('#log_wrap').hide();
            $(".nomes_wrap").hide();
        }
        //确定取消
        function cancelConfirm() {
            $('#editmes_success').show();
            $('#editmes').hide();
            $('#log_wrap').show();

        }
        ////////////////////////////////////////
        (function init() {
            var height = $(window).height() - 101;
            $('.menu_wrap').height(height);
            $(window).resize(function () {
                $('.menu_wrap').height(height);
            })
            $('.wrap').css('minHeight', height);
            $('.content').css('minHeight', height);
        })();
        $(function () {
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
            getLog(1, 10);
        })
        function getLog(startIndex, pageSize) {
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/LogInfoHandler.ashx",
                    Func: "GetLogInfoDataPage",
                    OperationObj: "Org_Mechanism",
                    PageIndex: startIndex,
                    pageSize: pageSize,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#tb_list").html('');
                        var rtnObj = json.result.retData;
                        $("#tr_list").tmpl(rtnObj.PagedData).appendTo("#tb_list");
                        $("#pageBar").show();
                        makePageBar(getLog, document.getElementById("pageBar"), rtnObj.PageIndex, rtnObj.PageCount, pageSize, rtnObj.RowCount);
                        $("#LogNum").html("共" + rtnObj.RowCount + "条")
                    }
                    else {
                        $("#tb_list").html("<tr><td colspan='5'>暂无系统日志！</td></tr>");
                        $("#pageBar").hide();
                    }
                },
                error: function (errMsg) {
                    $("#tb_list").html("<tr><td colspan='5'>暂无系统日志！</td></tr>");
                }
            });
        }
    </script>


    <%--组织机构相关--%>
    <script type="text/javascript">

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


        //function beforeClick(treeId, treeNode) {
        //    if (treeNode.level == 0) {
        //        var zTree = $.fn.zTree.getZTreeObj("treeMenu");
        //        zTree.expandNode(treeNode);
        //        return false;
        //    }
        //    return true;
        //}
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
            if (Pid != "0" || OrganType == "2") {
                layer.msg("只有根节点点可以添加子组织机构");
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
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
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
                data: {
                    "PageName": "/Organiz/Organiz.ashx", SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    "Func": "GetOrgMenu"
                },
                success: function (returnVal) {
                    if (returnVal.result.errNum == 0) {
                        var treeObj = $("#treeMenu");
                        $.fn.zTree.init(treeObj, setting, $.parseJSON(returnVal.result.retData));
                        zTree_Menu = $.fn.zTree.getZTreeObj("treeMenu");
                        zTree_Menu.expandAll(true);
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
        function EditOrgDetail() {

            var treeObj = $.fn.zTree.getZTreeObj("treeMenu");
            var nodes = treeObj.getSelectedNodes(true);
            var OrgID = nodes[0].id;
            var ImageInfo = $("#ImageInfo1").attr("src");
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
        function GetOrgByID(ID) {
            $('#editmes_success').show();
            $('#log_wrap').show();
            $('#editmes').hide();
            var oDate = new Date();
            var getTime = oDate.getTime();
            $.ajax({
                url: "../common.as?time=" + getTime,
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
                            if (this.OrganNo == "" || this.OrganNo == undefined) {
                                $(".nomes_wrap").show();
                                $(".title_nav").hide();
                                $("#editmes_success").hide();
                                $("#log_wrap").hide();
                            }
                            else {
                                $(".nomes_wrap").hide();
                                $(".title_nav").show();
                                $("#editmes_success").show();
                                $("#log_wrap").show();
                            }
                            $("#Name").html(this.Name);
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
                            $("#LegalUID").html("机构法人：" + this.LegalUID);
                            $("#EstabLish").html("成立时间：" + DateTimeConvert(this.EstabLish, "年月日"));
                            $("#LegalUID1").val(this.LegalUID);
                            $("#EstabLish1").val(DateTimeConvert(this.EstabLish, "yyyy-MM-dd"));
                            $("#OrganNo1").html("组织机构号：" + this.OrganNo);
                            $("#OrganNo").val(this.OrganNo);
                            $("#ImageInfo").attr("src", this.ImageInfo);
                            $("#ImageInfo1").attr("src", this.ImageInfo);
                            $("#Introduce").html(this.Introduce);
                            $("#UserCount").html("用户数：" + this.UserCount + "人");
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
</body>
</html>
