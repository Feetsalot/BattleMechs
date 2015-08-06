using UnityEngine;
using System.Collections;

public class Player_Mech : MonoBehaviour {
	
	private Animator anim;
	private Rigidbody2D rb2d;
	public Transform groundCheck;
	public GameObject gameManager;

	public int player_hp = 10;

	public Sequence playerSequence;

	private bool grounded;
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public bool isMoveAble = true;
	public bool isInBattle = false;
	public bool isTapable = false;

	public float jumpForce; 
	public float moveForce;
	public float maxSpeed;

	float elapsedTime;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		isGrounded ();	
		if (!isInBattle) {
			Movement ();
		} else if (isInBattle) {
		}
	}

//	public IEnumerator battleControls(float start, GameObject gem)
//	{
//		//Debug.Log ("In Coroutine");
//		if (isTapable) 
//		{
//
//			elapsedTime = Time.time;
//			Debug.Log ("elapsed time = " + (elapsedTime - start));
//			if(elapsedTime - start <= 0.1f)
//			{
//				if(Input.GetKeyDown(KeyCode.Space))
//				{
//					gameManager.GetComponent<Battle>().evaluateGemTime("good", gem);
//					StopCoroutine("battleControls");
//				}
//			}
//			else if(elapsedTime - start <= 0.3f && elapsedTime - start > 0.1f )
//			{
//				if(Input.GetKeyDown(KeyCode.Space))
//				{
//					gameManager.GetComponent<Battle>().evaluateGemTime("great", gem);
//					StopCoroutine("battleControls");
//				}
//			}
//			else if(elapsedTime - start <= 0.4f && elapsedTime - start > 0.3f )
//			{
//				if(Input.GetKeyDown(KeyCode.Space))
//				{
//					gameManager.GetComponent<Battle>().evaluateGemTime("good", gem);
//					StopCoroutine("battleControls");
//				}
//			}
//			else {
//				gameManager.GetComponent<Battle>().evaluateGemTime("miss", gem);
//				StopCoroutine("battleControls");
//			}
//
//		}
//		yield return new WaitForSeconds(0.001f);
//	}

	#region Platformer Controls
	void Movement()
	{
		if (isMoveAble) {

			if (Input.GetButtonDown ("Jump") && grounded) {
				jump = true;
			}

			float h = Input.GetAxis ("Horizontal");
		
			anim.SetFloat ("HorizontalSpeed", Mathf.Abs (h));
		
			if (h * rb2d.velocity.x < maxSpeed)
				rb2d.AddForce (Vector2.right * h * moveForce);
		
			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

			//anim.SetFloat("HorizontalSpeed", Mathf.Abs (Input.GetAxisRaw("Horizontal")));

			if (h > 0 && !facingRight)
				Flip ();
			else if (h < 0 && facingRight)
				Flip ();

			if (jump) {
				anim.SetTrigger ("Jump");
				rb2d.AddForce (new Vector2 (0f, jumpForce));
				jump = false;
			}

		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void isGrounded()
	{
		RaycastHit2D hit = Physics2D.Linecast (this.transform.position, groundCheck.position, 1 << 9);
		//Debug.Log (hit.collider);
		if (hit.collider) {
			grounded = true;
		} else {
			grounded = false;
		}
	}
	#endregion
}
