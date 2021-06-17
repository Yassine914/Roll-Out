using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndlessScore : MonoBehaviour
{
    private int highScore;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("EndlessHighScore", 0);
        highScoreText.text = PlayerPrefs.GetInt("EndlessHighScore").ToString();
    }

    public void AddHighScore(int score)
    {
        highScore = score;
        highScoreText.text = highScore.ToString();
    }
}
