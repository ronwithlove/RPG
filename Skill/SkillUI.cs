﻿using UnityEngine;
using System.Collections;

public class SkillUI : MonoBehaviour {

	public static SkillUI _instance;

	public int[] swordmanSkillList;
	public int[] magicainSkillList;
	public GameObject skillItemPrefab;
	public UIGrid grid;
	private TweenPosition skillTween;
	private bool isSkillUIOpen=false;
	private PlayerStatus ps;
	private int[] skillList;

	void Awake(){
		_instance=this;
		skillTween=this.GetComponent<TweenPosition>();	
		//skillTween.gameObject.SetActive(false);//这里一上来不能设为fale了。还是让他ture把动画勾去了就是
		ps=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
	}

	void Start(){
		switch(ps.playerClass){//用switch方便扩展多职业
			case PlayerClass.Swordman:skillList=swordmanSkillList;break;
			case PlayerClass.Magican:skillList=magicainSkillList;break;
		}
		foreach(int id in skillList){
		//	print (grid.gameObject);
			GameObject skillItemGo=NGUITools.AddChild(grid.gameObject,skillItemPrefab);
			grid.AddChild(skillItemGo.transform);//这样grid可以管理他，但是我去掉这行也没什么问题
			skillItemGo.GetComponent<SkillItem>().SetID(id);
		}
	}

	void loadIsSkill(){
		SkillItem[] skillItem=transform.GetComponentsInChildren<SkillItem>(); //Components 有个s,这里的Children不是子了，是子子子。。。
		foreach(SkillItem item in skillItem){//遍历每个技能
			item.isSkill(ps.lvl);//把角色等级传入isSkill方法
		}
	}

	public void SkillUIShowHide(){
		if(isSkillUIOpen){
			isSkillUIOpen=false;
			skillTween.PlayReverse();//反向播放装备栏动画，等于是退出动画
		}else{//如果当前状态是不显示的
			isSkillUIOpen=true;//把状态设为显示
			skillTween.gameObject.SetActive(true);//其中装备栏
			skillTween.PlayForward();//播放装备栏进入动画
			loadIsSkill();
		}
	}

}
