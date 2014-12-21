using UnityEngine;
using System.Collections;

public class StageContainerManager : MonoBehaviour {

	public  void OnAtackButtonClicked () {
		Player.instance.Atack ();
	}

	public void OnJumpButtonClicked () {
		Player.instance.Jump ();
	}

	public void OnJoystick (Vector2 delta) {
		Player.instance.Delta = delta;
	}

}
