using UnityEngine;
using System.Collections;
using System;

public class StoryContainerManager : MonoBehaviour {

	public static event Action OnStoryFinishedEvent;
	public string[] storyTextArray;
	public Texture[] backgroundTextureArray;
	public UILabel storyLabel;
	public UITexture backgroundTexture;
	private TypewriterEffect mTypewriterEffect;
	private int mStoryIndex;

	void Start () {
		mTypewriterEffect = storyLabel.GetComponent<TypewriterEffect>();
		backgroundTexture.mainTexture = backgroundTextureArray[mStoryIndex];
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
