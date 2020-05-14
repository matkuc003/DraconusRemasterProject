using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScoreScript : MonoBehaviour
{
    private Text scoreText;
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = "Score: " + ScoreScript.getScore();
        ScoreScript.resetStaticScore();
    }
}
