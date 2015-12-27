using UnityEngine;
using System.Collections;

public enum WolfState{
	Idel,
	Walk,
	Attack,
	Death
}

public class WolfBaby : MonoBehaviour {

	public WolfState state=WolfState.Idel;
	public float time=1;//每隔1秒从判断巡逻时候的状态
	public float speed=1;
	public int hp=100;
	public float dodgeRate=0.2f;
	public float attack2Rate=0.25f;//25%几率发动攻击2
	public GameObject HUDTextperfab;//HUDText的perfab
	public float attackRange;//这个距离的时候可以发起攻击
	public float targetRange;//超过这个距离小狼失去目标，未超过小狼将移动到attackRange
	public float attack1AniTime=0.633f;//攻击1播放时间
	public float attack2AniTime=0.733f;//攻击1播放时间
	public float attackRate=1f;//攻击率，每隔1秒攻击一次

	private float timer=0;
	private float attackTimer=0;
	private bool isWalking=false;
	private Animation wolfAnim;
	private CharacterController cc;
	private Color normalColor;
	private GameObject HUDTextGo;//新建后的HUDText
	private Transform beFollowedTarget;
	private Transform attackTarget;//小狼攻击对象


	void Awake(){
		wolfAnim=this.GetComponent<Animation>();
		cc=this.GetComponent<CharacterController>();
		normalColor=this.GetComponentInChildren<Renderer>().material.color;
		beFollowedTarget=transform.Find("BeFollowedTarget");
	}

	void Start(){
		HUDTextGo=NGUITools.AddChild(HUDTextContainer._instace.gameObject,HUDTextperfab);//NGUI中的物件避免用GameObject.Instantiate来创建一个实例
		HUDTextGo.GetComponent<UIFollowTarget>().target=beFollowedTarget;//设置一下UI Follow Target下的属性
		//只要赋值了target就好，Follow Target下的 Game Camera和 Ui Camera会自动找到的。
	}

	void Update(){

		if(Input.GetMouseButtonDown(1)){
			BeHit(1);
		}

		if(state==WolfState.Death){
			wolfAnim.CrossFade("WolfBaby-Death");
		}else if (state== WolfState.Attack){
			//攻击
		}else {//巡逻，有两种状态，发呆Idel或行走walk
			timer+=Time.deltaTime;
			if(timer>=time){
				timer=0;
				int randno= Random.Range(0,2);//随机0或1，来决定随机状态
				if(randno==0){
					isWalking=false;
					wolfAnim.CrossFade("WolfBaby-Idle");
				}else{//如果是行走，先随机一个行走方向，当然这里最好是在写一下如果撞倒墙了就改变方向
					isWalking=true;
					transform.Rotate(transform.up * Random.Range (0,360));//改变方向，up是Y轴
					wolfAnim.CrossFade ("WolfBaby-Walk");
				}// end if(timer>=time) else
			}//end if(timer==time)
			if(isWalking){//这里要用一个判断来做移动，不能直接写在改变方向后面
				cc.SimpleMove(transform.forward*speed);
			}
		}// end 巡逻

	}

	public void BeHit(int damage){
		if(state!=WolfState.Death){//不死才执行下面被打
			float value= Random.Range(0f,1f);
			if(value<dodgeRate){//小狼没被打中
				HUDTextGo.GetComponent<HUDText>().Add("Miss",Color.gray,1);
				print ("没被打中");
				print (HUDTextGo.GetComponent<HUDText>());
			}else{//被打中了
				HUDTextGo.GetComponent<HUDText>().Add(-damage,Color.red,1);
				hp-=damage;
				StartCoroutine(changeBodyColor());//被打中身体颜色变红 IEnumerator 配合StartCoroutine调用
				print ("被打中");
				if(hp<=0){
					state=WolfState.Death;
					GameObject.Destroy(this.gameObject,2f);
				}
			}
		}//end if(state!=WolfState.Death)
	}

	IEnumerator changeBodyColor(){//用协程来做
		this.GetComponentInChildren<Renderer>().material.color=Color.red;
		yield return new WaitForSeconds(0.15f);//IEnumerator 必须用yield return来返回结果
		this.GetComponentInChildren<Renderer>().material.color=normalColor;
	}

	void AutoAttack(){
		if(attackTarget!=null){
			attackTimer+=Time.deltaTime;
			float distance=Vector3.Distance (attackTarget.position,transform.position);
			if(distance<=attackRange){//在攻击范围，做攻击处理
				float value= Random.Range(0f,1f);
				if(value<attack2Rate){//攻击2
					wolfAnim.CrossFade("WolfBaby-Attack2");
					//造成伤害
					if(attackTimer>=attack2AniTime){ 
						wolfAnim.CrossFade("WolfBaby-Idle");//动画时间结束就播放空闲动画
					}
				}else{//攻击1
					wolfAnim.CrossFade("WolfBaby-Attack1");
					//造成伤害
					if(attackTimer>=attack1AniTime){
						wolfAnim.CrossFade("WolfBaby-Idle");//动画时间结束就播放空闲动画
					}
				}
				if(attackTimer>=attackRate){//攻击间隔，现在是设每隔1秒攻击一次
					attackTimer=0;
				}
			}else if(distance>attackRange&& distance<=targetRange){//超出攻击范围，但是在目标范围内。
				transform.LookAt(attackTarget);
				cc.SimpleMove(transform.forward*speed);
				wolfAnim.CrossFade ("WolfBaby-Walk");
			}else{//超出视线范围
				attackTarget=null;//失去目标
			}
		}else{
			state=WolfState.Idel;
		}
	}

	void OnDestroy() {//MonoBehaviour的函数，当他将被销毁时，调用这个函数
		GameObject.Destroy(HUDTextGo);
	}

	void OnMouseEnter(){//鼠标图标改变
			CursorManager._instance.SetCursorAttack();
	}
	void OnMouseExit(){//鼠标移出 恢复默认鼠标
		CursorManager._instance.SetCursorNormal();
	}
}
