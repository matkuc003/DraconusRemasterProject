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

    private float camSize = 5.3f;
    private float camAspect = 2.263291f;

    // Start is called before the first frame update
    void Start()
    {
        if (Camera.main.orthographicSize != camSize)
        {
            Camera.main.orthographicSize = camSize;
        }
        if (Camera.main.aspect != camAspect)
        {
            Camera.main.aspect = camAspect;
        }

        cameraPos = transform.position;
        Camera cam = Camera.main;
        
        horBound = 2f * cam.orthographicSize;
        Debug.Log(cam.orthographicSize);
        vertBound = horBound * cam.aspect;
        Debug.Log(cam.aspect);

        rightBound = vertBound/2;
        leftBound = -vertBound/2;
        topBound = horBound/2 + cameraPos.y;
        bottomBound = -horBound/2 + cameraPos.y;
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
