﻿using UnityEngine;
using System.Collections;

//GridItem只用来控制物品拖放，和在格子里有别的物品时候的换位
//这里的id是又Inventory传过来的。
public class GridItem : UIDragDropItem {//继承拖放

	private UISprite sprite;
	private InventoryItemGrid grid1;
	private InventoryItemGrid grid2;
	private int itemId=0;
	private bool isOnHover=false;


	void Awake(){
		sprite=this.GetComponent<UISprite>();
	}

	void Update(){
		if(isOnHover){
			if(Input.GetMouseButtonDown(1)){//当鼠标移动物品上右键的时候
				bool success=EquipmentUI._instance.EquipItem(itemId);//装备物品
				if(success){//如果装备物品成功，
					transform.parent.GetComponent<InventoryItemGrid>().GridPlusItem(itemId,-1);//就在原来的格子中减去他
				}
			}//end if(Input.GetMouseButtonDown(1))
		}//end if(isOnHover)
	}


	protected override void OnDragDropRelease(GameObject surface){//surface就是被拖动物品碰撞上的那个物品
		base.OnDragDropRelease(surface);
		if (surface != null) {
			if(surface.tag==Tags.inventory_grid){//如果是空的格子
				ItemToGrid(this.transform, surface.transform);//把原来格子的信息放到新的格子去

				this.transform.parent=surface.transform;//物品放置
				this.transform.localPosition=Vector3.zero;
			}else if(surface.tag==Tags.inventory_item){//如果格子中已经有物品，物品位置交换,把Grid1中的A和Grid2中的B交换
				SwitchGrid(this.transform, surface.transform);//两个格子交换一下

				Transform parent =surface.transform.parent;//保存Grid2位置
				surface.transform.parent=this.transform.parent;//把B移到Grid1中
				surface.transform.localPosition=Vector3.zero;//把B放在格子Grid1的中间
				this.transform.parent=parent;//把A物品放到Grid2中
				this.transform.localPosition=Vector3.zero;//把A放在格子Grid2中间
			}else if(surface.tag==Tags.shortCut){
				//调用shorcut,方法，要传个ID过去，item是没count的,count是ItemGrid中的属性
				surface.GetComponent<ShortCut>().SetItem(itemId);
				this.transform.localPosition=Vector3.zero;//这样在快捷键上设成之后，还要把他放回原来包中的位置。
			}else{
				this.transform.localPosition=Vector3.zero;//拖到别的什么东西时候放掉之后返回原处。
			}

		}else{
			this.transform.localPosition=Vector3.zero;//什么都没碰到的时候放掉之后返回原处。
		}//end if (surface != null) else

	}

	public void SetId(int id){
		ItemInfo info=ItemsInfo._instance.GetItemInfoByID(id);//按照字典里对应的ID找到info
		sprite.spriteName=info.icon_name;//把sripte的名字改了，就等于换了他的图标样子了。
		itemId=id;
	}
	//下面两个方法应该可以合并，要不ItemToGrid也不用这么麻烦，直接初始一个格子就好。
	void SwitchGrid(Transform trans1, Transform trans2){
		grid1=trans1.parent.gameObject.GetComponent<InventoryItemGrid>();
		int grid1Id=grid1.itemsID;
		int grid1Count=grid1.itemsCount;

		grid2=trans2.parent.gameObject.GetComponent<InventoryItemGrid>();
		int grid2Id=grid2.itemsID;
		int grid2Count=grid2.itemsCount;
		
		grid1.SetGridID(grid2Id,grid2Count);
		grid2.SetGridID(grid1Id,grid1Count);
	}

	void ItemToGrid(Transform trans1, Transform trans2){
		grid1=trans1.parent.gameObject.GetComponent<InventoryItemGrid>();
		int grid1Id=grid1.itemsID;
		int grid1Count=grid1.itemsCount;
		
		grid2=trans2.gameObject.GetComponent<InventoryItemGrid>();//这里没有parent 唯一的区别
		int grid2Id=grid2.itemsID;
		int grid2Count=grid2.itemsCount;
		
		grid1.SetGridID(grid2Id,grid2Count);
		grid2.SetGridID(grid1Id,grid1Count);
	}

	public void OnHoverOver(){//鼠标移到物品上
		isOnHover=true;			
		ItemDescription._instance.ShowDes(itemId);//显示物品信息

	}
	public void OnHoverOut(){
		isOnHover=false;
		ItemDescription._instance.HideDes();

	}

}

//限定物品移动范围，超过包包就提示扔掉，在包包内，但是不在格子上的时候放掉鼠标返回原来的格子
//还需要增加，在拖动物品时候，把他的Layer放到最上面。
//物品分拆慢点写