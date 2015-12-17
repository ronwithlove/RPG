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
		case  ItemType.Drug: desLabel.text=ShowDrugDes(info);break; 
		case ItemType.Equip: desLabel.text=ShowEqupDes(info);break;
		case ItemType.Mat: ShowMatDes();break;
		}
	}

	public void HideDes(){
		this.gameObject.SetActive(false);
	}

	//不同类型返回不同的字段，比如装备就不会有回血什么
	string ShowDrugDes(ItemInfo info){
		string str="";
		str+="物品："+info.name+"\n";
		str+="回血："+info.hp+"\n";
		str+="回魔："+info.mp+"\n";
		str+="出售："+info.price_sell+"金币"+"\n";
		str+="购买："+info.price_buy+"金币"+"\n";
		return str;
	}

	string ShowEqupDes(ItemInfo info){
		string str="";
		str+="物品："+info.name+"\n";
		switch(info.equipType){
		case EquipType.Headgear:str+="装备部位：头盔\n";break; //不要忘记break;
		case EquipType.Armor:str+="装备部位：盔甲\n";break;
		case EquipType.RightHand:str+="装备部位：右手\n";break;
		case EquipType.LeftHand:str+="装备部位：左手\n";break;
		case EquipType.Shoe:str+="装备部位：鞋子\n";break;
		case EquipType.Accessory:str+="装备部位：饰品\n";break;
		}
		switch(info.classType){
		case ClassType.Swordman:str+="适用职业：剑士\n";break;
		case ClassType.Magician:str+="适用职业：法师\n";break;
		case ClassType.Common:str+="适用职业：通用\n";break;
		}
		str+="力量："+info.strength+"\n";
		str+="防御："+info.defence+"\n";
		str+="速度："+info.speed+"\n";
		str+="出售："+info.price_sell+"金币"+"\n";
		str+="购买："+info.price_buy+"金币"+"\n";
		return str;
	}

	void ShowMatDes(){
		desLabel.text="物品："+info.name+"\n";

	}
}
