using UnityEngine;
using System.Collections;

public enum ShortCutyType{
	Skill,
	Drug,
	None
}

public class ShortCut : MonoBehaviour {

	public KeyCode keyCode;

	private int skillID;
	private int itemID;
	private UISprite icon;
	private SkillInfo skillInfo;
	private ItemInfo itemInfo;
	private ShortCutyType type=ShortCutyType.None;
	private PlayerStatus ps;
	private InventoryItemGrid invGridItem;
	private UILabel itemCountLabel;

	void Awake(){
		icon=transform.Find("icon").GetComponent<UISprite>();
		icon.gameObject.SetActive(false);//一上来不用显示，因为是空的
		ps=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
		itemCountLabel=transform.Find ("itemCount").GetComponent<UILabel>();
	}

	void Update(){
		if(Input.GetKeyDown (keyCode)){
			if(type==ShortCutyType.Drug){
				//setitem()处已经设置了type,如果是药剂，用药
				//用药表现在，先去包里检查药有没有
				bool success=Inventory._instance.useDrugItem(itemID);//如果有，减去成功
				print (success);
				if (success){//恢复HPMP
					ps.useDrug(itemInfo.hp,itemInfo.mp);
					itemCountLabel.text=""+invGridItem.itemsCount;
					if(invGridItem.itemsCount==0){
						itemCountLabel.text="";
					}
				}
				if(invGridItem.itemsCount<=0){//这里有个问题，就是当剩下最后一个时候返回时成功的，但是实际上已经没了，而快捷键上还会有1个，
					//因为他是在返回false之后才会清空快捷键上的图标,解决方法，快捷键绑定物品时候，同时也从GRID得到数量，当GRID数量为0时，清空快捷键
					print ("done");
					type=ShortCutyType.None;
					icon.gameObject.SetActive(false);
					itemID=0;
				}
			}
		}//end if(Input.GetKeyDown (keyCode))

	}



	public void SetSkill(int id){
		skillID=id;
		icon.gameObject.SetActive(true);
		skillInfo=SkillsInfo._instance.GetSkillInfoByID(skillID);//得到技能的信息
		icon.spriteName=skillInfo.icon_name;//更开技能图标
		type=ShortCutyType.Skill;
	}

	public void SetItem(int id){
		itemID=id;//其实可以和skill用一个Id.
		itemInfo=ItemsInfo._instance.GetItemInfoByID(itemID);
		if(itemInfo.type==ItemType.Drug){//只有药物的时候才可以装备
			icon.gameObject.SetActive(true);
			icon.spriteName=itemInfo.icon_name;
			type=ShortCutyType.Drug;
			//获得物品个数
			int index=Inventory._instance.getGridindex(id);
			invGridItem=Inventory._instance.itemGridList[index];
			itemCountLabel.text=""+invGridItem.itemsCount;
		}

	}

}
