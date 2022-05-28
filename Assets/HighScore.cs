using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    
    public WerewolfController wwController;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;


    public float score;
    public float highScore;

    void Start()
    {
        score = wwController.score;
        highscoreText.text = PlayerPrefs.GetFloat("highScore").ToString();
    }

   
    void Update()
    {
        scoreText.text = score.ToString();

        if(score > PlayerPrefs.GetFloat("highScore"))
        {
            PlayerPrefs.SetFloat("highScore", score);
            highscoreText.text = PlayerPrefs.GetFloat("highScore").ToString();
        }
        
    }


}
