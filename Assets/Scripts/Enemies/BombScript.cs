using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public bool direction; // false - down, true - up
    public float maxDistance;
    public float timeRest;
    private float timeRest2 = 0;
    private Vector2 startPosition;
    void Start()
    {
        startPosition = this.gameObject.transform.position;

        if (!direction)
            startPosition.y -= maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRest2 > 0)
        {
            timeRest2 -= Time.deltaTime;
        }
        else
        {
            Vector2 actualPosition = gameObject.transform.position;
            if (direction)
                actualPosition.y += 0.1f;
            else
                actualPosition.y -= 0.1f;

            this.gameObject.transform.position = actualPosition;

            if (startPosition.y >= actualPosition.y || startPosition.y + maxDistance <= actualPosition.y)
            {
                direction = !direction;
                timeRest2 = timeRest;
            }
        }
    }
}
