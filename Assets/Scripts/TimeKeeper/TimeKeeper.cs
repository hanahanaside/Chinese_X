using UnityEngine;
using System.Collections;
using System;

public class TimeKeeper : MonoBehaviour {

	public static event Action TimeUpEvent;

	public UILabel remainingTimeLabel;
	public float time;

	void Update () {
		time -= Time.deltaTime;
		remainingTimeLabel.text = "" + (int)time;
		if(time < 0 ){
			TimeUpEvent ();
		}
	}
}
