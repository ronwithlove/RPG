using UnityEngine;
using System.Collections;

public class PlayerDir : MonoBehaviour {
	
	public GameObject moveEffect;//点击特效
	
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){
			Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);//创建一条摄像机到鼠标点的射线
			RaycastHit hitInfo;//射线信息
			Physics.Raycast(ray, out hitInfo);//光线投射，返回的hitInfo带有射线的信息
			if(hitInfo.collider.tag==Tags.ground){//从返回的射线信息来判断，如果碰撞的物体是ground
				showClickEffect(hitInfo.point);//代入射线碰撞的坐标，显示点击效果
				this.transform.LookAt(hitInfo.point);// player 朝向点击的地方
			}
			
		}
	}
	
	void showClickEffect(Vector3 mousePoint){
		mousePoint=new Vector3(mousePoint.x,mousePoint.y+0.1f,mousePoint.z);//效果和地面有点重合，把y轴提高0.1, float记得加 f
		GameObject.Instantiate(moveEffect,mousePoint,Quaternion.identity);
	}
	
}
