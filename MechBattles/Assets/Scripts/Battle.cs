using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour {

	public GameObject pm;
	public GameObject enemy;
	public GameObject UIBattleBar;

	#region Indicators
	public GameObject early_Indicator;
	public GameObject good_Indicator;
	public GameObject perfect_Indicator;
	public GameObject slow_Indicator;
	public GameObject miss_Indicator;
	#endregion
	
	public AudioClip word_battle;
	public AudioClip word_commence;
	public AudioSource UIsound;

	public float timeToTravel = 2.85f;

	public GameObject gem;

	public AudioSource sequenceSound;

	public bool showStart = false;
	public bool showBattleUI = false;

	public bool soundStarted = false;

	public bool playSequences = false;

	Sequence playerSequence;
	Sequence[] playerSequences;
	
	Sequence enemySequence;
	Sequence[] enemySequences;

	int []times;

	// Use this for initialization
	void Start () {
		if (enemy != null) {
			enemySequence = enemy.GetComponent<EnemyMech> ().enemySequence;
		}
	}
	
	// Update is called once per frame
	void Update () {
//		if (playSequences == true) {
//			sendPlayerSequence();
//		}
	}

	IEnumerator sendPlayerSequence()
	{
		sequenceSound.clip = enemySequence.sound;
		for (int i = 0; i < enemySequence.TimeActTable.Length; i++) {
			StartCoroutine(playSequenceSound(timeToTravel));
			enemy.GetComponent<EnemyMech> ().Action (enemySequence.TimeActTable [i].action);
			GameObject new_Gem = (GameObject)Instantiate(gem, new Vector3(UIBattleBar.transform.position.x + 25.0f, UIBattleBar.transform.position.y, UIBattleBar.transform.position.z) , Quaternion.identity);
			new_Gem.GetComponent<Rigidbody2D>().AddForce(new Vector2(-8.86f,0), ForceMode2D.Impulse);
			new_Gem.GetComponent<Gem>().gameManager = this.gameObject;
			new_Gem.GetComponent<Gem>().isTapable = true;
			StartCoroutine(new_Gem.GetComponent<Gem>().battleControls(Time.time, new_Gem));
			//Debug.Log (enemySequence.TimeActTable [i].time + " " + enemySequence.TimeActTable [i].action);
			yield return new WaitForSeconds(enemySequence.TimeActTable [i].time - timeToTravel);

		}


	}

	IEnumerator playSequenceSound(float wait)
	{
		yield return new WaitForSeconds(wait);
		if(soundStarted == false)
		{
			sequenceSound.Play ();
			soundStarted = true;
		} else {
			StopCoroutine("playSound");
		}
	}

	IEnumerator playUISound(float wait , AudioClip sound)
	{
		yield return new WaitForSeconds(wait);
		UIsound.clip = sound;
		UIsound.Play ();
		StopCoroutine("playUISound");
	}
	
	public void evaluateGemTime(string quality, GameObject gem)
	{
		if (quality == "miss") {
			GameObject new_Indicator = (GameObject)Instantiate(miss_Indicator, new Vector3(gem.transform.position.x + 5.0f, gem.transform.position.y + 5.0f, 0) , Quaternion.identity);
			Destroy(gem);
		}

		if (quality == "good") {
			GameObject new_Indicator = (GameObject)Instantiate(good_Indicator, new Vector3(gem.transform.position.x + 5.0f, gem.transform.position.y + 5.0f, 0) , Quaternion.identity);
			Destroy(gem);
		}

		if (quality == "perfect") {
			GameObject new_Indicator = (GameObject)Instantiate(perfect_Indicator, new Vector3(gem.transform.position.x + 5.0f, gem.transform.position.y + 5.0f, 0) , Quaternion.identity);
			Destroy(gem);
		}

		if (quality == "slow") {
			GameObject new_Indicator = (GameObject)Instantiate(slow_Indicator, new Vector3(gem.transform.position.x + 5.0f, gem.transform.position.y + 5.0f, 0) , Quaternion.identity);
			Destroy(gem);
		}

		if (quality == "early") {
			GameObject new_Indicator = (GameObject)Instantiate(early_Indicator, new Vector3(gem.transform.position.x + 5.0f, gem.transform.position.y  + 5.0f, 0) , Quaternion.identity);
			Destroy(gem);
		}
	}

	public void OnGUI()
	{
		if (showStart) {
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2, 100, 100), "Start")) {
				showStart = false;
				StartCoroutine(playUISound(0f, word_battle));
				StartCoroutine(playUISound(1f, word_commence));
				StartCoroutine("sendPlayerSequence");

			}
		}

		if (showBattleUI) {

		}
	}


	public void showStartRequest()
	{
		showStart = true;
	}

	public void showPlayerOptions()
	{
		showBattleUI = true;
		showStartRequest ();
	}

	public void loadBattle(string name, GameObject enemy)
	{
		Application.LoadLevel (name);
		this.enemy = enemy;
		showPlayerOptions ();
	}
}
