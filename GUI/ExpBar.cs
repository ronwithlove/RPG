using UnityEngine;
using System.Collections;

public class ExpBar : MonoBehaviour {

	public static ExpBar _instance;
	private UISlider expBar;

	void Awake(){
		_instance=this;
		expBar=this.GetComponent<UISlider>();
	}

	public void SetExpBar(float value){//这里value是0-100%的一个值
		expBar.value=value;
	}
}
