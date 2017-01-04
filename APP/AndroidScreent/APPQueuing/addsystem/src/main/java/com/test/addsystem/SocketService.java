package com.test.addsystem;

import android.text.TextUtils;

import com.test.addsystem.util.LogUtil;
import com.test.addsystem.util.Pub;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

/**
 * Created by Duke on 2016/7/9.
 */
public class SocketService {

    private final static String TAG = SocketService.class.getSimpleName();

    private static SocketService instance = new SocketService();
    private ServerSocket mServerSocket = null;
    private boolean isServerRunning = true;
    private BlockingQueue<NewMessage> messageBlockingQueue = new LinkedBlockingQueue<>();

    private List<OnReceiveMsgListener> mReceiveMsgListener = new ArrayList<OnReceiveMsgListener>();

    public interface OnReceiveMsgListener{
        public void onReceiveMsg(NewMessage msg);
    }

    public void registerReceiveMgsListener(OnReceiveMsgListener receiveMsgListener) {
        if(receiveMsgListener != null) {
            mReceiveMsgListener.add(receiveMsgListener);
        }

    }

    public void unregisterReceiveMsgListener(OnReceiveMsgListener receiveMsgListener) {
        if(receiveMsgListener != null) {
            mReceiveMsgListener.remove(receiveMsgListener);
        }
    }

    public  void onReceiveMsg(NewMessage msg) {
        for(OnReceiveMsgListener listener : mReceiveMsgListener) {
            if(listener != null) {
                listener.onReceiveMsg(msg);
            }
        }
    }

    private SocketService() {

    }

    public static SocketService getInstance() {
        //饿汉模式该懒汉模式 edit by:tzw@2016.12.13
//        if (instance == null){
//            synchronized (SocketService.class){
//                if(instance == null) {
//                    instance = new SocketService();
//                }
//            }
//        }
        return instance;
    }

    public void startService() throws Exception{
        stopService();
        //启动线程从消息队列中查询消息
        new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    mServerSocket = new ServerSocket(Constant.SOCKET_SERVER_PORT);
                    isServerRunning = true;
                    while(isServerRunning) {
                        Socket client = mServerSocket.accept();
                        LogUtil.d(TAG, "accept client");
                        new Thread(new ReadThread(client)).start();
                    }

                }catch (Exception e) {
                    e.printStackTrace();
                }

            }
        }).start();


    }

    public void stopService() throws Exception{
        isServerRunning = false;
        if(mServerSocket != null) {
            if(!mServerSocket.isClosed()) {
                mServerSocket.close();
            }
            mServerSocket = null;
        }
    }

    public void sendMsg(final String receiverIp, final int receiverPort, final String msg) {
        if(TextUtils.isEmpty(msg) || TextUtils.isEmpty(receiverIp) || receiverPort <= 0) {
            return;
        }

        LogUtil.d(TAG, "msg-->" + receiverIp + " --" + receiverPort + "  " + msg);
        new Thread(new Runnable() {
            @Override
            public void run() {
                PrintWriter writer = null;
                Socket socket = null;
                try {
                    socket = new Socket();
                    socket.connect(new InetSocketAddress(receiverIp, receiverPort), 5000);
                    writer = new PrintWriter(socket.getOutputStream());
                    writer.write(msg);
                    writer.flush();
                }catch (Exception e) {
                    e.printStackTrace();
                } finally{
                    try {
                        writer.close();
                        socket.close();
                    }catch (Exception e) {
                        e.printStackTrace();
                    }
                }

            }
        }).start();
    }

    public NewMessage getNoticeMsgFromBlockQueue() {
        try {
            NewMessage msg = messageBlockingQueue.take();
            LogUtil.d(TAG, "msg-->" + msg);
            return msg;
        }catch (Exception e) {
            e.printStackTrace();
            return null;
        }

    }


    class ReadThread implements Runnable {
        private final  String TAG = ReadThread.class.getSimpleName();
        private Socket client;
        public ReadThread() {

        }

        public ReadThread(Socket client) {
            this.client = client;
        }

        @Override
        public void run() {
            //解析从客户端发来的数据
            BufferedReader reader = null;
            try {
                StringBuilder sb = new StringBuilder();
                reader = new BufferedReader(new InputStreamReader(client.getInputStream()));
                String tempStr = null;
                while((tempStr = reader.readLine()) != null) {
                    sb.append(tempStr);
                }
                String msgStr = sb.toString();
                try {
                    NewMessage msg = Pub.ToT(msgStr, NewMessage.class); //json序列化 add by:tzw@2016.7.15
                    if(msg.getMessageType() == MessageTypeEnum.Modify) {
                        onReceiveMsg(msg);
                    } else {
                        messageBlockingQueue.put(msg);
                        LogUtil.d(TAG, "put Msg to queue-->" + messageBlockingQueue.size());
                    }
                }catch (Exception e) {
                    e.printStackTrace();
                    LogUtil.e(TAG,"json 解析失败");
                }
                LogUtil.d(TAG, "client msg-->" + msgStr);
            }catch (Exception e) {
                e.printStackTrace();
                LogUtil.e(TAG, "read data error");
            }finally {
                try {
                    reader.close();
                    client.close();
                    client = null;
                }catch (Exception e) {
                    LogUtil.e(TAG, "关闭消息接收出现异常");
                }

            }


        }
    }



}


