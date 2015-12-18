using UnityEngine;
using System.Collections;

public class EquipmentItem : MonoBehaviour {

	public int itemId=0;
	private UISprite sprite;
	private bool isOnHover=false;

	void Awake(){
		sprite=this.GetComponent<UISprite>();
	}

	void Update(){
		if(isOnHover){
			if(Input.GetMouseButtonDown(1)){
				EquipmentUI._instance.RemoveItem(itemId,this.gameObject);
			}
		}
	}

	public void SetId(int id){
		ItemInfo info=ItemsInfo._instance.GetItemInfoByID(id);//按照字典里对应的ID找到info
		sprite.spriteName=info.icon_name;//把sripte的名字改了，就等于换了他的图标样子了。
		itemId=id;//得到这个物品的Id
	}

	void OnHover (bool isOver) {//鼠标放在物体上的时候isOver返回ture,不在就返回false
		isOnHover= isOver;
	}


}
