<?php if (!defined('THINK_PATH')) exit();?><!DOCTYPE html>
<html>

	<head>
		<meta charset="utf-8">
		<title></title>
	</head>

	<body>
		<div style="position:absolute;top:0px;left: 0px; width:768px; height:1366px; z-index:-1;background-image: url('/Public/Home/images/background.jpg');">
			<div style="line-height:108px;text-align:center;color: white;font-size: 45px;">
				<strong>深圳市第一人民医院--<?php echo $data[ksmc]; ?></strong>
			</div>
			<div style="position:absolute;top:200px;left:25px;width:290px;line-height:165px;text-align:left;color: white;font-size:45px;">
				<strong><?php echo $data[ysxm]; ?></strong>
			</div>
			<div style="position:absolute;top:420px;left:25px;width:290px;line-height:165px;text-align:left;color: white;font-size:45px;">
				<strong><?php echo $data[yszc]; ?></strong>
			</div>
			<div style="position:absolute;top:160px;left:320px;width:448px;line-height:180px;text-align:center;color: white;font-family:'微软雅黑';font-size:180px;">
				<strong><?php echo $data[zsdm]; ?></strong><br/><strong style="font-size: 130px;">诊室</strong>
			</div>
			<div style="position:absolute;top:640px;left:138px;width:635px;line-height:170px;text-align:center;color: yellow;font-family:'微软雅黑';font-size:130px;">
				<?php echo $data[brxm]; ?><br/>
				<?php echo $data[pdhm]; ?>
			</div>
			<div style="position:absolute;top:1020px;left:138px;width:635px;line-height:170px;text-align:center;color: white;font-family:'微软雅黑';font-size:130px;">
				<?php echo $data[waitpatient]; ?><br/>
				<?php echo $data[waitnumber]; ?>
			</div>
		</div>
	</body>

</html>