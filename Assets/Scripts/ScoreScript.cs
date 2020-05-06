using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update

    private int score;
    void Start()
    {
        score = 0;
    }

    public void addPoints(int points)
    {
        score += points;
    }

    public int getScore()
    {
        return this.score;
    }
}
