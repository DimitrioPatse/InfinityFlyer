using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeFly : MonoBehaviour {

	float speed = 1;
	public float turnVelocity;


	[SerializeField] float maxHeight, minHeight, maxSides, pitchStep, yawStep, swypeTime, pitchDegr, yawDegr, rollDegr;
    [SerializeField] float stopMultiplier = 1f;

    Vector3 moveVector;
	Quaternion rotation;

	[HideInInspector] public float metersRun;
	float difficultyLevel = 1;
	float maxDifficulty = 8;
	float metersToNextLvl = 2;

	float currentSpeed = 1;
	bool boosted = false;

	float newx = 0;
	float newy = 0;
	float newz = 0;

	float rotX = 0;
	float rotY = 0;
	float rotZ = 0;

	float startTime;
	public float timer;
	float refQ = 10;

	bool holdMove = false;
    bool holdR;
    bool holdL;
    bool holdU;
    bool holdD;

	SoundFx soundfx;
	CameraFollow mycamera;
    
    void Awake()
    {
        SwipeDetector.OnSwipe += FlyControls;
    }

    void Start ()
    {
		startTime = swypeTime / 3 ;
		rotation = transform.rotation;
		rotX = transform.eulerAngles.z;
		rotY = transform.eulerAngles.y;
		rotX = transform.eulerAngles.z;
		soundfx = GetComponent<SoundFx>();
		mycamera = Camera.main.gameObject.GetComponent<CameraFollow>();
	}
	
	void Update ()
    {
		metersRun = transform.position.z / 10;

		moveVector = new Vector3(newx,newy,newz);

		transform.position = new Vector3 (Mathf.Clamp(transform.position.x, -maxSides, maxSides),Mathf.Clamp(transform.position.y, minHeight, maxHeight),newz);
		transform.position = Vector3.Lerp(transform.position, moveVector, turnVelocity * Time.deltaTime);
		newz += currentSpeed;

		rotation = Quaternion.Euler(rotX, rotY, rotZ);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, currentSpeed * Time.deltaTime);

		if (timer > 0)
		timer -= Time.deltaTime;

		if (timer <= 0)
        {
			rotX = 0;
			rotY = 0;
			rotZ = 0;
			timer = 0;
		}

		FlyControls();
		LevelUp();
	}
    /// <summary>
    /// Mobile Swipe Controls
    /// </summary>
    /// <param name="data"></param>
    void FlyControls(SwipeData data)
    {
        if (data.Direction == SwipeDirection.Up) { MoveUp(); }
        else if (data.Direction == SwipeDirection.Down) { MoveDown(); }
        else if (data.Direction == SwipeDirection.Left) { MoveLeft(); }
        else if (data.Direction == SwipeDirection.Right) { MoveRight(); }
        else if (data.Direction == SwipeDirection.UpHold) { HoldUp(); }
        else if (data.Direction == SwipeDirection.Reset && holdU) { ReturnHoldUp(); }
        else if (data.Direction == SwipeDirection.DownHold) { HoldDown(); }
        else if (data.Direction == SwipeDirection.Reset && holdD) { ReturnHoldDown(); }
        else if (data.Direction == SwipeDirection.LeftHold) { HoldLeft(); }
        else if (data.Direction == SwipeDirection.Reset && holdL) { ReturnHoldLeft(); }
        else if (data.Direction == SwipeDirection.RightHold) { HoldRight(); }
        else if (data.Direction == SwipeDirection.Reset && holdR) { ReturnHoldRight(); }
    }

    /// <summary>
    /// Keyboard Controls
    /// </summary>
    void FlyControls()
    {
		if (Input.GetKeyDown(KeyCode.W)){ MoveUp();}
		else if (Input.GetKeyDown(KeyCode.S)){ MoveDown();}
		else if (Input.GetKeyDown(KeyCode.A)){ MoveLeft();}
		else if (Input.GetKeyDown(KeyCode.D)){ MoveRight();}
		else if (Input.GetKeyDown(KeyCode.Z) && !holdMove){ HoldUp();}
		else if (Input.GetKeyUp(KeyCode.Z) && holdMove){ ReturnHoldUp();}
		else if (Input.GetKeyDown(KeyCode.X) && !holdMove){ HoldDown();}
		else if (Input.GetKeyUp(KeyCode.X) && holdMove){ ReturnHoldDown();}
		else if (Input.GetKeyDown(KeyCode.C) && !holdMove){ HoldLeft();}
		else if (Input.GetKeyUp(KeyCode.C) && holdMove){ ReturnHoldLeft();}
		else if (Input.GetKeyDown(KeyCode.V) && !holdMove){ HoldRight();}
		else if (Input.GetKeyUp(KeyCode.V) && holdMove){ ReturnHoldRight();}
	}


	void MoveUp()
    {
		if (newy < maxHeight){
			newy += pitchStep;
			rotX = -pitchDegr;
			timer = startTime;
            if (newy > maxHeight)
            {
                newy = maxHeight;
            }
		}
	}
	void MoveDown()
    {
		if (newy > minHeight){
			newy -= pitchStep;
			rotX = pitchDegr;
			timer = startTime;
            if (newy < minHeight)
            {
                newy = minHeight;
            }
		}
	}

	void MoveRight()
    {
		if (newx < maxSides)
			newx += maxSides;
			timer = startTime;
			rotY = yawDegr;
			rotZ = -rollDegr;		
	}
	void MoveLeft()
    {		
		if (newx > -maxSides)
			newx -= maxSides;
			timer = startTime;
			rotY = -yawDegr;
			rotZ = rollDegr;
	}

	void HoldUp()
    {
		newy += pitchStep / 2;
		holdMove = true;
        holdU = true;
	}

	void ReturnHoldUp()
    {
		newy -= pitchStep / 2;
		holdMove = false;
        holdU = false;
    }

    void HoldDown()
    {
		newy -= pitchStep / 2;
		holdMove = true;
        holdD = true;
	}
	void ReturnHoldDown()
    {
		newy += pitchStep / 2;
		holdMove = false;
        holdD = false;
	}

	void HoldLeft()
    {
		newx -= yawStep / 2;
		holdMove = true;
        holdL = true;
	}

	void ReturnHoldLeft()
    {
		newx += yawStep / 2;
		holdMove = false;
		rotZ = 0;
        holdL = false;
	}

	void HoldRight()
    {
		newx += yawStep / 2;
		holdMove = true;
        holdR = true;
	}

	void ReturnHoldRight()
    {
		newx -= yawStep / 2;
		holdMove = false;
		rotZ = 0;
        holdR = false;
	}

	void LevelUp()
    {
		if(difficultyLevel == maxDifficulty)
        {
			return;
		}
        else if (metersRun > metersToNextLvl && !boosted)
        {
			metersToNextLvl *= 2;
			difficultyLevel ++;
			currentSpeed = difficultyLevel;
		}
	}

	public void SetBooster(float time)
    {
		boosted = true;
		currentSpeed = currentSpeed + 2f;
		StartCoroutine("Booster", time);
		soundfx.PlaySpeed();
	}

	IEnumerator Booster(float boostTime)
    {
		while (boostTime > 0 && boosted)
        {
			mycamera.booster = true;
			boostTime -= Time.deltaTime;
			yield return null;
		}
		while (boostTime <= 0 && currentSpeed > difficultyLevel)
        {
			currentSpeed -= 0.1f;
			if (currentSpeed <= difficultyLevel){
				boosted = false;
				mycamera.booster =false;
				StopCoroutine("Booster");
			}
		}
	}
    public void StartMovementStop()
    {
        StartCoroutine("Stoper");
    }
    IEnumerator Stoper()
    {
        while (currentSpeed > 0)
        {
            currentSpeed -= 0.1f * stopMultiplier;
            yield return null;
        }
    }
}
