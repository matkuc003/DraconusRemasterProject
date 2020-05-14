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
    private static bool toLeftB = false;
    private static bool toRightB = false;
    private bool toJumpB = false;
    private bool toFireB = false;
    private bool toPunchB = false;
    private static bool toTranformationB = false;
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

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask attackMask;

    private bool isCollision;
    private bool isFlicker;
    private float flickerTimeout;

    public float attackRate = 1f;
    float nextAttackTime = 0f;
    // Use this for initialization
    void Awake()
    {
        //		startTime = Time.time;
        GameObject.FindGameObjectWithTag("BossBar").GetComponent<BossBarHealth>().resetHealthBar();
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
        isCollision = false;
        moveXInput = Input.GetAxis("Horizontal");

        if ((grounded) && (Input.GetKeyDown("up")||toJumpB))
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
        if (toRightB)
        {
            moveXInput = 1;
        }
        else if (toLeftB)
        {
            moveXInput = -1;
        }
        anim.SetFloat("HSpeed", Mathf.Abs(moveXInput));
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        GetComponent<Rigidbody2D>().velocity = new Vector2((moveXInput * HSpeed), GetComponent<Rigidbody2D>().velocity.y);

        if (Time.time >= nextAttackTime)
        {
            if ((Input.GetKeyDown("c")||toPunchB) && (grounded))
            {
                anim.SetTrigger("Punch");
                SoundManager.PlaySound("punch");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);

                foreach (Collider2D enemy in hitEnemies)
                {
                    SoundManager.PlaySound("dead");
                    Destroy(enemy.gameObject);

                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

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
        if (Time.time >= nextAttackTime)
        {
            if ((Input.GetKeyDown("x")||toFireB) && (grounded || staffOfFindol) && !activeFire)
            {
                SoundManager.PlaySound("fire");
                fire.gameObject.SetActive(true);
                timeFire = Time.time;
                activeFire = true;
                nextAttackTime = Time.time + 1f / attackRate;
            }
            disableFire();
        }
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

        flickerTimeout -= Time.deltaTime;
        GetComponent<Flicker>().animate = flickerTimeout > 0;
        if (flickerTimeout <= 0)
            isFlicker = false;

        //Flipping direction character is facing based on players Input
        if (moveXInput > 0 && !facingRight)
            Flip();
        else if (moveXInput < 0 && facingRight)
            Flip();

        if (platformCheck == true && (Input.GetKeyDown("v")||toTranformationB) && mysticalMorphHelix)
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
        if (isCollision) return;
        isCollision = true;

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
            savePointSystem.setSavePoint(this.gameObject.transform.position);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollision || isFlicker) return;
        isCollision = true;

        if (collision.gameObject.tag == "Monster" && !activeFire)
        {
            HitMonster();
        }
/*        if (collision.gameObject.tag == "Boss")
        {
            HitMonster();
        }*/
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
        if(collision.gameObject.tag == "Potion")
        {
            SoundManager.PlaySound("diamond");
            Destroy(collision.gameObject);
            scoreScript.addPoints(50);
            healthBar.addHealth(20);
        }
        if (collision.gameObject.tag == "Artefact")
        {
            scoreScript.addPoints(250);
            Destroy(collision.gameObject);
            SoundManager.PlaySound("artifact");
            timeLeft = 5;
            switch (collision.gameObject.name)
            {
                case "Mystical Morph Helix":
                    this.mysticalMorphHelix = true;
                    textAfter.text= "Mystical Morph Helix - Mozesz teraz zamienic się w nurka na odpowiednich platformach po nacisnieciu v";
                break;
                case "Serekos Eye":
                    this.serekosEye = true;
                    this.gridScript.setVisibleHiddenWall(true);
                    textAfter.text = "Serekos Eye - Mozesz teraz przechodzic przez niewidzialne sciany";
                break;
                case "Demon Shield of Grom":
                    this.demonShieldOfGrom = true;
                    textAfter.text = "Demon Shield of Grom - Przetrwasz teraz upadki z dowolnej odleglosci";
                break;
                case "Staff of Findol":
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
        healthBar.takeDamage(UnityEngine.Random.Range(2, 4) * 5);
    }

    private void HitMonster()
    {
        SoundManager.PlaySound("dead");
        flickerTimeout = 2;
        isFlicker = true;
        healthBar.takeDamage(UnityEngine.Random.Range(1,3)*5);
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    public void goRightButton()
    {
        toRightB = true;
    }
    public void stopGoingRightButton()
    {
        toRightB = false;
    }
    public void goLeftButton()
    {
        toLeftB = true;
    }
    public void stopGoingLeftButton()
    {
        toLeftB = false;
    }
    public void startJumpButton()
    {
        toJumpB = true;
    }
    public void stopJumpButton()
    {
        toJumpB = false;
    }
    public void startFireButton()
    {
        toFireB = true;
    }
    public void stopFireButton()
    {
        toFireB = false;
    }
    public void startPunchButton()
    {
        toPunchB = true;
    }
    public void stopPunchButton()
    {
        toPunchB = false;
    }
    public void startTransformationButton()
    {
        toTranformationB = true;
    }
    public void stopTransformationButton()
    {
        toTranformationB = false;
    }
    public static bool getTransfortmationVar()
    {
        return toTranformationB;
    }
    public static bool getLeftButton()
    {
        return toLeftB;
    }
    public static bool getRightButton()
    {
        return toRightB;
    }
    public void clickExitButton()
    {
        Application.LoadLevel("Menu");
    }
}
