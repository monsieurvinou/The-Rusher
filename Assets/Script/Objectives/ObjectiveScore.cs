using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveScore : MonoBehaviour {
	public int ScoreToHave = 5000;
	public Text ScoreText;
	int currentScore = 0;
	
	public void AddPoints(int points) {
		currentScore += points;
		if ( currentScore >= ScoreToHave ) {
			ObjectiveEvent.Dispatch(true);
		}
		string pointString = " point";
		if ( currentScore > 1 ) pointString += "s";
		ScoreText.text = currentScore.ToString() + pointString;
	}
}
