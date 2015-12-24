using UnityEngine;
using System.Collections;

public class WeaponItem : MonoBehaviour {

	private UISprite sprite;
	private UILabel	name;
	private UILabel effect;
	private UILabel priceBuy;
	private int weaponItemID;

	void Awake(){
		sprite=transform.Find("Sprite").GetComponent<UISprite>();
		name=transform.Find ("Name").GetComponent<UILabel>();
		effect=transform.Find ("Effect").GetComponent<UILabel>();
		priceBuy=transform.Find ("PricBuy").GetComponent<UILabel>();
	}

	void Start(){

	}

	public void SetId(int id){
		weaponItemID=id;
		ItemInfo info= ItemsInfo._instance.GetItemInfoByID(weaponItemID);
		sprite.spriteName=info.icon_name;
		name.text=info.name;
		if(info.strength>0){	//这样的话，就是说装备只可能加一种属性，多了就会被忽略
			effect.text="力量"+info.strength;
		}else if(info.defence>0){
			effect.text="防御"+info.defence;
		}else if(info.speed>0){
			effect.text="速度"+info.speed;
		}
		priceBuy.text=""+info.price_buy;
	}

	public void OnBuyClick(){
		WeaponShopUI._instance.OnBuyClick(weaponItemID);//把id传给weaponShopUI
	}

}
