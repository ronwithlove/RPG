using UnityEngine;
using System.Collections;

public class EquipmentItem : MonoBehaviour {

	private UISprite sprite;
	public int itemId=0;

	void Awake(){
		sprite=this.GetComponent<UISprite>();
	}

	public void SetId(int id){
		ItemInfo info=ItemsInfo._instance.GetItemInfoByID(id);//按照字典里对应的ID找到info
		sprite.spriteName=info.icon_name;//把sripte的名字改了，就等于换了他的图标样子了。
		itemId=id;
	}

}
