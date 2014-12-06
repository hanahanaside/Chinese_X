using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

	public float moveForce;
	public float maxSpeed;
	[HideInInspector]
	public float life = 1.0f;
}
