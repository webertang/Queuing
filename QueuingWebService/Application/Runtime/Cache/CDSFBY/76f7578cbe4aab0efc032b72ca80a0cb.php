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
			margin-bottom: 2px;
			text-align: left;
			color: white;
			font-size: 58px;
			float: left;
			width: 625px;
			height:76px;
			px;
			margin-right: 5px;
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
			margin-left: 1050px;
		}
		
		#time {
			font-size: 50px;
			margin-top:20px;
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
				url: myUrl + 'CDSFBY/index/getdate',
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
				url: myUrl + 'CDSFBY/index/getdata?ScreenId=' + document.getElementById('ScreenId').value,
				dataType: 'json',
				timeout: 5000,
				success: function(data) {
					var finallist = '';
					for(i = data.length - 1; i >= 0; i--) {
						if (data[i].zsdm==9){
							finallist = finallist + '<li>　　　9-1诊室</li>';
						}else if(data[i].zsdm==10){
							finallist = finallist + '<li>　　　9-2诊室</li>';
						}else{
							finallist = finallist + '<li>　　　' + data[i].zsdm + '　诊室</li>';
						}
						finallist = finallist + '<li class="read">　　' + data[i].pdhm +'　'+ data[i].brxm + '</li>';
						finallist = finallist + '<li>　　' + data[i].waitnumber + '　' + data[i].waitpatient + '</li>';
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

	</script>

	<body>
		<input hidden="hidden" type="text" name="ScreenId" id="ScreenId" value="<?php echo $_GET[ScreenId] ?>" />
		<div id="date"></div>
		<div id="time"></div>
		<div style="position:absolute;top:0px;left: 0px; width:1920px; height:1080px; z-index:-1;background-image: url('/QueuingService/Public/CDSFBY/images/backgroundyxk.jpg');">
			<div style="margin-top: 210px;margin-left: 15px;width: 1890px;height: 778px;">
				<ul id="list">
				</ul>
			</div>
			<div id='bottom'>
				数据读取失败！
			</div>
		</div>
	</body>

</html>