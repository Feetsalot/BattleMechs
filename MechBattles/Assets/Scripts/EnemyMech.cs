using UnityEngine;
using System.Collections;

public class EnemyMech : MonoBehaviour {
	
	private Animator anim;
	GameObject projectile;


	public GameObject lineOfSight;
	public GameObject battleManager;

	public bool targeted = false;
	public bool inBattle = false;

	public Weapon primaryWeapon;

	public Sequence enemySequence;
	public Sequence[] enemySequences;

	public float moveSpeed = 5f;

	float step;


	// Use this for initialization
	void Start () {
		step = 0.001f;
		primaryWeapon = this.GetComponent<Weapon> ();
		anim = this.GetComponent<Animator> ();
		enemySequence = this.GetComponent<Sequence> ();
		//Debug.Log (enemySequence.TimeActTable[0].action);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (inBattle == false) {

			targetPlayer ();
		} else if (inBattle) {

		}
	}

	#region In Battle



	public void Action(string action)
	{
		if (action == "weapon_primary")
		{
			if(primaryWeapon.type != Weapon.attackType.Hammer)
			{
				projectile = (GameObject)(Resources.Load(primaryWeapon.type + "_projectile"));
				this.anim.SetBool("Shooting", true);
			} else {

			}
		}
	}

	public void setShooting()
	{
		this.anim.SetBool ("Shooting", false);
		GameObject new_projectile = (GameObject)Instantiate(projectile, new Vector3(this.transform.position.x - 1.0f, this.transform.position.y + 0.7f, this.transform.position.z) , Quaternion.identity);
		new_projectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(-8.5f,0), ForceMode2D.Impulse);
		//Debug.Log ("Fired Projectile " + new_projectile);
	}

	#endregion

	#region Out Of Battle
	void targetPlayer()
	{
		if (Physics2D.Linecast (this.transform.position, lineOfSight.transform.position, 1 << 8)) {
			RaycastHit2D hit = Physics2D.Linecast (this.transform.position, lineOfSight.transform.position, 1 << 8);
			if (hit.collider.name == "Player_Mech") {
				targeted = true;
				if(anim.GetBool("Moving") == false)
				{
					StartCoroutine(tryInitiateFight (hit.collider.gameObject));
				}
			}
		}
	}

	IEnumerator tryInitiateFight(GameObject target)
	{
		while(targeted){
			if (targeted) {
				step = 10f * (Time.deltaTime);
				Vector2 moveTo = Vector2.MoveTowards(new Vector2(this.transform.position.x,  this.transform.position.y), new Vector2(target.transform.position.x, this.transform.position.y), step);

				this.transform.position = new Vector3(moveTo.x , moveTo.y, 0);
				//this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300f,0), ForceMode2D.Force);
				//target.GetComponent<Player_Mech> ().isMoveAble = false;
				anim.SetBool ("Moving", true);
			} 
			if (this.transform.position.x <= target.transform.position.x + 3.5f) {
				targeted = false;
				anim.SetBool ("Moving", false);
				inBattle = true;
				target.GetComponent<Player_Mech> ().isInBattle = true;
				battleManager.GetComponent<Battle> ().loadBattle (this.gameObject.name + "_Battle", this.gameObject);
			}
			yield return new WaitForSeconds(0f);
		}
	}
	#endregion
}
