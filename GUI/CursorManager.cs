using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour {

	public static CursorManager _instance;//单例模式

	public Texture2D cursor_normal;
	public Texture2D cursor_npc_talk;
	public Texture2D cursor_Attack;
	public Texture2D cursor_LockTarget;
	public Texture2D cursor_Pick;

	private Vector2 hotspot=Vector2.zero;//图标左上角的点为点击的点
	private CursorMode mode=CursorMode.Auto;

	void Awake(){
		_instance=this;
	}

	public void SetCursorNormal(){
		Cursor.SetCursor(cursor_normal,hotspot,mode);
	}
	public void SetCursorNpcTalk(){
		Cursor.SetCursor(cursor_npc_talk,hotspot,mode);
	}
	public void SetCursorAttack(){
		Cursor.SetCursor(cursor_Attack,hotspot,mode);
	}
	public void SetCursorLockTarget(){
		Cursor.SetCursor(cursor_LockTarget,hotspot,mode);
	}
	public void SetCursorPick(){
		Cursor.SetCursor(cursor_Pick,hotspot,mode);
	}
}
