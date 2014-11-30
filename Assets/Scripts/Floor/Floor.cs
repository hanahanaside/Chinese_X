using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {
	public Transform cameraTransform;
	private float mTotalWidth = 1072 * 3;
	private Transform mTransForm;

	void Awake () {
		mTransForm = transform;
	}

	void Update () {
		float floorX = mTransForm.localPosition.x;
		if (floorX - mTotalWidth / 2.0f > cameraTransform.localPosition.x) {
			floorX -= mTotalWidth;
			mTransForm.localPosition = new Vector3 (floorX, 0, 0);
		}
		if (floorX + mTotalWidth / 2.0f < cameraTransform.localPosition.x) {
			floorX += mTotalWidth;
			mTransForm.localPosition = new Vector3 (floorX, 0, 0);
		}
	}
}
