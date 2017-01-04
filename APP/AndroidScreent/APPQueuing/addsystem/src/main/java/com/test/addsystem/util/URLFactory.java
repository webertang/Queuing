package com.test.addsystem.util;

import android.text.TextUtils;

import com.test.addsystem.NewMessage;

import java.util.List;

/**
 * Created by Administrator on 2016/7/10.
 */
public class URLFactory {

    private static final String TAG = URLFactory.class.getSimpleName();

    //医生登录刷新医生诊室信息
    public static String generatePageUrlWithNoContent(String basicPageUrl, NewMessage msg) {
        if(TextUtils.isEmpty(basicPageUrl) || msg == null) {
            return "";
        }
        StringBuilder sb = new StringBuilder();
        sb.append(basicPageUrl).append("?").append("OperatorId=").append(msg.getOperatorId()).append("& OfficeId=").append(msg.getOfficeId());
        LogUtil.d(TAG, "generatePageUrlWithNoContent-->" + sb.toString());
        return sb.toString();
    }
    //主显页面URL参数 http://www.baidu.com? OperatorId =1234& OfficeId=abcd&visit0=姓名1&wait1=姓名2&wiat2=姓名3……最多5个
    public static String generatePageUrlWithContent(String basicPageUrl, NewMessage msg) {
        if(TextUtils.isEmpty(basicPageUrl) || msg == null) {
            return "";
        }
        StringBuilder sb = new StringBuilder();
        sb.append(basicPageUrl).append("?").append("OperatorId=").append(msg.getOperatorId()).append("& OfficeId=").append(msg.getOfficeId());
        List<String> msgContent = msg.getContent();

        if(msgContent != null) {
            int maxWaitingVisitors = 5;
            int lastPos = msgContent.size() > maxWaitingVisitors ? maxWaitingVisitors : msgContent.size();
            for(int index = 0; index < lastPos; index++) {
//                sb.append("&visit=").append(msgContent.get(index));
                sb.append("&visit" + index + "=" + msgContent.get(index)); //add by:tzw@2016.7.14
            }
        }
        LogUtil.d(TAG, "generatePageUrlWithContent-->" + sb.toString());
        return sb.toString();
    }

    //弹出消息：显示就诊病人信息URL comment by:tzw@2106.7.20
    public static String generateAdUrlWithContent(String basicAdUrl, NewMessage msg) {
        if(TextUtils.isEmpty(basicAdUrl) || msg == null) {
            return "";
        }
        StringBuilder sb = new StringBuilder();
        sb.append(basicAdUrl).append("?").append("OperatorId=").append(msg.getOperatorId()).append("& OfficeId=").append(msg.getOfficeId());
        List<String> msgContent = msg.getContent();

        if(msgContent != null) {
            int maxWaitingVisitors = 5;
            int lastPos = msgContent.size() > maxWaitingVisitors ? maxWaitingVisitors : msgContent.size();
            for(int index = 0; index < lastPos; index++) {
                sb.append("&visit" + index + "=" + msgContent.get(index)); //add by:tzw@2016.7.14
            }
        }
        LogUtil.d(TAG, "generateAdUrlWithContent-->" + sb.toString());
        return sb.toString();

    }

    //消息回传：弹出提示框显示病人信息
    public static String generateAdUrlWithNoticeDataType(String basicAdUrl, NewMessage msg) {
        if(TextUtils.isEmpty(basicAdUrl) || msg == null) {
            return "";
        }
        StringBuilder sb = new StringBuilder();
        sb.append(basicAdUrl).append("?").append("OperatorId=").append(msg.getOperatorId()).append("& OfficeId=").append(msg.getOfficeId());
        List<String> msgContent = msg.getContent();

        if(msgContent != null && !TextUtils.isEmpty(msgContent.get(0))) {
            sb.append("&Brid=").append(msgContent.get(0));
        }
        LogUtil.d(TAG, "generateAdUrlWithNoticeDataType-->" + sb.toString());
        return sb.toString();
    }
}
