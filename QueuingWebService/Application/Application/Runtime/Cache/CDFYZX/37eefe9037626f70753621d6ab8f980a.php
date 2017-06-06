<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE html>
<html>

	<head>
		<meta charset="utf-8">
		<title></title>
	</head>
	<style type="text/css">
		* {
			font-family: "微软雅黑";
			color: white;
		}
		
		ul,
		li {
			margin: 0;
			padding: 0;
		}
		/*胎监*/
		
		li {
			list-style: none;
			margin-bottom: 2px;
			color: white;
			font-size: 38px;
			float: left;
			width: 318px;
			height: 50px;
			margin-right: 3px;
			text-align: center;
		}
		
		.read {
			margin-right: 0px;
			width: 305px;
			color: yellow;
		}
		/*脐血流*/
		
		#listQXL li {
			width: 312px;
		}
		
		#listQXL .read {
			width: 310px;
		}
		
		#bottom {
			position: absolute;
			color: red;
			text-align: center;
			bottom: 0;
			height: 70px;
			width: 1280px;
			font-size: 40px;
		}
		
		#tips {
			position: absolute;
			left: 650px;
			color: blue;
			text-align: left;
			bottom: 40px;
			height: 160px;
			width: 700px;
			font-size: 30px;
		}
		
		#date {
			float: left;
			margin-top: 10px;
			margin-left: 700px;
			color: cornflowerblue;
			width: 340px;
			font-size:30px;
		}
		
		#time {
			padding-left: 30px;
			width: 180px;
			font-size: 50px;
			margin-left: 1100px;
		}
	</style>
	<script src="/QueuingService/Public/Base/js/jquery-2.1.4.min.js"></script>
	<script src="/QueuingService/Public/Base/js/common.js"></script>
	<script type="text/javascript">
		$(function() {
			var idInt = setInterval("begin()", 1000);
		});

		function begin() {

			getdatetime();
			getdata();
		}

		function getdatetime() {
			$.ajax({
				url: myUrl + 'CDFYZX/index/getdate',
				dataType: 'json',
				timeout: 5000,
				success: function(data) {
					var list = document.getElementById('date');
					list.innerHTML = data['date'];
					list = document.getElementById('time');
					list.innerHTML = data['time'];
				},
				statusCode: {
					404: function() {
						//						alert("没有找到相关文件~~");
					}
				}
			});
		}

		function getdata() {
			var list = document.getElementById("list");
			var listQXL = document.getElementById("listQXL");
			$.ajax({
				url: myUrl + 'CDFYZX/index/getdata?ScreenId=' + document.getElementById('ScreenId').value,
				dataType: 'json',
				timeout: 5000,
				success: function(data) {
					var tj = data.tj;
					var qxl = data.qxl;
					var finallist = '';
					//加载胎监数据
					for(i = 0; i < tj.length; i++) {
						finallist = finallist + '<li class="read">' + tj[i] + '</li>';
						finallist = finallist + '<li></li>';
					}
					list.innerHTML = finallist;

					//加载脐血流数据
					finallist = '';
					for(i = 0; i < qxl.length; i++) {
						finallist = finallist + '<li class="read">' + qxl[i] + '</li>';
						finallist = finallist + '<li></li>';
					}
					listQXL.innerHTML = finallist;

					//数据加载成功清除底部数据
					list = document.getElementById('bottom');
					list.innerHTML = '';
				},
				statusCode: {
					404: function() {
						//						alert("没有找到相关文件~~");
					}
				}
			});
		}
	</script>

	<body>
		<input hidden="hidden" type="text" name="ScreenId" id="ScreenId" value="<?php echo $_GET[ScreenId] ?>" />
		<div id="date"></div>
		<div id="time"></div>
		<div style="position:absolute;top:0px;left: 0px; width:1280px; height:720px; z-index:-1;background-image: url('/QueuingService/Public/CDFYZX/images/background.jpg');">
			<!--胎监-->
			<div style="margin-top: 188px;margin-left: 10px;">
				<ul id="list" style="width: 630px;float: left;">
					<li>1</li>
					<li class="read">2</li>
					<li>3</li>
					<li class="read">4</li>
					<li>5</li>
					<li class="read">6</li>
					<li>7</li>
					<li class="read">8</li>
					<li>9</li>
					<li class="read">0</li>
					<li>11</li>
					<li class="read">22</li>
					<li>33</li>
					<li class="read">44</li>
					<li>55</li>
					<li class="read">66</li>
					<li>77</li>
					<li class="read">88</li>
					<li>99</li>
					<li class="read">00</li>
				</ul>
				<ul id="listQXL" style="margin-left: 635px;">
					<li>1</li>
					<li class="read"></li>
					<li>3</li>
					<li class="read"></li>
					<li>5</li>
					<li class="read"></li>
					<li>7</li>
					<li class="read"></li>
					<li>9</li>
					<li class="read"></li>

				</ul>
			</div>
			<!--脐血流-->

			<!--温馨提示-->
			<div id="tips">
				A--胎监 B--脐血流 </br>
				尾数为a--上午号 </br>
				尾数为p--下午号</br>
				过号请重新刷就诊卡签到排队！
			</div>
			<div id='bottom'>
				数据读取失败！
			</div>
		</div>

	</body>

</html>