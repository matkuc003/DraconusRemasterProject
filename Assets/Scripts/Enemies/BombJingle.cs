using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombJingle : MonoBehaviour
{
    public float rangeX;
    public float rangeY;
    public float speed = 5;
    private Vector2 startPosition, actualPosition;
    private Vector2 target;
    private ScoreScript scoreScript;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        actualPosition = startPosition;
        randomTarget();
        scoreScript = GameObject.Find("ScoreScript").GetComponent<ScoreScript>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(actualPosition == target)
        {
            randomTarget();
        } else
        {
            gameObject.transform.position = Vector2.MoveTowards(actualPosition, target, speed * Time.deltaTime);
            actualPosition = gameObject.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire")
        {
            float time = AnimationLength("Explosion");
            speed = 0;
            animator.Play("Explosion");
            Destroy(gameObject, time);
            scoreScript.addPoints(Random.Range(1, 20));
        }
    }

    void randomTarget()
    {
        float x = Random.Range(startPosition.x - rangeX, startPosition.x + rangeX);
        float y = Random.Range(startPosition.y - rangeY, startPosition.y + rangeY);
        target = new Vector2(x, y);
        Debug.Log("target: " + target);
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
