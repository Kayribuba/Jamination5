using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public WerewolfController wwController;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;


    public float score;
    public float highScore;

    void Start()
    {
        
        highscoreText.text = PlayerPrefs.GetFloat("highScore").ToString();
    }


    void Update()
    {
        score = wwController.score;
        scoreText.text = score.ToString();

        if (score > highScore)
        {
            PlayerPrefs.SetFloat("highScore", score);
            highscoreText.text = wwController.score.ToString();
        }

    }
}
