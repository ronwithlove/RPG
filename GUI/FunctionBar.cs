using UnityEngine;
using System.Collections;

public class FunctionBar : MonoBehaviour {

	public void OnSettingBtnClick(){

	}
	public void OnStatusBtnClick(){
		Status._instance.StatusShowHide();
	}
	public void OnSkillBtnClick(){
		
	}
	public void OnEquipBtnClick(){
		EquipmentUI._instance.EquipmentShowHide();
	}
	public void OnBagBtnClick(){
		Inventory._instance.BagShowHide();
	}



}
