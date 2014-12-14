using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public UILabel scoreLabel;
	private int score;
	private static ScoreKeeper sInstance;

	void Start(){
		sInstance = this;
		score = 0;
	}

	void Update () {
		scoreLabel.text = "score : " + score;
	}

	public static ScoreKeeper instance{
		get{
			return sInstance;
		}
	}

	public void AddScore(int addScore){
		score += addScore;
	}
}
