using UnityEngine;
using System.Collections;
using MiniJSON;

public class Test : MonoSingleton<Test> {

	void Start(){
		string url = "https://dl.dropboxusercontent.com/u/66223745/App/Sparutan/environment.json";
		WWWClient wwwClient = new WWWClient (this,url);
		wwwClient.OnSuccess = (WWW response) => {
			Debug.Log("success");
			string json = response.text;
			Debug.Log ("json = " + json);
			//	IList jsonArray = (IList)Json.Deserialize(json);
			IDictionary parentObject = (IDictionary)Json.Deserialize(json);
			IDictionary childObject = (IDictionary)parentObject["environments"];
			Debug.Log("" + childObject["1"]);
		};
		wwwClient.OnFail = (WWW response) => {
			Debug.Log("fail");
		};
		wwwClient.OnTimeOut = () => {
			Debug.Log("time out");
		};
		wwwClient.Request ();
	}

}
