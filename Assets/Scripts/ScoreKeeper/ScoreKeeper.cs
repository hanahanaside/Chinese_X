using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoSingleton<ScoreKeeper> {
	[HideInInspector]
	public int score{ get; set;}

	public void AddScore(int addScore){
		score += addScore;
	}
}
