using UnityEngine;
using System.Collections;

public class BottomColliderScript : MonoBehaviour {
	public AudioSource a;
		public GameObject player;
		public PlayerMovement pm;


		// Use this for initialization
		void Start ()
		{
			player = GameObject.FindGameObjectWithTag ("Player");
			pm = player.GetComponent<PlayerMovement> ();

		}


		void OnTriggerEnter (Collider other){
		Debug.Log ("Hi");

			a.Play ();

		}
	}
