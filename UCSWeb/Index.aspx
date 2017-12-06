<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UCSWeb.Index" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>首页</title>
    <link rel="stylesheet" href="css/reset.css" />
    <link rel="stylesheet" href="css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link href="Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <link href="Scripts/layer/skin/layer.css" rel="stylesheet" />
    <link href="Scripts/zTree/css/Common.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script src="Scripts/menu_top.js"></script>
    <script src="Scripts/zTree/js/jquery.ztree.core-3.5.js"></script>
    <script src="Scripts/zTree/js/jquery.ztree.excheck-3.5.js"></script>
    <script src="Scripts/zTree/js/jquery.ztree.exedit-3.5.js"></script>
    <script type="text/javascript" src="Scripts/echarts-all.js"></script>
    <script src="Scripts/jquery.tmpl.js"></script>
    <script src="Scripts/layer/layer.js"></script>
    <script src="Scripts/jquery.cookie.js"></script>
    <script src="Scripts/Common.js"></script>
    <style type="text/css">
        .echarts-tooltip {
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('.footer—wrap').load('CommonPage/footer.html');
            $('.header_wrap').load('CommonPage/header1.aspx');
            //$('.menu_wrap').load('/CommonPage/menu.html');
        })
    </script>
    <script type="text/x-jquery-tmpl" id="tr_User">
        <tr>
            <td>${pageIndex()}</td>
            <td>${Name} </td>
            <td>${LoginName}</td>
            <td>{{if Sex==0}}女
                {{else}}男
                {{/if}}</td>
            <td>${IDCard}</td>
            <td>${OrgName}</td>
            <td>{{if AuthenType==3}}<span class="colororange">审核失败</span>
                {{else}}<span class="colorred">新用户注册</span>
                {{/if}}               
            </td>
            <td>
                <div class="handle">
                    <i class="iconfont">&#xe67e;</i>
                    <span onclick="CheckUser(${Id})">审核</span>
                </div>
            </td>
        </tr>
    </script>
    <style>
        .menu_wrap .tool_left span {
            margin-top: 8px;
            margin-right: 0;
            margin-left: 8px;
            padding: 5px 8px;
        }
    </style>
