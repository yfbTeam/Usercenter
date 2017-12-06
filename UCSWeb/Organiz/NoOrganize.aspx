<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoOrganize.aspx.cs" Inherits="UCSWeb.Organiz.NoOrganize" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>组织结构管理</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script src="../Scripts/menu_top.js"></script>
    <script src="../Scripts/Common.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.footer—wrap').load('../CommonPage/footer.html');
            $('.header_wrap').load('/CommonPage/header.html');
        })
    </script>
    <style>
        .group_wrap .tool_left span {
            margin-top: 8px;
            margin-right: 0;
            margin-left: 8px;
            padding: 5px 8px;
        }

        .group_wrap .tool_left {
            position: fixed;
        }

        .add_organize {
            width: 325px;
            margin-left: 100px;
            position: relative;
        }

        .add_organizebtn {
            width: 100%;
            height: 100%;
            position: absolute;
            background: rgba(151,191,229,0.85);
            top: 0;
            left: 0;
        }

            .add_organizebtn a {
                display: block;
                text-align: center;
                margin-top: 180px;
            }

                .add_organizebtn a i {
                    display: block;
                    width: 72px;
                    height: 72px;
                    border: 3px solid #fff;
                    background: url(/images/add.png) no-repeat center;
                    margin: 0 auto;
                    border-radius: 50%;
                }

                .add_organizebtn a p {
                    font-size: 24px;
                    color: #fff;
                    line-height: 80px;
                }

        .group_wrap {
            width: 318px;
            border-left: 1px solid #DEEBFB;
            border-right: 1px solid #DEEBFB;
            margin-right: 55px;
            background: #fafdff;
        }

        .add_oran {
            padding: 44px 10px 10px 10px;
        }

        .dui {
            width: 32px;
            height: 32px;
            display: block;
            background: url(/images/duicuo.png) no-repeat 0px 2px;
            float: left;
            cursor: pointer;
        }

        .cuo {
            width: 32px;
            height: 32px;
            display: block;
            background: url(/images/duicuo.png) no-repeat 0px -32px;
            float: left;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <div class="header_wrap" style="height: 61px;"></div>
    <div id="main">
        <div class="w1200 clearfix wrap pr">
            <div class="menu_wrap fl" style="width: 278px;">
                <div class="nouser">
                    <img src="images/nogroup.jpg" alt="">
                    <p>
                        等你好久了，快快添加组织机构吧<br />
                        点我触发哦
                    </p>
                </div>
            </div>
            <div class="content fr clearfix" style="width: 919px;">
                <div class="add_organize fl none">
                    <div class="nouser">
                        <img src="images/nogroup.jpg" alt="">
                        <p>
                            等你好久了，快快添加组织机构吧<br />
                            点我触发哦
                        </p>
                    </div>
                    <div class="add_organizebtn">
                        <a href="javascript:;" id="addgroup">
                            <i></i>
                            <p>添加组织机构</p>
                        </a>
                    </div>
                </div>
                <div class="group_wrap fr none">
                    <div class="tool_left clearfix">
                        <span btncls="icon-plus2" style="display:none;" id="Add" onclick="addHoverDom()">添加</span>
                        <span btncls="icon-edit2" style="display:none;" id="Edit">编辑</span>
                        <span btncls="icon-del2" style="display:none;" id="Del">删除</span>
                        <span btncls="icon-moveup" style="display:none;" id="up">上移</span>
                        <span btncls="icon-movedown" style="display:none;" id="down">下移</span>
                    </div>
                    <div class="add_oran">
                        <ul>
                            <li>
                                <input type="text" name="name" id="Name" placeholder="请输入组织机构名称" class="text fl" /><span class="dui" onclick="AddOrg()"></span><%--<span class="cuo"></span>--%></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer—wrap" style="height: 40px;"></div>
    <script>
        (function init() {
            var height = $(window).height() - 101;
            $('.menu_wrap').height(height);
            $('.add_organize').height(height);
            $(window).resize(function () {
                $('.menu_wrap').height(height);
                $('.add_organize').height(height);
            })
            $('.wrap').css('minHeight', height);
            $('.content').css('minHeight', height);
            $('.group_wrap').css('minHeight', height);
        })();
        $(function () {
            $('.nouser').hover(function () {
                $('.add_organize').fadeIn();
            });
            $('#addgroup').click(function () {
                $('.group_wrap').fadeIn();
            });
        });
        function AddOrg() {
            $.ajax({
                type: "Post",
                url: "../common.ashx",
                async: false,
                dataType: "json",
                data: {
                    "PageName": "/Organiz/Organiz.ashx",
                    "func": "AddOrg",
                    "Name": $("#Name").val(),
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>",
                    "Pid": "0"
                },
                success: function (json) {
                    if (json.result.errNum == "0") {
                        window.location.href = "OrganizManag.aspx";
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
