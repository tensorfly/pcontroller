package com.zzzeek.testzxing;

import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.os.Bundle;
import android.os.Vibrator;
import android.util.Log;
import android.view.Menu;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.view.WindowManager;

public class ControlActivity extends Activity {
	
	
	private String SERVERIP;
	private int SERVERPORT;
	
	@Override	
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//hide title bar
		this.requestWindowFeature(Window.FEATURE_NO_TITLE);
		//hide inform bar
		this.getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
	
		
		Bundle b = getIntent().getExtras();
		String[] url = b.getString("url").split(":");
		SERVERIP = url[0];
		SERVERPORT = Integer.parseInt(url[1]);
		
		
		
		setContentView(R.layout.activity_control);
		bindEvents();

	}
	
	private void bindEvents(){
		final ControlActivity context = this;
		ViewGroup layout = (ViewGroup) findViewById(R.id.layout1);
		for(int i=0; i < layout.getChildCount(); i++) {
		    View btn = layout.getChildAt(i);
		    final Integer val = Integer.parseInt(btn.getTag().toString());
			btn.setOnTouchListener(new View.OnTouchListener(){
				public boolean onTouch(View v, MotionEvent e) {
					switch(e.getAction()) {
					case MotionEvent.ACTION_DOWN:
						new Thread(new Talker(val, 1)).start();
						Vibrator vibr = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
						vibr.vibrate(60);	
						return true;
					case MotionEvent.ACTION_UP:
						new Thread(new Talker(val, 0)).start();
						return true;
					default:
						return false;
					}
				}				
			});
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.control, menu);
		return true;
	}
	
	public class Talker implements Runnable {
		private int val, stat;
		public Talker(int v, int s){
			val = v;
			stat = s;
		}
		
		@Override
		public void run(){
			try {
				InetAddress serverAddr = InetAddress.getByName(SERVERIP);
				DatagramSocket skt = new DatagramSocket();
				byte[] buf = new byte[]{(byte) val, (byte)stat};
				DatagramPacket packet = new DatagramPacket(buf, buf.length, serverAddr, SERVERPORT);
				skt.send(packet); 
			} catch (Exception e) {
				Log.e("talker", "error");
			}
		}
	}

}
