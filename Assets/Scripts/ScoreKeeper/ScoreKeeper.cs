using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoSingleton<ScoreKeeper> {
	[HideInInspector]
	public int score;

	public void AddScore(int addScore){
		score += addScore;
	}
}
