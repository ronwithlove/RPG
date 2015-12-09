using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	private PlayerMove playerMove;
	// Use this for initialization
	void Start () {
		playerMove=this.GetComponent<PlayerMove>();
	}
	
	//注意这里用的是LateUpdate(),因为在PlayerMove的状态是写在Update()里的，所以这里写LateUpdate()比较好
	void LateUpdate () {
		if (playerMove.state==PlayerState.Idle){
			playAnim("Idle");
		}else if (playerMove.state==PlayerState.Move){
			playAnim("Run");
		}
	
	}

	void playAnim(string AnimName){
		this.GetComponent<Animation>().Play(AnimName);//直接可以用他的Animation组件来播放动画
	}

}
