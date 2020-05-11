using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour
{
    public float maxRotation; // degree: 0 - 60
    Quaternion target;
    void Start()
    {
        target = Quaternion.Euler(0, 0, maxRotation + 30);
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 1);
        if((int)transform.rotation.eulerAngles.z >= maxRotation && (int)transform.rotation.eulerAngles.z <= maxRotation+30)
        {
            target = Quaternion.Euler(0, 0, 360 - maxRotation - 30);
        } 
        else if((int)transform.rotation.eulerAngles.z <= 360 - maxRotation && (int)transform.rotation.eulerAngles.z >= 360 - maxRotation - 30)
        {
            target = Quaternion.Euler(0, 0, maxRotation + 30);
        }
    }
}
