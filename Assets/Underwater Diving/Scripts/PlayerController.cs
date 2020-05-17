using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

	public float moveSpeed;
	public bool rushing = false;
	private float speedMod = 0;
	private Vector3 tmpPosition,tmpPosition2;
	float timeLeft = 2f;
	private bool platformCheck2;
	private bool goUpB = false;
	private bool goDownB = false;
	private Rigidbody2D myRigidBody;

	private Animator myAnim;
	public GameObject bubbles;
	public GameObject player;
    private SavePointSystem savePointSystem;
    public HealthBarScript healthBar;
    private ScoreScript scoreScript;

	public GameObject upButton;
	public GameObject downButton;

	private bool isCollision;
    private bool isFlicker;
    private float flickerTimeout;

	// Use this for initialization
	void Start (){
		myRigidBody = GetComponent<Rigidbody2D> ();	
		myAnim = GetComponent<Animator> ();
        this.savePointSystem = GameObject.Find("SavePointSystem").GetComponent<SavePointSystem>();
        this.scoreScript = GameObject.Find("ScoreScript").GetComponent<ScoreScript>();
    }
	
	// Update is called once per frame
	void Update (){
        isCollision = false;

		resetBoostTime ();
		controllerManager ();

		myAnim.SetFloat ("Speed", Mathf.Abs(myRigidBody.velocity.x));

		if (platformCheck2 == true && (Input.GetKeyDown("v")||BearController.getTransfortmationVar()))
		{
			this.upButton.SetActive(false);
			this.downButton.SetActive(false);
			SoundManager.PlaySound("transformation");
			myAnim.SetBool("vPressed", true);
			tmpPosition = this.gameObject.transform.position;
			tmpPosition.y += 3;
			this.gameObject.SetActive(false);
			player.transform.position = tmpPosition;
			player.SetActive(true);
			platformCheck2 = false;
		}

        flickerTimeout -= Time.deltaTime;
        GetComponent<Flicker>().animate = flickerTimeout > 0;
        if (flickerTimeout <= 0)
            isFlicker = false;
    }

	void controllerManager (){
		if (Input.GetAxisRaw ("Horizontal") > 0f || BearController.getRightButton()) {
			transform.localScale = new Vector3(3f, 3f, 3f);
			movePlayer ();
		} else if (Input.GetAxisRaw ("Horizontal") < 0f || BearController.getLeftButton()) {			
			transform.localScale = new Vector3(-3f, 3f, 3f);
			movePlayer ();
		} else if (Input.GetAxisRaw ("Vertical") > 0f || goUpB) {
			myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, moveSpeed, 0f);
		} else if (Input.GetAxis ("Vertical") < 0f||goDownB) {
			myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, -moveSpeed, 0f);
		}

		if(Input.GetButtonDown("Jump") && !rushing ){
			rushing = true;
			speedMod = 2;
			Instantiate (bubbles, gameObject.transform.position, gameObject.transform.rotation);
			movePlayer ();
		}	
	}

	void movePlayer(){
		
		platformCheck2 = false;
		if (transform.localScale.x == 3f) {
			myRigidBody.velocity = new Vector3 (moveSpeed + speedMod, myRigidBody.velocity.y, 0f);	
		} else {
			myRigidBody.velocity = new Vector3 (- (moveSpeed + speedMod), myRigidBody.velocity.y, 0f);
		}	
	}

	void resetBoostTime(){
		if (timeLeft <= 0) {
			timeLeft = 2f;
			rushing = false;
			speedMod = 0;
		} else if(rushing) {
			timeLeft -= Time.deltaTime;
		}	
	}

	public void hurt(){
		if(!rushing){
			gameObject.GetComponent<Animator> ().Play ("PlayerHurt");		
		}

	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "water")
		{
			tmpPosition2=this.gameObject.transform.position;
			tmpPosition2.y -= 1;
			this.gameObject.transform.position = tmpPosition2;
		}
        if (collision.gameObject.tag == "ExtraPoint")
        {
            SoundManager.PlaySound("diamond");
            Destroy(collision.gameObject);
            scoreScript.addPoints(100);
        }
    }
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "PlatformForDiving")
		{
			platformCheck2 = true;
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollision || isFlicker) return;
        isCollision = true;

        if (collision.gameObject.tag == "Enemy")
        {
            HitMonster();
        }
    }

    private void HitMonster()
    {
        SoundManager.PlaySound("dead");
        flickerTimeout = 2;
        isFlicker = true;
        healthBar.takeDamage(10);
    }

	public void startUpButton()
	{
		goUpB = true;
	}
	public void stopUpButton()
	{
		goUpB = false;
	}
	public void startDownButton()
	{
		goDownB = true;

	}
	public void stopDownButton()
	{
		goDownB = false;
	}

}
