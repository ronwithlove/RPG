using UnityEngine;
using System.Collections;

public class HeadUI : MonoBehaviour {

	public static HeadUI _instance;

	private UILabel playerName;
	private UISlider hp;
	private UISlider mp;
	private UILabel hpLabel;
	private UILabel mpLabel;
	private PlayerStatus ps;

	void Awake(){
		_instance=this;
		playerName=transform.Find("Name").GetComponent<UILabel>();
		hp=transform.Find ("HP").GetComponent<UISlider>();
		mp=transform.Find ("MP").GetComponent<UISlider>();
		hpLabel=transform.Find("HP/Thumb/Label").GetComponent<UILabel>();
		mpLabel=transform.Find("MP/Thumb/Label").GetComponent<UILabel>();
	}
 
	void Start () {
		ps=GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
		HeadStatus();
	}

	void HeadStatus(){
		playerName.text=ps.playerName;
		hp.value=ps.hp/ps.maxHP;
		mp.value=ps.mp/ps.maxMP;
		hpLabel.text=ps.hp+"/"+ps.maxHP;//这里要把HP下的 On Value Change选着的Label去掉，才可以正常工作
		mpLabel.text=ps.mp+"/"+ps.maxMP;
	}


}
