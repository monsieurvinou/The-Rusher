using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	public float offsetX = 0f;
	public float offsetZ = 0f;

	public float cameraSpeedFollow = 10f;
	
	// Update is called once per frame
	void Update () {
		Vector3 positionTo = target.position;
		positionTo.x += offsetX;
		positionTo.z += offsetZ;
		positionTo.y = transform.position.y;

		transform.position = Vector3.Lerp (
			transform.position,
			positionTo,
			cameraSpeedFollow * Time.deltaTime
		);
	}
}
