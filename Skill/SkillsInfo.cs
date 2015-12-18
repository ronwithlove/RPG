using UnityEngine;
using System.Collections;
using System.Collections.Generic;//字典要用到


public class SkillsInfo : MonoBehaviour {

	public static SkillsInfo _instance;
	public TextAsset skillInfoText;

	private Dictionary<int,SkillInfo> SkillInfoDict= new Dictionary<int, SkillInfo>();

	void Awake(){
		_instance=this;
		InitSkillInfoDict();//把信息从文本读到字典
		//print (skillInfoDict.Keys.Count);
	}

	public SkillInfo GetSkillInfoByID(int id){// 写一个方法，通过用字典查找id来返回一个对应的skillInfo
		SkillInfo skillInfo=null;// 初始为null
		SkillInfoDict.TryGetValue(id, out skillInfo);//如果得到对应id的info就给到info咯
		return skillInfo;
	}

	void InitSkillInfoDict(){
		string text=skillInfoText.text;//记得把文本注册到public 的那个TextAsset
		string[] lineArray=text.Split('\n');//这里按回车来分,注意是'单引号',把文本中的每一行放到数组lineArray中

		foreach(string str in lineArray){
			SkillInfo skillInfo= new SkillInfo();

			string[] partArray=str.Split(',');//这里再用一个array,用逗号分开放入
			skillInfo.id=int.Parse(partArray[0]);
			skillInfo.name=partArray[1];
			skillInfo.icon_name=partArray[2];
			skillInfo.des=partArray[3];

			string str_skillType=partArray[4];
			switch(str_skillType){
				case "Passive":skillInfo.skillType=SkillType.Passive;break;
				case "Buff":skillInfo.skillType=SkillType.Buff;break;
				case"SingleTarget":skillInfo.skillType=SkillType.SingleTarget;break;
				case"MultiTarget":skillInfo.skillType=SkillType.MultiTarget;break;
			}
			string str_buffType=partArray[5];
			switch(str_buffType){
				case "Attack":skillInfo.buffType=BuffType.Attack;break;
				case"Def":skillInfo.buffType=BuffType.Def;break;
				case"Speed":skillInfo.buffType=BuffType.Speed;break;
				case"AttackSpeed":skillInfo.buffType=BuffType.AttackSpeed;break;
				case"HP":skillInfo.buffType=BuffType.HP;break;
				case"MP":skillInfo.buffType=BuffType.MP;break;
			}
			skillInfo.applyValue=int.Parse (partArray[6]);
			skillInfo.durationTime=int.Parse(partArray[7]);
			skillInfo.mpCost=int.Parse (partArray[8]);
			skillInfo.coolDown=int.Parse (partArray[9]);
			string str_classOfSkill=partArray[10];
			switch(str_classOfSkill){
				case "Swordman":skillInfo.classOfSkill=ClassOfSKill.Swordman;break;
				case "Magician":skillInfo.classOfSkill=ClassOfSKill.Magician;break;
			}
			skillInfo.level=int.Parse(partArray[11]);
			string str_castTarget=partArray[12];
			switch(str_castTarget){
				case "Self":skillInfo.castTarget=CastTarget.Self;break;
				case "Enemy":skillInfo.castTarget=CastTarget.Enemy;break;
				case "Position":skillInfo.castTarget=CastTarget.Position;break;
			}
			skillInfo.distance=float.Parse(partArray[13]);
			SkillInfoDict.Add (skillInfo.id,skillInfo);
		}//end foreach
	}//end void InitSkillInfoDict()
}

public enum ClassOfSKill{//适用类型
	Swordman,
	Magician
}

public enum SkillType{//作用类型
	Passive,
	Buff,
	SingleTarget,
	MultiTarget
}

public enum BuffType{//作用属性
	Attack,
	Def,
	Speed,
	AttackSpeed,
	HP,
	MP
}

public enum CastTarget{//释放类型
	Self,
	Enemy,
	Position
}

public class SkillInfo{//技能信息
	public int id;
	public string name;
	public string icon_name;
	public string des;
	public SkillType skillType;
	public BuffType buffType;
	public int applyValue;
	public int durationTime;
	public int mpCost;
	public int coolDown;
	public ClassOfSKill classOfSkill;
	public int level;
	public CastTarget castTarget;
	public float distance;
}