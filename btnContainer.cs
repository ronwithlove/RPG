using UnityEngine;
using System.Collections;

public class btnContainer : MonoBehaviour {

	public void onNewGame(){
		//新建角色，进入场景。
		print ("new game");
	}

	public void onLoadGame(){
		//加载保存游戏，进入场景。
		print ("load game");
	}
}
