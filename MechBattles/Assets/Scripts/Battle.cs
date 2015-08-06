using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour {

	public GameObject pm;
	public GameObject enemy;
	public GameObject UIBattleBar;

	public float timeToTravel = 3.25f;

	public GameObject gem;

	public AudioSource sequenceSound;

	public bool showStart = false;
	public bool showBattleUI = false;

	public bool soundStarted = false;

	public bool playSequences = false;

	Sequence enemySequence;

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
			StartCoroutine("playSound");
			enemy.GetComponent<EnemyMech> ().Action (enemySequence.TimeActTable [i].action);
			GameObject new_Gem = (GameObject)Instantiate(gem, new Vector3(UIBattleBar.transform.position.x + 25.0f, UIBattleBar.transform.position.y, UIBattleBar.transform.position.z) , Quaternion.identity);
			new_Gem.GetComponent<Rigidbody2D>().AddForce(new Vector2(-8.65f,0), ForceMode2D.Impulse);
			new_Gem.GetComponent<Gem>().gameManager = this.gameObject;
			new_Gem.GetComponent<Gem>().isTapable = true;
			StartCoroutine(new_Gem.GetComponent<Gem>().battleControls(Time.time, new_Gem));
			//Debug.Log (enemySequence.TimeActTable [i].time + " " + enemySequence.TimeActTable [i].action);
			yield return new WaitForSeconds(enemySequence.TimeActTable [i].time - timeToTravel);

		}


	}

	IEnumerator playSound()
	{
		yield return new WaitForSeconds(timeToTravel);
		if(soundStarted == false)
		{
			sequenceSound.Play ();
			soundStarted = true;
		} else {
			StopCoroutine("playSound");
		}
	}

	public void evaluateGemTime(string quality, GameObject gem)
	{
		if (quality == "miss") {
			Debug.Log ("Missed!");
			Destroy(gem);
		}
		if (quality == "good") {
			Debug.Log ("Good!");
			Destroy(gem);
		}
		if (quality == "great") {
			Debug.Log ("Great!");
			Destroy(gem);
		}
	}

//	public void sendPlayerSequence()
//	{
//		//Debug.Log(enemySequence.TimeActTable[0].time);
//		for (int i = 0; i < enemySequence.TimeActTable.Length; i++) {
//			float timer = enemySequence.TimeActTable[i].time;
//			if(timer <= 0)
//			{
//				enemy.GetComponent<EnemyMech> ().Action (enemySequence.TimeActTable [i].action);
//			} else {
//				timer -= Time.deltaTime;
//			}
//			
//		}
//
//	}
	public void OnGUI()
	{
		if (showStart) {
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2, 100, 100), "Start")) {
				showStart = false;
				StartCoroutine("sendPlayerSequence");
//				playSequences = true;
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
