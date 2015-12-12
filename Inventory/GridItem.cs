using UnityEngine;
using System.Collections;

//GridItem只用来控制物品拖放，和在格子里有别的物品时候的换位
//这里的id是又Inventory传过来的。
public class GridItem : UIDragDropItem {//继承拖放

	private UISprite sprite;
	private InventoryItemGrid grid;


	void Awake(){
		sprite=this.GetComponent<UISprite>();
	}
	protected override void OnDragDropRelease(GameObject surface){//surface就是被拖动物品碰撞上的那个物品
		base.OnDragDropRelease(surface);

		//物体会自动放到格子中间
		if(surface.tag==Tags.inventory_grid){
			this.transform.parent=surface.transform;
			this.transform.localPosition=Vector3.zero;
			//grid=this.transform.parent.gameObject;

		}else if(surface.tag==Tags.inventory_item){//如果格子中已经有物品，交换物品
			Transform parent =surface.transform.parent;//保存已经物品的parent
			surface.transform.parent=this.transform.parent;//把已有物品移动到我拖动的物品父类下
			surface.transform.localPosition=Vector3.zero;//放放好。。放格子中间
			
			this.transform.parent=parent;//我拖动的物品放到已有物品的位置。
			this.transform.localPosition=Vector3.zero;//放放好。。
		}

	}

	public void SetId(int id){
		ItemInfo info=ItemsInfo._instance.GetItemInfoByID(id);//按照字典里对应的ID找到info
		sprite.spriteName=info.icon_name;//把sripte的名字改了，就等于换了他的图标样子了。
	}



}

//还需要增加，在拖动物品时候，把他的Layer放到最上面。
//物品分拆慢点写