using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStasts : MonoBehaviour {

    [SerializeField] float updateDelay = 0.5f;
	[SerializeField] Text metersText;
    [SerializeField] float meterDevider = 10f;
	[SerializeField] Text pointsText;
	[SerializeField] Text healthText;
	[SerializeField] Text coinText;
	[SerializeField] Slider energySlider;
	[Space]
	[SerializeField] RawImage deathPanel;

    float points;
    float health;
    float energy;
    float coins;
	float meters;

	PlayerStats stats;
	ArcadeFly fly;

	// Use this for initialization
	void Start () {
		
		stats = GetComponent<PlayerStats>();
		fly = GetComponent<ArcadeFly>();

		energySlider.maxValue = stats.startingEnergy;
        StartCoroutine("UpdateUITimer");
	}
	
	// Update is called once per frame
	void Update () {
		meters = stats.gameObject.transform.position.z / meterDevider;
		metersText.text = Mathf.RoundToInt(meters).ToString();	

		energySlider.value = stats.currentEnergy;
	}
    IEnumerator UpdateUITimer()
    {
        UpdateUI();
        yield return new WaitForSeconds(updateDelay);
    }
	public void UpdateUI(){
		health = stats.currentHealth;
		healthText.text = health.ToString();
		points = stats.currentPoints;
		pointsText.text = points.ToString();
        coins = stats.coinsAquired;
		coinText.text = coins.ToString();
	}

	public void Death(){
		deathPanel.gameObject.SetActive(true);
		UpdateUI();
	}
}
