using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Your Score: " + Mathf.FloorToInt(ScoreManager.Instance.score).ToString();
        highScoreText.text = "High Score: " + Mathf.FloorToInt(PlayerPrefs.GetFloat("HighScore", 0)).ToString();
    }

}

   
