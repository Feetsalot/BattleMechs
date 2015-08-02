using UnityEngine;
using System.Collections;

public class EnemyMech : MonoBehaviour {

	public GameObject lineOfSight;
	public GameObject battleManager;

	public bool targeted = false;
	public bool inBattle = false;

	public Weapon primaryWeapon;

	public Sequence enemySequence;

	public float moveSpeed = 10f;

	float step;


	// Use this for initialization
	void Start () {
		enemySequence = this.GetComponent<Sequence> ();
		//Debug.Log (enemySequence.TimeActTable[0].action);
	}
	
	// Update is called once per frame
	void Update () {
		if (inBattle == false) {
			step = moveSpeed * Time.deltaTime;
			targetPlayer ();
		} else if (inBattle) {

		}
	}

	#region In Battle

	public void Action(string action)
	{
		if (action == "weapon_primary")
		{

		}
	}

	#endregion

	#region Out Of Battle
	void targetPlayer()
	{
		if (Physics2D.Linecast (this.transform.position, lineOfSight.transform.position, 1 << 8)) {
			RaycastHit2D hit = Physics2D.Linecast (this.transform.position, lineOfSight.transform.position, 1 << 8);
			if (hit.collider.name == "Player_Mech") {
				targeted = true;
				initiateFight (hit.collider.gameObject);
			}
		}
	}

	void initiateFight(GameObject target)
	{
		if (this.transform.position.x > target.transform.position.x + 3.5f) {
			Vector2 moveTo = Vector3.MoveTowards (new Vector3 (this.transform.position.x, this.transform.position.y, 0), new Vector3 (target.transform.position.x + 3.5f, target.transform.position.y, 0), step);
			this.transform.position = new Vector3 (moveTo.x, moveTo.y, 0);
			target.GetComponent<Player_Mech> ().isMoveAble = false;
		} else {
			inBattle = true;
			target.GetComponent<Player_Mech> ().isInBattle = true;
			battleManager.GetComponent<Battle>().loadBattle (this.gameObject.name + "_Battle", this.gameObject);
		}
	}
	#endregion
}
