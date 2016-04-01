using UnityEngine;
using System.Collections;

public class DebugCamera : MonoBehaviour {
	public GameObject player;
	public Vector3 target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(target);
		transform.Translate(Vector3.right * Time.deltaTime);
	}
}
