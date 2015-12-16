using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

	public static Status _instance;

	private UILabel strLabel;
	private UILabel defLabel;
	private UILabel speLabel;
	private UILabel remainPointsLabel;
	private UILabel summaryLabel;
	private GameObject strPlus;
	private GameObject defPlus;
	private GameObject spePlus;
	private TweenPosition statusTween;
	private bool isStatusOpen=false;
	private PlayerStatus playerStatus;


	void Awake(){
		_instance=this;
		statusTween=this.GetComponent<TweenPosition>();	
		statusTween.gameObject.SetActive(false);
		strLabel=transform.Find("Strength").GetComponent<UILabel>();
		defLabel=transform.Find("Defence").GetComponent<UILabel>();
		speLabel=transform.Find("Speed").GetComponent<UILabel>();
		remainPointsLabel=transform.Find("RemainPoints").GetComponent<UILabel>();
		summaryLabel=transform.Find("Summary").GetComponent<UILabel>();
		strPlus=transform.Find("Str_Plus").gameObject;//transform.Find("Str_Plus")返回的是一个transform,要获得的是这个transform的gameObject,所以要再加上gameObject
		defPlus=transform.Find("Def_Plus").gameObject;
		spePlus=transform.Find("Speed_Plus").gameObject;
		playerStatus=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
	}

	public void StatusShowHide(){
		if(isStatusOpen){
			isStatusOpen=false;
			statusTween.PlayReverse();
		}else{
			isStatusOpen=true;
			statusTween.gameObject.SetActive(true);
			UpdateStatus();
			statusTween.PlayForward();
		}
	}

	void UpdateStatus(){//更新面板的内容，这个单独写成一个方法，因为启动属性面板时候要用到，每次加属性值也要用到
		strLabel.text=playerStatus.strength+"+"+playerStatus.str_puls;
		defLabel.text=playerStatus.defence+"+"+playerStatus.def_plus;
		speLabel.text=playerStatus.speed+"+"+playerStatus.speed_plus;
		remainPointsLabel.text=playerStatus.remainPoints.ToString ();
		summaryLabel.text="力量："+(playerStatus.strength+playerStatus.str_puls)
			+" "+"防御："+(playerStatus.defence+playerStatus.def_plus)
				+" "+"速度："+(playerStatus.speed+playerStatus.speed_plus);
		if(playerStatus.remainPoints>0){
			strPlus.SetActive(true);
			defPlus.SetActive(true);
			spePlus.SetActive(true);
		}else{
			strPlus.SetActive(false);
			defPlus.SetActive(false);
			spePlus.SetActive(false);
		}
	}

	public void OnStrPlusClick(){
		playerStatus.StrengthPlusClick();
		UpdateStatus();
	}
	public void OnDefPlusClick(){
		playerStatus.DefencePlusClick();
		UpdateStatus();
	}
	public void OnSpePlusClick(){
		playerStatus.SpeedPlusClick();
		UpdateStatus();		
	}
}
