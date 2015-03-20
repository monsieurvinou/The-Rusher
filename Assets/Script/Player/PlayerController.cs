using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	// Moving
	public float speed = 10f;
	public float turnSpeed = 2f;

	// Rushing
	public float rushingSpeed = 20f;
	public float rushingMeter = 100f;
	public float rushingMeterMax = 100f;
	public float rushingCostPerSecond = 75f;
	public float rushingRegenerationPerSecond = 33f;

	// Controls
	// CharacterController cc;
	Rigidbody rb;
	Vector2 joystick;
	bool isRushing = false;
	float currentSpeed = 0f;
	float timeBeforeStop = 1f;
	float timeInAir = 0f;
	float distToGround;
	
	// Use this for initialization
	void Start () {
		//cc = GetComponent<CharacterController> ();
		joystick = new Vector2 ();
		distToGround = GetComponent<Collider>().bounds.extents.y;
		rb = GetComponent<Rigidbody>();
	}
	
	bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		// Move();
		ManageRushingMeter();
		ShowDebugInfo();
		ManageUI();
	}
	
	void FixedUpdate() {
		Move ();
	}
	
	void ShowDebugInfo() {
		Text textDebug = GameObject.Find ("TextDebug").GetComponent<Text>();
		textDebug.text = "IsRushing : " + isRushing.ToString();
	}
	
	void Move() {
		// We get the joystick
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		if ( h != 0 || v != 0 ) {
			joystick.x = h;
			joystick.y = v;
		}
		
		// Get the rotation
		Quaternion toRotation = Quaternion.Euler(0f, ((Mathf.Atan2 (joystick.y, joystick.x) * Mathf.Rad2Deg)), 0f);
		transform.rotation = Quaternion.Lerp (
			transform.rotation,
			toRotation,
			turnSpeed * Time.deltaTime
		);
		Vector3 moveDirection = transform.TransformDirection (Vector3.right);
		if ( IsGrounded() ) {
			timeInAir = 1f;
			if (isRushing)
				currentSpeed = rushingSpeed;
			else
				currentSpeed = speed;
		} else {
			timeInAir += Time.deltaTime;
			if ( currentSpeed > speed ) currentSpeed = Mathf.Lerp(currentSpeed, speed, timeBeforeStop * Time.deltaTime); // speedDecreasePerSecond / Time.deltaTime;
			else currentSpeed = Mathf.Lerp(currentSpeed, 0f, timeBeforeStop * Time.deltaTime); // speedDecreasePerSecond / Time.deltaTime;
		}
		
		moveDirection *= currentSpeed * Time.deltaTime;
		
		// moveDirection.y += (Physics.gravity.y * Mathf.Pow(timeInAir,2)) * Time.deltaTime * 0.3f ;
		
		// rb.velocity = moveDirection;
		// rb.AddForce(Physics.gravity); // reapply gravity
		rb.MovePosition( transform.position + moveDirection);
		
		
		//cc.Move (moveDirection);
	}
	
	void ManageRushingMeter() {
		if (Input.GetButton ("Submit")) {
			if (!isRushing && rushingMeter < rushingMeterMax) {
				// is recharging
			} else if (isRushing && rushingMeter <= 0) {
				rushingMeter = 0f;
				isRushing = false;
			} else if (!isRushing) {
				isRushing = true;
			} else {
				// Continue rushing!
				rushingMeter -= rushingCostPerSecond * Time.deltaTime;
			}
		} else if ( isRushing ) {
			isRushing = false;
		}
		
		if ( !isRushing && rushingMeter < 100 ) {
			rushingMeter += rushingRegenerationPerSecond * Time.deltaTime;
			if (rushingMeter > 100f) rushingMeter = 100f;
		}
	}
	
	public bool GetIsRushing() {
		return isRushing;
	}
	
	void ManageUI() {
		RectTransform meterBackground = GameObject.Find ("Meter_Background").GetComponent<RectTransform>();
		RectTransform meterCurrent = GameObject.Find ("Meter_Current").GetComponent<RectTransform>();
		float rightOffset = (meterBackground.rect.width * (rushingMeter/rushingMeterMax)) - meterBackground.rect.width;
		meterCurrent.offsetMax = new Vector2( rightOffset, meterCurrent.offsetMax.y );
	}
}
