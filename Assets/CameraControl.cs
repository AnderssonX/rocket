using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	public GameObject m_Player;
	PlayerMovement pm;
	private Transform pt;
	public float speed;
	public float lastHeight;
	public float currentHeight;
	public float playerVelocity;
	private float originalZoom;
	private float originalSpeed;
	Vector3 originalPosition;
	Vector3 targetDestination;
	public float desiredZoom;

	void Start ()
	{
		originalSpeed = speed;
		pt = m_Player.GetComponent<Transform> ();
		originalZoom = transform.position.z;
		pm = m_Player.GetComponent<PlayerMovement> ();
		transform.position = new Vector3 (pt.transform.position.x, transform.position.y, transform.position.z);
		originalPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}

	void Update ()
	{
		playerVelocity = Mathf.RoundToInt(m_Player.GetComponent<Rigidbody> ().velocity.magnitude);
		//Debug.Log (playerVelocity);


		if (pm.isLanded ()) {
			speed = 5;
			desiredZoom = originalZoom;

			// Tilt camera up when player's on goal.
			//transform.rotation = Quaternion.Euler (359f, transform.rotation.y, transform.rotation.z);
		} else {
			transform.position = new Vector3 (pt.transform.position.x, pt.transform.position.y + 2f, transform.position.z);
			currentHeight = pt.transform.position.y;

			if (pm.isCrashed ()) {
			
				speed = 40f;
				desiredZoom = originalZoom + (pt.position.z);

			} else {
			
				// this is old -> if (m_Player.GetComponent<Rigidbody> ().velocity.y > 2 || m_Player.GetComponent<Rigidbody> ().velocity.y < -1) {

				// Zoom in and out depending on player height
				if (pm.getHeight () > 5 && pm.getHeight () < 45) {
					desiredZoom = originalZoom - 5;
				} else if (pm.getHeight () > 45) {
					desiredZoom = originalZoom - 8;
				}
			 
//			 if(playerVelocity > 8){	
//				if (desiredZoom == originalZoom) {
//					desiredZoom = transform.position.z - 5;
//				} 
			//		}
else {
					desiredZoom = originalZoom;	
				}
			}
		}
		targetDestination = new Vector3 (transform.position.x, transform.position.y, desiredZoom);
		MoveToDestination (targetDestination);

		lastHeight = pt.transform.position.y;
	}

	private void MoveToDestination (Vector3 destination)
	{
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetDestination, step);
	}

	public void ResetCamera ()
	{
		speed = originalSpeed;
		transform.position = originalPosition;
	}

}
