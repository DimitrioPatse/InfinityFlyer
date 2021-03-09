using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public bool magnetized = false;

	GameObject player;
	Transform playersTr;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward);
		playersTr = player.transform;

		if (magnetized){
			transform.position = Vector3.MoveTowards(transform.position, playersTr.position, Time.deltaTime * 800);
		}
	}


}
