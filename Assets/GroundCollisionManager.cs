using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GroundCollisionManager : MonoBehaviour
{
	private GameObject player;
	private bool restarting;
	PlayerMovement pm;
	AudioSource lose;
	AudioSource crash;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		pm = player.GetComponent<PlayerMovement> ();

		crash = GameObject.Find ("CrashSound").GetComponent<AudioSource> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("RocketCollider")) {
			crash.Play ();
			pm.flying = false;
			StartCoroutine (Restart ());	
			if (restarting == false) {
				StartCoroutine (Restart ());	
			}
			if (!crash.isPlaying) {
				crash.Play ();
			}
		}

	}

	IEnumerator Restart ()
	{
		restarting = true;
		Debug.Log ("Restarting");
		yield return new WaitForSeconds (4);
		pm.RestartPlayer ();
	}
}

			