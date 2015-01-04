using UnityEngine;
using System.Collections;
using System;

public class StoryContainerManager : MonoBehaviour {

	public static event Action OnStoryFinishedEvent;
	public Entity_Story entityStory;
	private string[] storyTextArray;
	public Texture[] backgroundTextureArray;
	public UILabel storyLabel;
	public UITexture backgroundTexture;
	private TypewriterEffect mTypewriterEffect;
	private int mStoryIndex;

	void Start () {
		mTypewriterEffect = storyLabel.GetComponent<TypewriterEffect>();
		backgroundTexture.mainTexture = backgroundTextureArray[mStoryIndex];
		storyTextArray = new string[3];
		string story = "";
		Entity_Story.Param param = entityStory.param[MainSceneManager.instance.GameLevel -1];
		for(int i = 0;i < storyTextArray.Length;i ++){
			switch(i){
			case 0:
				story = param.story_1;
				break;
			case 1:
				story = param.story_2;
				break;
			case 2:
				story = param.story_3;
				break;
			}
			storyTextArray [i] = story;
		}
		storyLabel.text = storyTextArray[mStoryIndex];
	}

	public void OnSkipButtonClicked(){
		mTypewriterEffect.Finish ();
		mStoryIndex++;
		if(mStoryIndex >= storyTextArray.Length){
			OnStoryFinishedEvent ();
			Destroy (transform.parent.gameObject);
		}else {
			backgroundTexture.mainTexture = backgroundTextureArray[mStoryIndex];
			storyLabel.text = storyTextArray[mStoryIndex];
			mTypewriterEffect.ResetToBeginning ();
		}
		SoundManager.instance.PlaySE (SoundManager.SECannel.Button);
	}
}
