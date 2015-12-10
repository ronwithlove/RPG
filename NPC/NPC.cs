using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	void OnMouseEnter(){//这里是不是over因为over是一直持续的状态，换鼠标样式只要一次性的，所以enter就好了
		if( UICamera.hoveredObject==null){
			CursorManager._instance.SetCursorNpcTalk();
		}
	}

	void OnMouseExit(){//鼠标移出 恢复默认鼠标
		CursorManager._instance.SetCursorNormal();
	}

}
