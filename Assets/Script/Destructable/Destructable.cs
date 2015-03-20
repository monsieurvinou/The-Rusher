using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destructable : MonoBehaviour {
	public float forceExplosion = 20f;
	public float radiusExplosion = 10f;
	public float offsetY = 0f;
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			if ( other.gameObject.GetComponent<PlayerController>().GetIsRushing() ) {
				GetHit();
			}
		}
	}
	
	protected void GetHit() {
		Collider collider = GetComponent<Collider>();
		if ( collider ) collider.enabled = false;
		Transform mainForm = transform.Find("MainForm");
		Transform particles = transform.Find ("Particles");
		
		mainForm.gameObject.SetActive(false);
		particles.gameObject.SetActive(true);
		
		Explode(transform.position);
		Invoke("DestroyThis", 4);
	}
	
	public void DestroyThis() {
		Destroy(gameObject);
	}
	
	void Explode(Vector3 explosionPos) {
		// Dispatch Event for the other classes
		DestructableEvent.Dispatch(gameObject);
		// Shake Camera
		Camera.main.GetComponent<PerlinShake>().ShakeCamera();
	
		// Explose
		Collider[] colliders = Physics.OverlapSphere (explosionPos, radiusExplosion);
		
		foreach (Collider hit in colliders) {
			//Here the colliders with tag PhysAffected will be affected by the force
			if (hit && hit.GetComponent<Rigidbody>() && (hit.GetComponent<Collider>().tag=="Particle" || hit.GetComponent<Collider>().tag=="AffectedByExplosion")) {
				hit.GetComponent<Rigidbody>().AddExplosionForce(forceExplosion, explosionPos, radiusExplosion, offsetY);
			}
				
		}
	}
}
