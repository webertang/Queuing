package com.test.addsystem;

import android.animation.Animator;
import android.animation.AnimatorSet;
import android.animation.ObjectAnimator;
import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.os.Handler;
import android.text.Editable;
import android.text.TextUtils;
import android.text.TextWatcher;
import android.view.KeyEvent;
import android.view.View;
import android.view.WindowManager;
import android.webkit.JsResult;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.EditText;
import android.widget.RelativeLayout;

import com.test.addsystem.util.LogUtil;
import com.test.addsystem.util.Pub;
import com.test.addsystem.util.SystemTools;
import com.test.addsystem.util.URLFactory;

import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;

public class MainActivity extends Activity implements IMainView {

    private final static String TAG = MainActivity.class.getSimpleName();

    private MainPresenter mPresenter;
    private int screenWidth;
    private int screenHeight;

    private WebView mAdWebView;
    private WebView mPageWebView;
    private EditText mEdtSendMsg;
    private RelativeLayout mShadowLayout;
    private RelativeLayout mLoadingLayout;

    private String mReceiverIp;
    private int mReceiverPort;
    private String mOperatorId;//操作员ID add by:tzw@2016.7.25
    private String mOfficeId;//诊室代码 add by:tzw@2016.7.25

    private Handler myHandler = new Handler();

    private final static String RECEIVER_INFO_PREFS = "Receiver_Info_Prefs";
    private final static String RECEIVER_INFO_PREFS_IPKEY = "Receiver_Info_Prefs_IPKey";
    private final static String RECEIVER_INFO_PREFS_PORTKEY = "Receiver_Info_Prefs_PortKey";
    private final static String RECEIVER_INFO_PREFS_OPERATORIDKEY = "Receiver_Info_Prefs_OperatorIdKey";
    private final static String RECEIVER_INFO_PREFS_OFFICEIDKEY = "Receiver_Info_Prefs_OfficeIdKey";
    private final static String RECEIVER_INFO_PREFS_PAGEURL_KEY = "Receiver_Info_Prefs_PageUrl_Key";
    private final static String RECEIVER_INFO_PREFS_ADURL_KEY = "Receiver_Info_Prefs_AdUrl_Key";
    private final static String RECEIVER_INFO_PREFS_NOTICEADURL_KEY = "Receiver_Info_Prefs_NoticeAdUrl_Key";


    private List<String> mPageAndAdUrls = new ArrayList<String>();

    float startPos;
    float endPos;
    private ObjectAnimator downAnimator;
    private ObjectAnimator waitingAnimator;
    private ObjectAnimator upAnimator;
    private AnimatorSet animationSet;
    private boolean animatorStatus;
    private boolean isAdPageFinished = false;

    private WebViewClient mAdWebViewClient = new WebViewClient() {
        @Override
        public void onPageStarted(WebView view, String url, Bitmap favicon) {
            super.onPageStarted(view, url, favicon);
        }

        @Override
        public void onPageFinished(WebView view, String url) {
            super.onPageFinished(view, url);
            //页面加载完成，开始播放动画显示
            if (!isAdPageFinished) {
                isAdPageFinished = true;
                showAd();
            }
        }


    };

    private WebViewClient mPageWebViewClient = new WebViewClient() {
        @Override
        public void onPageStarted(WebView view, String url, Bitmap favicon) {
            super.onPageStarted(view, url, favicon);
        }

        @Override
        public void onPageFinished(WebView view, String url) {
            super.onPageFinished(view, url);
        }

        public void onReceivedError(final WebView view, int errorCode, String description, final String failingUrl) {
            refreshPage(Constant.LOAD_ERROR);
            myHandler.postDelayed(new Runnable() {
                @Override
                public void run() {
                    refreshPage(failingUrl);
                }
            }, 10000);
        }


    };

    private WebChromeClient mWebChromeClient = new WebChromeClient() {
        @Override
        public boolean onJsAlert(WebView view, String url, String message, JsResult result) {
            return super.onJsAlert(view, url, message, result);
        }

    };


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mPresenter = new MainPresenter(this);
        SystemTools.registerNetReceiver(this);
        WindowManager wm = this.getWindowManager();

