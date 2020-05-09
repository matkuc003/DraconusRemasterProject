using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onPlay()
    {
        Application.LoadLevel("GameScene");
    }
    public void onExit()
    {
        Application.Quit();
    }
}
