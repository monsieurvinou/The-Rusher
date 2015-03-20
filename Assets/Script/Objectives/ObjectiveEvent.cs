using UnityEngine;
using System.Collections;

public class ObjectiveEvent {
	public delegate void ObjectiveAction(bool completed);
	public static event ObjectiveAction ObjectiveDispatch;
	
	public static void Dispatch(bool completed) {
		if ( ObjectiveDispatch != null ) ObjectiveDispatch(completed);
	}
}
