using UnityEngine;
using System.Collections;

public abstract class ContainerManager<T> : MonoSingleton<T> where T : ContainerManager<T> {

	public void DestoryContainer(){
		Destroy (transform.parent.gameObject);
	}
}
