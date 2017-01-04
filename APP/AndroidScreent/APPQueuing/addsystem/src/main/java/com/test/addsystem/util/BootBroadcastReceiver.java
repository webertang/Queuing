package com.test.addsystem.util;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

import com.test.addsystem.MainActivity;

public class BootBroadcastReceiver extends BroadcastReceiver {
    //重写onReceive方法
    @Override
    public void onReceive(Context context, Intent intent) {
        Intent service = new Intent(context, MainActivity.class);
        service.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        context.startActivity(service);
        Log.v("TAG", "开机自动服务自动启动.....");
    }

}
