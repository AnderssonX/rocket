using UnityEngine;
using System.Collections;

public class StartPlate : MonoBehaviour {
	public bool playerGrounded = true;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		
	}
	void OnTriggerStay (Collider other){
		if (other == player.GetComponent<Collider> ()) {
			playerGrounded = true;
		} 
	}
	void OnTriggerExit(Collider other){

		if (other == player.GetComponent<Collider>() || other.CompareTag("RocketCollider")) {
			playerGrounded = false;
		}
	}


}

