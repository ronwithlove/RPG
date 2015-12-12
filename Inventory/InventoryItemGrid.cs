using UnityEngine;
using System.Collections;

//这个包里的格子，用来记录存放物品的ID和数量
public class InventoryItemGrid : MonoBehaviour {

	public int itemsID=0; //存放物品的id
	public int itemsCount=0;//存放物品的个数

	private UILabel itemCountLabel;

	void Awake(){
		itemCountLabel=this.GetComponentInChildren<UILabel>();

	}


	public void GridPlusItem(int id, int count=1){//默认数量1
		itemsID=id;
		itemsCount=itemsCount+count;
		itemCountLabel.text=""+itemsCount;
		}

	public void SetGridID(int id, int count=0){//默认数量0
		itemsID=id;
		itemsCount=count;
		if(itemsCount==0){
			itemCountLabel.text="";
		}else{
			itemCountLabel.text=""+count;
		}
	}

	}
