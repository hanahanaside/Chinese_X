using UnityEngine;
using System.Collections;

public class SoundManager : MonoSingleton<SoundManager> {

	public enum SECannel{
		Damage,
		Jump,
		Death,
		Step,
		HighKick,
		LowKick
	}

	public enum BGMChannel{
		Opening,
		Story,
		Stage_1,
		Stage_2,
		Stage_3,
		Boss,
		StageInfo,
		Gameover
	}

	public AudioClip[] seClipArray;
	public AudioClip[] bgmClipArray;
	private AudioSource[] mSEAudioSourceArray;
	private AudioSource mBGMAudioSource;

	public override void OnInitialize(){
		mBGMAudioSource = gameObject.AddComponent<AudioSource> ();
		mBGMAudioSource.loop = true;

		mSEAudioSourceArray = new AudioSource[seClipArray.Length];
		for (int i = 0; i < mSEAudioSourceArray.Length; i++) {
			mSEAudioSourceArray[i] = gameObject.AddComponent<AudioSource> ();
			mSEAudioSourceArray[i].clip = seClipArray[i];
		}
	}

	public void PlayBGM(BGMChannel channel){
		mBGMAudioSource.clip = bgmClipArray[(int)channel];
		mBGMAudioSource.Play ();
	}

	public void PlaySE(SECannel channel){
		mSEAudioSourceArray [(int)channel].Play ();
	}
}
