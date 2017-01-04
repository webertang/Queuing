package com.example.gallery;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;

import com.example.testgallary.R;

public class MainActivity extends Activity {
	//»ÃµÆÆ¬ÑÝÊ¾Ð§¹û
	private Button btnfir;
	//·ÂÌÔ±¦ÏêÇéÒ³ÃæÍ¼Æ¬ä¯ÀÀÐ§¹û
	private Button btnsec;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		init();
		addEvn();
	}
	void init(){
		btnfir = (Button)findViewById(R.id.firbtn);
		btnsec = (Button)findViewById(R.id.secbtn);
	}
	void addEvn(){
		btnfir.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent intent = new Intent(MainActivity.this, ImgSwitchActivity.class);
				startActivity(intent);
			}
		});
		
		btnsec.setOnClickListener(new OnClickListener() {
			
			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				Intent intent = new Intent(MainActivity.this, TaoBaoImgShowActivity.class);
				startActivity(intent);
			}
		});
	}
	
}
