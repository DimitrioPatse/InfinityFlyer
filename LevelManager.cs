using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance = null;

	public float autoLoadNextLevel;
	public Slider loadingBar;
	public GameObject loadingImage;

	private AsyncOperation async;

	void Start(){
		if (instance == null){
			instance = this;
		}else if (instance !=this){
			Destroy(gameObject);
		}
		if(autoLoadNextLevel >0){
		Invoke ("LoadNextLevel",autoLoadNextLevel);
		}else{
			Debug.Log("Level auto load disabled, use positive number in seconds to activate");
		}
	}
	public void LoadLevel(string name){
		if(loadingImage && loadingBar){
		loadingBar.gameObject.SetActive(true);
		//loadingImage.SetActive(true);
		StartCoroutine(LoadLevelWithBar(name));
		}else {
			SceneManager.LoadScene(name);
		}
	}

	IEnumerator LoadLevelWithBar(string name){
		async = SceneManager.LoadSceneAsync(name);
		while (!async.isDone){
			loadingBar.value = async.progress;
			yield return null;
		}

	}
	public void QuitRequest(){
		Application.Quit ();
	}
	
	public void LoadNextLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void RestartLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	
}

