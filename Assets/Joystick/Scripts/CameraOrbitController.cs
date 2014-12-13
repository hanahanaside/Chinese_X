using UnityEngine;
using System.Collections;

public class CameraOrbitController : MonoBehaviour
{
	public Transform target;
	public bool autoRotate = false;
	public bool isMouseControl = false;
	public float x = 0f;
	public float y = 2f;
	public float distance = 3f;
	public float distanceMin = 1f;
	public float distanceMax = 25f;
	public float offsetY = 0.5f;
	public float xSpeed = 0.1f;
	public float ySpeed = 0.25f;
	public float rotAngleYMin = 5f;
	public float rotAngleYMax = 45f;


	void Update()
	{
		if (isMouseControl)
		{
			if (Input.GetMouseButton(1))
				Rotate(new Vector2(Input.GetAxis("Mouse X") * 2f, Input.GetAxis("Mouse Y") * 2f));
			
			Zoom(Input.GetAxis("Mouse ScrollWheel") * 2f);
		}
	}
	
	
	void LateUpdate()
	{
		if (target)
		{
			if (autoRotate)
				x += Time.deltaTime * xSpeed;
			
			// Move the camera in the target position
			transform.position = target.position;
			
			// Rotate the camera

			transform.rotation = Quaternion.Euler(y, 
												  target.transform.rotation.eulerAngles.y + x, 
												  transform.rotation.eulerAngles.z);
			
			// Move the camera back to the distance
			transform.position = transform.position -(transform.forward * distance) +(transform.up * offsetY);
		}
	}
	
	
	/// <summary>
	/// Zoom the camera
	/// </summary>
	public void Zoom(float zoom)
	{
		if (target && zoom != 0)
		{
			distance += zoom;
			if (distance < distanceMin)
				distance = distanceMin;
			if (distance > distanceMax)
				distance = distanceMax;
		}
	}


	/// <summary>
	/// Rotate camera around target
	/// </summary>
	public void Rotate(Vector2 delta)
	{
		x += delta.x * xSpeed;
		y -= delta.y * ySpeed;
		y = ClampAngle(y, rotAngleYMin, rotAngleYMax);
	}


	static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
}