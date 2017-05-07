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
		
		li {
			list-style: none;
			margin-bottom: 8px;
			text-align: center;
			color: white;
			font-size: 118px;
			line-height: 128px;
			float: left;
			overflow: hidden;
			white-space:nowrap;
			/*background-color: red;*/
			/*opacity: 0.5;*/
		}
		
		#departmentNmae {
			width: 430px;
		}
		
		#officeId {
			width: 210px;
			font-size: 80px;
			line-height: 108px;
			padding-top: 15px;
			color: yellow;
		}
		
		#doctorName {
			width: 424px;
		}
		
		#visiterPatient {
			width: 404px;
			color: yellow;
		}
		
		#waitPatient {
			width: 417px;
		}
		
		.read {
			color: yellow;
		}
		
		#bottom {
			position: absolute;
			text-align: center;
			bottom: 0;
			height: 60px;
			width: 1920px;
			font-size: 40px;
		}
		
		#date {
			margin-top: 10px;
			color: cornflowerblue;
			width: 530px;
			font-size: 45px;
			float: left;
			margin-left: 1040px;
		}
		
		#time {
			font-size: 50px;
			margin-top: 20px;
			margin-left: 1730px;
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
				url: myUrl + 'NJYYY/index/getdate',
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
			$.ajax({
				url: myUrl + 'NJYYY/index/getdata?ScreenId=' + document.getElementById('ScreenId').value,
				dataType: 'json',
				timeout: 5000,
				success: function(data) {
					var finallist = '';
					for(i = data.length - 1; i >= 0; i--) {
						finallist = finallist + '<li id="departmentNmae">' + (data[i].ksmc == null ? "　" : data[i].ksmc) + '</li>';
						finallist = finallist + '<li id="officeId">' + (data[i].zsdm == null ? "　" : data[i].zsdm) + '</li>';
						finallist = finallist + '<li id="doctorName">' + (data[i].ysmc == null ? "　" : data[i].ysmc) + '</li>';
						finallist = finallist + '<li id="visiterPatient">' + (data[i].brxm == null ? "　" : data[i].brxm) + '</li>';
						finallist = finallist + '<li id="waitPatient">' + (data[i].waitpatient == null ? "　" : data[i].waitpatient) + '</li>';
					}
					list.innerHTML = finallist;

					list = document.getElementById('bottom');
					list.innerHTML = '提示信息：';
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
		<div id="date"></div>
		<div id="time"></div>
		<div style="position:absolute;top:0px;left: 0px; width:1920px; height:1080px; z-index:-1;background-image: url('/QueuingService/Public/NJYYY/images/backgroundyxk.jpg');">
			<div style="margin-top: 300px;margin-left: 14px;width: 1890px;height: 778px;">
				<ul id="list">
				</ul>
			</div>
			<div id='bottom'>
				数据读取失败！
			</div>
		</div>
		<input style="opacity: 0;width: 0px;height: 0px;" hidden="hidden" type="text" name="ScreenId" id="ScreenId" value="<?php echo $_GET[ScreenId] ?>" />

	</body>

</html>