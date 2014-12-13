using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TouchCamera : MonoBehaviour
{
	public GameObject targetCamera;
	
	
	void Start()
	{
		if (targetCamera == null)
			FindTargetCamera();
	}
	
	
	void OnDrag(Vector2 delta)
	{
		if (targetCamera != null)
			targetCamera.SendMessage("Rotate", delta, SendMessageOptions.DontRequireReceiver);
	}
	
	
	/// <summary>
	/// Automatically find the target camera
	/// </summary>
	
	bool FindTargetCamera()
	{
		targetCamera = GameObject.FindWithTag("MainCamera");
		
		if (targetCamera != null)
			return true;
		else
			Debug.LogWarning("CameraTouch.targetCamera did not assign!");
			
		return false;
	}
}
