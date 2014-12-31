using UnityEngine;
using System.Collections;

using Kayac.Lobi.SDK;

public class LobiRecScene : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Debug.Log("IsReady() = " + (LobiCoreBridge.IsReady() ? "true" : "false"));
		LobiRecBridge.RegisterMicEnableErrorObserver(
			name,
			"MicEnableErrorCallback"
		);
		LobiRecBridge.RegisterDismissingPostVideoViewNotification(
			name,
			"DismissingPostVideoViewCallback"
		);
		
		// set app link listener
		// LobiChatBridge.SetAppLinkListener(name, "SetAppLinkListenerCallback");
	}
	
	void OnGUI()
	{
		if (GUI.Button(new Rect(50, 50, 200, 50), "<-")){
			LobiRecBridge.StopCapturing();
			Application.LoadLevel("MainScene");
		}
		if (GUI.Button(new Rect(50, 150, 200, 50), "StartCapturing")){
			LobiRecBridge.SetMicEnable(true);
			LobiRecBridge.SetMicVolume(1.0f);
			LobiRecBridge.SetGameSoundVolume(0.2f);
			
			LobiRecBridge.SetLiveWipeStatus(LobiRecBridge.LiveWipeStatus.InCamera);
			LobiRecBridge.SetWipePositionX(100.0f);
			LobiRecBridge.SetWipePositionY(100.0f);
			LobiRecBridge.SetWipeSquareSize(100.0f);
			
			LobiRecBridge.SetPreventSpoiler(false);
			LobiRecBridge.SetCapturePerFrame(2);
			LobiRecBridge.StartCapturing();
		}
		if (GUI.Button(new Rect(50, 250, 200, 50), "StopCapturing")){
			LobiRecBridge.StopCapturing();
		}
		if (GUI.Button(new Rect(50, 350, 200, 50), "PresentLobiPost")){
			LobiRecBridge.PresentLobiPost("sample title", "sample description", 0, "", "{\"type\":\"sample video\",\"game_engine\":\"unity\"}");
		}
		if (GUI.Button(new Rect(50, 450, 200, 50), "PresentLobiPlay")){
			LobiRecBridge.PresentLobiPlay();
			// LobiRecBridge.PresentLobiPlay("", "", false, "{\"game_engine\":\"unity\"}");
		}
		if (GUI.Button(new Rect(50, 550, 200, 50), "Snap")){
			LobiRecBridge.Snap(
				name,
				"SnapCallback"
			); 
		}
		if (GUI.Button(new Rect(50, 650, 200, 50), "SnapFace")){
			LobiRecBridge.SnapFace(
				name,
				"SnapFaceCallback"
			); 
		}
		if (GUI.Button(new Rect(50, 750, 200, 50), "IsMicEnabled")){
			LobiRecBridge.IsMicEnabled(
				name,
				"IsMicEnabledCallback"
			);
		}
		if (GUI.Button(new Rect(50, 850, 200, 50), "IsSupported")){
			Debug.Log("IsSupported() = " + (LobiRecBridge.IsSupported() ? "true" : "false"));
		}
	}
 
	void SnapCallback(string message)
	{
		Debug.Log("SnapCallback");
		Debug.Log(message);
	}

	void SnapFaceCallback(string message)
	{
		Debug.Log("SnapFaceCallback");
		Debug.Log(message);
	}

	void IsMicEnabledCallback(string message)
	{
		Debug.Log("IsMicEnabledCallback");
		Debug.Log(message);
	}

	void MicEnableErrorCallback(string message)
	{
		Debug.Log("micEnableError");
	}
	
	void DismissingPostVideoViewCallback(string message)
	{
		Debug.Log(message);
	}

	void SetAppLinkListenerCallback(string data){
		Debug.Log("get data from lobi chat applink:");
		Debug.Log(data);
	}
}
