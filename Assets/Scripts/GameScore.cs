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
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreTextUI = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format ("{0:000000}",score);
        scoreTextUI.text = scoreStr;
    }
}
