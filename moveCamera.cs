using UnityEngine;
using System.Collections;

public class moveCamera : MonoBehaviour {

	public float speed=5;
	public float endZ=-20;
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z <endZ){
			//transform.position=new Vector3(transform.position.x,transform.position.y,transform.position.z+speed*Time.deltaTime);
			transform.Translate(Vector3.forward*speed*Time.deltaTime);
		}
	
	}
}
