using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoSingleton<ScoreKeeper> {

	public UILabel scoreLabel;
	private int score;

	void Start(){
		score = 0;
	}

	void Update () {
		scoreLabel.text = "score : " + score;
	}

	public void AddScore(int addScore){
		score += addScore;
	}
}
