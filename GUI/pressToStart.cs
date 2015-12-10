using UnityEngine;
using System.Collections;

public class pressToStart : MonoBehaviour {

	private bool isPressAnyKey=false;
	private GameObject btnContainer;

	void Start(){
		btnContainer=this.transform.parent.Find("btnContainer").gameObject;//用parent把节点设到他的父类，再找到btnContainer,由于用的是transform所以返回的也是transform
		//所以还需要用.gameObject 来获得他的gameObject
	}

	// Update is called once per frame
	void Update () {

		if (isPressAnyKey ==false&& Input.anyKey){
			ShowButton();
		}
	}

	void ShowButton(){
		isPressAnyKey=true;
		btnContainer.SetActive(true);
		this.gameObject.SetActive(false);//直接获得gameObject,不需要再写transform. this.transform.gameObject.SetActive(false); 

	}
}
