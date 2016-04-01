using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private Text timer;
	public int time;
	public int counter;

	// Use this for initialization
	void Start () {
		timer = GameObject.Find ("Timer").GetComponent <Text> ();
		timer.text = time.ToString();
		counter = time;
		StartCoroutine (Timer ());
	
	}

	IEnumerator Timer(){
		if (counter > 0) {
			timer.text = counter.ToString();
			counter--;
			yield return new WaitForSeconds (1);
			StartCoroutine (Timer());
		} 
	}

	public void StartTimer(){
		StartCoroutine (Timer());
				}
		}
