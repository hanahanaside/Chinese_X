using UnityEngine;
using System.Collections;

/// <summary>
/// This is a float joystick script, automatically show and hide. Show on touch position.
/// 
/// Author: Syberex (syberex@rambler.ru 2013)
/// Version: 1.2
/// </summary>

public class JoystickFloat : MonoBehaviour
{
	/// <summary>
	/// Constraints move Joystick
	/// </summary>
	public enum Constraints
	{
		None,
		Horizontal,
		Vertical
	}

	public Transform joystick;
	public Transform center;
	/// <summary>
	/// If true, generate message OnJoystickRotate(float rot).
	/// rot = (left) -Pi .. 0 (forward) .. Pi (right)
	/// </summary>
	public bool isRotation = false;
	public Constraints constraint = Constraints.None;
	
	float joystickRadius = 0f;
	Plane plane;
	int cntFrame;
	int cntFramePressed = 0;
	bool isPressed = false;
	//Vector3 lastPos = Vector3.zero;
	Vector3 prevPos = Vector3.zero;
	Transform mTrans;
	
	
	void Awake()
	{
		mTrans = transform;
	}
	
	
	void Start()
	{
		// Create the plane to drag along
		plane = new Plane(transform.forward, transform.position);
		
		if (joystick == null)
		{
			joystick = transform.FindChild("Joystick");
			if (joystick == null)
				Debug.LogWarning("Child object Joystick is not found.");
			else if (center == null)
			{
				center = joystick.FindChild("Center");
				if (center == null)
					Debug.LogWarning("Child object Center is not found.");
			}
		}
		
		if (joystick != null)
		{
			joystickRadius = ((SphereCollider) joystick.collider).radius;
			joystick.collider.enabled = false;	// need only radius
			joystick.gameObject.SetActive(false);
		}
	}


	// Update is called once per frame
	void Update()
	{
		cntFrame++;
	}
	
	
	void LateUpdate()
	{
		if (isPressed && cntFramePressed < cntFrame)
		{
			SendMessageOnJoystick(prevPos);
			cntFramePressed = cntFrame;
		}
	}


	/// <summary>
	/// Press and show the joystick
	/// </summary>
	void OnPress(bool pressed)
	{
		if (joystick != null)
		{
			if (pressed && !isPressed)
			{
				prevPos = Vector3.zero;
				CalcPositionJoystick();
				// Show joystick
				joystick.gameObject.SetActive(true);		
				center.localPosition = Vector3.zero;
				isPressed = true;
			}
			else if (pressed && isPressed)
			{
				CalcPositionCenter();
			}
			else
			{
				// Hide joystick
				joystick.gameObject.SetActive(false);
				isPressed = false;
			}
		}
	}


	/// <summary>
	/// Drag the center
	/// </summary>
	void OnDrag(Vector2 delta)
	{
		prevPos = delta;
		if (center != null)
			CalcPositionCenter();
	}
	
	
	void CalcPositionJoystick()
	{
		Ray ray = UICamera.currentCamera.ScreenPointToRay (UICamera.lastTouchPosition);
		float dist = 0f;
		Vector3 newPos1 = joystick.position;
		
		if (plane.Raycast(ray, out dist))
			newPos1 = ray.GetPoint(dist);
		
		joystick.localPosition = mTrans.InverseTransformPoint(newPos1);
	}
	

	void CalcPositionCenter()
	{
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.lastTouchPosition);
		float dist = 0f;
		Vector3 newPos1 = center.position;
		
		if (plane.Raycast(ray, out dist))
			newPos1 = ray.GetPoint(dist);
		
		newPos1 = joystick.InverseTransformPoint(newPos1);
		
		if (newPos1.magnitude > joystickRadius)
			newPos1 = newPos1.normalized * joystickRadius;
		
		// Apply constraint
		if (constraint == Constraints.Horizontal)
			newPos1.y = 0f;
		else if (constraint == Constraints.Vertical)
			newPos1.x = 0f;
		
		center.localPosition = newPos1;
		
		SendMessageOnJoystick(newPos1);
	}
	

	/// <summary>
	/// Joystick event.
	/// Return Vector2, x - rotation (-Pi..Pi), y - radius (0..1)
	/// </summary>
	void SendMessageOnJoystick(Vector3 newPos)
	{
		float x = newPos.x / joystickRadius;
		float y = newPos.y / joystickRadius;
				
		Vector2 delta = new Vector2(x > 1f ? 1f : x, y > 1f ? 1f : y);
		if (!isRotation)
			gameObject.SendMessage("OnJoystick", delta, SendMessageOptions.DontRequireReceiver);
		else
		{
			Vector2 rot = new Vector2(Polar(delta.y, delta.x), delta.magnitude);
			gameObject.SendMessage("OnJoystickRotation", rot, SendMessageOptions.DontRequireReceiver);
		}
		prevPos = newPos;
	}
	
	
	/// <summary>
	/// Converting between Cartesian coordinates and polar,
	/// return angle in rad
	/// </summary>
	float Polar(float x, float y)
	{
		float f = 0f;	// if (x == 0 && y == 0)
		if (x > 0)
			f = Mathf.Atan (y / x);
		else if (x < 0 && y >= 0)
			f = Mathf.Atan (y / x) + Mathf.PI;
		else if (x < 0 && y < 0)
			f = Mathf.Atan (y / x) - Mathf.PI;
		else if (x == 0 && y > 0)
			f = Mathf.PI / 2f;
		else if (x == 0 && y < 0)
			f = -Mathf.PI / 2f;
		
		return f;
	}
}
