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
	public float time=1;
	public float speed=1;
	public int hp=100;
	public float dodgeRage=0.2f;
	public GameObject HUDTextperfab;//HUDText的perfab

	private float timer=0;
	private bool isWalking=false;
	private Animation wolfAnim;
	private CharacterController cc;
	private Color normalColor;
	private GameObject HUDTextGo;//新建后的HUDText
	private Transform beFollowedTarget;

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
		float value= Random.Range(0f,1f);
		if(value<dodgeRage){//小狼没被打中
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
	}

	IEnumerator changeBodyColor(){//用协程来做
		this.GetComponentInChildren<Renderer>().material.color=Color.red;
		yield return new WaitForSeconds(0.15f);//IEnumerator 必须用yield return来返回结果
		this.GetComponentInChildren<Renderer>().material.color=normalColor;
	}

	void OnDestroy() {//MonoBehaviour的函数，当他将被销毁时，调用这个函数
		GameObject.Destroy(HUDTextGo);//死了以后再造成伤害就不显示了，全完成之后检查一下
	}
}