        screenWidth = wm.getDefaultDisplay().getWidth();
        screenHeight = wm.getDefaultDisplay().getHeight();
        LogUtil.d(TAG, "screenHeight-->" + screenHeight);
        initUrlData();
        initView();

//        startPos = -(screenHeight / 2);
        startPos = (screenHeight / 4); //edit by:tzw@取消动画显示
        endPos = (screenHeight) / 4;
        downAnimator = ObjectAnimator.ofFloat(mAdWebView, "translationY", startPos, endPos);
        waitingAnimator = ObjectAnimator.ofFloat(mAdWebView, "translationY", endPos, endPos);
        upAnimator = ObjectAnimator.ofFloat(mAdWebView, "translationY", endPos, startPos);
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        if (mPresenter != null) {
            SystemTools.unRegisterNetReceiver(this);
            mPresenter.destroy();
        }
    }


    private void initUrlData() {
        SharedPreferences sp = MainActivity.this.getSharedPreferences(RECEIVER_INFO_PREFS, Context.MODE_PRIVATE);
        String pageUrl = sp.getString(RECEIVER_INFO_PREFS_PAGEURL_KEY, "");//主显页面
        if (!TextUtils.isEmpty(pageUrl)) {
            mPageAndAdUrls.add(pageUrl);
        }
        String adUrl = sp.getString(RECEIVER_INFO_PREFS_ADURL_KEY, "");
        if (!TextUtils.isEmpty(adUrl)) {
            mPageAndAdUrls.add(adUrl);
        }
        String noticeAdUrl = sp.getString(RECEIVER_INFO_PREFS_NOTICEADURL_KEY, "");
        if (!TextUtils.isEmpty(noticeAdUrl)) {
            mPageAndAdUrls.add(noticeAdUrl);
        }
        mReceiverIp = sp.getString(RECEIVER_INFO_PREFS_IPKEY, "");
        mReceiverPort = sp.getInt(RECEIVER_INFO_PREFS_PORTKEY, 0);
        mOperatorId = sp.getString(RECEIVER_INFO_PREFS_OPERATORIDKEY, "");//操作员ID add by:tzw@2016.7.25
        mOfficeId = sp.getString(RECEIVER_INFO_PREFS_OFFICEIDKEY, "");//诊室代码 add by:tzw@2016.7.25
    }

    private void initView() {
        mShadowLayout = (RelativeLayout) findViewById(R.id.layout_shadow);
        mLoadingLayout = (RelativeLayout) findViewById(R.id.layout_loading);
        initEditTextView();
        initAdWebView();
        initPageWebView();

        if (mPageAndAdUrls != null && mPageAndAdUrls.size() > 0 && !TextUtils.isEmpty(mPageAndAdUrls.get(0))) {
            refreshPage(mPageAndAdUrls.get(0));
        }
    }

    private void initAdWebView() {
        mAdWebView = (WebView) findViewById(R.id.webview_ad);
        RelativeLayout.LayoutParams adWebViewLayoutParams = new RelativeLayout.LayoutParams(screenWidth / 2, screenHeight / 2);
        adWebViewLayoutParams.addRule(RelativeLayout.CENTER_HORIZONTAL);
        mAdWebView.setLayoutParams(adWebViewLayoutParams);
        mAdWebView.setTranslationY(-(screenHeight / 2));
        WebSettings adWebsettings = mAdWebView.getSettings();
        adWebsettings.setJavaScriptEnabled(true);
        adWebsettings.setAllowFileAccess(true);
        adWebsettings.setCacheMode(WebSettings.LOAD_NO_CACHE);
        adWebsettings.setSupportZoom(true);
        mAdWebView.setWebViewClient(mAdWebViewClient);
        mAdWebView.setWebChromeClient(mWebChromeClient);

    }

    private void initPageWebView() {
        mPageWebView = (WebView) findViewById(R.id.webview_page);
        WebSettings pageWebSetting = mPageWebView.getSettings();
        pageWebSetting.setJavaScriptEnabled(true);
        pageWebSetting.setAllowFileAccess(true);
        pageWebSetting.setCacheMode(WebSettings.LOAD_NO_CACHE);
        pageWebSetting.setSupportZoom(true);
        mPageWebView.setWebViewClient(mPageWebViewClient);
        mPageWebView.setWebChromeClient(mWebChromeClient);
    }

    public void onClick(View v) {
        //捕获单击事件，以免文本框无法获取焦点
    }

    private void initEditTextView() {
        mEdtSendMsg = (EditText) findViewById(R.id.edt_sendmsg);
        if (mPageAndAdUrls != null && mPageAndAdUrls.size() > 0 && TextUtils.isEmpty(mPageAndAdUrls.get(0))) {
            refreshPage(mPageAndAdUrls.get(0));
        }
        mEdtSendMsg.requestFocus();
        mEdtSendMsg.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
//                Toast.makeText(MainActivity.this, "输入文本-->" + s.toString(), Toast.LENGTH_SHORT).show();
//                mEdtSendMsg.removeTextChangedListener(this);
//                mEdtSendMsg.setText(""); //add by:tzw@2016.7.14 清空文本框
//                mEdtSendMsg.addTextChangedListener(this);
            }
        });
        mEdtSendMsg.setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                if (keyCode == KeyEvent.KEYCODE_ENTER && event.getAction() == KeyEvent.ACTION_DOWN) {
                    sendMsg();
                    mEdtSendMsg.setText(""); //add by:tzw@2016.7.14 清空文本框
                    return true;
                }
                return false;
            }
        });
    }

    private void sendMsg() {
        NewMessage msg = new NewMessage();
        //消息类型
        msg.setMessageType(MessageTypeEnum.Notice);
        //返回读取信息 add by:tzw@2016.7.25
        List<String> content = new ArrayList<String>();
        content.add(mEdtSendMsg.getText().toString());
        msg.setContent(content);
        msg.setOfficeId(mOfficeId);//操作员ID add by:tzw@2016.7.25
        msg.setOperatorId(mOperatorId);//诊室代码 add by:tzw@2016.7.25
        //设置数据类型 add by:tzw@2016.7.25
        List<String> dataType = new ArrayList<String>();
        dataType.add("Notice");
        msg.setDataType(dataType);
        //设置服务器IP和端口 此返回数据留作以后扩展功能使用 add by:tzw@2016.7.19
        msg.setSenderIp(mReceiverIp);
        msg.setSenderPort(mReceiverPort);
        //设置回传地址 add by:tzw@2016.7.19
        msg.setReceiverIp(getLocalIpAddress());
        msg.setReceiverPort(Constant.SOCKET_SERVER_PORT); //本地端口 add by:tzw@2016.7.25
        String msgSend = Pub.ToJson(msg); //add by:tzw@2016.7.15
//        Toast.makeText(MainActivity.this, "sendMsg-->" + msgSend, Toast.LENGTH_SHORT).show();
        mPresenter.sendMsg(mReceiverIp, mReceiverPort, msgSend);
    }

    public String getLocalIpAddress() {
        try {
            for (Enumeration<NetworkInterface> en = NetworkInterface.getNetworkInterfaces(); en.hasMoreElements(); ) {
                NetworkInterface intf = en.nextElement();
                for (Enumeration<InetAddress> enumIpAddr = intf.getInetAddresses(); enumIpAddr.hasMoreElements(); ) {
                    InetAddress inetAddress = enumIpAddr.nextElement();
                    if (!inetAddress.isLoopbackAddress() && !inetAddress.isLinkLocalAddress()) {
                        return inetAddress.getHostAddress().toString();
                    }
                }
            }
        } catch (SocketException ex) {
            LogUtil.d(TAG, ex.toString());
        }
        return null;
    }

    private void showShadow() {
        animatorStatus = true;
        mShadowLayout.setVisibility(View.VISIBLE);
    }

    private void hideShadow() {
        animatorStatus = false;
        mShadowLayout.setVisibility(View.GONE);
        mAdWebView.setVisibility(View.GONE);
        isAdPageFinished = false;
    }

    private void showAd() {
        mAdWebView.postDelayed(new Runnable() {
            @Override
            public void run() {
                if (isAdPageFinished) {
                    mAdWebView.setVisibility(View.VISIBLE);
                    showShadow();//显示蒙版 comment by:tzw@2016.7.14
                    mAdWebView.setVisibility(View.VISIBLE);
                    runAnimator();
                }

            }
        }, 10);
    }

    private void refreshAd(String url) {
        if (mAdWebView != null) {
            mAdWebView.loadUrl(url);
        }
    }

    private void refreshPage(String url) {
        LogUtil.d(TAG, "refreshPage-->" + url);
        if (mPageWebView != null) {
            mLoadingLayout.setVisibility(View.GONE);
            mPageWebView.loadUrl(url);
        }
    }

    private void runAnimator() {
        LogUtil.d(TAG, "runAnimator");
        if (animationSet != null && animationSet.isRunning()) {
            animationSet.cancel();
            animationSet = null;
        }
        animationSet = new AnimatorSet();
        animationSet.playSequentially(downAnimator, waitingAnimator, upAnimator);
        animationSet.setTarget(mAdWebView);
        animationSet.setDuration(5000);
        animationSet.addListener(new Animator.AnimatorListener() {
            @Override
            public void onAnimationStart(Animator animation) {

            }

            @Override
            public void onAnimationEnd(Animator animation) {
                if (mAdWebView.getTranslationY() <= startPos) {
                    hideShadow();//隐藏蒙版 comment by:tzw@2016.7.14
                }
            }

            @Override
            public void onAnimationCancel(Animator animation) {

            }

            @Override
            public void onAnimationRepeat(Animator animation) {

            }
        });
        animationSet.start();
    }


    //INHERITED METHOD


    @Override
    public void onReceiveSettingMsg(NewMessage msg) {
        SharedPreferences sp = MainActivity.this.getSharedPreferences(RECEIVER_INFO_PREFS, Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sp.edit();
        editor.putString(RECEIVER_INFO_PREFS_IPKEY, msg.getReceiverIp());
        editor.putInt(RECEIVER_INFO_PREFS_PORTKEY, msg.getReceiverPort());
        mReceiverIp = msg.getReceiverIp();
        mReceiverPort = msg.getReceiverPort();

        if (msg.getContent() != null && !TextUtils.isEmpty(msg.getContent().get(0))) {
            editor.putString(RECEIVER_INFO_PREFS_PAGEURL_KEY, msg.getContent().get(0));
            editor.putString(RECEIVER_INFO_PREFS_ADURL_KEY, msg.getContent().get(1));
            editor.putString(RECEIVER_INFO_PREFS_NOTICEADURL_KEY, msg.getContent().get(2));
            LogUtil.d(TAG, "url 0-->" + msg.getContent().get(0));
            refreshPage(msg.getContent().get(0));
            mPageAndAdUrls.clear();
            mPageAndAdUrls.addAll(msg.getContent());
            mPageWebView.loadUrl(msg.getContent().get(0));
        }
        editor.commit();
    }

    @Override
    public void onReceiveMsgWithPage(NewMessage msg) {
        SettingMsg(msg);
        if (mPageAndAdUrls.size() >= 3) {
            //刷新主页
            String pageUrl = URLFactory.generatePageUrlWithNoContent(mPageAndAdUrls.get(0), msg);
            refreshPage(pageUrl);
        }
    }

    @Override
    public void onReceiveMsgWithAd(NewMessage msg) {
        if (mPageAndAdUrls.size() >= 3) {
            //弹出回传信息
            String adUrl = URLFactory.generateAdUrlWithNoticeDataType(mPageAndAdUrls.get(2), msg);
            refreshAd(adUrl);
        }
    }

    @Override
    public void onReceiveMsgWithAdAndPage(NewMessage msg) {
        SettingMsg(msg);
        if (mPageAndAdUrls.size() >= 3) {
            String adUrl = URLFactory.generateAdUrlWithContent(mPageAndAdUrls.get(1), msg);
            refreshAd(adUrl);

            final String pageUrl = URLFactory.generatePageUrlWithContent(mPageAndAdUrls.get(0), msg);
//            refreshPage(pageUrl);

            mPageWebView.postDelayed(new Runnable() {
                String url = pageUrl;

                public void run() {
                    refreshPage(url);
                }
            }, 5000);

        }
    }

    //设置保存医生信息和诊室信息以便回传使用add by:tzw@016.7.15
    public void SettingMsg(NewMessage msg) {
        SharedPreferences sp = MainActivity.this.getSharedPreferences(RECEIVER_INFO_PREFS, Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sp.edit();
        editor.putString(RECEIVER_INFO_PREFS_OPERATORIDKEY, msg.getOperatorId());
        editor.putString(RECEIVER_INFO_PREFS_OFFICEIDKEY, msg.getOfficeId());
        mOfficeId = msg.getOfficeId();
        mOperatorId = msg.getOperatorId();
    }

    @Override
    public boolean isAnimatorRunning() {
        return animatorStatus;
    }

    @Override
    public void netConnect() {
        if (mPageAndAdUrls != null && mPageAndAdUrls.size() > 0 && !TextUtils.isEmpty(mPageAndAdUrls.get(0))) {
            refreshPage(mPageAndAdUrls.get(0));
        }
    }

    @Override
    public void netDisConnect() {
        myHandler.removeCallbacksAndMessages(null);
        refreshPage(Constant.NET_ERROR);
    }

}
