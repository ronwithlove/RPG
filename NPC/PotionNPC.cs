using UnityEngine;
using System.Collections;

public class PotionNPC : NPC {


	public TweenPosition potionShopTween;




	void OnMouseOver(){//当鼠标移动到这个collider上的时候，每一帧都会检测,不需要写在update里
		if(Input.GetMouseButtonDown(0)){
			this.GetComponent<AudioSource>().Play();
			ShowPotionShop();
		}		
	}


	void ShowPotionShop(){
		potionShopTween.gameObject.SetActive(true);
		potionShopTween.PlayForward();//对话框移入动画
	}

	public void OnCloseBtn(){//按下关闭按键执行
		potionShopTween.PlayReverse();//对话框移出动画
	}

}
