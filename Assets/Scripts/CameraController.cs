using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    public Transform diver;
    private Transform tmpPlayer;
    private Vector3 cameraPos;
    private float vertBound;
    private float horBound;
    private float rightBound;
    private float leftBound;
    private float topBound;
    private float bottomBound;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = transform.position;
        Camera cam = Camera.main;
        horBound = 2f * cam.orthographicSize;
        vertBound = horBound * cam.aspect;
        rightBound = vertBound/2;
        leftBound = -vertBound/2;
        topBound = horBound/2 + cameraPos.y;
        Debug.Log(topBound);
        bottomBound = -horBound/2 + cameraPos.y;
        Debug.Log(bottomBound);
        player = GameObject.FindWithTag("Player").transform;
        tmpPlayer = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x >= rightBound)
        {
            cameraPos.x += vertBound;
            rightBound += vertBound;
            leftBound += vertBound;
            transform.position = cameraPos;
        }
        else if(player.position.x <= leftBound)
        {
            cameraPos.x -= vertBound;
            rightBound -= vertBound;
            leftBound -= vertBound;
            transform.position = cameraPos;
        }
        else if(player.position.y >= topBound)
        {
            cameraPos.y += horBound;
            topBound += horBound;
            bottomBound += horBound;
            transform.position = cameraPos;
        }
        else if(player.position.y <= bottomBound)
        {
            cameraPos.y -= horBound;
            topBound -= horBound;
            bottomBound -= horBound;
            transform.position = cameraPos;
        }
 
    }
    private void FixedUpdate()
    {
        if (tmpPlayer.gameObject.activeSelf==false)
        {
         
            player = diver;
        }
        else if (diver.gameObject.activeSelf==false)
        {
            player = tmpPlayer;
        }
    }
}
