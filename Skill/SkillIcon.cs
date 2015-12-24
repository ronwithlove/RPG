using UnityEngine;
using System.Collections;

public class SkillIcon : UIDragDropItem {

	private int skillID;

	protected override void OnDragDropStart(){//重写UIDragDropItem中的OnDragDropStart方法
		base.OnDragDropStart();	
		skillID=transform.GetComponentInParent<SkillItem>().skillID;//这个得写在下以上的上面，先获取父物件上的id再改变目录到root
		transform.parent=transform.root;
		this.GetComponent<UISprite>().depth=20;//改成了20，大于技能空格的depth
	}

	protected override void OnDragDropRelease(GameObject surface){
		base.OnDragDropRelease(surface);
		if(surface!=null && surface.tag==Tags.shortCut){//当技能放到快捷栏空格的时候
			surface.GetComponent<ShortCut>().SetSkill(skillID);//调用ShortCut类中的SetSkill方法去改变图标
		}
	}
}
