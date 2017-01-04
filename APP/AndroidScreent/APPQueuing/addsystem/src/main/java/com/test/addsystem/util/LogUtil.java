package com.test.addsystem.util;

import android.text.TextUtils;
import android.util.Log;

import com.test.addsystem.AdSystemConfig;

/**
 * Created by Administrator on 2016/7/9.
 */
public class LogUtil {

    public static void e(String TAG, String msg) {
        if(!AdSystemConfig.isDebug) {
            return;
        }
        if(!TextUtils.isEmpty(TAG) && !TextUtils.isEmpty(msg)) {
            Log.e(TAG, msg);
        }
    }
    public static void d(String TAG, String msg) {
        if(!AdSystemConfig.isDebug) {
            return;
        }
        if(!TextUtils.isEmpty(TAG) && !TextUtils.isEmpty(msg)) {
            Log.d(TAG, msg);
        }
    }


}
