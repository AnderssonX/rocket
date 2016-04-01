using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{
	private GameObject player;
	PlayerMovement pm;
	private UIController uiController;
	AudioSource goal;
	private bool finished;
	private bool checking;
	public string nextScene;

	void Start ()
	{
		uiController = GameObject.Find ("Game Controller").GetComponent<UIController> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		pm = player.GetComponent<PlayerMovement> ();
		goal = GameObject.Find ("GoalSound").GetComponent<AudioSource> ();

	}

	void OnTriggerStay (Collider other)
	{
		if (other == player.GetComponent<Collider> ()) {

			if (!pm.isLanded () && !finished && !checking) {

				checking = true;
				StartCoroutine (OnGoal (player.GetComponent<Rigidbody> ()));
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other == player.GetComponent<Collider> () && !pm.isLanded () && !finished && !checking) {
			checking = true;
			StartCoroutine (OnGoal (player.GetComponent<Rigidbody> ()));
		}
	}

	IEnumerator OnGoal (Rigidbody rb)
	{
		yield return new WaitForSeconds (2);
	
		if (pm.getVelocity ()) {
			finished = true;
			Debug.Log ("safe landing! Yay!");
			pm.hasLanded ();
		
			uiController.GetScore ();
			goal.Play ();
			StartCoroutine (NextLevel (nextScene));
		} else {
			Debug.Log ("not safe landing");
		}
		checking = false;
	}

	IEnumerator NextLevel (string scene)
	{
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene (scene);
	}
}
