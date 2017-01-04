package com.example.viewpagetest;

import javax.crypto.Cipher;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.DESKeySpec;
import javax.crypto.spec.IvParameterSpec;

import android.annotation.SuppressLint;
import android.util.Base64;

import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.TypeReference;

/**
 * Created by Administrator on 2016/1/14.
 */
public class Pub {
	public static String ToJson(Object ret) {
		return JSON.toJSONString(ret);
	}

	public static <T> T ToT(String ret, TypeReference<T> cls) {
		return JSON.parseObject(ret, cls);
	}

	public static <T> T ToT(String ret, Class<T> cls) {
		return JSON.parseObject(ret, cls);
	}


	/**
	 * des解密
	 * 
	 * @param message
	 *            密文
	 * @param key
	 *            秘钥
	 * @param iv
	 *            公钥
	 * @return 明文
	 * @throws Exception
	 */
	public static String Decrypt(String message, String key, String iv)
			throws Exception {
		 byte[] bytesrc = Base64.decode(message.getBytes("UTF-8"),
		 Base64.DEFAULT);

		byte[] Key = key.getBytes("UTF-8");
		byte[] IV = iv.getBytes("UTF-8");
		if (Key.length != 8) {
			byte[] tmp = new byte[8];

			for (int i = 0; i < tmp.length; i++) {
				tmp[i] = Key[i];

				if (i == Key.length - 1) {
					break;
				}
			}

			Key = tmp;
		}

		if (IV.length != 8) {
			byte[] tmp = new byte[8];

			for (int i = 0; i < tmp.length; i++) {
				tmp[i] = IV[i];

				if (i == IV.length - 1) {
					break;
				}
			}

			IV = tmp;
		}

		Cipher cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");
		DESKeySpec desKeySpec = new DESKeySpec(Key);
		SecretKeyFactory keyFactory = SecretKeyFactory.getInstance("DES");
		SecretKey secretKey = keyFactory.generateSecret(desKeySpec);
		IvParameterSpec ivp = new IvParameterSpec(IV);
		cipher.init(Cipher.DECRYPT_MODE, secretKey, ivp);

		byte[] retByte = cipher.doFinal(bytesrc);
		return new String(retByte, "UTF-8");
	}

	/**
	 * des加密
	 * 
	 * @param message
	 *            明文
	 * @param key
	 *            秘钥
	 * @param iv
	 *            公钥
	 * @return 密文
	 * @throws Exception
	 */
	@SuppressLint("TrulyRandom")
	public static String Encrypt(String message, String key, String iv)
			throws Exception {
		byte[] Key = key.getBytes("UTF-8");
		byte[] IV = iv.getBytes("UTF-8");
		if (Key.length != 8) {
			byte[] tmp = new byte[8];

			for (int i = 0; i < tmp.length; i++) {
				tmp[i] = Key[i];

				if (i == Key.length - 1) {
					break;
				}
			}

			Key = tmp;
		}

		if (IV.length != 8) {
			byte[] tmp = new byte[8];

			for (int i = 0; i < tmp.length; i++) {
				tmp[i] = IV[i];

				if (i == IV.length - 1) {
					break;
				}
			}

			IV = tmp;
		}

		Cipher cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");
		DESKeySpec desKeySpec = new DESKeySpec(Key);
		SecretKeyFactory keyFactory = SecretKeyFactory.getInstance("DES");
		SecretKey secretKey = keyFactory.generateSecret(desKeySpec);
		IvParameterSpec ivp = new IvParameterSpec(IV);
		cipher.init(Cipher.ENCRYPT_MODE, secretKey, ivp);

		return new String(Base64.encode(
				cipher.doFinal(message.getBytes("UTF-8")), Base64.DEFAULT),
				"UTF-8");
	}
}
