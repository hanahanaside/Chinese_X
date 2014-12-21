using UnityEngine;
using System.Collections;

public class StageContainerManager : MonoBehaviour {

	public  void OnAtackButtonClicked () {
//		float v = Input.GetAxis ("Vertical");
//		if (v < 0) {
//			Player.instance.LowKick ();
//		} else {
//			Player.instance.HighKick ();
//		}
		Player.instance.Atack ();
	}

	public void OnJumpButtonClicked () {
		Player.instance.Jump ();
	}

	public void OnJoystick (Vector2 delta) {
		Player.instance.Delta = delta;
	}

}
