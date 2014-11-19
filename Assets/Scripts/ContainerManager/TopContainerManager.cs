using UnityEngine;
using System.Collections;
using System;

public class TopContainerManager : ContainerManager<TopContainerManager> {

	public static event Action OnStartButtonClickedEvent;

	public void OnStartButtonClicked(){
		OnStartButtonClickedEvent ();
		DestoryContainer ();
	}
}
