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
	public float timer=0;
	public float speed=1;

	private bool isWalking=false;
	private Animation wolfAnim;
	private CharacterController cc;

	void Awake(){
		wolfAnim=this.GetComponent<Animation>();
		cc=this.GetComponent<CharacterController>();
	}

	void Update(){
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
					print ("Idel");
				}else{//如果是行走，先随机一个行走方向，当然这里最好是在写一下如果撞倒墙了就改变方向
					print ("Walk");
					isWalking=true;
					transform.Rotate(transform.up * Random.Range (0,360));//改变方向，up是Y轴
					wolfAnim.CrossFade ("WolfBaby-Walk");
					print (transform.forward);
				}// end if(timer>=time) else
			}//end if(timer==time)
			if(isWalking){//这里要用一个判断来做移动，不能直接写在改变方向后面
				cc.SimpleMove(transform.forward*speed);
			}
		}// end 巡逻
	}

}
