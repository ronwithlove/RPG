using UnityEngine;
using System.Collections;

public class PotionShop : MonoBehaviour {


	public UIInput inputField;

	private GameObject potionNumberGo;
	private int buyID=0;


	void Awake(){
		potionNumberGo=this.transform.FindChild("PotionNumber").gameObject;
		potionNumberGo.SetActive(false);
	}

	public void OnBtn1Click(){
		buyID=1001;//这里就是固定写死的小血瓶在字典里的id
		ShowPotionNumber();
	}
	public void OnBtn2Click(){
		buyID=1002;
		ShowPotionNumber();
	}
	public void OnBtn3Click(){
		buyID=1003;
		ShowPotionNumber();
	}

	void ShowPotionNumber(){
		potionNumberGo.SetActive(true);
		inputField.value="0";
	}

	public void OkBtnClick(){
		ItemInfo info=ItemsInfo._instance.GetItemInfoByID(buyID);
		int potionCount=int.Parse(inputField.value);
		int totalPrice=potionCount*info.price_buy;
		bool success=Inventory._instance.BuyStaff(totalPrice);//购买成功会返回ture
		if (success){
			Inventory._instance.PickItems(buyID,potionCount);	//往包里添加物品
		}else{
			//提示金币不够
			print ("dont have enough money.");
		}
	}
}
