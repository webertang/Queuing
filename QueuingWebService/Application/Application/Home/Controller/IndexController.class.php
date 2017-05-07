<?php
namespace Home\Controller;
use Think\Controller;
class IndexController extends BaseController {
	public function index() {
		//http://localhost/queuingService/home/index?OfficeId=30601&OperatorId=3896&visit0=1
		//通过诊室代码获取科室信息
		if ($_GET[OfficeId] and $_GET[OperatorId]) {
			$data = $_GET[OfficeId];
			$field = 'ksmc';
			$where = "zsdm='" . $_GET[OfficeId] . "'";
			$data = A('Base') -> Select('vPdYsks', $field, $where);
			$result[ksmc] = $data[0][ksmc];
			$result[zsdm] = $_GET[OfficeId];
			//通过医生代码获取医生姓名和医生职称
			$field = 'ygxm,ygzw';
			$where = "ygdm='" . $_GET[OperatorId] . "'";
			$data = A('Base') -> Select('vPdYgxx', $field, $where);
			$result[ysxm] = $data[0][ygxm];
			$result[yszc] = $data[0][ygzw];
			if ($_GET[visit0]) {
				//通过vist0（PMID）+诊室代码取就诊病人信息和等候病人信息
				$field = 'brxm,pdhm,waitpatient,waitnumber';
				$where = "zsdm='" . $_GET[OfficeId] . "' and pmid=" . $_GET[visit0];
				$data = M('pdDisplay') -> field($field) -> where($where) -> select();
				$result[brxm] = $data[0][brxm];
				$result[pdhm] = $data[0][pdhm];
				$result[waitpatient] = $data[0][waitpatient];
				$result[waitnumber] = $data[0][waitnumber];
			}
			//向HTML页面传入数据
			$this -> assign("data", $result);
		}
		$this -> display();
	}

}
