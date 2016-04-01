using UnityEngine;
using System.Collections;

public class GroundCollision : MonoBehaviour {
	private PlayerMovement pm;
	private Collider player;
	private GameObject p;
	private bool restarting;
	private AudioSource crashSound;

	// Use this for initialization
	void Start () {
		p = GameObject.Find ("Player");
		crashSound = GameObject.Find ("CrashSound").GetComponent<AudioSource> ();
		pm = p.GetComponent<PlayerMovement> ();
		player = GameObject.FindGameObjectWithTag("RocketCollider").GetComponent<Collider> ();

	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		

	if(other == player){
			crashSound.Play ();

			if (!pm.isCrashed()) {
				pm.crash ();

			}


	}
}
}