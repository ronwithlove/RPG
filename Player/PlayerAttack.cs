using UnityEngine;
using System.Collections;

public enum PlayerAttackStatus{
	NoAttack,
	NormalAttack,
	SkillAttack
}

public class PlayerAttack : MonoBehaviour {

	public float attackRange=5;
	public PlayerAttackStatus attStatus=PlayerAttackStatus.NoAttack;
	public float attackRate=1f;//攻击率，每隔1秒攻击一次
	public float attack1AniTime= 0.8333f;

	private Transform target;
	private Animation playerAni;
	private float attackTimer=0;

	void Awake(){
		playerAni=this.GetComponent<Animation>();
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);//创建一条摄像机到鼠标点的射线
			RaycastHit hitInfo;//射线信息
			bool isCollider=Physics.Raycast(ray, out hitInfo);//光线投射，返回的hitInfo带有射线的信息
			if(isCollider && hitInfo.collider.tag==Tags.enemy ){
				target=hitInfo.collider.transform;
				transform.LookAt(target);
			}else{//如果鼠标点的不是怪物，是其他的，那么
				target=null;//就没有攻击对象
				attStatus=PlayerAttackStatus.NoAttack;//就是行走（非攻击状态）
			}
		}

		if(target!=null){//只要有攻击目标就要检测
			float distance=Vector3.Distance (target.position,transform.position);//print (distance);
			if(distance<=attackRange-4){//当距离敌人1米，停止走动时候开始攻击
				attStatus=PlayerAttackStatus.NormalAttack;//设为自动攻击状态
			}else if(distance>attackRange){
				print ("超出攻击范围");
			}
		}

		if(attStatus==PlayerAttackStatus.NormalAttack){
			attackTimer+=Time.deltaTime;
				playerAni.CrossFade("Attack1");
				if(attackTimer>=attack1AniTime){
					playerAni.CrossFade("Idle");
				}
			if(attackTimer>=attackRate){//攻击间隔，现在是设每隔1秒攻击一次
				attackTimer=0;
			}
		}
	}

}
