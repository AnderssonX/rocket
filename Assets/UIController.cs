using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	GameObject player;
	private PlayerMovement pm;
	private ParticleSystem fireworks;

	// Score variables
	private AudioSource scoreSound;
	int score = 0;
	public Text scoreText;
	private Text finalScore;
	public int finalScoreCount;
	private bool fresh = true;
	private int startHeight;

	// Timer variables
	private bool updateOn = true;
	private Text timer;
	public int time;
	public int counter;
	private AudioSource lowTimeSound;


	void Awake ()
	{

		timer = GameObject.Find ("Timer").GetComponent <Text> ();
		finalScore = GameObject.Find ("Final Score").GetComponent<Text> ();
		finalScore.enabled = false;

		scoreSound = GameObject.Find ("ScoreSound").GetComponent<AudioSource> ();
		player = GameObject.Find ("Player");
		fresh = false;
		pm = player.GetComponent<PlayerMovement> ();
		startHeight = Mathf.RoundToInt (player.transform.position.y);
		counter = time;
		lowTimeSound = GameObject.Find ("LowTimeSound").GetComponent<AudioSource> ();	
		fireworks = GameObject.Find ("Fireworks").GetComponent<ParticleSystem> ();
		fireworks.Stop ();
	}

	void Start ()
	{
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();
		timer.text = time.ToString () + " s";
		StartCoroutine (Timer ());
	}

	void Update ()
	{
		if (updateOn) {
			scoreText.text = (Mathf.RoundToInt (player.transform.position.y) - startHeight).ToString () + " m";
		}
	}

	// Timer methods
	IEnumerator Timer ()
	{
		if (!pm.isLanded () && fresh == false) {
			if (counter > 0) {
				if (counter > 0 && counter < 10) {
					timer.text = "";
					yield return new WaitForSeconds (0.1f);
					lowTimeSound.Play ();
					timer.text = counter.ToString () + " s";
					yield return new WaitForSeconds (0.1f);
					timer.text = "";
					yield return new WaitForSeconds (0.1f);
					timer.text = counter.ToString () + " s";
					yield return new WaitForSeconds (0.1f);
					timer.text = "";
					yield return new WaitForSeconds (0.1f);
					timer.text = counter.ToString () + " s";
				}
				timer.text = counter.ToString () + " s";
				counter--;
				yield return new WaitForSeconds (1);
				StartCoroutine (Timer ());
			} else {
				pm.OutOfTime ();
			}
		} 
	}

	public void StartTimer ()
	{
		StartCoroutine ("Timer");
	}

	public void Restart ()
	{
		StopCoroutine ("Timer");
		fresh = true;
		counter = time;
		score = 0;
		StartCoroutine ("Timer");
		scoreText.text = " m";
		fresh = false;
	}

	IEnumerator FinalScore ()
	{
		fireworks.Play ();
		finalScoreCount = counter * score;

		finalScore.enabled = true;
		finalScore.text = "YOUR SCORE";
	
		for (int i = 0; i < finalScoreCount; i = i + 33) {

			finalScore.text = "YOUR SCORE " + i;
			scoreSound.Play ();
			yield return new WaitForSeconds (0.01f - i * 0.1f);
		}
		for (int i = 0; i < 5; i++) {
			finalScore.enabled = false;
			yield return new WaitForSeconds (0.25f);
			finalScore.enabled = true;
			yield return new WaitForSeconds (0.5f);
		}
	}

	public void GetScore ()
	{
		score = (Mathf.RoundToInt (player.transform.position.y) - startHeight);
		updateOn = false;
		StartCoroutine (FinalScore ());
	}
}