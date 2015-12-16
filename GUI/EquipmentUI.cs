using UnityEngine;
using System.Collections;

public class EquipmentUI : MonoBehaviour {
	public static EquipmentUI _instance;

	private TweenPosition equipmentTween;
	private bool isequipmentOpen=false;//目前状态
	private GameObject headGrear;
	private GameObject armor;
	private GameObject rightHand;
	private GameObject leftHand;
	private GameObject shoe;
	private GameObject accessory;


	void Awake(){		
		_instance=this;
		equipmentTween=this.GetComponent<TweenPosition>();	
		equipmentTween.gameObject.SetActive(false);//刚开始的时候让他不显示，节约消耗
		headGrear=this.transform.Find("Headgear").gameObject;
		armor=this.transform.Find ("Armor").gameObject;
		rightHand=this.transform.Find("RightHand").gameObject;
		leftHand=this.transform.Find ("LeftHand").gameObject;
		shoe=this.transform.Find ("Shoe").gameObject;
		accessory=this.transform.Find("Accessory").gameObject;

	}

	public void EquipmentShowHide(){
		if(isequipmentOpen){
			isequipmentOpen=false;
			equipmentTween.PlayReverse();//反向播放装备栏动画，等于是退出动画
		}else{//如果当前状态是不显示的
			isequipmentOpen=true;//把状态设为显示
			equipmentTween.gameObject.SetActive(true);//其中装备栏
			equipmentTween.PlayForward();//播放装备栏进入动画
		}
	}
}
