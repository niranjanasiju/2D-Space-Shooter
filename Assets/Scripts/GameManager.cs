using UnityEngine;
using TMPro; // Added this line for TextMesh Pro
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject GameTitleGO;
    public GameObject highScoreUITextGO; // Reference to the high score UI text
    public GameObject leaderboardUITextGO; // Reference to the leaderboard UI text
    public GameObject viewLeaderboardButton; // Reference to the View Leaderboard button
    public GameObject backButton; // Reference to the Back button

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
        Leaderboard,
    }

    GameManagerState GMState;
    HighScoreManager highScoreManager; // Reference to HighScoreManager

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GMState = GameManagerState.Opening;
        highScoreManager = FindObjectOfType<HighScoreManager>(); // Get reference to HighScoreManager
        highScoreUITextGO.SetActive(true); // Ensure high score is visible initially
        leaderboardUITextGO.SetActive(false); // Ensure leaderboard is visible initially
        backButton.SetActive(false); // Hide back button initially
    }

    //function to update game manager state
    void UpdateGameManagerState()
    {
        switch(GMState)
        {
        case GameManagerState.Opening:
            //hide game over
            GameOverGO.SetActive(false);
            //display the game title
            GameTitleGO.SetActive(true);
            //set play button visible
            playButton.SetActive(true);
            viewLeaderboardButton.SetActive(true); // Show View Leaderboard button
            break;
        case GameManagerState.Gameplay:
            //hide game title
            GameTitleGO.SetActive(false);
            //reset score to zero
            scoreUITextGO.GetComponent<GameScore>().Score = 0;
            //hide play button
            playButton.SetActive(false);
            //show player ship
            playerShip.GetComponent<PlayerControl>().Init();
            //show enemy spawner
            enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
            //show time counter
            TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
            // Hide high score and leaderboard
            highScoreUITextGO.SetActive(false);
            leaderboardUITextGO.SetActive(false);
            backButton.SetActive(false); // Hide back button
            viewLeaderboardButton.SetActive(false);
            break;
        case GameManagerState.GameOver:
            //stop the time counter
            TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

            //stop enemy spawner
            enemySpawner.GetComponent<EnemySpawner>().UnsheduleEnemySpawner();

            //display game over
            GameOverGO.SetActive(true);
            // Display the high score
            int currentHighScore = highScoreManager.GetHighScore();
            highScoreUITextGO.GetComponent<TMP_Text>().text = "High Score: " + currentHighScore.ToString();
            // Show high score
            highScoreUITextGO.SetActive(true);
            
            // Update and display the leaderboard
            highScoreManager.UpdateLeaderboard(scoreUITextGO.GetComponent<GameScore>().Score);
            List<int> leaderboard = highScoreManager.GetLeaderboard();
            leaderboardUITextGO.GetComponent<TMP_Text>().text = "Leaderboard:\n";
            for (int i = 0; i < leaderboard.Count; i++)
            {
                leaderboardUITextGO.GetComponent<TMP_Text>().text += (i + 1) + ". " + leaderboard[i].ToString() + "\n";
            }
            // Show leaderboard
            leaderboardUITextGO.SetActive(false);
            viewLeaderboardButton.SetActive(false);
            //change game manager state to opening state
            Invoke("ChangeToOpeningState",8f);
            break;
        case GameManagerState.Leaderboard:
            // Hide other UI elements and show leaderboard
            GameTitleGO.SetActive(false);
            playButton.SetActive(false);
            viewLeaderboardButton.SetActive(false);
            highScoreUITextGO.SetActive(false);
            backButton.SetActive(true); // Show back button
            leaderboardUITextGO.SetActive(true);
            break;
        }
    }

    //function to set game manager state
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    //function called by play button when clicked
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState ();
    }

    //function to change game manager state to opening state
    public void ChangeToOpeningState()
    {
        SetGameManagerState (GameManagerState.Opening);
    }

    //function to view leaderboard
    public void ViewLeaderboard()
    {
        SetGameManagerState(GameManagerState.Leaderboard);
    }

    //function to go back to opening state
    public void GoBackToOpening()
    {
        SetGameManagerState(GameManagerState.Opening);
        backButton.SetActive(false);
        leaderboardUITextGO.SetActive(false);
        highScoreUITextGO.SetActive(true);
    }
}
