using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TMP_Text scoreText; 
    [SerializeField] private TMP_Text highScoreText; 
    [SerializeField] private float scoreMultiplier = 1f;

    [HideInInspector] public float score;
    private float highScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        //lấy điểm cao nhất qua hàm PlayerPrefs với từ khóa HighScore
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        UpdateUI();
    }


    // Update is called once per frame
    void Update()
    {
        if(PlayerController.Instance.state == PlayerController.State.Playing)
        {
            score += Time.deltaTime * scoreMultiplier;
            UpdateUI();
        }
    }

    public void AddScore(float points)
    {
        score += points;
        UpdateUI();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }

    public void SaveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            //Lưu trữ điểm cao nhất qua hàm PlayerPrefs qua từ khóa HighScore
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }


    private void UpdateUI()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore).ToString();
    }


}
