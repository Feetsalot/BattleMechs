using UnityEngine;
using System.Collections;

public class Player_Mech : MonoBehaviour {
	
	private Animator anim;
	private Rigidbody2D rb2d;
	public Transform groundCheck;

	private bool grounded;
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public float jumpForce; 
	public float moveForce;
	public float maxSpeed;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		isGrounded ();
		Movement ();
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{

	}
	
	void Movement()
	{
		if (Input.GetButtonDown("Jump") && grounded)
		{
			jump = true;
		}

		float h = Input.GetAxis("Horizontal");
		
		anim.SetFloat("HorizontalSpeed", Mathf.Abs(h));
		
		if (h * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * h * moveForce);
		
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		//anim.SetFloat("HorizontalSpeed", Mathf.Abs (Input.GetAxisRaw("Horizontal")));

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();

		if (jump)
		{
			anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
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
}
