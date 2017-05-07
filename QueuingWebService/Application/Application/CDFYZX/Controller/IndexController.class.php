<?php
namespace CDFYZX\Controller;
use Think\Controller;
class IndexController extends BaseController {
	//显示大屏内容
	public function index() {
		//		http://192.168.0.110/queuingService/CDSFBY/index?ScreenId=4
		$this -> display();
	}

	//大屏显示数据查询
	public function getdata() {
		if ($_GET[ScreenId]) {
//			$field = 'zsdm,brxm,pdhm,waitpatient,waitnumber,sort';
			//获取胎监数据
			$where = "pmid='" . $_GET[ScreenId] ."' and zsdm=1";
			$tmp = M('pdDisplay') -> field('brxm') -> where($where) -> select();
			$data[tj] = explode("|",$tmp[0][brxm]);
			//获取脐血流数据
			$where = "pmid='" . $_GET[ScreenId] ."' and zsdm=2";
			$tmp = M('pdDisplay') -> field('brxm') -> where($where) -> select();
			$data[qxl] = explode("|",$tmp[0][brxm]);
		}
		echo json_encode($data);
	}

	public function getdate() {
		$weekarry = array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
		$data['date'] = date('Y年m月d日', strtotime('now')) . " " . $weekarry[date("w")];
		$data['time'] = date('H:i', strtotime('now'));
		echo json_encode($data);

	}

}
