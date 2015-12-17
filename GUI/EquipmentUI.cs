﻿using UnityEngine;
using System.Collections;

public class EquipmentUI : MonoBehaviour {
	public static EquipmentUI _instance;

	public GameObject equipItem;//手动把prefab拖过去

	private TweenPosition equipmentTween;
	private bool isequipmentOpen=false;//目前状态
	private GameObject headGrearGo;
	private GameObject armorGo;
	private GameObject rightHandGo;
	private GameObject leftHandGo;
	private GameObject shoeGo;
	private GameObject accessoryGo;
	private PlayerStatus playerStatus;


	void Awake(){		
		_instance=this;
		equipmentTween=this.GetComponent<TweenPosition>();	
		equipmentTween.gameObject.SetActive(false);//刚开始的时候让他不显示，节约消耗
		headGrearGo=this.transform.Find("Headgear").gameObject;
		armorGo=this.transform.Find ("Armor").gameObject;
		rightHandGo=this.transform.Find("RightHand").gameObject;
		leftHandGo=this.transform.Find ("LeftHand").gameObject;
		shoeGo=this.transform.Find ("Shoe").gameObject;
		accessoryGo=this.transform.Find("Accessory").gameObject;
		playerStatus=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
	}

	//装备物品
	public bool EquipItem(int itemID){
		ItemInfo info=ItemsInfo._instance.GetItemInfoByID(itemID);
		if(info.type!=ItemType.Equip){
			print ("it is not a equipment");
			return false;
		}
		if(playerStatus.playerClass==PlayerClass.Magican && info.classType==ClassType.Swordman){//如果玩家的职业是法师，而物品适合职业是剑士（这里应该用如果职业是法师，而物品不是法师也不是通用比较好，这样职业多了也没问题。）
			print ("cannot equip on Mag");
			return false;
		}
		if(playerStatus.playerClass==PlayerClass.Swordman && info.classType==ClassType.Magician){//如果玩家的职业是法师，而物品适合职业是剑士（这里应该用如果职业是法师，而物品不是法师也不是通用比较好，这样职业多了也没问题。）
			print ("cannot equip on Swordman");
			return false;
		}
		//上面三种情况是物品不能穿的情况，接下来写可以穿的时候
		//穿装备也要判断，1，已经有装备了，那就换一下，2，没有装备，去实例化一个。
		GameObject parent=null;
		switch(info.equipType){
			case EquipType.Headgear:parent=headGrearGo;break;
			case EquipType.Armor:parent=armorGo;break;
			case EquipType.RightHand:parent=rightHandGo;break;
			case EquipType.LeftHand:parent=leftHandGo;break;
			case EquipType.Shoe:parent=shoeGo;break;
			case EquipType.Accessory:parent=accessoryGo;break;
		}//得到装备类型的，这个装备是属于哪个部位的。
		EquipmentItem item=parent.GetComponentInChildren<EquipmentItem>();//取这个部位下的子物件，赋值给item,如果是空的，就说明这个部位的装备还没东西（用.transform.childCount也行）
		if(item==null){//说明这个部位上还没装备物品
			GameObject newItemGo= NGUITools.AddChild(parent,equipItem); //创建物品，就是添加一个子物件，那在这里相当于把物品放到格子里，第一个参数是父，第二个是子
			newItemGo.transform.localPosition=Vector3.zero;
			newItemGo.transform.GetComponent<EquipmentItem>().SetId(info.id);//改图标
		}else{//已经有物品了，更换物品
			int equipeditemID=item.GetComponent<EquipmentItem>().itemId;
			Inventory._instance.PickItems(equipeditemID,1);//在包里添加同样id的物件
			item.GetComponent<EquipmentItem>().SetId(info.id);//把原来的物件改成装备上去的
			//再从包里减去装备上的物件就OK了。这个放在GirdItem去做
		}

		return true;
	}













	public void EquipmentShowHide(){
		if(isequipmentOpen){
			isequipmentOpen=false;
			equipmentTween.PlayReverse();//反向播放装备栏动画，等于是退出动画
		}else{//如果当前状态是不显示的
			isequipmentOpen=true;//把状态设为显示
			equipmentTween.gameObject.SetActive(true);//其中装备栏
			equipmentTween.PlayForward();//播放装备栏进入动画
		}
	}
}
