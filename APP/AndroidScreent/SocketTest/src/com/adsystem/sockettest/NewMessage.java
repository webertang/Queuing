package com.adsystem.sockettest;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

/**
 * Created by Administrator on 2016/1/14.
 */
public class NewMessage implements Serializable {
    public NewMessage() {

    }

    private MessageTypeEnum MessageType;
    private String SenderIp;
    private int SenderPort;
    private String ReceiverIp;
    private int ReceiverPort;
    private List<String> DataType;
    private List<String> Content;
    private int Second;
    private Date SendTime;
    private String OfficeId;
    private String OperatorId;
    private String Parameter;

    public String getParameter() {
		return Parameter;
	}

	public void setParameter(String parameter) {
		Parameter = parameter;
	}

	public MessageTypeEnum getMessageType() {
        return MessageType;
    }

    public void setMessageType(MessageTypeEnum messageType) {
        MessageType = messageType;
    }

    public String getSenderIp() {
        return SenderIp;
    }

    public void setSenderIp(String senderIp) {
        SenderIp = senderIp;
    }

    public int getSenderPort() {
        return SenderPort;
    }

    public void setSenderPort(int senderPort) {
        SenderPort = senderPort;
    }

    public String getReceiverIp() {
        return ReceiverIp;
    }

    public void setReceiverIp(String receiverIp) {
        ReceiverIp = receiverIp;
    }

    public int getReceiverPort() {
        return ReceiverPort;
    }

    public void setReceiverPort(int receiverPort) {
        ReceiverPort = receiverPort;
    }

    public List<String> getDataType() {
        return DataType;
    }

    public void setDataType(List<String> dataType) {
        DataType = dataType;
    }

    public List<String> getContent() {
        return Content;
    }

    public void setContent(List<String> content) {
        Content = content;
    }

    public int getSecond() {
        return Second;
    }

    public void setSecond(int second) {
        Second = second;
    }

    public Date getSendTime() {
        return SendTime;
    }

    public void setSendTime(Date sendTime) {
        SendTime = sendTime;
    }

    public String getOfficeId() {
        return OfficeId;
    }

    public void setOfficeId(String officeId) {
        OfficeId = officeId;
    }

    public String getOperatorId() {
        return OperatorId;
    }

    public void setOperatorId(String operatorId) {
        OperatorId = operatorId;
    }
}
