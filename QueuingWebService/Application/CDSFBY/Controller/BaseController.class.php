<?php
namespace CDSFBY\Controller;
use Think\Controller;
class BaseController extends Controller {
    public function index(){
    }
	
	//数据库连接地址
	public function getDB(){
		return 'sqlsrv://sa:@127.0.0.1/v';
	}
	
	//=========================
	//数据查询
	//table:数据库对应的表名
	//field：需要显示的字段名
	//where：条件
	//=========================
    public function Select($table,$field,$where){
		$DBResult=M($table,'',A('Base')->getDB())->field($field)->where($where)->select(); 
		Return $DBResult;
	}
}