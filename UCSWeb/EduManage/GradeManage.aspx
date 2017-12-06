<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GradeManage.aspx.cs" Inherits="UCSWeb.EduManage.GradeManage" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>年级设置</title>
    <link rel="stylesheet" href="../css/reset.css" />
    <link rel="stylesheet" href="../css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <!--[if IE]>
			<script src="../Scripts/html5.js"></script>
		<![endif]-->
    <script type="text/javascript" src="../Scripts/layer/layer.js"></script>
    <script src="../Scripts/jquery.cookie.js"></script>
    <script src="../Scripts/Common.js?V=1.102"></script>
    <script type="text/javascript" src="../Scripts/jquery.tmpl.js"></script>
    <script type="text/javascript" src="../Scripts/PageBar.js"></script>
    <script src="EduManage.js"></script>
    <script id="tr_Grade" type="text/x-jquery-tmpl">
        <tr>
            <td style="width: 5%;">
                <input type="checkbox" name="Check_box" id="" value="${Id}" />
            </td>
            <td>${Academic}</td>
            <td>${GradeName}</td>
            <td>${Leader}</td>
            <td>{{if IsGraduate==0}}否
                {{else}}是
                {{/if}} </td>
        </tr>
    </script>
</head>
<body>
    <!--header-->
    <div class="header_wrap" style="height: 61px;"></div>
    <div id="main">
        <div class="w1200 clearfix wrap pr">
            <div class="menu_wrap fl leftmenuclass"></div>
            <div class="content fr">
                <div class="content_wrap">
                    <div class="title_nav clearfix">
                        <div class="title_nav_left fl">
                            <a href="javascript:;" class="active">年级设置</a>
                        </div>
                    </div>
                    <div class="toolsbar clearfix mt10">
                        <div class="tool_left fl">
                            <span btncls="icon-edit" style="display:none;" onclick="EditGrade()">修改</span>
                            <span btncls="icon-del" style="display:none;" onclick="DelGrade()">删除</span>
                        </div>
                        <div class="tool_right fr clearfix">
							<div class="fl mr10 pr search">
								<input type="text" name="" id="Name" value="" placeholder="请输入关键字" />
								<i class="iconfont" onclick="getGradeData(1,10)">&#xe604;</i>
							</div>
							<div class="fl">
								<lable>学期:</lable>
								<select class="select" id="StudyTerm" onchange="getGradeData(1,10)">
								</select>
                                <lable>专业:</lable>
								<select class="select" id="Major" onchange="getGradeData(1,10)">
								</select>
							</div>
						</div>
                    </div>
                    <div class="table_wrap mt10">
                        <table>
                            <thead>
                                <tr>
                                    <th>
                                        <input type="checkbox" name="" id="" value="" /></th>
                                    <th>学期名称</th>
                                    <th>年级名称</th>
                                    <th>年级负责人</th>
                                    <th>是否毕业班</th>
                                </tr>
                            </thead>
                            <tbody id="tb_Grade"></tbody>
                        </table>

                    </div>
                    <!--分页-->
                    <div class="page" id="pageBar"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer—wrap" style="height:40px;"></div>
  
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
            $('.footer—wrap').load('../CommonPage/footer.html');
            $('.header_wrap').load('../CommonPage/header.aspx');
            GetTerm();
            getGradeData(1, 10);
        });
        
      
    </script>
</body>
</html>
