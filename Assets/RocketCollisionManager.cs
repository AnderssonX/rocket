using UnityEngine;
using System.Collections;

public class RocketCollisionManager : MonoBehaviour
{
	public GameObject player;
	public PlayerMovement pm;
	private AudioSource lose;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		pm = player.GetComponent<PlayerMovement> ();
		lose = GameObject.Find ("LoseSound").GetComponent<AudioSource> ();
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Obstacle")) {
			if (!pm.crashed) {
				Debug.Log ("Crash!");
				pm.crash ();
			}
			lose.Play ();
		}
	}
}
