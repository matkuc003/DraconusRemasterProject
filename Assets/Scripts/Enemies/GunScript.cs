using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject ammo;
    public float frequency;
    private float time = 0;
    private Vector3 position;
    private Quaternion rotation;
    void Start()
    {
        position = gameObject.transform.position;
        position.y -= 1.5f;
        rotation = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0) {
            time = frequency;
            Instantiate(ammo, position, rotation);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
