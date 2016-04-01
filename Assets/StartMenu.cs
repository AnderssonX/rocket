using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour
{
	private Text startPointer;
	private Text tutorialPointer;
	private Text start;
	private Text tutorial;
	private AudioSource sound;
	private AudioSource confirm;
	public string startScene;
	public string tutorialScene;

	// Use this for initialization
	void Start ()
	{
		
		startPointer = GameObject.Find ("StartText").GetComponent<Text> ();
		tutorialPointer = GameObject.Find ("TutorialText").GetComponent<Text> ();
		start = GameObject.Find ("Start game").GetComponent<Text> ();
		tutorial = GameObject.Find ("How to play").GetComponent<Text> ();
		startPointer.enabled = true;
		tutorialPointer.enabled = false;
		sound = GameObject.Find ("MenuSound").GetComponent<AudioSource> ();
		confirm = GameObject.Find ("ConfirmSound").GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (Input.GetKeyDown ("down")) {
			startPointer.enabled = false;
			tutorialPointer.enabled = true;
			sound.Play ();

		}
		if (Input.GetKeyDown ("up")) {
			startPointer.enabled = true;
			tutorialPointer.enabled = false;
			sound.Play ();

		}
		if (Input.GetKeyDown ("e")) {
			if (startPointer.enabled == true) {
				StartCoroutine (Go (start));
			}
			if (tutorialPointer.enabled) {
				StartCoroutine (Go (tutorial));
			}
		}


	}

	IEnumerator Go (Text text)
	{
		confirm.Play ();
		for (int i = 0; i < 10; i++) {
			if (!text.enabled) {
				text.enabled = true;
			} else {
				text.enabled = false;
			}
				yield return new WaitForSeconds (.1f);

			
		}
		text.enabled = true;
		yield return new WaitForSeconds (.5f);

		if (text == start) {
			float fadeTime = GameObject.Find ("Game Controller").GetComponent<Fading> ().BeginFade (1);
			yield return new WaitForSeconds (fadeTime);
			SceneManager.LoadScene ("BetaLevel");
		
		}
		if (text == tutorial) {
			SceneManager.LoadScene("TutorialScene");
		}
	}
}
