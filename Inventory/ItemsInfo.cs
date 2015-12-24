using UnityEngine;
using System.Collections;
using System.Collections.Generic;//字典要用到

public enum ItemType{ //物品种类
	Drug,
	Equip,
	Mat    //材料
}
public enum EquipType{
	Headgear,
	Armor,
	RightHand,
	LeftHand,
	Shoe,
	Accessory
}

public enum ClassType{
	Swordman,//剑士
	Magician,//法师
	Common //通用
}

//这里都用了一个类，之后去完善，把不同种类的属性分开，比如装备，材料是不会回血回魔的
public class ItemInfo {//这个类不是 ItemsInfo，在 Item后少了个s哦。
	public int id;
	public string name;//技能名字
	public string icon_name;//存储在图集中的名称
	public ItemType type;//物品种类
	public int hp;			//回血值
	public int mp;		//回魔值
	public int price_sell;	//出售价
	public int price_buy;	//购买价
	public int strength;
	public int defence;
	public int speed;
	public EquipType equipType;
	public ClassType classType;
}

public class ItemsInfo : MonoBehaviour {

	public static ItemsInfo _instance;
	public TextAsset ItemsInfoText;//API可以去官网查下，主要就两个方法: byte, text

	private Dictionary<int,ItemInfo> ItemInfoDict = new Dictionary<int,ItemInfo>();//默认字典是空的，存放的是ItemInfo
	void Awake(){
		_instance=this;
		ReadItemsInfo();//通过读取本文内容，把他们加到字典中
//		print (ItemInfoDict.Keys.Count);//测试下看看字典中有几个元素
	}

	public ItemInfo GetItemInfoByID(int id){// 写一个方法，通过用字典查找id来返回一个对应的ItemInfo
		ItemInfo info=null;// 初始为null
		ItemInfoDict.TryGetValue(id, out info);//如果得到对应id的info就给到info咯
		return info;
	}

	void ReadItemsInfo(){	
		string text=ItemsInfoText.text;//记得把文本注册到public 的那个TextAsset
		string[] lineArray=text.Split('\n');//这里按回车来分,注意是'单引号',把文本中的每一行放到数组lineArray中
		//print (strArray[1]);

		foreach(string str in lineArray){//这里用foreach历遍每个strArray,
			ItemInfo info= new ItemInfo();//新建一个ItemInfo的类，然后把所有得到的信息放进去。

			string[] partArray=str.Split(',');//再把每一行的字符串按中间的逗号分成一小部分，这样就得到每个物品的属性了。
			info.id=int.Parse(partArray[0]);//把每个属性分别赋值给不同的变量， int.Parse 强转
			info.name=partArray[1];
			info.icon_name=partArray[2];

			//因为文本中是直接记录Drug, Equip,Mat,而在ItemInfo是用了枚举，所以不能直接赋值，所以要swtich一下
			string str_type=partArray[3];
			switch(str_type){
			case "Drug": info.type=ItemType.Drug;break;//当得到的 str_type=Drug时,  info.type=ItemType.Drug。就不会有错了
			case "Equip": info.type=ItemType.Equip;break;
			case "Mat": info.type=ItemType.Mat;break; 
			}
			//如果类型是Drug,继续获得接下来的属性
			if(info.type==ItemType.Drug){
				info.hp=int.Parse (partArray[4]);
				info.mp= int.Parse(partArray[5]);
				info.price_sell=int.Parse(partArray[6]);
				info.price_buy=int.Parse(partArray[7]);
			}else if(info.type==ItemType.Equip){
				info.strength=int.Parse(partArray[4]);
				info.defence=int.Parse (partArray[5]);
				info.speed=int.Parse (partArray[6]);
				info.price_sell=int.Parse(partArray[9]);
				info.price_buy=int.Parse (partArray[10]);
				string str_equipType=partArray[7];
				switch(str_equipType){
					case "Headgear":info.equipType=EquipType.Headgear;break;
					case "Armor":info.equipType=EquipType.Armor;break;
					case "RightHand":info.equipType=EquipType.RightHand;break;
					case "LeftHand":info.equipType=EquipType.LeftHand;break;
					case "Shoe":info.equipType=EquipType.Shoe;break;
					case "Accessory":info.equipType=EquipType.Accessory;break;
				}
				string str_classType=partArray[8];
				switch(str_classType){
				case "Swordman":info.classType=ClassType.Swordman;break;
				case "Magician":info.classType=ClassType.Magician;break;
				case"Common":info.classType=ClassType.Common;break;
				}
			}
			ItemInfoDict.Add (info.id,info);//最后把这个info加入字典，这里字典的id就用文本里的ID就可以了
		}//foreach结束
	}//ReadItemsInfo() 结束
}
