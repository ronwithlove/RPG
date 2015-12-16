using UnityEngine;
using System.Collections;

public class BarNPC : NPC{

	public TweenPosition questTween;
	public UILabel questLabel;
	public GameObject acceptBtn;
	public GameObject cancelBtn;
	public GameObject okBtn;

	private bool onTask=false;
	public int wolfKillcount=0;
	private PlayerStatus playerstatus;


	void Start(){
		playerstatus=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
	}

	void OnMouseOver(){//当鼠标移动到这个collider上的时候，每一帧都会检测,不需要写在update里
		if(Input.GetMouseButtonDown(0)){
			this.GetComponent<AudioSource>().Play();
			ShowQuest ();
			if (onTask){
				isOnTask();
			}else{
				isNotOnTask();
			}
		}
	}

	void ShowQuest(){
		questTween.gameObject.SetActive(true);
		questTween.PlayForward();//对话框移入动画
	}

	public void OnClose(){//按下关闭按键执行
		questTween.PlayReverse();//对话框移出动画
	}

	public void OnAcceptClick(){
		onTask=true;
		isOnTask();

	}

	public void OnOkClick(){
		if(wolfKillcount>=10){
			wolfKillcount=0;
			onTask=false;
			playerstatus.getCoins(1000);
			questTween.PlayReverse();
		}else{
			questTween.PlayReverse();
		}
	}

	public void OnCancelClick(){
		questTween.PlayReverse();//对话框移出动画
	}

	void isOnTask(){
		questLabel.text="已击败 "+(wolfKillcount)+"/10头幼狼\n\n获得：1000金币";
		acceptBtn.SetActive(false);
		cancelBtn.SetActive(false);
		okBtn.SetActive(true);
	}

	void  isNotOnTask(){
		questLabel.text="任务：第一个任务\n\n击败10只幼狼\n\n奖励：1000金币";
		acceptBtn.SetActive(true);
		cancelBtn.SetActive(true);
		okBtn.SetActive(false);
	}

}
