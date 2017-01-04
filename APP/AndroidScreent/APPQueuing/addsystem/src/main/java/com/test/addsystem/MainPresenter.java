package com.test.addsystem;

import android.os.Handler;
import android.os.HandlerThread;
import android.os.Looper;
import android.os.Message;
import android.text.TextUtils;

import com.test.addsystem.util.LogUtil;

import java.util.List;

/**
 * Created by Administrator on 2016/7/9.
 */
public class MainPresenter {

    private final static String TAG = MainPresenter.class.getSimpleName();

    private IMainView mainView;
    private SocketService mSocketService;
    private HandlerThread mMsgQueueHandlerThread;
    private Runnable mTimerRunnable;
    private Handler mMsgQueueHandler;


    interface HandlerMsg{
        public static final int MODIFY_MSG = 0;
        public static final int NOTICE_MSG = 1;
    }

    private Handler mHandler = new Handler(Looper.getMainLooper()) {
      public void handleMessage(Message msg) {
          switch (msg.what) {
              case HandlerMsg.MODIFY_MSG:
                  mainView.onReceiveSettingMsg((NewMessage)msg.obj);
                  break;
              case HandlerMsg.NOTICE_MSG:
                  if(msg.obj instanceof  NewMessage) {
                      NewMessage newMessage = (NewMessage)msg.obj;
                      List<String> newMessasgeDataType = newMessage.getDataType();
                      if(newMessasgeDataType != null) {
                          String dataType = newMessasgeDataType.get(0);
                          if(TextUtils.equals(dataType, "NewMessage")) {
                              if(newMessage.getContent() == null  || newMessage.getContent().size() == 0) {
                                  mainView.onReceiveMsgWithPage(newMessage);
                              } else {
                                  mainView.onReceiveMsgWithAdAndPage(newMessage);
                              }
                          } else if(TextUtils.equals(dataType, "Notice")) {
                              mainView.onReceiveMsgWithAd(newMessage);
                          }

                      }

                  }

                  break;
          }

      }
    };




    private SocketService.OnReceiveMsgListener mOnReceiveMsgListener = new SocketService.OnReceiveMsgListener() {
        @Override
        public void onReceiveMsg(NewMessage msg) {
            LogUtil.d(TAG, "get msg-->" + msg);
            MessageTypeEnum messageTypeEnum = msg.getMessageType();
            switch (messageTypeEnum) {
                case Modify:
                    Message modifyHandlerMsg = mHandler.obtainMessage(HandlerMsg.MODIFY_MSG);
                    modifyHandlerMsg.obj = msg;
                    mHandler.sendMessage(modifyHandlerMsg);
                    break;
                case Notice:
                    Message noticeHandlerMsg = mHandler.obtainMessage(HandlerMsg.NOTICE_MSG);
                    noticeHandlerMsg.obj = msg;
                    mHandler.sendMessage(noticeHandlerMsg);
                    break;
                default:
                    break;
            }

        }
    };

    public MainPresenter(IMainView mainView) {
        this.mainView = mainView;
        mSocketService = SocketService.getInstance();
        mSocketService.registerReceiveMgsListener(mOnReceiveMsgListener);
        initNoticeMsgQueueHandler();
    }

    public void destroy() {
        if(mSocketService != null) {
            mSocketService.unregisterReceiveMsgListener(mOnReceiveMsgListener);
        }
    }

    public void sendMsg(String receiverIp, int receiverPort, String msg) {
        if(TextUtils.isEmpty(msg) || TextUtils.isEmpty(receiverIp) || receiverPort <= 0) {
            return;
        }
        SocketService.getInstance().sendMsg(receiverIp, receiverPort, msg);
    }

    private void initNoticeMsgQueueHandler() {
        mMsgQueueHandlerThread = new HandlerThread("queryMsg");
        mMsgQueueHandlerThread.start();
        mTimerRunnable = new Runnable() {
            @Override
            public void run() {
                if(mainView.isAnimatorRunning()) {
                    mMsgQueueHandler.postDelayed(this, 500);
                } else {
                    NewMessage noticeMessage = mSocketService.getNoticeMsgFromBlockQueue();
                    if(noticeMessage != null) {
                        mOnReceiveMsgListener.onReceiveMsg(noticeMessage);
                    }
                    mMsgQueueHandler.postDelayed(this, 500);
                }
            }
        };
        mMsgQueueHandler = new Handler(mMsgQueueHandlerThread.getLooper());
        mMsgQueueHandler.postDelayed(mTimerRunnable, 500);
    }





}
