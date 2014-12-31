using UnityEngine;
using System.Collections;

public class StageContainerManager : MonoBehaviour {

	public UILabel scoreLabel;

	void Update(){
		scoreLabel.text = "score : " + ScoreKeeper.instance.score;
	}

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
