package com.example.gallery;

import java.util.ArrayList;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.os.SystemClock;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Gallery;
import android.widget.ImageView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;

import com.example.testgallary.R;

public class ImgSwitchActivity extends Activity {
	private Gallery myGallery;
	private RadioGroup gallery_points;
	private RadioButton[] gallery_point;
	private LinearLayout layout;
	private LayoutInflater inflater;
	private Context context;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.imgswitch);
		context = getApplicationContext();
		inflater = LayoutInflater.from(context);
		init();
		addEvn();
	}
	//初始化
	void init(){
		myGallery = (DetailGallery)findViewById(R.id.myGallery);
		gallery_points = (RadioGroup) this.findViewById(R.id.galleryRaidoGroup);
		ArrayList<Integer> list = new ArrayList<Integer>();
		list.add(R.drawable.banner1);
		list.add(R.drawable.banner2);
		list.add(R.drawable.banner3);
		GalleryIndexAdapter adapter = new GalleryIndexAdapter(list, context);
		myGallery.setAdapter(adapter);
		//设置小按钮
		gallery_point = new RadioButton[list.size()];
		for (int i = 0; i < gallery_point.length; i++) {
			layout = (LinearLayout) inflater.inflate(R.layout.gallery_icon, null);
			gallery_point[i] = (RadioButton) layout.findViewById(R.id.gallery_radiobutton);
			gallery_point[i].setId(i);/* 设置指示图按钮ID */
			int wh = Tool.dp2px(context, 10);
			RadioGroup.LayoutParams layoutParams = new RadioGroup.LayoutParams(wh, wh); // 设置指示图大小
			gallery_point[i].setLayoutParams(layoutParams);
			layoutParams.setMargins(4, 0, 4, 0);// 设置指示图margin值
			gallery_point[i].setClickable(false);/* 设置指示图按钮不能点击 */
			layout.removeView(gallery_point[i]);//一个子视图不能指定了多个父视图
			gallery_points.addView(gallery_point[i]);/* 把已经初始化的指示图动态添加到指示图的RadioGroup中 */
		}
	}
	//添加事件
	void addEvn(){
		myGallery.setOnItemSelectedListener(new OnItemSelectedListener() {

			@Override
			public void onItemSelected(AdapterView<?> arg0, View arg1,
					int arg2, long arg3) {
				// TODO Auto-generated method stub
				gallery_points.check(gallery_point[arg2%gallery_point.length].getId());
			}

			@Override
			public void onNothingSelected(AdapterView<?> arg0) {
				// TODO Auto-generated method stub
				
			}
		});
	}
	/** 展示图控制器，实现展示图切换 */
	final Handler handler_gallery = new Handler() {
		public void handleMessage(Message msg) {
			/* 自定义屏幕按下的动作 */
			MotionEvent e1 = MotionEvent.obtain(SystemClock.uptimeMillis(),
					SystemClock.uptimeMillis(), MotionEvent.ACTION_UP,
					89.333336f, 265.33334f, 0);
			/* 自定义屏幕放开的动作 */
			MotionEvent e2 = MotionEvent.obtain(SystemClock.uptimeMillis(),
					SystemClock.uptimeMillis(), MotionEvent.ACTION_DOWN,
					300.0f, 238.00003f, 0);
			
			myGallery.onFling(e2, e1, -800, 0);
			/* 给gallery添加按下和放开的动作，实现自动滑动 */
			super.handleMessage(msg);
		}
	};
	protected void onResume() {
		autogallery();
		super.onResume();
	};
	private void autogallery() {
		/* 设置定时器，每5秒自动切换展示图 */
		Timer time = new Timer();
		TimerTask task = new TimerTask() {
			@Override
			public void run() {
				Message m = new Message();
				handler_gallery.sendMessage(m);
			}
		};
		time.schedule(task, 8000, 5000);
	}
	public class GalleryIndexAdapter extends BaseAdapter {
		List<Integer> imagList;
		Context context;
		public GalleryIndexAdapter(List<Integer> list,Context cx){
			imagList = list;
			context = cx;
		}
		@Override
		public int getCount() {
			// TODO Auto-generated method stub
			return Integer.MAX_VALUE;
		}

		@Override
		public Object getItem(int arg0) {
			// TODO Auto-generated method stub
			return null;
		}

		@Override
		public long getItemId(int arg0) {
			// TODO Auto-generated method stub
			return 0;
		}

		@Override
		public View getView(int position, View convertView, ViewGroup arg2) {
			// TODO Auto-generated method stub
			ImageView imageView = new ImageView(context);
			imageView.setBackgroundResource(imagList.get(position%imagList.size()));
			imageView.setScaleType(ScaleType.FIT_XY);
			imageView.setLayoutParams(new Gallery.LayoutParams(Gallery.LayoutParams.FILL_PARENT
					, Gallery.LayoutParams.WRAP_CONTENT));
			return imageView;
		}	
	}
}