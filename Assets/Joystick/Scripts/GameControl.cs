using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{
	public TankController tank;
	
	
	void OnJoystick(Vector2 delta)
	{
		if (tank != null)
			tank.Move(delta.x, delta.y);
	}
	
	
	void OnJoystickRotation(Vector2 rot)
	{
		if (tank != null)
		{
			// If joystick moved less 0.5, then only shoot
			if (rot.y > 0.5f)
				tank.TurretRotate(rot.x);
			tank.Shoot();
		}
	}
	
	
	void OnClick()
	{
		if (tank != null && gameObject.name == "ButtonFire")
			tank.Shoot();
	}
}
