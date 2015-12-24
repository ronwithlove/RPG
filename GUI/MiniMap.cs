using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	private Camera minimapCamera;

	void Awake(){
		minimapCamera=GameObject.FindGameObjectWithTag(Tags.minimapCamera).GetComponent<Camera>();
	}

	public void OnZooInClikc(){// Size控制在5-10比较好
		if(minimapCamera.orthographicSize>5){
			minimapCamera.orthographicSize--;
		}
	}

	public void OnZooOutClikc(){
		if(minimapCamera.orthographicSize<10){
			minimapCamera.orthographicSize++;
		}
	}

}
