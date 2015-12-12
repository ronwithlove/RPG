using UnityEngine;
using System.Collections;

public class FunctionBar : MonoBehaviour {

	public TweenPosition inventoryTween;
	private bool isBagOpen=false;


	public void OnSettingBtnClick(){

	}
	public void OnStatusBtnClick(){
		
	}
	public void OnSkillBtnClick(){
		
	}
	public void OnEquipBtnClick(){
		
	}
	public void OnBagBtnClick(){
		if(isBagOpen){
			isBagOpen=false;
			Inventory._instance.HideInventory();
		}else{
			isBagOpen=true;
			inventoryTween.gameObject.SetActive(true);
		Inventory._instance.ShowInventory();
		}
	}



}
