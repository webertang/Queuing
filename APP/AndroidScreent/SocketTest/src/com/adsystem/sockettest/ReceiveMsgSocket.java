package com.adsystem.sockettest;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;

public class ReceiveMsgSocket {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		receiveMsg();
	}
	
	private static void receiveMsg() {
		new Thread(new ReadRunnable()).start();
	}
	
	static class ReadRunnable implements Runnable{
		@Override
		public void run() {
			ServerSocket server = null;
			BufferedReader reader = null;
			Socket socket = null;
			try {
				server = new ServerSocket(Constant.RECEIVER_PORT);
				socket = server.accept();
				reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
				String tempStr = null;
				StringBuilder sb = new StringBuilder();
				while((tempStr = reader.readLine()) != null) {
					sb.append(tempStr);
				}
				System.out.println("收到Android断消息===》" + sb.toString());
			}catch(Exception e) {
				e.printStackTrace();
			}finally {
				try {
					if(socket != null) {
						socket.close();
						socket = null;
					}
					if(server != null) {
						server.close();
						server = null;
					}
					if(reader != null) {
						reader.close();
						reader = null;
					}
				}catch(Exception e) {
					e.printStackTrace();
				}
	
			}
		}
	}

}
