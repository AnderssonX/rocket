using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GoalControl : MonoBehaviour
{
	private PlayerMovement pm;
	public Collider player;
	private UIController uiController;
	AudioSource goal;
	private bool finished;
	private bool checking;
	public string nextScene;


	void Start ()
	{
		uiController = GameObject.Find ("Game Controller").GetComponent<UIController> ();
		goal = GameObject.Find ("GoalSound").GetComponent<AudioSource>();
		pm = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		pm.getVelocity ();
		player = GameObject.Find ("Player").GetComponent<BoxCollider> ();
	}

	void OnTriggerEnter(Collider other){

		if (other == player && !pm.isLanded() && !finished && !checking) {

			StartCoroutine (OnGoal ());

		}
	}

	void OnTriggerStay(Collider other){
		if (other == player  && !pm.isLanded() && !finished && !checking) {
			StartCoroutine (OnGoal ());
		}
	}
	IEnumerator OnGoal(){
		checking = true;
		yield return new WaitForSeconds (2);
			if (pm.getVelocity()) {
			finished = true;
			pm.hasLanded ();

			uiController.GetScore ();
			goal.Play ();
			StartCoroutine (NextLevel (nextScene));
		} else {
			Debug.Log ("not safe landing");
			checking = false;
		}
		yield return new WaitForSeconds (1);

}
	IEnumerator NextLevel(string scene){
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene (scene);
	}
}
