using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
	
    Magnet magneticTrigger;
	SoundFx sound;
	UiStasts ui;
    ArcadeFly arcadeFly;
	bool shielded = false;
	float shieldTime = 2f;
	float shieldTimer;
    bool dead;

    public float startingHealth = 10;		//se allagh na ginei kai sto UiStats
	public float startingEnergy = 100;		//				>>
	public float energyPerSec;
	public float healthUpgMultiplier = 10f;
	public float energyUpgMultiplier = 2.5f;

	[HideInInspector] 
	public float currentHealth;
	[HideInInspector]
	 public float currentPoints;
	[HideInInspector] 
	public float coinsAquired;
	[HideInInspector] 
	public float currentEnergy;

	void Start()
    {
		startingHealth += SaveManager.instance.f2 * healthUpgMultiplier;
		currentHealth = startingHealth;
		startingEnergy += SaveManager.instance.f4 * energyUpgMultiplier;
		currentEnergy = startingEnergy;
		shieldTime += SaveManager.instance.f5 * 0.5f;
        arcadeFly = GetComponent<ArcadeFly>();
		sound = GetComponent<SoundFx>();
		ui = GetComponent<UiStasts>();
        magneticTrigger = GetComponentInChildren<Magnet>();
	}


	void Update()
    {
		currentEnergy -= Time.deltaTime * energyPerSec;
		CheckForDeath ();

		if (shielded){
			shieldTimer -= Time.deltaTime;
			if (shieldTimer <=0){
				shielded = false;
				}
		}
	}

	void OnTriggerEnter(Collider col)
    {
		if(col.CompareTag("Coin"))
        {
			currentPoints += 1;
			coinsAquired ++;
			Destroy(col.gameObject);
			sound.PlayCoin();
			ui.UpdateUI();
		}
        else if (col.gameObject.tag == "DoDamage" && !shielded)
        {
            ObjectStats stats = col.gameObject.GetComponent<ObjectStats>();
            currentHealth -= stats.damage;
            sound.PlayHit();
            ui.UpdateUI();
        }
        else if (col.gameObject.tag == "Points")
        {
            ObjectStats stats = col.gameObject.GetComponent<ObjectStats>();
            currentPoints += stats.points;
            Destroy(col.gameObject);
            ui.UpdateUI();
        }
        else if (col.CompareTag("Magnet"))
        {
			ObjectStats stats = col.gameObject.GetComponent<ObjectStats>();
			currentPoints += stats.points;
			Destroy(col.gameObject);
            magneticTrigger.EnableMagnet();
			sound.PlayMagnet();	
			ui.UpdateUI();
            print("Magnetized");
        }
        else if (col.CompareTag("Shield"))
        {
			ObjectStats stats = col.gameObject.GetComponent<ObjectStats>();
			currentPoints += stats.points;
			Destroy(col.gameObject);
			shielded = true;
			shieldTimer = shieldTime;
			sound.PlayShield();
			ui.UpdateUI();
            print("Shield ON");
        }
        else if (col.CompareTag("Energy"))
        {
			ObjectStats stats = col.gameObject.GetComponent<ObjectStats>();
			currentPoints += stats.points;
			Destroy(col.gameObject);
			currentEnergy += 20;
			if (currentEnergy > startingEnergy)
            {
				currentEnergy = startingEnergy;
			}
			sound.PlayEnergy();
			ui.UpdateUI();
        }
    }

	void CheckForDeath ()
    {
		if (!dead && (currentHealth <= 0 || currentEnergy <= 0)) {
            dead = true;
            arcadeFly.StartMovementStop();
			ui.Death();
			sound.PlayDeath();
            dead = true;
        }
	}
}
