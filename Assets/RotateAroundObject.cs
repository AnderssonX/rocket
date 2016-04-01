using UnityEngine;
using System.Collections;

public class RotateAroundObject : MonoBehaviour
{
	Vector3 rotationMask = new Vector3 (0, 1, 0);
	public float rotationSpeed = 25.0f;
	public Transform rotateAround;
	public bool rotateAroundSelf;

	// Use this for initialization
	void Start ()
	{
		if (GameObject.Find ("StartPlate")) {
			rotateAround = GameObject.Find ("StartPlate").GetComponent<Transform> ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{

		transform.RotateAround (rotateAround.transform.position, rotationMask, -rotationSpeed * Time.deltaTime);
		if (rotateAroundSelf) {
			transform.Rotate (new Vector3 (rotationMask.x * rotationSpeed * 1 * Time.deltaTime, 
				rotationMask.y * rotationSpeed * 3 * Time.deltaTime, 
				rotationMask.z * rotationSpeed * 6 * Time.deltaTime));
		}
	}
}
