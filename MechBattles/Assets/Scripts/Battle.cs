using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour {

	public GameObject pm;
	public GameObject enemy;

	public bool showStart = false;

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
	
	}

	public void sendPlayerSequence(Player_Mech player, Sequence seq)
	{
		Debug.Log(enemySequence.TimeActTable[0].time);
//		for (int i = 0; i < times.Length; i++) {
//			//enemy.GetComponent<EnemyMech>().shoot
//			Debug.Log(enemySequence.actionTimeTable.Values);
//		}
	}
	public void OnGUI()
	{
		if (showStart) {
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2, 100, 100), "Start")) {
				this.sendPlayerSequence (pm.GetComponent<Player_Mech> (), enemySequence);
			}
		}
	}


	public void showStartRequest()
	{
		showStart = true;
	}

	public void showPlayerOptions()
	{
		showStartRequest ();
	}

	public void loadBattle(string name, GameObject enemy)
	{
		Application.LoadLevel (name);
		this.enemy = enemy;
		showPlayerOptions ();
	}
}
