using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour
{
	private GameObject player;
	private PlayerMovement pm;
	private AudioSource lose;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		pm = player.GetComponent<PlayerMovement> ();
		lose = GameObject.Find ("LoseSound").GetComponent<AudioSource> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other == player.GetComponent<Collider> ()) {
			pm.crash ();
			lose.Play ();
		}
	}
}
