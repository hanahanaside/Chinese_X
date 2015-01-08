using UnityEngine;
using System.Collections;

public class Test : MonoSingleton<Test> {

	void Start(){
		TweenAlpha ta = TweenAlpha.Begin (gameObject,0,1);
		ta.style = UITweener.Style.PingPong;
	}

}
