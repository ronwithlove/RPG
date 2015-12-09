using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	private Transform player;
	private Vector3 offset=Vector3.zero;
	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag(Tags.player).transform;
		offset=transform.position-player.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position=player.position+offset;
	
	}
}
