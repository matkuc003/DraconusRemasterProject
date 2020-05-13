using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 actualPosition = gameObject.transform.position;
        actualPosition.y -= 0.1f;
        gameObject.transform.position = actualPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Base" || other.tag == "Player" || other.tag == "water")
        {
            Destroy(gameObject);
        }
    }
}
