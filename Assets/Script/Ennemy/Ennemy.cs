using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour {
	public Transform target;
	public float sightDistance = 30f;
	GameObject mainPart;
	NavMeshAgent agent;
	
	void Start() {
		mainPart = transform.Find("MainForm").gameObject;
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( target && mainPart.activeSelf ) {
			RaycastHit info = new RaycastHit();
			Vector3 direction = target.position - transform.position;
			if ( Physics.Raycast(transform.position, direction,out info, sightDistance ) ) {
				if( info.collider.tag == "Player" ) {
					// We see the player
					FindPath ();
				} else {
					agent.ResetPath();
				}
			}
			
		} else if ( !mainPart.activeSelf ) {
			GetComponent<NavMeshAgent>().enabled = false;
		}
	}
	
	void FindPath() {
		NavMeshPath path = new NavMeshPath();
		bool hasFoundPath = agent.CalculatePath(target.position, path);
		if ( hasFoundPath ) {
			if(path.status == NavMeshPathStatus.PathComplete)
			{
				agent.SetPath(path);
			}
			else if(path.status == NavMeshPathStatus.PathPartial )
			{
				agent.ResetPath();
			}
			else if(path.status == NavMeshPathStatus.PathInvalid )
			{
				agent.ResetPath();
			}
		} else {
			agent.ResetPath();
		}
	}
}





