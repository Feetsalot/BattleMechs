using UnityEngine;
using System.Collections;

public class EnemyMech : MonoBehaviour {

	public GameObject lineOfSight;

	public bool targeted = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		targetPlayer ();
	}

	void targetPlayer()
	{
		if (Physics2D.Linecast (this.transform.position, lineOfSight.transform.position, 1 << 8)) 
		{
			RaycastHit2D hit = Physics2D.Linecast (this.transform.position, lineOfSight.transform.position, 1 << 8);
			if (hit.collider.name == "Player_Mech")
			{
				targeted = true;
				initiateFight(hit.collider.gameObject);
			}
		}
	}

	void initiateFight(GameObject target)
	{
		if (this.transform.position.x > target.transform.position.x - 1)
		{
			Vector2 moveTo = Vector3.MoveTowards(new Vector3(this.transform.position.x, this.transform.position.y,0),new Vector3(target.transform.position.x, target.transform.position.y,0),2f);
			this.transform.position = new Vector3(moveTo.x,moveTo.y,0);
		}
	}
}
