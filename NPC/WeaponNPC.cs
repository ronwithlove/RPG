using UnityEngine;
using System.Collections;

public class WeaponNPC : NPC {

	void OnMouseOver(){//当鼠标移动到这个collider上的时候，每一帧都会检测,不需要写在update里
		if(Input.GetMouseButtonDown(0)){
			this.GetComponent<AudioSource>().Play();
			WeaponShopUI._instance.ShowWeaponShop();
		}		
	}
}
