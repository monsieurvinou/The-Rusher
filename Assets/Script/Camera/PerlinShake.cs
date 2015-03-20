using UnityEngine;
using System.Collections;

public class PerlinShake : MonoBehaviour {
	public float decay = 0.02f;
	public float intensity = 0.1f;
	
	private float shake_decay;
	private float shake_intensity;
	
	
	void Update (){
		if (shake_intensity > 0){
			float originY = transform.position.y;
			Vector3 shakedPosition = transform.position + Random.insideUnitSphere * shake_intensity;
			shakedPosition.y = originY;
			transform.position = shakedPosition;
			shake_intensity -= shake_decay;
		}
	}
	
	public void ShakeCamera(){
		shake_intensity = intensity;
		shake_decay = decay;
	}
}
