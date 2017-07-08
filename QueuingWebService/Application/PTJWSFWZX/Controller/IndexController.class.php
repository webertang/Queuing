<?php
namespace PTJWSFWZX\Controller;
use Think\Controller;
class IndexController extends Controller {
    public function index(){
		$this -> display();
    }
	
	//大屏显示数据查询
	public function getdata() {
		if ($_GET[ScreenId]) {
			$field = 'zsdm,ksmc,ysmc,brxm,pdhm,waitpatient,waitnumber,sort';
			$where = "pmid='" . $_GET[ScreenId] . "'";
			$data = M('pdDisplay') -> field($field) -> where($where) -> order('sort desc') -> select();
		}
		echo json_encode($data);
	}
	
	//获取网络数据
	public function GetYYDA(){
		$handle = fopen("http://www.pitongjiedao.xyz/Home/GetReserveData","rb"); 
		$content = "";
		 while (!feof($handle)) 
		 {
		 	$content .= fread($handle, 10000); 
		 } 
		 fclose($handle);
		 $content = json_decode($content,TRUE);
		

		foreach ($content as $value) {
			$dataList[] = array('xm'=>$value[姓名],
			'xb'=>$value[性别],
			'sfzh'=>$value[身份证号码],
			'csny'=>$value[出生日期],
			'yyrq'=>$value[预约日期],
			'yykssj'=>explode('-',$content[0][预约时间])[0],
			'yyjssj'=>explode('-',$content[0][预约时间])[1],
			'yyzt'=>$value[预约状态]);
		}
		
		$res = M('pdYyda') -> count();
		if($res){
			echo "存在数据";
		}else{
			$res = $data = M('pdYyda') ->addAll($dataList);
			echo $res;
		}
	}
}
