package com.test.addsystem;

/**
 * Created by Administrator on 2016/7/9.
 */
public interface IMainView {

    public void onReceiveSettingMsg(NewMessage msg);

    public void onReceiveMsgWithPage(NewMessage msg);

    public void onReceiveMsgWithAdAndPage(NewMessage msg);

    public void onReceiveMsgWithAd(NewMessage msg);

    public boolean isAnimatorRunning();

    public void netConnect();

    public void netDisConnect();

}
