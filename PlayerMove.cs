using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public static PlayerMove instance = null;

	SoundFx soundfx;

	[SerializeField] float returnSpeedToNormal;
	[SerializeField] float pitchDegrees, pitchSmoothness,minHeight,maxHeight;
	[SerializeField] float yawDegrees, yawSmoothness,sideMaxPos;
	[SerializeField] float rollDegrees, rollSmoothness;

	float refYaw;
	float refPitch;
	float refRoll;

	[HideInInspector] public float metersRun;
	float difficultyLevel = 1;
	float maxDifficulty = 7;
	float metersToNextLvl = 1;

	public float currentSpeed = 1;
	bool boosted = false;

	Vector3 flyRotation;
	CameraFollow mycamera;

	// Use this for initialization
	void Start () {
		if (instance == null){
			instance = this;
		}else if (instance !=this){
			Destroy(gameObject);
		}

		flyRotation = transform.rotation.eulerAngles.normalized;

		pitchDegrees += SaveManager.instance.f2 * 2;
		yawDegrees += SaveManager.instance.f2 * 2;
		soundfx = GetComponent<SoundFx>();
		mycamera = Camera.main.gameObject.GetComponent<CameraFollow>();
	}
	
	// Update is called once per frame
	void Update () {
		metersRun = transform.position.z / 100;
		Fly();
		LevelUp();

		Yaw();
		Pitch ();
		Roll ();

		transform.rotation = Quaternion.Euler(flyRotation);
	}

	void Fly(){
		transform.Translate(Vector3.forward * currentSpeed);
		}

	void Yaw(){
		if (transform.position.x < -sideMaxPos){
			flyRotation.y = Mathf.Clamp(flyRotation.y, 5, yawDegrees);

		 }else if (transform.position.x > sideMaxPos){
			flyRotation.y = Mathf.Clamp(flyRotation.y, -yawDegrees, -5);

		 }else{
			flyRotation.y = Mathf.Clamp(flyRotation.y, -yawDegrees, yawDegrees);
			}
		float newYaw = Input.GetAxis("Horizontal") * yawDegrees;
		//float newYaw = VirtualJoystick.instance.InputDirection.x * yawDegrees;
		flyRotation.y = Mathf.SmoothDampAngle(flyRotation.y, newYaw,ref refYaw, yawSmoothness);
	}

	void Pitch (){
		 if (transform.position.y < minHeight){
			flyRotation.x = Mathf.Clamp(flyRotation.x, -pitchDegrees, -5);

		 }else if (transform.position.y > maxHeight){
			flyRotation.x = Mathf.Clamp(flyRotation.x, 5, pitchDegrees);

		 }else{
			flyRotation.x = Mathf.Clamp(flyRotation.x, -pitchDegrees, pitchDegrees);
			}
		float newPitch = Input.GetAxis("Vertical") * pitchDegrees;
		//float newPitch = VirtualJoystick.instance.InputDirection.z * pitchDegrees;
		flyRotation.x = Mathf.SmoothDampAngle(flyRotation.x, newPitch,ref refPitch, pitchSmoothness);
	}

	void Roll (){
		flyRotation.z = Mathf.Clamp(flyRotation.z , -rollDegrees, rollDegrees);
		float newroll = Input.GetAxis ("Horizontal") * rollDegrees;
		//float newroll = VirtualJoystick.instance.InputDirection.x * rollDegrees;
		flyRotation.z = Mathf.SmoothDampAngle (flyRotation.z, -newroll, ref refRoll, rollSmoothness);
	}

	void LevelUp(){
		if(difficultyLevel == maxDifficulty){
			return;
		}else if (metersRun >metersToNextLvl && !boosted){
			metersToNextLvl *= 2;
			difficultyLevel ++;
			currentSpeed = difficultyLevel;
			}
	}

	public void SetBooster(float time){
		boosted = true;
		currentSpeed = currentSpeed + 2f;
		StartCoroutine("Booster", time);
		soundfx.PlaySpeed();
	}

	IEnumerator Booster(float boostTime){
		Debug.Log("Boosted");
		while (boostTime > 0 && boosted){
			mycamera.booster = true;
			boostTime -= Time.deltaTime;
			yield return null;
		}

		while (boostTime <= 0 && currentSpeed > difficultyLevel){
			currentSpeed -= returnSpeedToNormal;
			if (currentSpeed <= difficultyLevel){
				Debug.Log("End Boost");
				boosted = false;
				mycamera.booster =false;
				StopCoroutine("Booster");
			}
		}
	}
}
