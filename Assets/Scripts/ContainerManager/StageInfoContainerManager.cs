using UnityEngine;
using System.Collections;
using System;

public class StageInfoContainerManager : MonoBehaviour {

	public static event Action OnStartButtonClickedEvent;
	public UILabel storyLabel;
	public Entity_StageInfo entityStageInfo;
	public int stageInfoId;

	// Use this for initialization
	void Start () {
		string story = "";
		switch(stageInfoId){
		case 1:
			story = entityStageInfo.param [MainSceneManager.instance.GameLevel-1].story_1;
			break;
		case 2:
			story = entityStageInfo.param [MainSceneManager.instance.GameLevel-1].story_2;
			break;
		case 3:
			story = entityStageInfo.param [MainSceneManager.instance.GameLevel-1].story_3;
			break;
		}
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
