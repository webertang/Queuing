package com.test.addsystem.util;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;

import com.test.addsystem.IMainView;

/**
 * @author tzw
 * @date 2016/12/15 11:05
 */
public class SystemTools {

    private static ConnectivityManager mConnectivityManager;
    private static NetworkInfo netInfo;
    private static IMainView mIMainView;

    /////////////监听网络状态变化的广播接收器
    private static BroadcastReceiver myNetReceiver = new BroadcastReceiver() {

        @Override
        public void onReceive(Context context, Intent intent) {

            String action = intent.getAction();
            if (action.equals(ConnectivityManager.CONNECTIVITY_ACTION)) {

                mConnectivityManager = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
                netInfo = mConnectivityManager.getActiveNetworkInfo();
                if (netInfo != null && netInfo.isAvailable()) {
                    /////////////网络连接
                    mIMainView.netConnect();

                    String name = netInfo.getTypeName();
                    if (netInfo.getType() == ConnectivityManager.TYPE_WIFI) {
                        /////WiFi网络

                    } else if (netInfo.getType() == ConnectivityManager.TYPE_ETHERNET) {
                        /////有线网络

                    } else if (netInfo.getType() == ConnectivityManager.TYPE_MOBILE) {
                        /////////3g网络

                    }
                } else {
                    ////////网络断开
                    mIMainView.netDisConnect();
                }
            }

        }
    };

    public static void unRegisterNetReceiver(Context context) {
        /////////解除广播
        if(myNetReceiver!=null)
        {
            context.unregisterReceiver(myNetReceiver);
        }
    }

    public static void registerNetReceiver(Context context){
        mIMainView = (IMainView) context;
        /////////动态注册广播
        IntentFilter mFilter = new IntentFilter();
        mFilter.addAction(ConnectivityManager.CONNECTIVITY_ACTION);
        context.registerReceiver(myNetReceiver, mFilter);
    }


}
