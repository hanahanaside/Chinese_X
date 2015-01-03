using UnityEngine;
using System.Collections;

public class PrefsManager : PlayerPrefsX {
	private static PrefsManager sInstance;

	public static PrefsManager instance {
		get {
			if (sInstance == null) {
				sInstance = new PrefsManager ();
			}
			return sInstance;
		}
	}

	public enum Kies {
		BestScore
	}

	public int BestScore {
		get {
			int bestScore = PlayerPrefs.GetInt (Kies.BestScore.ToString ());
			return bestScore;
		}
		set {
			PlayerPrefs.SetInt (Kies.BestScore.ToString (), value);
			PlayerPrefs.Save ();
		}
	}
}
