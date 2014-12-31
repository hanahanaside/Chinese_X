using UnityEngine;
using System.Collections;

using Kayac.Lobi.SDK;

// ---------------------------------------------------------
// LobiUtil.cs
// https://github.com/kayac/Lobi/wiki/iOS-Implement-LobiRankingSDK-Unity
// ---------------------------------------------------------
public class LobiUtil : SingletonMonoBehaviour<LobiUtil>
{
	// ==================
	//      フィールド 
	// ==================
	public string[] RANKING_IDS;


	// ==================
	//    ライフサイクル 
	// ==================

	// constructor 
	public LobiUtil() : base()  {}


	/// <summary>
	/// 初期化
	/// </summary>
	protected override void Start () 
	{
		base.Start ();
	}
		

	// ==================
	//    Public メソッド 
	// ==================

	/// <summary>
	/// Lobiのランキングを表示します。
	/// </summary>
	public void showRanking ()
	{
		LobiRankingBridge.PresentRanking ();
	}


	/// <summary>
	/// スコアを送信
	/// </summary>
	/// <param name="collback">Collback.</param>
	/// <param name="ranking_id">Ranking_id.</param>
	/// <param name="score">Score.</param>
	public void sendScore(GameObject callback, string ranking_id, int score)
	{
		// アカウントが作成済みかチェック
		if (LobiCoreBridge.IsReady ()) {
			Debug.Log("== ランキング 送信 ==");
			Debug.Log("== "+ ranking_id + " ==");
			Debug.Log("== "+ score + "点 ==");
			LobiRankingAPIBridge.SendRanking (this.gameObject.name, 
				"RankingCallback", 
				ranking_id,
				score);
		} else {
			Debug.Log("== Lobiアカウントが有効ではありません");
		}
	}




	// ==================
	//    Private メソッド 
	// ==================

	/// <summary>
	/// SendRanking 処理終了後呼び出されるコールバック関数です
	/// </summary>
	/// <param name="message">Message.</param>
	void RankingCallback(string message) 
	{
		Debug.Log("== SendRankingCallback : " + message + " == ");
		/** 
		 成功時 : message = {"id" : "id_hogehoge", status_code : "0", "result" : {"success" : "1"}}
		 失敗時 : message = {"id" : "id_hogehoge", status_code : "0" 以外, error : "error message"}
		 または : message = {"id" : "id_hogehoge", "error" : "1"}
		/**/

	}
}
