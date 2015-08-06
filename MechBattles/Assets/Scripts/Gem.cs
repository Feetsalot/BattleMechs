using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {

	public GameObject gameManager;

	float gemTime = 0.1f;

	bool undeclared = true;
	public bool isTapable = false;
	float elapsedTime;
	float zoneStart;

//	public Gem(IEnumerator battleControls){
//
//	}

	public IEnumerator battleControls(float start, GameObject gem)
	{
		for (int i = 0; i < (start + gameManager.GetComponent<Battle> ().timeToTravel + gemTime) * 100 + 50; i++) {
			//Debug.Log (Time.time);
			//Debug.Log ("In Coroutine");
			if (isTapable && Time.time >= (start + gameManager.GetComponent<Battle> ().timeToTravel + gemTime)) {
				if (undeclared) {
					zoneStart = Time.time;
					undeclared = false;
				}
				elapsedTime = Time.time;
				//Debug.Log ("elapsed time = " + (elapsedTime - zoneStart));
				if (elapsedTime - zoneStart <= 0.2f) {
					if (Input.GetKeyDown (KeyCode.Space)) {
						Debug.Log ("good from gem");
						isTapable = false;
						gameManager.GetComponent<Battle> ().evaluateGemTime ("good", gem);
						StopCoroutine ("battleControls");
					}
				} else if (elapsedTime - zoneStart <= 0.3f && elapsedTime - zoneStart > 0.2f) {
					if (Input.GetKeyDown (KeyCode.Space)) {
						Debug.Log ("good from gem");
						isTapable = false;
						gameManager.GetComponent<Battle> ().evaluateGemTime ("great", gem);
						StopCoroutine ("battleControls");
					}
				} else if (elapsedTime - zoneStart <= 0.5f && elapsedTime - zoneStart > 0.3f) {
					if (Input.GetKeyDown (KeyCode.Space)) {
						Debug.Log ("good from gem");
						isTapable = false;
						gameManager.GetComponent<Battle> ().evaluateGemTime ("good", gem);
						StopCoroutine ("battleControls");
					}
				} else {
					isTapable = false;
					Debug.Log ("miss from gem");
					gameManager.GetComponent<Battle> ().evaluateGemTime ("miss", gem);
					StopCoroutine ("battleControls");
				}

			}
			//Debug.Log ("checked if");
			yield return new WaitForSeconds (0.01f);
		}
	}
}
