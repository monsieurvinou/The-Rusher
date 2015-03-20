using UnityEngine;
using System.Collections;

public class DestructableEvent {
	public delegate void DestructableAction(GameObject destructed);
	public static event DestructableAction OnDestruction;
	
	public static void Dispatch(GameObject destructed) {
		if ( OnDestruction != null ) OnDestruction(destructed);
	}
}
