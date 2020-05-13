using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalMovement : MonoBehaviour
{
    public bool direction; // false - left, true - right
    public float maxDistance;
    public float speed = 0.1f;
    public bool mayDie = true;
    private Vector2 startPosition;
    private ScoreScript scoreScript;
    private Animator animator;
    void Start()
    {
        startPosition = this.gameObject.transform.position;
        scoreScript = GameObject.Find("ScoreScript").GetComponent<ScoreScript>();
        animator = gameObject.GetComponent<Animator>();

        if (!direction)
            startPosition.x -= maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 actualPosition = gameObject.transform.position;
        if (direction)
            actualPosition.x += speed;
        else
            actualPosition.x -= speed;

        this.gameObject.transform.position = actualPosition;

        if (startPosition.x >= actualPosition.x || startPosition.x + maxDistance <= actualPosition.x)
        {
            direction = !direction;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((mayDie) && other.tag == "Fire")
        {
            float time = AnimationLength("death");
            animator.Play("death");
            speed = 0;
            Destroy(gameObject, time);

            scoreScript.addPoints(Random.Range(1, 10));
        }
    }
    private float AnimationLength(string name)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name == name)
                time = ac.animationClips[i].length;

        return time;
    }
}
