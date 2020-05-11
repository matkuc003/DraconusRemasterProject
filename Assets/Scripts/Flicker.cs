using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flicker : MonoBehaviour
{
    public bool animate = true;

    public float tickTime = .1f;    
    public float alphaScale = .3f;

    float timeout = 0;
    float fullAlpha = 1;
    bool full = true;

    void Start()
    {
        fullAlpha = GetComponent<Renderer>().material.color.a;
        timeout = 0;
        full = true;
    }

    void Update()
    {
        Color c = GetComponent<Renderer>().material.color;
        c.a = fullAlpha;

        if (animate)
        {
            timeout += Time.deltaTime;
            while (timeout > tickTime)
            {
                timeout -= tickTime;
                full = !full;
            }
            if (!full)
            {
                c.a *= alphaScale;
            }

        }
        GetComponent<Renderer>().material.color = c;
    }

}