</head>
<body>
    <input type="hidden" id="Authented" />
    <input type="hidden" id="NoAuthent" />
    <!--header-->
    <div class="header_wrap" style="height: 61px;"></div>
    <div id="main">
        <div class="w1200 clearfix wrap pr">
            <div class="menu_wrap fl none">
                <ul class="ztree" id="treeMenu"></ul>
            </div>
            <div class="content" style="width: 1198px; border-left: 1px solid #DDECFA">
                <div class="content_wrap">
                    <div class="pie_wrap clearfix pr">
                        <div id="pie" style="width: 450px; height: 270px; position: absolute; left: 0; top: 0;"></div>
                        <ul class="lei_lists clearfix">
                            <li>
                                <a href="/UserManage/UserManagement.aspx">
                                    <i class="iconfont icon-outgit colorblue"></i>
                                    <p class="name" id="Type1"></p>
                                </a>
                            </li>
                            <li>
                                <a href="/UserManage/UserManagement.aspx">
                                    <i class="iconfont icon-depart colorgreen"></i>
                                    <p class="name" id="Type2"></p>
                                </a>
                            </li>
                            <li>
                                <a href="/UserManage/UserManagement.aspx">
                                    <i class="iconfont icon-active-group colororange"></i>
                                    <p class="name" id="ActiveUser"></p>
                                </a>
                            </li>
                        </ul>
                    </div>
                    <div class="title_nav clearfix">
                        <div class="title_nav_left fl">
                            <a href="javascript:;" class="active">新增用户</a>
                        </div>
                        <div class="title_nav_right fr">
                            <span id="PNum"></span>
                        </div>
                    </div>
                    <div class="table_wrap mt10">
                        <table>
                            <thead>
                                <tr>
                                    <th>编号<i class="iconfont colorgreen" style="font-size: 8px; transform: rotate(180deg); display: inline-block;">&#xe62b;</i></th>
                                    <th>姓名</th>
                                    <th>账号</th>
                                    <th>姓别</th>
                                    <th>身份证号</th>
                                    <th>组织机构</th>
                                    <th>状态</th>
                                    <th>操作</th>
                                </tr>
                            </thead>
                            <tbody id="tbUser">
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="footer—wrap" style="height: 40px;"></div>
    <script type="text/javascript">
        (function init() {
            var height = $(window).height() - 101;
            $(window).resize(function () {
                $('.menu_wrap').height(height);
            })
            $('.wrap').css('minHeight', height);
            $('.content').css('minHeight', height);
        })();


        $(function () {
            if ($.cookie('LoginCookie_Author') == null && $.cookie('LoginCookie_Author') == "null" && $.cookie('LoginCookie_Author') == "") {
                window.location.href = "Login_unity.aspx";
            }
            UseUserNum("2");
            UseUserNum("0,1,3");

            getData();
            AnalisyOrg("0,1,2");
            AnalisyOrg("3");
            CensusActiveUser();
            $('.handle').hover(function () {
                $(this).find('span').slideDown();
            }, function () {
                $(this).find('span').stop().slideUp();
            })
            var myChart = echarts.init(document.getElementById('pie'));
            option = {
                tooltip: {
                    show: true,
                    showContent: true,
                    trigger: 'item',
                    borderColor: '#E5E5E5',
                    padding: 20,
                    borderWidth: 2,
                    enterable: false,
                    backgroundColor: '#fff',
                    borderRadius: 3,
                    textStyle: {
                        color: '#333',
                        decoration: 'none',
                        fontFamily: 'Arial, Verdana, sans...',
                        fontSize: 12,
                        fontStyle: 'normal',
                        fontWeight: 'normal',
                    },

                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },
                color: ['#55A1E8', '#9AD35B'],
                calculable: false,
                series: [
                    {
                        name: '用户状态',
                        type: 'pie',
                        radius: ['50%', '70%'],
                        itemStyle: {
                            normal: {
                                label: {
                                    show: false
                                },
                                labelLine: {
                                    show: false
                                }
                            },
                            emphasis: {
                                label: {
                                    show: true,
                                    position: 'center',
                                    textStyle: {
                                        fontSize: '30',
                                        fontWeight: 'bold'
                                    }
                                }
                            }
                        },
                        data: [
                          { value: $("#NoAuthent").val(), name: '未激活' },
                            { value: $("#Authented").val(), name: '已激活' },
                        ]
                    }
                ]
            };

            // 使用刚指定的配置项和数据显示图表。
            myChart.setOption(option);
        })

        function CheckUser(ID) {
            OpenIFrameWindow("用户审核", "UserManage/CheckUser.aspx?ID=" + ID, "400px", "230px")
        }
        //获取用户信息
        function getData() {
            //初始化序号 
            $.ajax({
                url: "Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "UserManage/UserInfo.ashx",
                    Func: "GetData",
                    IsPage: "false",
                    AuthenType: "0,3",
                    //IsStu: "false"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#tbUser").html('');
                        $("#tr_User").tmpl(json.result.retData).appendTo("#tbUser");
                        $("#PNum").html("共" + json.result.retData.length + "人");
                    }
                    else {
                        $("#tbUser").html("<tr><td colspan='100'>暂无新用户！</td></tr>");
                    }
                },
                error: function (errMsg) {
                    $("#tbUser").html("<tr><td colspan='100'>暂无新用户！</td></tr>");
                }
            });
        }
        function AnalisyOrg(OrgType) {
            $.ajax({
                url: "Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "Organiz/Organiz.ashx",
                    func: "CensusOrg",
                    OrgType: OrgType,
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        if (OrgType == "3") {
                            $("#Type2").html("部门" + json.result.retData + "个");
                        }
                        else {
                            $("#Type1").html("机构" + json.result.retData + "个");
                        }
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
        function CensusActiveUser() {
            $.ajax({
                url: "Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    func: "CensusActiveUser",
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        $("#ActiveUser").html("活跃人数" + json.result.retData + "个");

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
        function UseUserNum(AuthenType) {
            $.ajax({
                url: "Common.ashx",
                type: "post",
                dataType: "json",
                async: false,
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    func: "CensusUser",
                    AuthenType: AuthenType,
                },
                success: function (json) {
                    if (json.result.errNum == 0) {
                        if (AuthenType == "2") {
                            $("#Authented").val(json.result.retData);
                        }
                        else {
                            $("#NoAuthent").val(json.result.retData);
                        }

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
    </script>
   
    <script type="text/javascript">

        var second = 0;
        window.setInterval(function () {
            second++;
        }, 1000);

        window.onbeforeunload = function () {
            var ToUrl = location.href;
            var SkinLong = second;
            var FromUrl = getReferrer();
            var LoginCookie = $.cookie('LoginCookie_Author');
            var UniqueNo = '';
            var LoginName = '';
            if (LoginCookie != undefined && LoginCookie != 'null' && LoginCookie != "" && LoginCookie != null) {
                LoginCookie = decodeURIComponent(LoginCookie);
                UniqueNo = window.JSON.parse(LoginCookie)["UniqueNo"];
                LoginName = window.JSON.parse(LoginCookie)["LoginName"];
            }
            $.ajax({
                url: "common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "UserSkinHander.ashx",
                    Func: "AddSkim",
                    UniqueNo: UniqueNo,
                    ToUrl: ToUrl,
                    SkinLong: SkinLong,
                    FromUrl: FromUrl,
                    LoginName: LoginName,
                    SkinLong: SkinLong,
                    WebSite: "UnifiedCertificationCenter_System"
                },
                success: function (json) {
                    //alert(json);
                },
                error: function (errMsg) {
                    //alert(errMsg);
                }
            });
        };
        $(function () {
            var LoginCookie = $.cookie('LoginCookie_Author');
            var UniqueNo = '';
            var LoginName = '';
            if (LoginCookie != undefined && LoginCookie != 'null' && LoginCookie != "" && LoginCookie != null) {
                LoginCookie = decodeURIComponent(LoginCookie);
                UniqueNo = window.JSON.parse(LoginCookie)["UniqueNo"];
                LoginName = window.JSON.parse(LoginCookie)["LoginName"];
            }
            SetPageButton(UniqueNo.toString().trim(), LoginName.toString().trim());
        });
        //获取URL参数
        function GetUrlDate(str) {
            str = arguments[0] || location.href;
            var name, value;
            var num = str.indexOf("?")
            str = str.substr(num + 1); //取得所有参数   stringvar.substr(start [, length ]

            var arr = str.split("&"); //各个参数放到数组里
            for (var i = 0; i < arr.length; i++) {
                num = arr[i].indexOf("=");
                if (num > 0) {
                    name = arr[i].substring(0, num);
                    value = arr[i].substr(num + 1);
                    this[name] = value;
                }
            }
        }

        function SetPageButton(curUniqueNo, curLoginName, element) {
            element = arguments[2] || "span";
            var menucode = '';
            var UrlDate = new GetUrlDate();
            if (UrlDate && UrlDate.btncls) { menucode = UrlDate.btncls }
            var host = 'http://' + window.location.host;
            var href = window.location.href;
            var cururl = href.split(host)[1];
            if (cururl.indexOf("?") != -1) {
                var queindex = cururl.lastIndexOf("?");
                cururl = cururl.substring(0, queindex);
            }
            if (cururl.indexOf(".") == -1 || cururl.indexOf("Login_unity.aspx") != -1 || cururl.indexOf("CommonPage/header.aspx") != -1 || cururl.indexOf("CommonPage/SystemMenu.aspx") != -1) {
                return;
            }
            if (curUniqueNo == "") {
                window.location.href = "Login_unity.aspx";
            }
            else {
                $.ajax({
                    url: "common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/SystemSettings/MenuHandler.ashx",
                        Func: "GetSubButtonByUrl",
                        Url: cururl,
                        UniqueNo: curUniqueNo,
                        MenuCode: menucode,
                        SysAccountNo: SysAccountNo,
                        LoginName: curLoginName
                    },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            var curpage = json.result.retData[0];
                            var btnfield = curpage.ButtonField;
                            if (btnfield != undefined && btnfield.length) {
                                var btnArray = btnfield.split(',');
                                for (var btn in btnArray) {
                                    var curbtn = btnArray[btn].split('|');
                                    var $spanObj = $(element + "[btncls='" + curbtn[2] + "']");
                                    if ($spanObj) {
                                        $spanObj.show();
                                    }
                                }
                            }
                        } else if (json.result.errMsg != "1") {
                            window.location.href = "CommonPage/NoPower.html";
                            //if (confirm("无权限访问,是否跳转登录页面")) {
                            //    window.location.href = "/Login_unity.aspx";
                            //    return;
                            //}
                            //else {
                            //    window.location.href = "/CommonPage/NoPower.html";
                            //    return;
                            //}
                        }
                    },
                    error: function (errMsg) {
                        layer.msg(errMsg);
                    }
                });
            }
        }
        function SetPageButton_Back(btnfield) {
            alert(btnfield);
            if (btnfield != undefined && btnfield.length) {
                var btnArray = btnfield.split(',');
                for (var btn in btnArray) {
                    var curbtn = btnArray[btn].split('|');
                    var $spanObj = $("span[btncls='" + curbtn[2] + "']");
                    if ($spanObj) {
                        $spanObj.show();
                    }
                }
            }
        }
        function getReferrer() {
            var referrer = '';
            try {
                referrer = window.top.document.referrer;
            } catch (e) {
                if (window.parent) {
                    try {
                        referrer = window.parent.document.referrer;
                    } catch (e2) {
                        referrer = '';
                    }
                }
            }
            if (referrer === '') {
                referrer = document.referrer;
            }
            return referrer;
        }
    </script>
</body>
</html>
