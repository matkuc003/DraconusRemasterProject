using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 cameraPos;
    private int vertBound = 13;
    private int horBound = 5;
    private int rightBound;
    private int leftBound;
    private int topBound;
    private int bottomBound;

    // Start is called before the first frame update
    void Start()
    {
        rightBound = vertBound;
        leftBound = -vertBound;
        topBound = horBound;
        bottomBound = -horBound;
        player = GameObject.FindWithTag("Player").transform;
        cameraPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x >= rightBound)
        {
            cameraPos.x += vertBound*2;
            rightBound += vertBound * 2;
            leftBound += vertBound * 2;
            transform.position = cameraPos;
        }
        else if(player.position.x <= leftBound)
        {
            cameraPos.x -= vertBound * 2;
            rightBound -= vertBound * 2;
            leftBound -= vertBound * 2;
            transform.position = cameraPos;
        }
        else if(player.position.y >= topBound)
        {
            cameraPos.y += horBound*2;
            topBound += horBound*2;
            bottomBound += horBound * 2;
            transform.position = cameraPos;
        }
        else if(player.position.y <= bottomBound)
        {
            cameraPos.y -= horBound * 2;
            topBound -= horBound * 2;
            bottomBound -= horBound * 2;
            transform.position = cameraPos;
        }
    }
}
