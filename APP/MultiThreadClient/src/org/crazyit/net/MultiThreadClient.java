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
	// ��������ϵ������ı���
	EditText input1, ip1;
	TextView show;
	// ��������ϵ�һ����ť
	Button send1, btn, send2;
	Handler handler;
	// �����������ͨ�ŵ����߳�
	// ClientThread clientThread;
	private static final int PORT = 9999;
	private List<Socket> mList = new ArrayList<Socket>();
	private volatile ServerSocket server = null;
	private ExecutorService mExecutorService = null; // �̳߳�
	private Handler myHandler = null;
	private volatile boolean flag = true;// �̱߳�־λ
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
		ip1.setText(this.getLocalIpAddress());// ����������ʼIP��ַ
		ServerThread serverThread = new ServerThread();
		flag = true;
		serverThread.start();// ��������
		// ȡ�÷�UI�̴߳�����msg���Ըı����
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
			mExecutorService = Executors.newCachedThreadPool(); // ����һ���̳߳�
			System.out.println("������������...");
			Socket client = null;
			while (flag) {
				try {
					client = server.accept();
					// �ѿͻ��˷���ͻ��˼�����
					mList.add(client);
					mExecutorService.execute(new Service(client)); // ����һ���µ��߳�����������
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
				// msgLocal.obj = "��ͻ������ӳɹ�";
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

		// ��ͻ��˷�����Ϣ
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