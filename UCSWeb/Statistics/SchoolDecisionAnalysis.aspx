<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolDecisionAnalysis.aspx.cs" Inherits="UCSWeb.Statistics.SchoolDecisionAnalysis" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <title>校决策分析</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link href="/Scripts/layer/skin/layer.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script src="../Scripts/menu_top.js"></script>
    <script type="text/javascript" src="../Scripts/echarts-all.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <style type="text/css">
        .echarts-tooltip {
            opacity: 0.7;
        }
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
            <div class="content" style="width: 1198px; border-right: 1px solid #DDECFA;">
                <div class="title_nav clearfix">
                    <div class="title_nav_left fl">
                        <a href="javascript:;" class="active">四中网校</a>
                    </div>
                    <div class="title_nav_right fr">
                        <span id="PNum">0</span>
                    </div>
                </div>
                <div class="content_wrap">
                    <div class="pie_wrap clearfix pr">
                        <div id="pie" style="width: 450px; height: 270px; position: absolute; left: 0; top: 0;"></div>
                        <ul class="lei_lists clearfix">
                            <li>
                                <a href="/Organiz/OrganizManag.aspx">
                                    <i class="iconfont icon-outgit colorblue"></i>
                                    <p class="name" id="Type1"></p>
                                </a>
                            </li>
                            <li>
                                <a href="/Organiz/OrganizManag.aspx">
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
                    <div id="div_User" class="clearfix" style="height:360px;"></div>
                    <div id="div_SectionStu" class="clearfix" style="height:360px;"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer—wrap" style="height: 40px;"></div>
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

        $(function () {
            $('.header_wrap').load('../CommonPage/header.aspx');
            $('.footer—wrap').load('../CommonPage/footer.html');
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
                calculable: true,
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
            GetSectionStuEcharts();
        });
        var UserChart = echarts.init(document.getElementById('div_User'));
        useroption = {
            title: {
                text: '用户统计',
                x: 'center'
            },
            tooltip: {
                trigger: 'axis'
            },
            calculable: true,
            xAxis: [
                {
                    name: '用户类别',
                    type: 'category',                   
                    data: ['学生','家长','教师']
                }
            ],
            yAxis: [
                {
                    name: '人数',
                    type: 'value',
                    axisLabel: {
                        formatter: '{value} '
                    }
                }
            ],
            series: [
                {
                    name: '人数',
                    type: 'bar',
                    data: []

                }
            ]
        };
        //获取用户信息
        function getData() {
            var stucount = 0, parcount = 0, teacount = 0;
            //初始化序号 
            $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/UserManage/UserInfo.ashx",
                    Func: "GetData",
                    IsPage: "false"                  
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {                        
                        $("#PNum").html("共" + json.result.retData.length + "人");
                        $(json.result.retData).each(function (i, n) {
                            if (n.UserType == 1) { teacount += 1; }
                            else if (n.UserType == 2) { stucount += 1; }
                            else if (n.UserType == 3) { parcount += 1;}
                        });                        
                    }
                    useroption.series[0].data = [stucount, parcount, teacount];
                    UserChart.setOption(useroption);
                }
            });
        }
        function AnalisyOrg(OrgType) {
            $.ajax({
                url: "../common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/Organiz/Organiz.ashx",
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
                url: "../common.ashx",
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
                url: "../common.ashx",
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
        function GetSectionStuEcharts() {
            var SectionChart = echarts.init(document.getElementById('div_SectionStu'));
            sectionoption = {
                title: {
                    text: '学年学期-学生分析',
                    x: 'center'
                },
                tooltip: {
                    trigger: 'axis'
                },                              
                calculable: true,
                xAxis: [
                    {
                        name:'学年学期',
                        type: 'category',
                        boundaryGap: false,
                        data: []
                    }
                ],
                yAxis: [
                    {
                        name:'学生人数',
                        type: 'value',
                        axisLabel: {
                            formatter: '{value} '
            }
                    }
                ],
                series: [
                    {
                        name: '学生人数',
                        type: 'line',
                        data: []
                        
                    }
                ]
            };            
          $.ajax({
                url: "../common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/EduManage/StudySection.ashx",
                    Func: "GetSectionStudentData",
                    SysAccountNo: SysAccountNo,
                    LoginName: "<%=loginName%>"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var sectionArray = [],stuCountArray=[];
                        var rtnObj = json.result.retData;                                             
                        $(rtnObj).each(function (i, n) {
                            (sectionoption.xAxis[0].data).push(n.Academic);
                            (sectionoption.series[0].data).push(n.StuCount);
                        });
                        SectionChart.setOption(sectionoption);
                    }                    
                }
            });
        }
    </script>
</body>
</html>
