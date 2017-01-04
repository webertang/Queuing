//package com.test.addsystem.util;
//
//
///**
// * Created by Administrator on 2016/7/10.
// */
//public class Gson {
//
//    private final static String TAG = Gson.class.getSimpleName();
//
//    private static Gson instance = new Gson();
//    private static com.google.gson.Gson mGoogleGson;
//    private Gson() {
//        mGoogleGson = new com.google.gson.Gson();
//    }
//
//    public static Gson getInstance() {
//        return instance;
//    }
//
//    public String toJson(Object srcObj) {
//        return mGoogleGson.toJson(srcObj);
//    }
//
//    public <T> T fromJson(String jsonStr, Class<T> cls) {
//        return mGoogleGson.fromJson(jsonStr, cls);
//    }
//
//
//}
