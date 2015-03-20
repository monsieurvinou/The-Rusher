using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjectiveDestroy : MonoBehaviour {
	public List<GameObject> ListTargets;

	bool hasSentEvent = false;
		
	void Update () {
		int index = ListTargets.FindIndex(
			delegate(GameObject obj) { 
				return obj == null; 
			}
		);
		if ( index >= 0 ) ListTargets.RemoveAt(index);
		
		if ( ListTargets.Count == 0 && !hasSentEvent ) {
			hasSentEvent = true;
			// Dispatch the event, the listeners will catch it
			ObjectiveEvent.Dispatch(true);
		}
	}
	
	
}
