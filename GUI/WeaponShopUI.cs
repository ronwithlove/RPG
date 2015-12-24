using UnityEngine;
using System.Collections;

public class WeaponShopUI : MonoBehaviour {

	public static WeaponShopUI _instance;

	public int[] weaponIDArray;//这里用个数组没用循环，这个可以调节武器装备顺序
	public GameObject weaponItem;//拖一个prefab上去
	public UIGrid grid;
	private TweenPosition weaponShopTween;
	private GameObject buyNumber;
	private UIInput input;
	private int buyId=0;

	void Awake(){
		_instance=this;
		weaponShopTween=this.GetComponent<TweenPosition>();
		buyNumber=transform.Find ("BuyNumber").gameObject;
		input=transform.Find ("BuyNumber/InputField").GetComponent<UIInput>();
		buyNumber.SetActive(false);
	}

	void Start(){
		foreach(int id in weaponIDArray){
			GameObject weaponItemGo=NGUITools.AddChild(grid.gameObject,weaponItem);
			grid.AddChild(weaponItemGo.transform);
			weaponItemGo.GetComponent<WeaponItem>().SetId(id);
		}
	}


	public void ShowWeaponShop(){
		weaponShopTween.gameObject.SetActive(true);
		weaponShopTween.PlayForward();//对话框移入动画
	}
	
	public void OnCloseBtn(){//按下关闭按键执行
		weaponShopTween.PlayReverse();//对话框移出动画
	}


	public void OnOkClick(){
		int count=int.Parse(input.value);
	
		int price=ItemsInfo._instance.GetItemInfoByID(buyId).price_buy;
		int totalPrice=count*price;
		if(count>0){
			bool success=Inventory._instance.BuyStaff(totalPrice);
			if (success){
				Inventory._instance.PickItems(buyId,count);
			}
		}
		buyNumber.SetActive(false);
		buyId=0;
		//input.value="0";不需要，因为每次按Buy的时候都会设成1
	}

	public void OnBuyClick(int id){
		buyId=id;
		buyNumber.SetActive(true);
		input.value="1";//默认点开始购买一件
	}

	public void OnClick(){//点击Shop空白的地方隐藏 number
		buyNumber.SetActive(false);
	}

}
