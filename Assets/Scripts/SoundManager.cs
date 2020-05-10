using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public static AudioClip jumpSound, artifact, diamond, transformation, music, punch, fire, dead, savepoint;
    public static AudioSource audioSrc;
    private static bool createdSound = false;

    void Start()
    {

        jumpSound = Resources.Load<AudioClip>("jump");
        artifact = Resources.Load<AudioClip>("artifact");
        diamond = Resources.Load<AudioClip>("diamond");
        transformation = Resources.Load<AudioClip>("transformation");
        music = Resources.Load<AudioClip>("backgroundMusic");
        punch = Resources.Load<AudioClip>("punch");
        fire = Resources.Load<AudioClip>("fire");
        dead = Resources.Load<AudioClip>("dead");
        savepoint = Resources.Load<AudioClip>("savepoint"); 
        audioSrc = GetComponent<AudioSource>();
        if (createdSound == false)
        {
            DontDestroyOnLoad(this.gameObject);
            createdSound = true;
            audioSrc.loop = true;
            audioSrc.Play();
        };
    }

    // Update is called once per frame

    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "artifact":
                audioSrc.PlayOneShot(artifact);
                break;
            case "diamond":
                audioSrc.PlayOneShot(diamond);
                break;
            case "transformation":
                audioSrc.PlayOneShot(transformation);
                break;
            case "punch":
                audioSrc.PlayOneShot(punch);
                break;
            case "fire":
                audioSrc.PlayOneShot(fire);
                break;
            case "dead":
                audioSrc.PlayOneShot(dead);
                break;
            case "savepoint":
                audioSrc.PlayOneShot(savepoint);
                break;
        }
    }
}
