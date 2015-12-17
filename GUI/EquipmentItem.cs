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
				Inventory._instance.PickItems(itemId);//包里多设成一个他这样的装备 ，注意这里没有判断如果包满的情况
				GameObject.Destroy(this.gameObject);//把自己灭了
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
