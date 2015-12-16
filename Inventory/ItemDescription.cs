using UnityEngine;
using System.Collections;

public class ItemDescription : MonoBehaviour {

	public static ItemDescription _instance;

	private UILabel desLabel;
	private ItemInfo info=null;

	void Awake(){
		_instance=this;
		desLabel=this.GetComponentInChildren<UILabel>();
		this.gameObject.SetActive(false);//Awake里把自己SetActive(false)和直接在inspector把勾去掉有点不一样
		//如果是在Awake中自己禁用了，是可以直接在GridItem中用ItemDescription._instance.ShowDes(itemId); 
		//但是如果在inspector把勾去掉，也就是这个脚本启动的时候就没加载过，就得要在GridItem中先去把他SetActive了。。
	}

	public void ShowDes(int itemId){
		this.gameObject.SetActive(true);
		transform.position=UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);//最好物品描述显示在这个物品格子的右下角再过去一点，这样不会被鼠标挡住
		info=ItemsInfo._instance.GetItemInfoByID(itemId);
		switch(info.type){//注意类型用的是枚举，所以注意case后面的类型，不是string哦
		case  ItemType.Drug: ShowDrugDes();break; 
		case ItemType.Equip: ShowEqupDes();break;
		case ItemType.Mat: ShowMatDes();break;
		}
	}

	public void HideDes(){
		this.gameObject.SetActive(false);
	}

	//不同类型返回不同的字段，比如装备就不会有回血什么
	void ShowDrugDes(){
		desLabel.text="物品："+info.name+"\n";
		desLabel.text+="回血："+info.hp+"\n";
		desLabel.text+="回魔："+info.mp+"\n";
		desLabel.text+="出售："+info.price_sell+"金币"+"\n";
		desLabel.text+="购买："+info.price_buy+"金币"+"\n";
	}
	void ShowEqupDes(){
		desLabel.text="物品："+info.name+"\n";

	}
	void ShowMatDes(){
		desLabel.text="物品："+info.name+"\n";

	}
}
