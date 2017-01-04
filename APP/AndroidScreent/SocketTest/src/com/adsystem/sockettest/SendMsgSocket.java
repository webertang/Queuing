package com.adsystem.sockettest;

import java.io.PrintWriter;
import java.net.InetSocketAddress;
import java.net.Socket;

public class SendMsgSocket {
	
	private final static String ANDROID_IP = "";
	private final static int PORT = 9999;
	
	public static void main(String[] args) {
		sendMsg();
	}
	
	private static void sendMsg() {
		new Thread(new WriteRunnable()).start();
	}
	
	static class WriteRunnable implements Runnable {
		@Override
		public void run() {
			Socket socket = null;
			PrintWriter writer = null;;
			try{
				socket = new Socket();
				socket.connect(new InetSocketAddress(ANDROID_IP, PORT), 5000);
				writer = new PrintWriter(socket.getOutputStream());
				String msg = NewMessageFactory.createMofiyMessage(Constant.RECEIVER_IP, Constant.RECEIVER_PORT);
				System.out.println(msg);
				writer.write(msg);
				writer.flush();
			}catch(Exception e) {
				e.printStackTrace();
			}finally {
				try {
					if(socket != null) {
						socket.close();
						socket = null;
					}
					if(writer != null) {
						writer.close();
						writer = null;
					}
				}catch(Exception e) {
					e.printStackTrace();
				}
			}
		}
	}
	
	

}
