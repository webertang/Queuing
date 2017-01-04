package com.test.addsystem;

import android.app.Application;
import android.content.Context;

import com.test.addsystem.util.LogUtil;

/**
 * Created by Administrator on 2016/7/9.
 */
public class AdSystemApplication extends Application {
    private final static String TAG = AdSystemApplication.class.getSimpleName();

    private static Context mContext;


    public static Context getContext() {
        return mContext;
    }

    @Override
    public void onCreate() {
        super.onCreate();
        mContext = getApplicationContext();

        initSocketService();
    }

    private void initSocketService() {
        try {
            SocketService.getInstance().startService();
        }catch (Exception e) {
            e.printStackTrace();
            LogUtil.e(TAG, "initSocketServiceError");
        }
    }

    @Override
    public void onTerminate() {
        super.onTerminate();
        try{
            SocketService.getInstance().stopService();
        }catch (Exception e) {
            e.printStackTrace();
        }
    }
}
