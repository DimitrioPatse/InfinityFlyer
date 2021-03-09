using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {

	public static SaveManager instance;

	[HideInInspector] public int f1;	//High Score
	[HideInInspector] public int f2;	//Health
	[HideInInspector] public int f3;	//Turn Degrees
	[HideInInspector] public int f4; 	//Energy
	[HideInInspector] public int f5;	//Invisility Time
	[HideInInspector] public int f6;	//Magnet Time
	[HideInInspector] public int f7;	//Booster Time
	[HideInInspector] public int f8;	//Coins
	[HideInInspector] public int f9;	//Feathers


	// Use this for initialization
	void Awake () {
		if (instance == null){
			instance = this;
		}else if (instance !=this){
			Destroy(gameObject);
		}
		Load();
	}

	public int SetHighScore(int newf1){
		if (newf1 >= f1)
			f1 = newf1;
		return f1;
	}

	public int UpgadeHealth(int newf2){
		if(newf2>f2)
			f2 = newf2;
		return f2;
	}

	public int UpgradeSteering(int newf3){
		if(newf3>f3)
			f3 = newf3;
		return f3;
	}

	public int UpgradeEnergy(int newf4){
		if(newf4>f4)
			f4 = newf4;
		return f4;
	}

	public int UpgradeShield(int newf5){
		if(newf5>f5)
			f5 = newf5;
		return f5;
	}

	public int UpgradeMagnet(int newf6){
		if(newf6>f6)
			f6 = newf6;
		return f6;
	}

	public int UpgradeBooster(int newf7){
		if(newf7>f7)
			f7 = newf7;
		return f7;
	}

	public int SetCoins(int newCoins){
	f8 += newCoins;
	return f8;
	}

	public int SetFeathers (int newFeathers){
	f9 += newFeathers;
	return f9;
	}



	public void Save () {
		Debug.Log("Save");
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/foreverPlayer.dat");

		PlayerData data = new PlayerData();
		data.f1 = f1;
		data.f2 = f2;
		data.f3 = f3;
		data.f4 = f4;
		data.f5 = f5;
		data.f6 = f6;
		data.f7 = f7;
		data.f8 = f8;
		data.f9 = f9;

		bf.Serialize(file,data);
		file.Close();
	}

	public void Load(){
		if (File.Exists(Application.persistentDataPath + "/foreverPlayer.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/foreverPlayer.dat",FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			f1 = data.f1;
			f2 = data.f2;
			f3 = data.f3;
			f4 = data.f4;
			f5 = data.f5;
			f6 = data.f6;
			f7 = data.f7;
			f8 = data.f8;
			f9 = data.f9;
		}
	}
	public void DeleteSaves(){
		File.Delete(Application.persistentDataPath + "/foreverPlayer.dat");
		Reset();
	}

	void Reset(){
		f1 = 0;
		f2 = 0;
		f3 = 0;
		f4 = 0;
		f5 = 0;
		f6 = 0;
		f7 = 0;
		f8 = 0;
		f9 = 0;
	}
}

[Serializable]
class PlayerData{
	public int f1;
	public int f2;
	public int f3;
	public int f4;
	public int f5;
	public int f6;
	public int f7;
	public int f8;
	public int f9;

}