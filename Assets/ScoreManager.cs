using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;

    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Debug.Log(scoreText);
        scoreText.text = "Score: " + score.ToString();
    }

    public void ChangePoints(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }
}
