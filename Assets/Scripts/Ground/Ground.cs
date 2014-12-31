using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	private Transform mCameraTransform;
	private Transform mTransForm;

	// Use this for initialization
	void Start () {
		mTransForm = transform;
		mCameraTransform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
		mTransForm.position = new Vector3 (mCameraTransform.position.x, -2.15f, 0);
	}
}
