using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameScore : MonoBehaviour
{
    TMP_Text scoreTextUI;
    int score;

    public int Score
    {
        get 
        { 
            return this.score; 
        }
        set
        {
            this.score = value;
            UpdateScoreTextUI();
            SetHighScore(score); // Update high score if current score exceeds it
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreTextUI = GetComponent<TMP_Text>();
        // Check if scoreTextUI is null
        if (scoreTextUI == null)
        {
            Debug.LogError("TMP_Text component is not assigned to the GameScore script.");
        }
    }

    // Update is called once per frame
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format ("{0:000000}", score);
        scoreTextUI.text = scoreStr;
    }

    private void SetHighScore(int score)
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
}
