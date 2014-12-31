using UnityEngine;
using System.Collections;

/// <summary>
/// シングルトンクラスのベース
/// </summary>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	// 唯一のインスタンス
	private static T _Instance;

	/// <summary>
	/// インスタンス取得
	/// </summary>
	public static T Instance {
		get {
			if (_Instance == null) {
				_Instance = (T)FindObjectOfType (typeof(T));

				if (_Instance == null) {
					Debug.LogError (typeof(T) + "is nothing");
				}
			}
			return _Instance;
		}
	}
	// シーン切り替えで死なないフラグ
	[SerializeField]
	private bool _DontDestroyOnLoad = true;

	/// <summary>
	/// ※継承先でAwakeをオーバーライドする場合には、「base.Awake();」を必ず呼び出すこと
	/// </summary>
	protected virtual void Awake ()
	{
		if (this != Instance) {
			// 2個以上ある場合は死ぬ
			GameObject obj = this.gameObject;
			Destroy (this);
			Destroy (obj);
			return;
		}
		if (_DontDestroyOnLoad) {
			// シーン切替で死なない
			DontDestroyOnLoad (this.gameObject);
		}
	}

	protected virtual void Start (){
	}


	protected virtual void Update (){
	}


	/// <summary>
	/// 強制インスタンス削除
	/// </summary>
	public void InstanceDestroy ()
	{
		Destroy (_Instance);
	}
}

