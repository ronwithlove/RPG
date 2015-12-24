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
	private UISprite icon;
	private SkillInfo info;
	private ShortCutyType type=ShortCutyType.None;

	void Awake(){
		icon=transform.Find("icon").GetComponent<UISprite>();
		icon.gameObject.SetActive(false);//一上来不用显示，因为是空的
	}

	public void SetSkill(int id){
		skillID=id;
		icon.gameObject.SetActive(true);
		info=SkillsInfo._instance.GetSkillInfoByID(skillID);//得到技能的信息
		icon.spriteName=info.icon_name;//更开技能图标
		type=ShortCutyType.Skill;
	}

}
