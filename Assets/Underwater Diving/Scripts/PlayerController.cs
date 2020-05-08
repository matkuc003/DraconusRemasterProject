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

	private Rigidbody2D myRigidBody;

	private Animator myAnim;
	public GameObject bubbles;
	public GameObject player;
    private SavePointSystem savePointSystem;
	// Use this for initialization
	void Start (){
		myRigidBody = GetComponent<Rigidbody2D> ();	
		myAnim = GetComponent<Animator> ();
        this.savePointSystem = GameObject.Find("SavePointSystem").GetComponent<SavePointSystem>();
    }
	
	// Update is called once per frame
	void Update (){

		resetBoostTime ();
		controllerManager ();



		myAnim.SetFloat ("Speed", Mathf.Abs(myRigidBody.velocity.x));

		if (platformCheck2 == true && Input.GetKeyDown("v"))
		{
			myAnim.SetBool("vPressed", true);
			tmpPosition = this.gameObject.transform.position;
			tmpPosition.y += 3;
			this.gameObject.SetActive(false);
			player.transform.position = tmpPosition;
			player.SetActive(true);
			platformCheck2 = false;
		}
	}

	void controllerManager (){
		if (Input.GetAxisRaw ("Horizontal") > 0f) {
			transform.localScale = new Vector3(3f, 3f, 3f);
			movePlayer ();
		} else if (Input.GetAxisRaw ("Horizontal") < 0f) {			
			transform.localScale = new Vector3(-3f, 3f, 3f);
			movePlayer ();
		} else if (Input.GetAxisRaw ("Vertical") > 0f) {
			myRigidBody.velocity = new Vector3 (myRigidBody.velocity.x, moveSpeed, 0f);
		} else if (Input.GetAxis ("Vertical") < 0f) {
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
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			this.gameObject.SetActive(false);
			player.transform.position = tmpPosition;
			player.SetActive(true);
            player.transform.position = savePointSystem.getSavePoint();

		}
		if (collision.gameObject.tag == "PlatformForDiving")
		{
			platformCheck2 = true;

		}
	}

}
