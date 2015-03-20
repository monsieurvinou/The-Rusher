using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveSurvive : MonoBehaviour {
	public float TimeToSurvive = 120f;
	public Text Chronometer;
	public bool IsPaused = false;
	
	float timeSurvived = 0f;
	
	// Update is called once per frame
	void Update () {
		if ( !IsPaused ) {
			timeSurvived += Time.deltaTime;
			if ( timeSurvived >= TimeToSurvive ) {
				timeSurvived = TimeToSurvive;
				ObjectiveEvent.Dispatch(true);
				Pause ();
			}
			
			UpdateChronometer();
		}
	}
	
	void UpdateChronometer() {
		float timeLeft = TimeToSurvive - timeSurvived;
		float seconds = timeLeft % 60;
		float minutes = timeLeft / 60;
		// Strings
		
		string secondString = System.Math.Truncate(seconds).ToString();
		string minuteString = System.Math.Truncate(minutes).ToString();
		if ( secondString.Length == 1 ) secondString = "0" + secondString;
		if ( minuteString.Length == 1 ) minuteString = "0" + minuteString;
		
		Chronometer.text = minuteString + ":" + secondString;
	}
	
	public void Resume() {
		IsPaused = false;
	}
	
	public void Pause() {
		IsPaused = true;
	}
}
