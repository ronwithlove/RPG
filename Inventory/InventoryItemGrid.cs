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


	public void SetGridID(int id, int count=1){
		itemsID=id;
		itemsCount=count;
		itemCountLabel.text=""+count;
	}


}
