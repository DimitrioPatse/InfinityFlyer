using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

	float distance;
	float coins;
	float points;

    PlayerStats playerStats;
    ArcadeFly arcadeFly;
	SaveManager save;

	[SerializeField] Text distanceText;
	[SerializeField] Text pointsText;
	[SerializeField] Text coinText;

	void OnEnable(){
        arcadeFly = FindObjectOfType<ArcadeFly>();
        playerStats = FindObjectOfType<PlayerStats>();
        save = SaveManager.instance;
        distance = arcadeFly.metersRun;
		distanceText.text = Mathf.RoundToInt(distance).ToString();
		coins = playerStats.coinsAquired;
		save.SetCoins((int)coins);
		coinText.text = coins.ToString();
		points = playerStats.currentPoints;
		pointsText.text = points.ToString();
		save.SetHighScore((int)points);
		save.Save();
	}
}
