using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraconusController : MonoBehaviour
{
    float speed = 40f;
    float moveHor = 0f;
    bool jump = false;
    bool crouch = false;
    public CharacterController2D characterController;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHor = Input.GetAxis("Horizontal") * speed;
        Debug.Log(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
           
        }

    }
    public void FixedUpdate()
    {
        characterController.Move(moveHor * Time.fixedDeltaTime,crouch,jump);
        jump = false;
    }

}
