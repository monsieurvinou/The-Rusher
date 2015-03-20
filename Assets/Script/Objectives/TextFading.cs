using UnityEngine;
using System.Collections;

public class TextFading : MonoBehaviour {
	public CanvasGroup canvComp;
	public float speedFade = 0.5f;
	
	bool isAppearing = true;
	bool isDisappearing = false;
	
	void Start() {
		if ( canvComp == null ) canvComp = GetComponent<CanvasGroup>();
		if ( canvComp == null ) {
			isAppearing = false;
			isDisappearing = false;
		}
	}
	
	void Update () {
		// UI Fade
		if ( isAppearing ) {
			canvComp.alpha += Time.deltaTime * speedFade;
			if ( canvComp.alpha >= 1f ) {
				isAppearing = false;
				Invoke("StartDisappearing", 5f);
			}
		} else if ( isDisappearing ) {
			canvComp.alpha -= Time.deltaTime * speedFade;
			if ( canvComp.alpha <= 0f ) isDisappearing = false;
		}
	}
	
	void StartDisappearing() {
		isDisappearing = true;
	}


}
