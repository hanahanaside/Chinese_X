using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class FelloPush : MonoBehaviour {

	#if UNITY_EDITOR || UNITY_STANDALONE
	#elif UNITY_IPHONE
	bool tokenSent = false;

	[DllImport("__Internal")]
	private static extern void _FelloPushBridgePlugin_SetDeviceId(string deviceId);
	[DllImport("__Internal")]
	private static extern void _FelloPushBridgePlugin_ScheduleLocalNotification(string text, long date, string label);
	[DllImport("__Internal")]
	private static extern void _FelloPushBridgePlugin_CancelLocalNotification(string label);
	[DllImport("__Internal")]
	private static extern void _FelloPushBridgePlugin_SetTagString(string name, string value);
	[DllImport("__Internal")]
	private static extern void _FelloPushBridgePlugin_SetTagInt(string name, int value);

	#elif UNITY_ANDROID
	AndroidJavaObject felloBridge;
	#endif
	
	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR || UNITY_STANDALONE
		#elif UNITY_ANDROID
		felloBridge = new AndroidJavaObject("com.unicon_ltd.konect.sdk.bridge.FelloPushBridgeUnityPlugin");
		felloBridge.Call("init");
		#endif
	}
	
	void OnApplicationPause (bool pause) {
		#if UNITY_EDITOR || UNITY_STANDALONE
		#elif UNITY_ANDROID
		if ( !pause && felloBridge != null ) {
			felloBridge.Call("focus");
		}
		#endif
	}
	
	//	private static DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0);
	
	public void ScheduleLocalNotification (string text, DateTime date, string label) {
		// Unix

		#if UNITY_EDITOR || UNITY_STANDALONE
		#elif UNITY_IPHONE
		DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		TimeSpan span = date - UNIX_EPOCH;
		long unixtime = (long)span.TotalSeconds;
		_FelloPushBridgePlugin_ScheduleLocalNotification(text, unixtime, label);
		#elif UNITY_ANDROID
		if ( felloBridge != null ) {
		DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		TimeSpan span = date - UNIX_EPOCH;
		long unixtime = (long)span.TotalSeconds;
			felloBridge.Call("scheduleLocalNotification", text, unixtime, label);
		}
		#endif
	}
	
	public void CancelLocalNotification (string label) {
		#if UNITY_EDITOR || UNITY_STANDALONE
		#elif UNITY_IPHONE
		_FelloPushBridgePlugin_CancelLocalNotification(label);
		#elif UNITY_ANDROID
		if ( felloBridge != null ) {
			felloBridge.Call("cancelLocalNotification", label);
		}
		#endif
	}

	public void SetNotificationsEnabled (bool enabled) {
		#if UNITY_EDITOR || UNITY_STANDALONE
		#elif UNITY_IPHONE
		#elif UNITY_ANDROID
		if ( felloBridge != null ) {
			felloBridge.Call("setNotificationsEnabled", enabled);
		}
		#endif
	}

	public void SetTag (string name, string value) {
		#if UNITY_EDITOR || UNITY_STANDALONE
		#elif UNITY_IPHONE
		_FelloPushBridgePlugin_SetTagString(name, value);
		#elif UNITY_ANDROID
		if ( felloBridge != null ) {
			felloBridge.Call("setTag", name, value);
		}
		#endif
	}
	
	public void SetTag (string name, int value) {
		#if UNITY_EDITOR || UNITY_STANDALONE
		#elif UNITY_IPHONE
		_FelloPushBridgePlugin_SetTagInt(name, value);
		#elif UNITY_ANDROID
		if ( felloBridge != null ) {
			felloBridge.Call("setTag", name, value);
		}
		#endif
	}

	void Update () {
		#if UNITY_EDITOR || UNITY_STANDALONE
		#elif UNITY_IPHONE
		if ( !tokenSent ) {
			byte[] token = NotificationServices.deviceToken;
			if ( token != null ) {
				string hexToken = System.BitConverter.ToString(token).Replace("-", "").ToLower();
				_FelloPushBridgePlugin_SetDeviceId(hexToken);
				tokenSent = true;
			}
		}
		#endif
	}
	
	public void onLaunchFromNotification(string param) {
		Debug.Log("onLaunchFromNotification:" + param);
	}
}
