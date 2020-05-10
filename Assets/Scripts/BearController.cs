using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

//This Script is intended for demoing and testing animations only.
public class BearController : MonoBehaviour {

    private float HSpeed = 10f;
    //private float maxVertHSpeed = 20f;
    private bool facingRight = true;
    private float moveXInput;
    private Vector3 tmpPosition;

    //Used for flipping Character Direction
    public static Vector3 theScale;
    private Text textAfter;
    //Jumping Stuff
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool grounded = false;
    private bool crouch = false;
    private float groundRadius = 0.15f;
    private float jumpForce = 12f;

    private Animator anim;
    public GameObject diver;
    private Boolean platformCheck;
    public Animator fire;
    private float timeFire;
    private bool activeFire = false;

    private SavePointSystem savePointSystem;
    private ScoreScript scoreScript;
    private GridScript gridScript;
    public HealthBarScript healthBar;

    private float currentFallTime;
    private float maxFallTime = 1.2f;
    private bool playerIsFall = false;

    private bool mysticalMorphHelix = false;
    private bool demonShieldOfGrom = false;
    private bool serekosEye = false;
    private bool staffOfFindol = false;
    private float timeLeft = 5;
    // Use this for initialization
    void Awake()
    {
        //		startTime = Time.time;
        anim = GetComponent<Animator>();
        fire.gameObject.SetActive(false);
        this.gridScript = GameObject.Find("Grid").GetComponent<GridScript>();
        this.savePointSystem = GameObject.Find("SavePointSystem").GetComponent<SavePointSystem>();
        this.scoreScript = GameObject.Find("ScoreScript").GetComponent<ScoreScript>();
        textAfter = GameObject.FindGameObjectWithTag("TextAfterArtifact").GetComponent<Text>();
        healthBar.resetHealthBar();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("ground", grounded);
    }

    void Update()
    {
        moveXInput = Input.GetAxis("Horizontal");

        if ((grounded) && Input.GetKeyDown("up"))
        {
            anim.SetBool("ground", false);
            SoundManager.PlaySound("jump");

            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.y, jumpForce);
        }

        if ((grounded) && Input.GetKey("down"))
        {
            Debug.Log("crouch");
            anim.SetBool("crouch", true);
            crouch = true;
        } else
        {
            // anim.SetBool("crouch", false);
            crouch = false;
        }

        anim.SetFloat("HSpeed", Mathf.Abs(moveXInput));
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        GetComponent<Rigidbody2D>().velocity = new Vector2((moveXInput * HSpeed), GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetKeyDown("c") && (grounded)) { anim.SetTrigger("Punch"); SoundManager.PlaySound("punch"); }

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

        if (Input.GetKeyDown("x") && (grounded || staffOfFindol) && !activeFire)
        {
            SoundManager.PlaySound("fire");
            fire.gameObject.SetActive(true);
            timeFire = Time.time;
            activeFire = true;
        }
        disableFire();

        if (!(grounded) && !(demonShieldOfGrom)) playerIsFall = true;
        else playerIsFall = false;

        if (playerIsFall)
            currentFallTime += Time.deltaTime;

        if ((grounded) && currentFallTime < maxFallTime)
            currentFallTime = 0;

        if (currentFallTime >= maxFallTime && (grounded))
        {
            currentFallTime = 0;
            PlayerDead();
        }
        //Flipping direction character is facing based on players Input
        if (moveXInput > 0 && !facingRight)
            Flip();
        else if (moveXInput < 0 && facingRight)
            Flip();

        if (platformCheck == true && Input.GetKeyDown("v") && mysticalMorphHelix)
        {
            SoundManager.PlaySound("transformation");
            anim.SetBool("vPressed", true);
            tmpPosition = this.gameObject.transform.position;
            tmpPosition.y -= 3;
            this.gameObject.SetActive(false);
            diver.transform.position = tmpPosition;
            diver.SetActive(true);
            platformCheck = false;
        }
        if (mysticalMorphHelix || serekosEye|| demonShieldOfGrom || staffOfFindol)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                textAfter.text = "";
            }
        }
        anim.SetBool("vPressed", false);
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            PlayerDead();
        }

        if (collision.gameObject.tag == "PlatformForDiving")
        {
            platformCheck = true;
        }
        if (collision.gameObject.tag == "Base")
        {
            platformCheck = false;
        }
        if(collision.gameObject.tag == "SavePoint")
        {
            Debug.Log("save point: " + this.gameObject.transform.position.x + ", " + this.gameObject.transform.position.y);
            savePointSystem.setSavePoint(this.gameObject.transform.position);
        }
        if (collision.gameObject.tag == "Monster")
        {
            PlayerDead();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water")
        {
            PlayerDead();
        }

        if (collision.gameObject.tag == "ExtraPoint")
        {
            SoundManager.PlaySound("diamond");
            Destroy(collision.gameObject);
            scoreScript.addPoints(100);
        }
        if(collision.gameObject.tag == "Artefact")
        {
            scoreScript.addPoints(250);
            Destroy(collision.gameObject);
            switch(collision.gameObject.name)
            {
                case "Mystical Morph Helix":
                    SoundManager.PlaySound("artifact");
                    timeLeft = 5;
                    this.mysticalMorphHelix = true;
                
                    textAfter.text= "Mystical Morph Helix - Mozesz teraz zamienic się w nurka na odpowiednich platformach po nacisnieciu v";

                    break;
                case "Serekos Eye":
                    SoundManager.PlaySound("artifact");
                    timeLeft = 5;
                    this.serekosEye = true;
                    this.gridScript.setVisibleHiddenWall(true);
                    textAfter.text = "Serekos Eye - Mozesz teraz przechodzic przez niewidzialne sciany";
                break;
                case "Demon Shield of Grom":
                    SoundManager.PlaySound("artifact");
                    timeLeft = 5;
                    this.demonShieldOfGrom = true;
                    textAfter.text = "Demon Shield of Grom - Przetrwasz teraz upadki z dowolnej odleglosci";
                break;
                case "Staff of Findol":
                    SoundManager.PlaySound("artifact");
                    timeLeft = 5;
                    this.staffOfFindol = true;
                    textAfter.text = "Staff of Findol - Mozesz teraz ziac ogniem podczas skoku";
                break;
            }
        }
    }

    public void PlayerDead()
    {
        SoundManager.PlaySound("dead");
        this.gameObject.transform.position = savePointSystem.getSavePoint();
        healthBar.takeDamage(20);
    }
}
