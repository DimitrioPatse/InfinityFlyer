using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsShowUpg : MonoBehaviour {

	Text mytext;


	// Use this for initialization
	void Start () {
		mytext = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		mytext.text = ((int)SaveManager.instance.f8).ToString();
	}
}
