using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {
	private GameObject mStepTexture;
	private Transform mCameraTransform;
	private Transform mTransForm;
	private float mTotalWidth =10 * 3;

	void Awake () {
		mTransForm = transform;
		mCameraTransform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		//	mStepTexture = transform.Find ("StepTexture").gameObject;
	}

	void Update () {
		float floorX = mTransForm.localPosition.x;
		if (floorX - mTotalWidth / 2.0f > mCameraTransform.localPosition.x) {
			floorX -= mTotalWidth;
			mTransForm.localPosition = new Vector3 (floorX, 0, 0);
		}
		if (floorX + mTotalWidth / 2.0f < mCameraTransform.localPosition.x) {
			floorX += mTotalWidth;
			mTransForm.localPosition = new Vector3 (floorX, 0, 0);
		}

		//クリアかどうかをチェックする
//		if (floorX < -5000) {
//			mStepTexture.SetActive (true);
//		} else if (mStepTexture.activeSelf) {
//			mStepTexture.SetActive (false);
//		}
	}
}
