package org.crazyit.net;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketException;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class MultiThreadClient extends Activity {
	// 定义界面上的两个文本框
	EditText input1, ip1;
	TextView show;
	// 定义界面上的一个按钮
	Button send1, btn, send2;
	Handler handler;
	// 定义与服务器通信的子线程
	// ClientThread clientThread;
	private static final int PORT = 9999;
	private List<Socket> mList = new ArrayList<Socket>();
	private volatile ServerSocket server = null;
	private ExecutorService mExecutorService = null; // 线程池
	private Handler myHandler = null;
	private volatile boolean flag = true;// 线程标志位
	private int count = 0;
	public EditText input2;

	// private Socket socket;

	public static String getLocalIpAddress() {
		try {
			for (Enumeration<NetworkInterface> en = NetworkInterface
					.getNetworkInterfaces(); en.hasMoreElements();) {
				NetworkInterface intf = en.nextElement();
				for (Enumeration<InetAddress> enumIpAddr = intf
						.getInetAddresses(); enumIpAddr.hasMoreElements();) {
					InetAddress inetAddress = enumIpAddr.nextElement();
					if (!inetAddress.isLoopbackAddress()
							&& !inetAddress.isLinkLocalAddress()) {
						return inetAddress.getHostAddress().toString();
					}
				}
			}
		} catch (SocketException ex) {
			Log.e("WifiPreference IpAddress", ex.toString());
		}
		return null;
	}

	String msg1 = null;
	int linkflag = 0;

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main);
		show = (TextView) findViewById(R.id.show);
		ip1 = (EditText) findViewById(R.id.ip1);
		ip1.setText(this.getLocalIpAddress());// 设置输入框初始IP地址
		ServerThread serverThread = new ServerThread();
		flag = true;
		serverThread.start();// 启动服务
		// 取得非UI线程传来的msg，以改变界面
		myHandler = new Handler() {
			@SuppressLint("HandlerLeak")
			public void handleMessage(Message msg) {
				// if (msg.what == 0x1234 && count < 25) {
				// count++;
				// show.append(msg.obj.toString());
				show.setText(msg.obj.toString());
				// } else {
				// show.setText("");
				// show.append(msg.obj.toString() + '\n');
				// count = 0;
				// }
			}
		};
	}

	class ServerThread extends Thread {
		public void stopServer() {
			try {
				if (server != null) {
					server.close();
					System.out.println("close task successed");
				}
			} catch (IOException e) {
				System.out.println("close task failded");
			}
		}

		public void run() {

			try {
				server = new ServerSocket(PORT);
			} catch (IOException e1) {
				e1.printStackTrace();
			}
			mExecutorService = Executors.newCachedThreadPool(); // 创建一个线程池
			System.out.println("服务器已启动...");
			Socket client = null;
			while (flag) {
				try {
					client = server.accept();
					// 把客户端放入客户端集合中
					mList.add(client);
					mExecutorService.execute(new Service(client)); // 启动一个新的线程来处理连接
				} catch (IOException e) {
					e.printStackTrace();
				}
			}

		}
	}

	class Service implements Runnable {
		private volatile boolean kk = true;
		private Socket socket;
		private BufferedReader in = null;
		private String msg = "";

		public Service(final Socket socket) {
			this.socket = socket;
			try {
				this.socket.setSoTimeout(10000);
				in = new BufferedReader(new InputStreamReader(
						socket.getInputStream()));
				msg = "connect service success";
				this.sendmsg(msg);
				// Message msgLocal = new Message();
				// msgLocal.what = 0x1234;
				// msgLocal.obj = "与客户端连接成功";
				// System.out.println(msgLocal.obj.toString());
				System.out.println(msg);
				// myHandler.sendMessage(msgLocal);
			} catch (IOException e) {
				e.printStackTrace();
			}
		}

		public void run() {
			while (kk) {
				try {
					if ((msg = in.readLine()) != null) {
						Message msgLocal = new Message();
						msgLocal.what = 0x1234;
						msgLocal.obj = java.net.URLDecoder.decode(msg, "utf-8");
						;
						myHandler.sendMessage(msgLocal);
					}
					mList.remove(socket);
					in.close();
					socket.close();
					break;
				} catch (IOException e) {
					System.out.println("close");
					kk = false;
					e.printStackTrace();
				}

			}
		}

		// 向客户端发送信息
		public void sendmsg(String msg) {
			System.out.println(msg);
			PrintWriter pout = null;
			try {
				pout = new PrintWriter(new BufferedWriter(
						new OutputStreamWriter(socket.getOutputStream())), true);
				pout.println(msg);
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
	}
}