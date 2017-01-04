<?php
namespace Home\Controller;
use Think\Controller;
class MessageController extends BaseController {
	public function index() {
		//http://192.168.0.101/queuingService/home/message?OfficeId=30601&OperatorId=3896&visit0=1
		//通过诊室代码获取科室信息
		if ($_GET[OfficeId] and $_GET[visit0]) {
			//通过vist0（PMID）+诊室代码取就诊病人信息和等候病人信息
			$field = 'brxm,pdhm,waitpatient,waitnumber';
			$where = "zsdm='" . $_GET[OfficeId] . "' and pmid=" . $_GET[visit0];
			$data = M('pdDisplay') -> field($field) -> where($where) -> select();
			$result[brxm] = $data[0][brxm];
			$result[pdhm] = $data[0][pdhm];
//			$result[t] = $_GET[OfficeId].$_GET[visit0];
			//向HTML页面传入数据
			$this -> assign("data", $result);
		}
		$this -> display();
	}

}
