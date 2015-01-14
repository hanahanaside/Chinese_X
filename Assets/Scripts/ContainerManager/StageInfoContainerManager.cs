using UnityEngine;
using System.Collections;
using System;

public class StageInfoContainerManager : MonoBehaviour {

	public static event Action OnStartButtonClickedEvent;
	public UILabel storyLabel;
	public Entity_StageInfo entityStageInfo;

	// Use this for initialization
	void Start () {
		string story = "";
		if (Application.systemLanguage == SystemLanguage.Japanese) {
			int rand = UnityEngine.Random.Range (0,9);
			story = entityStageInfo.param[rand].story_1;
		}else {
			int rand = UnityEngine.Random.Range (0,4);
			story = entityStageInfo.param[rand].story_2;
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
