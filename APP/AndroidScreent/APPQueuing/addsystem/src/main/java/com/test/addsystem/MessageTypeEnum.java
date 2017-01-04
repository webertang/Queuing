package com.test.addsystem;

/**
 * Created by Administrator on 2016/1/14.
 */
public enum MessageTypeEnum {
    Character(0),Image(1),Voice(2),Video(3),Login(4),Exit(5),ShutDown(6),PowerOff(7),Restart(8),Prompt(9),Modify(10),Increase(11),Delete(12),Collection(13),Notice(14),Stream(15),Heartbeat(16);

    private int value;

    private MessageTypeEnum(int value){

    }

    public int getValue(){
        return value;
    }
}
