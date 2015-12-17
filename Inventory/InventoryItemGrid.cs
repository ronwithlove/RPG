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

	public void GridPlusItem(int id, int count=1){//默认数量1 把Grid加上某个物品和他的数量
		if(itemsCount+count>=0){//排除如果是减成负数的情况。
			itemsID=id;
			itemsCount+=count;
			itemCountLabel.text=""+itemsCount;
			//下面是用来判断加-1的情况，也就是减物品
			if(itemsCount==0){//格子里物品为0了，就初始化这个物品格子
					SetGridID(0,0);//初始化要把itemsID 和itemsCount都设置为0；
				GameObject.Destroy(this.transform.GetComponentInChildren<GridItem>().gameObject);//把自己下面的物件删除。
			}
		}
	}

	public void SetGridID(int id, int count=0){//默认数量0  直接把Grid设置成某个物品，和他的数量,当如数为0,0就是初始化了
		itemsID=id;
		itemsCount=count;
		if(itemsCount==0){
			itemCountLabel.text="";
		}else{
			itemCountLabel.text=""+count;
		}
	}


}
