using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_Booster : MonoBehaviour {

	GameObject player;
	ArcadeFly playerMove;
	float boosterlvl = 1;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerMove = player.GetComponent<ArcadeFly>();	
		boosterlvl += SaveManager.instance.f7; 
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player"){
			playerMove.SetBooster(boosterlvl);
			Destroy(gameObject);
		}
	}
}
