using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Kayac.Lobi.SDK
{
	public class LobiRec : MonoBehaviour {

		private static int width = -1;
		private static int height = -1;
		
		public static void SetResolution(int w, int h) {
			width = w;
			height = h;
			#if !UNITY_ANDROID
			Debug.Log("unsupported");
			#endif
		}

		#if UNITY_ANDROID || ((UNITY_IOS || UNITY_IPHONE) && ! UNITY_EDITOR)
		private static LobiRec instance = null;

		void Awake() {
			if(instance != null) {
				Destroy(this);
				return;
			}
			DontDestroyOnLoad(this.gameObject);
			instance = this;
			InitLobiRec();
			AutoRecord();
		}

		void OnLevelWasLoaded(int level) {
			AutoRecord();
		}

		private static LobiRec Instance {
			get {
				return instance;
			}
		}

		void OnApplicationPause(bool pauseStatus) {
			if (pauseStatus) {
				OnPauseActivity();
			} else {
				OnResumeActivity();
			}
		}

		void AutoRecord() {
			Camera[] cameraList = (Camera[]) FindObjectsOfType (typeof(Camera));
			int attached = 0;
			foreach (Camera camera in cameraList) {
				if (camera.gameObject.GetComponent<LobiRecCamera>() != null) {
					attached++;
				}
			}
			if (attached == 0) {
				foreach (Camera camera in cameraList) {
					if (camera.targetTexture == null) {
						camera.gameObject.AddComponent("LobiRecCamera");
						attached++;
					}
				}
			}
		}
		
		IEnumerator RecordRenderBuffer() {
		    while(true) {
		        yield return new WaitForEndOfFrame();
				OnEndOfFrame();
		    }
		} 
		#endif

		#if ((UNITY_IOS || UNITY_IPHONE) && ! UNITY_EDITOR)
		void Start() {
        	AudioSettings.outputSampleRate = 44100;
			StartCoroutine(RecordRenderBuffer());
		}
		#elif UNITY_ANDROID
		void Start() {
			StartCoroutine(RecordRenderBuffer());
		}
		#endif

		public static void InitLobiRec() {
			string version = 
			#if UNITY_4_2
				"4.2";
			#elif UNITY_4_3
				"4.3";
			#else
				"4.5";
			#endif
			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass jclz = new AndroidJavaClass("com.kayac.lobi.sdk.rec.unity.LobiRecUnity");
			jclz.CallStatic("initCapture", activity, version, width, height);
			#endif
			#if ((UNITY_IOS || UNITY_IPHONE) && ! UNITY_EDITOR)
			initLobiRec_();
			#endif
		}

		public static void CameraPreRender() {
			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.kayac.lobi.sdk.rec.unity.LobiRecUnity");
			jc.CallStatic("cameraPreRender");
			#endif
			#if ((UNITY_IOS || UNITY_IPHONE) && ! UNITY_EDITOR)
			cameraPreRender_();
			#endif
		}

		public static void CameraCaptureAndRender() {
			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.kayac.lobi.sdk.rec.unity.LobiRecUnity");
			jc.CallStatic("cameraCaptureAndRender");
			#endif
		}
		
		public static void OnEndOfFrame() {
			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.kayac.lobi.sdk.rec.unity.LobiRecUnity");
			jc.CallStatic("onEndOfFrame");
			#endif
			#if ((UNITY_IOS || UNITY_IPHONE) && ! UNITY_EDITOR)
			onEndOfFrame_();
			#endif
		}

		public static void OnResumeActivity() {
			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.kayac.lobi.sdk.rec.unity.LobiRecUnity");
			jc.CallStatic("onResumeActivity");
			#endif
			#if ((UNITY_IOS || UNITY_IPHONE) && ! UNITY_EDITOR)
			LobiRec_resume_();
			#endif
		}

		public static void OnPauseActivity() {
			#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass jc = new AndroidJavaClass("com.kayac.lobi.sdk.rec.unity.LobiRecUnity");
			jc.CallStatic("onPauseActivity");
			#endif
			#if ((UNITY_IOS || UNITY_IPHONE) && ! UNITY_EDITOR)
			LobiRec_pause_();
			#endif
		}

		#if ((UNITY_IOS || UNITY_IPHONE) && ! UNITY_EDITOR)
		[DllImport("__Internal")]
		private static extern void cameraPreRender_();

		[DllImport("__Internal")]
		private static extern void onEndOfFrame_();
		
		[DllImport("__Internal")]
		private static extern void initLobiRec_();

		[DllImport("__Internal")]
		private static extern void LobiRec_pause_();

		[DllImport("__Internal")]
		private static extern void LobiRec_resume_();
		#endif
	}
}
