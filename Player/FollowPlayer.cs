using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public float scrollSpeed=3.0f;
	public float rotateSpeed=2.0f;

	private Transform player;
	private Vector3 offset=Vector3.zero;
	private float distance=0.0f;
	private bool isMouseDown=false;

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag(Tags.player).transform;
		offset=transform.position-player.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position=player.position+offset;
		RoateView();//他和ScrollView()的顺序不能颠倒，因为ScrllView()有修改offset大小，但是到了RoateVIew()又被重置了，所以得先用他再修改offset大小。
		ScrollView();
	}

	void ScrollView(){
		distance=offset.magnitude-Input.GetAxis("Mouse ScrollWheel")*scrollSpeed; //.magnitude获得长度，去掉方向
		distance=Mathf.Clamp (distance,3.0f,9.0f);//控制一下远近范围
		offset=offset.normalized*distance;//规范化后就得到方向，乘以长度，offset又回来了，好方法
	}

	void RoateView(){
		if(Input.GetMouseButtonDown(1)){
			isMouseDown=true;
		}
		//这里不能用else 因为鼠标可以按下，抬起，一直按下，一直抬起。
		if(Input.GetMouseButtonUp (1)){
			isMouseDown=false;
		}

		if(isMouseDown){
			transform.RotateAround(player.position,player.up,Input.GetAxis("Mouse X")*rotateSpeed);//左右的时候是围绕player的Y轴转

			Vector3 CameraPosition=transform.position;
			Quaternion CameraRotation=transform.rotation;//先保存摄像机的位置和旋转
			transform.RotateAround(player.position,transform.right,-Input.GetAxis("Mouse Y")*rotateSpeed);//上下的话是应该按照摄像机的X轴，而不是player的
			if(transform.eulerAngles.x<10||transform.eulerAngles.x>70){//超出范围之后把他的值变成超出前的值，就等于限定了范围。
				transform.position=CameraPosition;
				transform.rotation=CameraRotation;
			}
		}
		offset=transform.position-player.position;
	}

}
