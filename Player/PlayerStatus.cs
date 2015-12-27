using UnityEngine;
using System.Collections;

public enum PlayerClass{
	Swordman,
	Magican
}
public class PlayerStatus : MonoBehaviour {

	public int lvl=1;// level=100+level*30;
	public string playerName="施大宝";
	public int maxHP=100;
	public int maxMP=100;
	public float hp=100;//游戏时候的即时HP
	public float mp=100;
	public float experience=0;//当前XP

	public int strength=20;//人物初始属性点
	public int defence=20;
	public int speed=20;
	public int str_puls=0;//升级之后加的属性点
	public int def_plus=0;
	public int speed_plus=0;
	public int remainPoints=0;


	public PlayerClass playerClass=PlayerClass.Magican;


	void Start(){
		GetExp(0);//启动游戏的时候加载一下目前的经验
	}

	public void StrengthPlusClick(){//获得技能点数
		if (remainPoints>0){
			str_puls++;
			remainPoints--;
		}
	}
	public void DefencePlusClick(){
		if (remainPoints>0){
			def_plus++;
			remainPoints--;
		}
	}
	public void SpeedPlusClick(){
		if (remainPoints>0){
			speed_plus++;
			remainPoints--;
		}
	}

	public void useDrug(int drugHP,int drugMP){//获得药品治疗效果
		hp=Mathf.Min(hp+drugHP,maxHP);
		mp=Mathf.Min(mp+drugMP,maxMP);
	}

	public void GetExp(float exp){ //得到经验
		experience+=exp;
		float totalExp=100+lvl*30;//本级的最大经验
		while(experience>=totalExp){//经验升级
			lvl++;
			experience-=totalExp;
			totalExp=100+lvl*30;
		}
		ExpBar._instance.SetExpBar(experience/totalExp);//更新经验条
	}

}
