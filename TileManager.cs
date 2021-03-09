using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {



	Transform player;

	[SerializeField] float tileLength = 500;
	float spawnInZ;
	float safeZone = 700;
	[SerializeField] int onScreenTiles = 5;
	int lastPrefabIndex = 0;
	public GameObject[] tilePrefabs;

	List<GameObject> activeTiles = new List<GameObject>();


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		spawnInZ = tileLength / 2;
		for (int i=0; i<onScreenTiles; i++){
			if (i>2){
				SpawnTile(0);
				}
			else
				SpawnTile();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player.position.z - safeZone> (spawnInZ - onScreenTiles * tileLength)){
			SpawnTile();
			DeleteTile();
		}
	}

	void SpawnTile(int prefabIndex = -1){
		GameObject tile;
		if(prefabIndex == -1)
		tile = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
		else
		tile = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
		tile.transform.SetParent(transform);
		tile.transform.position = Vector3.forward * spawnInZ;
		spawnInZ += tileLength;
		activeTiles.Add(tile);
	}

	void DeleteTile(){
		Destroy (activeTiles[0]);
		activeTiles.RemoveAt(0);
	}

	int RandomPrefabIndex(){
		if (tilePrefabs.Length <= 1)
			return 0;

		int randomIndex = lastPrefabIndex;
		while(randomIndex == lastPrefabIndex){
			randomIndex = Random.Range(0,tilePrefabs.Length);
		}

		lastPrefabIndex = randomIndex;
		return randomIndex;
	}
}
