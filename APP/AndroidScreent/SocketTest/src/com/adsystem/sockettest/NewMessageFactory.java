package com.adsystem.sockettest;

import java.util.ArrayList;
import java.util.List;


public class NewMessageFactory {
	
	public static String createMofiyMessage(String receiverIp, int receiverPort) {
		NewMessage msg = new NewMessage();
		msg.setMessageType(MessageTypeEnum.Modify);
		List<String> content = new ArrayList<String>();
		content.add("https://www.baidu.com");
		content.add("http://www.163.com");
		content.add("http://www.126.com");
		msg.setReceiverIp(receiverIp);
		msg.setReceiverPort(receiverPort);
		return Gson.getInstance().toJson(msg);
		
	}
	
	public static String createNoticeMsgWithPage() {
		NewMessage msg = new NewMessage();
		msg.setMessageType(MessageTypeEnum.Notice);
		msg.setOperatorId("1234");
		msg.setOfficeId("abcd");
		List<String> dataType = new ArrayList<String>();
		dataType.add("NewMessage");
		msg.setDataType(dataType);
		return Gson.getInstance().toJson(msg);
	}
	
	public static String createNoticeMsgWithPageAndAd() {
		NewMessage msg = new NewMessage();
		msg.setMessageType(MessageTypeEnum.Notice);
		List<String> content = new ArrayList<String>();
		content.add("name1");
		content.add("name2");
		content.add("name3");
		msg.setContent(content);
		msg.setOperatorId("1234");
		msg.setOfficeId("abcd");
		List<String> dataType = new ArrayList<String>();
		dataType.add("NewMessage");
		msg.setDataType(dataType);
		return Gson.getInstance().toJson(msg);
	}
	
	public static String createNoticeMsgWithAd() {
		NewMessage msg = new NewMessage();
		msg.setMessageType(MessageTypeEnum.Notice);
		List<String> content = new ArrayList<String>();
		content.add("adnotice");
		msg.setContent(content);
		List<String> dataType = new ArrayList<String>();
		dataType.add("Notice");
		msg.setDataType(dataType);
		return Gson.getInstance().toJson(msg);
	}

}
