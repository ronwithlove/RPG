using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public float speed=4;

	private PlayerDir playerDir;
	private CharacterController playerController;

	// Use this for initialization
	void Start () {
		playerDir=this.GetComponent<PlayerDir>();
		playerController=this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		float distance=Vector3.Distance(playerDir.movePosition,transform.position);

		if(distance>0.1){//这样有个bug,player有时会过了目标点，然后又继续往前进。
			print (distance);
			playerController.SimpleMove(transform.forward*speed);
		}
	}
}
