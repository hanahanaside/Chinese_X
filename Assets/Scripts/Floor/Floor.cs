using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {
	private GameObject mStepObject;
	private Transform mCameraTransform;
	private Transform mTransForm;
	private float mTotalWidth = 10 * 3;

	void Start () {
		mTransForm = transform;
		mCameraTransform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		mStepObject = transform.Find ("StepSprite").gameObject;
		mStepObject.SetActive (false);
	}

	void Update () {
		float floorX = mTransForm.position.x;
		if (floorX - mTotalWidth / 2.0f > mCameraTransform.position.x) {
			floorX -= mTotalWidth;
			mTransForm.position = new Vector3 (floorX, 0, 0);
		}
		if (floorX + mTotalWidth / 2.0f < mCameraTransform.position.x) {
			floorX += mTotalWidth;
			mTransForm.position = new Vector3 (floorX, 0, 0);
		}

//		//クリアかどうかをチェックする
		if (floorX < -30) {
			mStepObject.SetActive (true);
		} else if (mStepObject.activeSelf) {
			mStepObject.SetActive (false);
		}
	}
}
