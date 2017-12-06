<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemMenu.aspx.cs" Inherits="UCSWeb.CommonPage.SystemMenu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>首页</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <style>
        .menu ul li h1.selected {
            color: #fff;
            background: #AAD0F3;
        }

            .menu ul li h1.selected i {
                color: #fff;
            }

        .menu ul li h1 a {
            width: 100%;
            display: block;
            color: #666;
        }

        .menu ul li.active h1:before {
            background: #fff;
        }

        .menu ul li.active h1 a {
            color: #fff;
        }

        .menu ul li h1:hover {
            color: #fff;
            background: #AAD0F3;
        }

            .menu ul li h1:hover .iconfonta {
                color: #fff;
            }

            .menu ul li h1:hover:before {
                background: #fff;
            }

            .menu ul li h1:hover a {
                color: #fff;
            }
    </style>
</head>
<body>
    <div class="menu menua">
        <ul id="ul_LeftMenu"></ul>
    </div>
    <script type="text/javascript">
        var menu_list = [];
        (function menu() {
            $("#ul_LeftMenu").html('');
            var topparid = $("#hid_leftmenu").val();
            var cururl = location.href;
            var name, value;
            var num = cururl.indexOf("?")
            cururl = cururl.substr(num + 1); //取得所有参数   stringvar.substr(start [, length ]
            var arr = cururl.split("&"); //各个参数放到数组里
            for (var i = 0; i < arr.length; i++) {
                num = arr[i].indexOf("=");
                if (num > 0) {
                    name = arr[i].substring(0, num);
                    if (name == "p") {
                        topparid = arr[i].substr(num + 1);
                        $("#hid_leftmenu").val(topparid);
                        break;
                    }
                }
            }
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/MenuHandler.ashx",
                    Func: "GetNavigationMenu",
                    UniqueNo: "<%=UniqueNo%>",
                    Pid: topparid,
                    IsAllLeaf: true,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        menu_list = json.result.retData;
                        BindLeftMenu(topparid);
                        $('.menu').find('li:has(ul)').children('h1').click(function () {
                            var $next = $(this).next('ul');
                            if ($next.is(':hidden')) {
                                $(this).parent().siblings().children().removeClass('selected');
                                $(this).addClass('selected');
                                $next.stop().slideDown();
                                if ($(this).parent('li').siblings('li').children('ul').is(':visible')) {
                                    $(this).parent("li").siblings("li").find("h1").removeClass('selected');
                                    $(this).parent("li").siblings("li").find("ul").slideUp();
                                }
                            } else {
                                $(this).removeClass('selected');
                                $next.stop().slideUp();
                                $(this).next("ul").children("li").find("ul").slideUp();
                                $(this).next("ul").children("li").find("h1").removeClass('selected');
                                $('.menu ul li').removeClass('active');
                            }
                        });
                        var host = 'http://' + window.location.host;
                        var href = window.location.href;
                        var url = href.split(host);
                        $('.menu').find('a').each(function () {
                            var attrhref = $(this).attr('href');
                            if (attrhref == url[1]) {
                                $(this).parent().parent().addClass('active');
                                $(this).parent().parent().parent().slideDown('fast');
                                $(this).parent().parent().parent().prev().addClass('selected');
                            }
                        });
                        //导航选中
                        var topNav = $('#ul_TopMenu li[flag=' + topparid + ']');
                        if (topNav.length) {
                            topNav.addClass('active').siblings().removeClass('active');
                        }
                    }
                    else {
                        layer.msg(json.result.errMsg);
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            })
        })();
        function BindLeftMenu(topparid) {
            var IsHeaderShow = $("#IsHeaderShow").val();
            var href = window.location.href;
            var cururl = href.split('http://' + window.location.host);
            for (var menu in menu_list) {
                var curmenu = menu_list[menu];
                $obj = curmenu.Pid == topparid ? $("#ul_LeftMenu") : $("#ul_menu_" + curmenu.Pid);
                if (curmenu.ChildCount > 0) { //如果有子节点             
                    $obj.append("<li><h1><i class='iconfont iconfonta'>&#xe65c;</i>" + curmenu.Name + "</h1><ul id='ul_menu_" + curmenu.Id + "'></ul></li>");
                }
                else {  //如果该节点没有子节点
                    var $li = $("<li></li>");
                    var p = "?p=";
                    if (IsHeaderShow == "0") {
                        p = "?IsHeaderShow=0&p="
                    }
                    if (curmenu.Url.indexOf("?") > 0) {
                        p = "&p=";
                    }
                    if (curmenu.Pid == topparid) {

                        $li.append("<h1><a href='" + curmenu.Url + p + topparid + "'><i class='iconfont iconfonta'>&nbsp;</i>" + curmenu.Name + "</a></h1>");
                    } else {
                        $li.append("<h1><a href='" + curmenu.Url + p + topparid + "'>" + curmenu.Name + "</a></h1>");
                    }
                    $obj.append($li);
                    if (href.indexOf("?") == -1 && cururl[1] == curmenu.Url) {
                        $li.addClass('active');
                        $li.parent().slideDown('fast');
                        $li.parent().prev().addClass('selected');
                    }
                }
            }
        }
    </script>
</body>
</html>
