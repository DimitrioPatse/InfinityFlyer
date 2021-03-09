using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {

	float timer = 3f;
	float timeReset;
    bool activeMagnet;

	// Use this for initialization
	void Start () {
		timer += SaveManager.instance.f6;
	}

	void Update()
    {
        if (activeMagnet)
        {
            timeReset -= Time.deltaTime;
            if (timeReset <= 0)
            {
                activeMagnet = false;
            }
        }
	}

	void OnTriggerEnter(Collider col)
    {
		if (activeMagnet && col.gameObject.tag == "Coin"){
			Coin coin = col.GetComponent<Coin>();
			coin.magnetized = true;
		}	
	}

	public void EnableMagnet()
    {
        timeReset = timer;
        activeMagnet = true;
	}
}
