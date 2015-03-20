using UnityEngine;
using System.Collections;

public class ScoreGiver : MonoBehaviour {
	public int ScoreValue = 150;
	
	static int chain = 0;
	static float chainDuration = 3f;
	static float chainDurationBase = 3f;
	static float timeLeft = 0f;
	
	void Start() {
		DestructableEvent.OnDestruction += ScorePoint;
	}
	
	void ScorePoint(GameObject destructed ) {
		if ( destructed == gameObject ) {
			chain++;
			chainDuration = chainDurationBase + ( (chain*0.5f) );
			timeLeft = chainDuration;
			// Find the scoreManager
			ObjectiveScore scoreManager = GameObject.FindObjectOfType<ObjectiveScore>();
			if ( scoreManager ) {
				scoreManager.AddPoints(ScoreValue * chain);
			}
			DestructableEvent.OnDestruction -= ScorePoint;
			Debug.Log (chainDuration);
		}
	}
	
	public static float TimeLeft {
		get {return ScoreGiver.timeLeft;}
		set {timeLeft = value;}
	}
	public static int Chain {
		get {return ScoreGiver.chain;}
		set {chain = value;}
	}
	public static float ChainDuration {
		get {return ScoreGiver.chainDuration;}
	}
	
}
