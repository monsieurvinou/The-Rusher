using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChainStreakManager : MonoBehaviour {
	Text textfield;
	RectTransform bar;
	float backgroundMaxWidth = 200f;
	CanvasGroup cg;
	
	// fades
	bool isAppearing = false;
	bool isDisappearing = false;
	float fadeInSpeed = 5f;
	float fadeOutSpeed = 1.2f;
	
	
	void Start() {
		backgroundMaxWidth = this.GetComponent<RectTransform>().rect.width;
		cg = GetComponent<CanvasGroup>();
		textfield = transform.FindChild("ChainStreakText").GetComponent<Text>();
		bar = transform.FindChild("ForegroundChainStreak").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if ( ScoreGiver.TimeLeft > 0 ) {
			ScoreGiver.TimeLeft -= Time.deltaTime;
			if ( ScoreGiver.TimeLeft <= 0 ) {
				// End of chain
				ScoreGiver.Chain = 0;
				ScoreGiver.TimeLeft = 0f;
			}
		}
		
		if ( ScoreGiver.TimeLeft > 0 ) {
			float rightOffset = (backgroundMaxWidth * (ScoreGiver.TimeLeft/ScoreGiver.ChainDuration)) - backgroundMaxWidth;
			bar.offsetMax = new Vector2( rightOffset, bar.offsetMax.y );
			
			textfield.text = "x" + (ScoreGiver.Chain + 1);
		}
		
		// FADES
		if ( ScoreGiver.Chain > 0 && (cg.alpha == 0 || isDisappearing) ) {
			isDisappearing = false;
			isAppearing = true;
		}
		
		if ( ScoreGiver.Chain == 0 && (cg.alpha == 1 || isAppearing ) ) {
			isDisappearing = true;
			isAppearing = false;
		}
		
		if ( isAppearing ) {
			cg.alpha += Time.deltaTime * fadeInSpeed;
			if ( cg.alpha >= 1 ) {
				cg.alpha = 1;
				isAppearing = false;
				isDisappearing = false;
			}
		}
		if ( isDisappearing ) {
			cg.alpha -= Time.deltaTime * fadeOutSpeed;
			if ( cg.alpha <= 0 ) {
				cg.alpha = 0;
				isDisappearing = false;
				isAppearing = false;
			}
		}
		
	}
}
