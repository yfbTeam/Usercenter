/**
 * Created by BYM on 2016/8/29.
 */
var second = 0;
window.setInterval(function () {
    second++;
}, 1000);
var tjArr = localStorage.getItem("jsArr") ? localStorage.getItem("jsArr") : '[{}]';
$.cookie('tjRefer', getReferrer(), { expires: 1, path: '/' });

window.onbeforeunload = function () {
    if ($.cookie('tjRefer') == '') {
        var tjT = eval('(' + localStorage.getItem("jsArr") + ')');
        if (tjT) {
            tjT[tjT.length - 1].time += second;
            var jsArr = JSON.stringify(tjT);
            localStorage.setItem("jsArr", jsArr);
        }
    }
    else {
        var ToUrl = location.href;
        var SkinLong = second;
        var FromUrl = getReferrer();
        var IDCard = window.JSON.parse($.cookie('LoginCookie_Author'))["IDCard"];
        var LoginName = window.JSON.parse($.cookie('LoginCookie_Author'))["IDCard"];

        $.ajax({
            url: "/Common.ashx",
            type: "post",
            dataType: "json",
            data: {
                PageName: "/UserSkinHander.ashx",
                IDCard: IDCard,
                ToUrl: ToUrl,
                SkinLong: SkinLong,
                FromUrl: FromUrl,
                LoginName: LoginName
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

        //var tjArr = localStorage.getItem("jsArr") ? localStorage.getItem("jsArr") : '[{}]';
        /*var dataArr = {
            'url': location.href,
            'time': second,
            'refer': getReferrer(),
            'timeIn': Date.parse(new Date()),
            'timeOut': Date.parse(new Date()) + (second * 1000),
            'IDCard': window.JSON.parse($.cookie('LoginCookie_Author'))["IDCard"],
            'Name': window.JSON.parse($.cookie('LoginCookie_Author'))["Name"],
            'LoginName': window.JSON.parse($.cookie('LoginCookie_Author'))["LoginName"]
        };
        tjArr = eval('(' + tjArr + ')');
        tjArr.push(dataArr);
        tjArr = JSON.stringify(tjArr);
        localStorage.setItem("jsrr", tjArr);*/
    }
};
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