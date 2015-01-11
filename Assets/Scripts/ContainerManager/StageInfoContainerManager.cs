using UnityEngine;
using System.Collections;
using System;

public class StageInfoContainerManager : MonoBehaviour {

	public static event Action OnStartButtonClickedEvent;
	public UILabel storyLabel;
	public Entity_StageInfo entityStageInfo;

	// Use this for initialization
	void Start () {
		int rand = UnityEngine.Random.Range (0,9);
		string story = entityStageInfo.param[rand].story_1;
		storyLabel.text = story;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartButtonClicked(){ 
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
		OnStartButtonClickedEvent ();
		Destroy (transform.parent.gameObject);
	}
}
