using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 0.8f;

    private float turnTimer;
    public float timeTrigger;

    private Rigidbody2D myRigidbody;
    private ScoreScript scoreScript;
    // Start is called before the first frame update
    void Start()
    {
        scoreScript = GameObject.Find("ScoreScript").GetComponent<ScoreScript>();
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        turnTimer = 0;
        timeTrigger = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector3(myRigidbody.transform.localScale.x * speed, myRigidbody.velocity.y, 0f);

        turnTimer += Time.deltaTime;
        if (turnTimer >= timeTrigger)
        {
            turnAround();
            turnTimer = 0;
        }
    }
    void turnAround()
    {
        if (transform.localScale.x == 0.5f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire")
        {
            Destroy(gameObject);
            scoreScript.addPoints(Random.Range(1, 10));
        }
    }
}
