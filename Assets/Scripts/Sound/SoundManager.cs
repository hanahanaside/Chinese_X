using UnityEngine;
using System.Collections;

public class SoundManager : MonoSingleton<SoundManager> {

	public enum SECannel{
		Damage_Player,
		Jump,
		Death_Player,
		Step,
		HighKick,
		LowKick,
		Go,
		Button,
		Damage_Enemy,
		Death_Enemy
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
			switch(i){
			case (int)SECannel.Go:
				mSEAudioSourceArray [i].loop = true;
				break;
			}
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

	public void StopSE(SECannel channel){
		mSEAudioSourceArray [(int)channel].Stop ();
	}
}
