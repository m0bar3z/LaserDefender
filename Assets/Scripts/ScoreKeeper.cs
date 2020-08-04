using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static int score;
    private Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
        Reset();
    }

    public void addScore(int ScorePoint)
    {
        score = score + ScorePoint;
        myText.text = score.ToString();
    }

    public static void Reset()
    {
        score = 0;
    }
}
