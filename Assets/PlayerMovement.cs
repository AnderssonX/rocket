using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public GameObject playerRender;
	public ParticleSystem burst;
	public AudioSource m_Motor;
	public float thrust = 10f;
	public float lockPos = 0f;
	public float turnForce = 1.5f;
	public float maxSpeed;
	public float originalMass;
	public Rigidbody rb;
	 
	public bool zLocked = true;
	public bool flying = false;
	public bool searchingForPlayer;
	public bool xLocked;
	public bool crashed;

	private CameraControl cameraControl;
	private Quaternion rot;
	private StartPlate startPlate;
	private UIController uiController;
	private GameObject gameController;
	private AudioSource respawnSound;
	private Vector3 pos;
	private Vector3 fixedPos;
	private float height;
	private float originalThrust;
	private bool timeIsUp;
	private bool updateOn = true;
	private bool landed = false;

	void Awake(){
		pos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}


	void Start ()
	{
		m_Motor = GameObject.Find ("RocketSound").GetComponent<AudioSource> ();
		gameController = GameObject.Find ("Game Controller");
		uiController = gameController.GetComponent<UIController> ();
		startPlate = GameObject.Find ("StartPlate").GetComponent<StartPlate> ();
		rb = GetComponent<Rigidbody> (); 
	
		originalThrust = thrust;
		var em = burst.emission;
		em.enabled = false;

		rot = transform.rotation;
		originalMass = rb.mass;

		respawnSound = GameObject.Find ("respawnSound").GetComponent<AudioSource> ();
		cameraControl = GameObject.Find ("Main Camera").GetComponent<CameraControl> ();

	}

	void Update ()
	{
		
		if (updateOn) {

			height = (Mathf.RoundToInt (transform.position.y) - pos.y);
			Debug.Log (height + "ada");
			if (timeIsUp) {
				var em = burst.emission;
				em.enabled = false;
			}
			if (!timeIsUp) {

				if (Input.GetKeyDown ("space") && !timeIsUp) {

					if (!flying) {
						flying = true;
						StartCoroutine (RocketFlying (rb));
					}
					m_Motor.Play ();


				}
				if (!Input.GetKey ("space")) {
					flying = false;
					thrust = originalThrust;


				}

				if (Input.GetKeyUp ("space")) {
					m_Motor.Stop ();
				}
		 
				if (Input.GetKey ("left") && !startPlate.playerGrounded && !xLocked) {

					transform.Rotate (0, 0 * Time.deltaTime, turnForce);
				}
						
				
				if (Input.GetKey ("right") && !startPlate.playerGrounded && !xLocked) {
					transform.Rotate (0, 0 * Time.deltaTime, -turnForce);
				}
				if (Input.GetKey ("up")) {
					transform.Rotate (1, 0 * Time.deltaTime, 0);
				}
				if (Input.GetKey ("down")) {
					transform.Rotate (-1, 0 * Time.deltaTime, 0);
				}
				if (lockPos == 0) {
					transform.rotation = Quaternion.Euler (lockPos, lockPos, transform.rotation.eulerAngles.z);
			
					transform.position = new Vector3 (transform.position.x, transform.position.y, 0f);


				}
			}
		}
	}



	IEnumerator RocketFlying (Rigidbody go)
	{
		
		while (flying && !timeIsUp) {
			thrust = thrust + 0.1f;
			var em = burst.emission;
			em.enabled = true;
		
	
		
			if (rb.velocity.magnitude < maxSpeed) {
				rb.AddForce(transform.up * thrust); 
			}
//	

			yield return null;
		}
	
		flying = false;
		var em_ = burst.emission;
		em_.enabled = false;
	}

	public void crash ()
	{
		lockPos = 100f;	

		crashed = true;
	
		updateOn = false;
		flying = false;
		m_Motor.Stop ();
		rb.mass = rb.mass + 2000f;

		rb.velocity = new Vector3 (0, -3, -1f) * 12;

		StartCoroutine (LookForPlayer ());

	}

	public bool isCrashed ()
	{

		return crashed;
	}

	public bool isLanded ()
	{
		return landed;
	}

	public void hasLanded ()
	{
		landed = true;

		LockPosition ();
		inGoal ();
	}

	public void RestartPlayer ()
	{
		rb.angularVelocity = Vector3.zero;
		rb.velocity = Vector3.zero;
		transform.position = pos;
		transform.rotation = rot;
		lockPos = 0f;
		crashed = false;
		updateOn = true;
		flying = false;
		timeIsUp = false;
		uiController.Restart ();
		rb.mass = originalMass;

		StartCoroutine (FlashPlayer ());


	}

	IEnumerator FlashPlayer ()
	{
		bool flashing = true;
		respawnSound.Play ();
		cameraControl.ResetCamera ();
		while (flashing) {
			for (int i = 0; i < 10; i++) {
		
				if (playerRender.GetComponent<Renderer> ().enabled == false)
					playerRender.GetComponent<Renderer> ().enabled = true;
				else {
					playerRender.GetComponent<Renderer> ().enabled = false;
				}
				yield return new WaitForSeconds (0.1f);
			}
			flashing = false;
		}
	}

	public void LockPosition ()
	{
		

		gameObject.GetComponent<Rigidbody> ().velocity.Set (0, 0, 0);
		fixedPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
// 		inGoal = true;

	}

	public void OutOfTime ()
	{
		
		lockPos = 100f;
		timeIsUp = true;
		var em = burst.emission;
		em.enabled = false;
		crashed = true;
		m_Motor.Stop ();
		gameObject.GetComponent<Rigidbody> ().velocity.Set (0, 0, 65);
		rb.AddForce (transform.forward * -15 * 10.5f);
		rb.AddForce (transform.up * -thrust * 25f);
		rb.mass = 125f;
	}


	public void inGoal ()
	{
		updateOn = false;
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		gameObject.GetComponent<Rigidbody> ().velocity.Set (0, 0, 0);
		transform.rotation = Quaternion.Euler (lockPos, lockPos, transform.rotation.eulerAngles.z);
		transform.position = fixedPos;
	}

	IEnumerator LookForPlayer ()
	{
		yield return new WaitForSeconds (5);
		crashed = false;
		RestartPlayer ();

	
	}

	public bool getVelocity ()
	{
		string v;
		v = rb.velocity.ToString ();
		if (v == "(0.0, 0.0, 0.0)") {
			return true;
		} else {

			return false;
		}
	}
	public int getHeight(){
		return Mathf.RoundToInt(height);

	}
}

