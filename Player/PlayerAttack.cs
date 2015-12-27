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
	public GameObject effect1;

	private Transform target;
	private Animation playerAni;
	private float attackTimer=0;
	private bool hit=true;
	private PlayerStatus ps;

	void Awake(){
		playerAni=this.GetComponent<Animation>();
		ps=this.GetComponent<PlayerStatus>();
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);//创建一条摄像机到鼠标点的射线
			RaycastHit hitInfo;//射线信息
			bool isCollider=Physics.Raycast(ray, out hitInfo);//光线投射，返回的hitInfo带有射线的信息
			if(isCollider && hitInfo.collider.tag==Tags.enemy ){
				target=hitInfo.collider.transform;
				transform.LookAt(target);
				target.GetComponent<WolfBaby>().SetMinimapMark();
			}else{//如果鼠标点的不是怪物，是其他的，那么
				target=null;//就没有攻击对象
				attStatus=PlayerAttackStatus.NoAttack;//就是行走（非攻击状态）
			}
		}

		if(target!=null){//只要有攻击目标就要检测
			float distance=Vector3.Distance (target.position,transform.position);//print (distance);
			if(distance<=2){//当距离敌人2米，停止走动时候开始攻击
				attStatus=PlayerAttackStatus.NormalAttack;//设为自动攻击状态
			}else if(distance>attackRange){
				print ("超出攻击范围");
			}
		}

		if(attStatus==PlayerAttackStatus.NormalAttack){
			if (target.GetComponent<WolfBaby>().state!=WolfState.Death){
				attackTimer+=Time.deltaTime;
					playerAni.CrossFade("Attack1");//hit放在这里的话动作刚开始就减伤害，不太好
					if(attackTimer>=attack1AniTime){
					if(hit){//上面一个攻击效果完成后放特效，减血
						GameObject.Instantiate(effect1,target.position,Quaternion.identity);
						target.GetComponent<WolfBaby>().BeHit(getDamage());
						hit=false;//打完一次
					}
						playerAni.CrossFade("Idle");
					}
				if(attackTimer>=attackRate){//攻击间隔，现在是设每隔1秒攻击一次
					attackTimer=0;
					hit=true;//一次攻击间隔完成，变成true
				}
			}else{//狼死了
				target=null;//就没有攻击对象
				attStatus=PlayerAttackStatus.NoAttack;//就是行走（非攻击状态）
			}
		}
	}//end void Update()

	int getDamage(){
		return ps.strength+ps.str_puls+EquipmentUI._instance.strength;
	}
}
