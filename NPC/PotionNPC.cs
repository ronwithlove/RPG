using UnityEngine;
using System.Collections;

public class PotionNPC : NPC {

	public TweenPosition potionShopTween;

	void OnMouseOver(){//当鼠标移动到这个collider上的时候，每一帧都会检测,不需要写在update里
		if(Input.GetMouseButtonDown(0)){
			this.GetComponent<AudioSource>().Play();
			PotionShop._instance.ShowPotionShop();
		}		
	}

}
