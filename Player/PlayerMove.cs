using UnityEngine;
using System.Collections;

public enum PlayerState{
	Move,
	Idle
}

public class PlayerMove : MonoBehaviour {
	public float speed=4;
	public bool isMoving= false;
	public PlayerState state=PlayerState.Idle;

	private PlayerDir playerDir;
	private CharacterController playerController;
	private PlayerAttack playerAttack;

	// Use this for initialization
	void Awake () {
		playerAttack=this.GetComponent<PlayerAttack>();
		playerDir=this.GetComponent<PlayerDir>();//这里<>中是不要冒号的
		playerController=this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerAttack.attStatus==PlayerAttackStatus.NoAttack){//不等于攻击的时候播放行走动画
			float distance=Vector3.Distance(playerDir.movePosition,transform.position);//注意格式 
			if(distance>0.1){
				isMoving=true;
				playerController.SimpleMove(transform.forward*speed);//注意格式 括号内的是移动方向
				state=PlayerState.Move;//动画需要
			}else{
				isMoving=false;//这里判断Player是否在移动，PlayerDir会得到这个值，通过他来判断是否要调整Player的朝向
				state=PlayerState.Idle;
			}
		}
	}
}

