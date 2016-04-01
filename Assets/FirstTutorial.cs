using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class FirstTutorial : MonoBehaviour {

	private bool faded;
	private bool atStepOne = true;
	private bool stepOneOver = false;
	private bool atStepTwo = false;
	private bool stepTwoOver = false;
	private bool stepThreeOver = false;
	private bool running = false;
	private bool tutorialFinished;
	private StartPlate startPlate;

	public Text firstText;
	public Text secondText;
	public Text thirdText;
	public Text fourthText;
	public Text fifthText;
	public Text sixthText;
	public Text seventhText;

	private GameObject player;

	// Use this for initialization
	void Start () {
		startPlate = GameObject.Find ("StartPlate").GetComponent<StartPlate> ();
		player = GameObject.Find ("Player");
		secondText.CrossFadeAlpha (0.0f, 0.1f, false);
		thirdText.CrossFadeAlpha (0.0f, 0.1f, false);
		fourthText.CrossFadeAlpha (0.0f, 0.1f, false);
		fifthText.CrossFadeAlpha (0.0f, 0.1f, false);
		sixthText.CrossFadeAlpha (0.0f, 0.1f, false);
		seventhText.CrossFadeAlpha (0.0f, 0.1f, false);
	
	}
	void Update(){
		if (Input.GetKeyDown ("space")) {
			if (atStepOne && !stepOneOver) {
				StartCoroutine (Fade (firstText, secondText));
				stepOneOver = true;
			}
		}
		if (Input.GetKey("space") && atStepTwo && player.GetComponent<Transform>().position.y >5 && !running && !stepTwoOver) {
			Debug.Log ("step two" + stepTwoOver);
				StartCoroutine (Fade (thirdText, fourthText));
				stepTwoOver = true;
			}
		if(Input.GetKey("space") && stepTwoOver && !running && !stepThreeOver && player.GetComponent<Transform>().position.y < 6){
			StartCoroutine (Fade (fifthText, sixthText));
			tutorialFinished = true;
			stepThreeOver = true;
		}
		if (tutorialFinished && startPlate.playerGrounded &&!running) {
			StartCoroutine (Fade (sixthText, seventhText));
		}
	
}
	IEnumerator Fade (Text fadeOut, Text fadeIn)
	{
		running = true;
		fadeOut.CrossFadeAlpha (0.0f, 1.0f, false);
		yield return new WaitForSeconds (1);
		fadeIn.CrossFadeAlpha (1.0f, 1.0f, false);
		if (atStepOne) {
			atStepOne = false;
			yield return new WaitForSeconds (1);
			StartCoroutine (Fade (secondText, thirdText));
			atStepTwo = true;
		}
		if (fadeIn == fourthText) {
			yield return new WaitForSeconds (2);
			StartCoroutine (Fade (fourthText, fifthText));

		}
		if (fadeIn == seventhText) {
			yield return new WaitForSeconds (5);
			float fadeTime = GameObject.Find ("Game Controller").GetComponent<Fading> ().BeginFade (1);
			yield return new WaitForSeconds (fadeTime);
			SceneManager.LoadScene ("StartScene");
		}
		running = false;

	}
}
