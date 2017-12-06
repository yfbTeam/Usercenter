<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="header.aspx.cs" Inherits="UCSWeb.CommonPage.header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>首页</title>

    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/jquery.SuperSlide.2.1.1.js"></script>
</head>
<body>
    <input type="hidden" id="hid_leftmenu" />
    <input type="hidden" id="IsHeaderShow" />

    <header class="header_wrap manage_header">
        <div class="w1200 header clearfix">
            <a href="../Index.aspx" class="logo fl">
                <img src="../images/logo.png" /></a>
            <div class="testsystem  fl"></div>
            <nav class="navbar menu_mid fl">
                <ul id="ul_TopMenu" class="clearfix"></ul>
                <a class="prev" href="javascript:void(0)"><</a>
                <a class="next" href="javascript:void(0)">></a>
            </nav>
            <div class="search_account fr clearfix">
                <ul class="account_area fl">
                    <li>
                        <a href="javascript:;" class="login_area clearfix">
                            <div class="avatar">
                                <%if (!string.IsNullOrWhiteSpace(this.headPic))
                                  { %>
                                <img src="<%=this.headPic %>" />
                                <%}
                                  else
                                  { %>
                                <img src="../images/teacher_img.png" />
                                <%} %>
                            </div>
                            <h2>
                                <span id="LoginName"><%=this.uName %></span>
                            </h2>
                        </a>
                    </li>
                </ul>
                <div class="settings fl">
                    <a href="javascript:;" onclick="loginOut()">
                        <i class="iconfont icon-close"></i>
                    </a>
                </div>
            </div>
        </div>
    </header>
    <script>
        var UrlDate = new GetUrlDate();
        if (UrlDate.IsHeaderShow == "0")
        {
            $("#IsHeaderShow").val("0");
        }
        (function GetTopMenuInfo() {
            $("#ul_TopMenu").html('');
            $.ajax({
                url: "../Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/SystemSettings/MenuHandler.ashx",
                    Func: "GetNavigationMenu",
                    UniqueNo: "<%=UniqueNo%>",
                    Pid: 0,
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=UserInfo.LoginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function (i, n) {
                            $("#ul_TopMenu").append('<li currentclass="active" flag="' + n.Id + '"><a href="' + n.Url + '">' + n.Name + '</a></li>');

                        });
                        var host = 'http://' + window.location.host;
                        var href = window.location.href;
                        var url = href.split(host);
                        var curUrl = url[1].split('?')[0].toUpperCase();
                        $('.navbar').find('a').each(function () {
                            var attrhref = $(this).attr('href').toUpperCase();
                            //if (attrhref == url[1]) {
                            if (attrhref.indexOf(curUrl) >= 0) {
                                $(this).parent().addClass('active').siblings().removeClass('active');
                            }
                        });
                        var activeli = $('#ul_TopMenu li.active');
                        if ($('.leftmenuclass').length) {
                            var parentid = activeli.length ? activeli.attr('flag') : 5;
                            $("#hid_leftmenu").val(parentid);
                            $('.leftmenuclass').load('../CommonPage/SystemMenu.aspx');
                        }
                        $(".navbar").slide({ mainCell: "ul", effect: "left", vis: 5, pnLoop: false, scroll: 5 });
                        if (json.result.retData.length > 5) {
                            $('.prev,.next').show();
                        } else {
                            $('.prev,.next').hide();
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
        //退出
        function loginOut() {
            window.location.href = '../Login_unity.aspx';
            $.cookie('TokenID', null, { path: '/' });
            $.cookie('LoginCookie_Author', null, { path: '/' });
            $.cookie('RememberCookie_Cube', null, { path: '/' });
        }
    </script>
</body>
</html>
