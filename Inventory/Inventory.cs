﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//捡起功能
public class Inventory : MonoBehaviour {

	public static Inventory _instance;

	public List<InventoryItemGrid> itemGridList= new List<InventoryItemGrid>();//手动把20个格子都注册上
	public UILabel coinNumber;
	public GameObject gridItem;//这里先要指定一个物品的prefab

	private TweenPosition inventoryTween;//可以不声明直接在show,hide方法中用this.getcompent，但是还是在awake()先获得比较好，用来打开和关掉包包
	void Awake(){
		_instance=this;
		inventoryTween=this.GetComponent<TweenPosition>();	
	}

	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.X)){
			int reNo=Random.Range (1001,1004);
			PickItems(reNo);//得到一个id,就等于捡起了一个物品。
			print (reNo);
			//现在默认先捡起物品数量是1
		}
	
	}


	void PickItems(int id){
		//1.查找捡起物品在包里是不是已经有了
		//Yes,如果有了，叠加
		//No,如果没有，找空格子放，如果没有空格子，就不让放。
		//InventoryItemGrid grid= null;
		int gridIndex=0;
		int firstBlankGridIdex=1000;//初始化用1000，包包共有20个格子，index在0-19
		foreach(InventoryItemGrid temp in itemGridList){//遍历所有的格子,这里的itemGridList已经含有手动绑定上的20个格子了。
			//在遍历所有格子的时候，顺便检查有没有空的格子
			if(firstBlankGridIdex==1000 && temp.itemsID==0){//当firstBlankGridIdex还没被赋值的时候，这样他就被赋值一次，也就是记录第一个空格子的index
				firstBlankGridIdex=itemGridList.IndexOf(temp);
			}
			if(temp.itemsID==id){ //和格子的ID去比，格子的ID就是1001，1002也是在他里面物体的ID，如果ID一样
				//grid= temp;break;//获得这个格子
				gridIndex=itemGridList.IndexOf(temp);break;//获得相同包包的Index
				//最好在这里里边的时候看，要么拿到一样的BREAK，要么获得第一个空的GRID，他是第几个itemgridlist[?],下面就可以直接放了
			}
		}//foreach结束

		if(gridIndex==0){//如果遍历了格子index还是0，说明没有找到相同的，没有找到相同的就放到第一个空的格子里，如果有空的格子的话。
			if(firstBlankGridIdex!=1000){//不等于1000，说明有空的格子了，就放到这个格子下，
				GameObject itemGo= NGUITools.AddChild(itemGridList[firstBlankGridIdex].gameObject,gridItem); //就去创建这个捡起的物品	// 注意这个格式，这个意思就是添加一个子物件，那在这里相当于把物品放到格子里，第一个参数是父，第二个是子
				itemGo.transform.localPosition=Vector3.zero;
				itemGridList[firstBlankGridIdex].SetGridID(id);//把捡到物品的ID赋给空的格子（InventoryItemGrid）,数量先不写，为空就是默认1
				//把这个物品ID通过GridItem里的SETID方法给到GRID
				itemGo.transform.GetComponent<GridItem>().SetId(id);//通过用GetCompont方法获取itemGo的物件GridItem,然后再用GridItem中的SetId方法，来改变Sprite名字，从而达到改变图标。

			}else{
				//所有格子都有物品了，提示包包满了
			}

		}else{// gridInde不为0，有找到相同的物品，叠加

		}//if(gridIndex==0)结束

	}




	public void ShowInventory(){
		inventoryTween.PlayForward();
	}

	public void HideInventory(){
		inventoryTween.PlayReverse();
	}

}
