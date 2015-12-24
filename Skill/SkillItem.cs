using UnityEngine;
using System.Collections;

public class SkillItem : MonoBehaviour {

	public int skillID;
	private SkillInfo info;
	private UISprite iconName;
	private UILabel skillName;
	private UILabel skillType;
	private UILabel skillDes;
	private UILabel mpCost;
	private GameObject skillMask;
	 
	void InitSkillDes(){
		iconName=transform.Find("Skill_icon").GetComponent<UISprite>();
		skillName=transform.Find ("SkillDescription/name_bg/nameLabel").GetComponent<UILabel>();
		skillType=transform.Find ("SkillDescription/skillType_bg/skillTypeLabel").GetComponent<UILabel>();
		skillDes=transform.Find ("SkillDescription/skillDes_bg/skillDesLabel").GetComponent<UILabel>();
		mpCost=transform.Find ("SkillDescription/mpCost_bg/mpCostLabel").GetComponent<UILabel>();
		skillMask= transform.Find ("Skill_mask").gameObject;
	}


	public void SetID(int id){
		InitSkillDes();
		skillID=id;
		info =SkillsInfo._instance.GetSkillInfoByID(id);
		iconName.spriteName=info.icon_name;//注意这里改图片就是改sprite名字
		skillName.text=info.name;
		switch(info.skillType){
			case SkillType.Buff:skillType.text="增强";break;
			case SkillType.Passive:skillType.text="被动";break;
			case SkillType.SingleTarget:skillType.text="单体技能";break;
			case SkillType.MultiTarget:skillType.text="群体技能";break;
			}
		skillDes.text=info.des;
		mpCost.text=info.mpCost+"MP";
	}
	
	public void isSkill(int lvl){//判断是否要遮盖掉技能图标
		if(lvl>=info.level){
			skillMask.SetActive(false);
			this.transform.GetComponentInChildren<SkillIcon>().enabled=true;
		}else{
			skillMask.SetActive(true);//遮盖
			this.transform.GetComponentInChildren<SkillIcon>().enabled=false;//拖放也不能用咯
		}
	}

}
