using UnityEngine;
using System.Collections;

public class Test : MonoSingleton<Test> {

	public void OnButtonClicked(){
		Destroy (gameObject);
	}

}
