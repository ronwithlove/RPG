using UnityEngine;
using System.Collections;

public class FunctionBar : MonoBehaviour {

	private bool isBagOpen=false;


	public void OnSettingBtnClick(){

	}
	public void OnStatusBtnClick(){
		Status._instance.StatusShowHide();
	}
	public void OnSkillBtnClick(){
		
	}
	public void OnEquipBtnClick(){
		
	}
	public void OnBagBtnClick(){
		Inventory._instance.BagShowHide();
	}



}
