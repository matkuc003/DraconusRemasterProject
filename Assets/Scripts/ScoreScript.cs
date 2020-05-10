using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private int score = 0;
    public Text textScore;
    public void resetScore()
    {
        score = 0;
        textScore.text = "Score: 0";
    }

    public void addPoints(int points)
    {
        score += points;
        textScore.text = "Score: " + score; 
    }

    public int getScore()
    {
        return this.score;
    }
}
