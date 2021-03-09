using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgEnergyBut : MonoBehaviour {

	[SerializeField] int buttonNumber;
	[SerializeField] int upgradeCost;

	[SerializeField] Sprite activeSprite;
	[SerializeField] Sprite enableSprite;

	Button myButton;
	Image mySprite;

	int key;
	int coinsIHave;

	// Use this for initialization
	void Start () {
		myButton = gameObject.GetComponent<Button>();
		mySprite = myButton.GetComponent<Image>();
		}

	void Update(){
		coinsIHave = SaveManager.instance.f8;
		key = SaveManager.instance.f4;

		if (key >= buttonNumber){
			mySprite.sprite = activeSprite;
		}else if (buttonNumber == key + 1 && coinsIHave >= upgradeCost){
			mySprite.sprite = enableSprite;
			myButton.interactable = true;
		}else {
			mySprite.sprite = enableSprite;
			myButton.interactable = false;
		}
	}

	public void Upgrade(){
		SaveManager.instance.UpgradeEnergy(buttonNumber);
		SaveManager.instance.Save();
	}
}
