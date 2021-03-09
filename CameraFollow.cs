using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour {

	[SerializeField] Transform target;
	[SerializeField] Vector3 distanceFromTarget;
	[Range(0,10)]
	[SerializeField] float xSmoothness, ySmoothness, zSmoothness;
	[Space]
	[SerializeField] float boostFxVelocity;

	float refXVelocity = 0.0f;
	float refYVelocity = 0.0f;
	float refZVelocity = 0.0f;

	public bool booster = false;

	Camera myCam;


	void Awake(){
		Application.targetFrameRate = 30;
		myCam = Camera.main;
	}

	void LateUpdate () {
		float xAngle= Mathf.SmoothDampAngle(transform.eulerAngles.x, target.eulerAngles.x, ref refXVelocity, xSmoothness/10);
		float yAngle= Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref refYVelocity, ySmoothness/10);
		float zAngle= Mathf.SmoothDampAngle(transform.eulerAngles.z, target.eulerAngles.z, ref refZVelocity, zSmoothness/10);

		Vector3 position = target.position;
		position += Quaternion.Euler(xAngle, yAngle, zAngle) * distanceFromTarget;
		transform.position = position;
		transform.LookAt(target);

		BoostFx();
	}

	void BoostFx(){
		if(booster){
			myCam.fieldOfView = Mathf.Lerp(myCam.fieldOfView, 80, Time.deltaTime * boostFxVelocity);
		}
		else {
			myCam.fieldOfView = Mathf.Lerp(myCam.fieldOfView, 60, Time.deltaTime * boostFxVelocity);
		} 
	}
}