using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour {

	public float damage;
	public float points;


	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag == "Player"){
			Destroy(gameObject);
		}
	}
}
