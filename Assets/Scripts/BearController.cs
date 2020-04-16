using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//This Script is intended for demoing and testing animations only.


public class BearController : MonoBehaviour {

	private float HSpeed = 10f;
	//private float maxVertHSpeed = 20f;
	private bool facingRight = true;
	private float moveXInput;

    //Used for flipping Character Direction
	public static Vector3 theScale;

	//Jumping Stuff
	public Transform groundCheck;
	public LayerMask whatIsGround;
	private bool grounded = false;
    private bool crouch = false;
	private float groundRadius = 0.15f;
	private float jumpForce = 12f;

	private Animator anim;

    public Animator fire;
    private float timeFire;
    private bool activeFire = false;
	// Use this for initialization
	void Awake ()
	{
//		startTime = Time.time;
		anim = GetComponent<Animator> ();
        fire.gameObject.SetActive(false);
	}

	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("ground", grounded);
    }

	void Update()
	{

        moveXInput = Input.GetAxis("Horizontal");

        if ((grounded) && Input.GetKeyDown("up"))
        {
            anim.SetBool("ground", false);

            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.y, jumpForce);
        } 
        
        if((grounded) && Input.GetKey("down"))
        {
            Debug.Log("crouch");
            anim.SetBool("crouch", true);
            crouch = true;
        } else
        {
            anim.SetBool("crouch", false);
            crouch = false;
        }


        anim.SetFloat("HSpeed", Mathf.Abs(moveXInput));
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);


        GetComponent<Rigidbody2D>().velocity = new Vector2((moveXInput * HSpeed), GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetKeyDown("c") && (grounded)) { anim.SetTrigger("Punch"); }

        if (Input.GetKey("left shift"))
        {
            anim.SetBool("Sprint", true);
            HSpeed = 14f;
        }
        else
        {
            anim.SetBool("Sprint", false);
            HSpeed = 10f;
        }

        if(Input.GetKeyDown("x"))
        {
            fire.gameObject.SetActive(true);
            timeFire = Time.time;
            activeFire = true;
        }
        disableFire();

        //Flipping direction character is facing based on players Input
        if (moveXInput > 0 && !facingRight)
            Flip();
        else if (moveXInput < 0 && facingRight)
            Flip();
    }
    ////Flipping direction of character
    void Flip()
	{
		facingRight = !facingRight;
		theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    void disableFire()
    {
        if((activeFire) && Time.time - timeFire >= 0.5)
        {
            fire.gameObject.SetActive(false);
            activeFire = false;
        }
    }
}
