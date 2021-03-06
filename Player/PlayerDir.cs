﻿using UnityEngine;
using System.Collections;

public class PlayerDir : MonoBehaviour {

	public GameObject moveEffect;//点击特效
	public Vector3 movePosition=Vector3.zero;

	private bool isMouseButtonDown=false;
	private PlayerMove playerMove;

	void Awake(){
		movePosition=transform.position;//一开始的时候移动目标位置就是自己，也就是不移动
		playerMove=this.GetComponent<PlayerMove>();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)&& UICamera.hoveredObject==null){//鼠标按下后执行一次
			isMouseButtonDown=true;
			Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);//创建一条摄像机到鼠标点的射线
			RaycastHit hitInfo;//射线信息
			bool isCollider=Physics.Raycast(ray, out hitInfo);//光线投射，返回的hitInfo带有射线的信息
			if(isCollider && hitInfo.collider.tag==Tags.ground){//从返回的射线信息来判断，如果碰撞的物体是ground
				showClickEffect(hitInfo.point);//代入射线碰撞的坐标，显示点击效果
			}
		}

		if(isMouseButtonDown){//鼠标左键按下一直没有抬起
			Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);//创建一条摄像机到鼠标点的射线
			RaycastHit hitInfo;//射线信息
			Physics.Raycast(ray, out hitInfo);//光线投射，返回的hitInfo带有射线的信息
			if(hitInfo.collider.tag==Tags.ground || hitInfo.collider.tag==Tags.enemy ){//从返回的射线信息来判断，如果碰撞的物体是ground
				//showClickEffect(hitInfo.point);//一直按下就不需要不停的显示特效了
				playerFacing(hitInfo.point);
			}
		}else{ //如果鼠标抬起了
			if(playerMove.isMoving){//鼠标抬起，但是还在移动中就还是要不断的更新向最终位置的朝向，避免被物体碰撞到改变方向。
				playerFacing(movePosition);
			}
		}


		if(Input.GetMouseButtonUp(0)){//鼠标左键抬起
			isMouseButtonDown=false;
		}
	}

	void showClickEffect(Vector3 mousePoint){
		mousePoint=new Vector3(mousePoint.x,mousePoint.y+0.1f,mousePoint.z);//效果和地面有点重合，把y轴提高0.1, float记得加 f
		GameObject.Instantiate(moveEffect,mousePoint,Quaternion.identity);
	}

	void playerFacing(Vector3 facePoint){
		movePosition=new Vector3(facePoint.x,this.transform.position.y,facePoint.z);//player朝向方向，Y轴保持和player的一致，这样player不会出现"抬头"或"低头"的情况
		this.transform.LookAt(movePosition);// player 朝向点击的地方
	}

}
