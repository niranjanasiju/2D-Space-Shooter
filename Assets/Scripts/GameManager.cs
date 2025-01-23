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
    public GameObject newHighScoreText; // Reference to the new high score notification text
    public GameObject playerNameInputText;
    public GameObject submitButton;
    public GameObject quitButton; // Reference to the Quit button

    public string playerName = ""; // to store the player name

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
        newHighScoreText.SetActive(false); // Hide new high score notification initially
        playerNameInputText.SetActive(false);
        submitButton.SetActive(false);
        playButton.SetActive(true);
        GameOverGO.SetActive(false);
        GameTitleGO.SetActive(true);
        quitButton.SetActive(false); // Hide quit button initially
        highScoreManager.ResetHighScore();
    }

    // Function to update game manager state
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                // Hide game over
                GameOverGO.SetActive(false);
                // Display the game title
                GameTitleGO.SetActive(true);
                // Set play button visible
                playButton.SetActive(true);
                viewLeaderboardButton.SetActive(true); // Show View Leaderboard button
                // Reset score to zero
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                //reset time counter to zero
                TimeCounterGO.GetComponent<TimeCounter>().ResetTimeCounter();
                //show high score
                highScoreUITextGO.SetActive(true);
                leaderboardUITextGO.SetActive(false);
                backButton.SetActive(false);
                newHighScoreText.SetActive(false);
                playerNameInputText.SetActive(false);
                submitButton.SetActive(false);
                quitButton.SetActive(false); // Hide quit button
                break;
            case GameManagerState.Gameplay:
                // Hide game title
                GameTitleGO.SetActive(false);
                // Reset score to zero
                scoreUITextGO.GetComponent<GameScore>().Score = 0;
                // Hide play button
                playButton.SetActive(false);
                // Show player ship
                playerShip.GetComponent<PlayerControl>().Init();
                // Show enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                // Show time counter
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
                // Hide high score and leaderboard
                highScoreUITextGO.SetActive(false);
                leaderboardUITextGO.SetActive(false);
                backButton.SetActive(false); // Hide back button
                viewLeaderboardButton.SetActive(false);
                newHighScoreText.SetActive(false);
                playerNameInputText.SetActive(false);
                submitButton.SetActive(false);
                GameOverGO.SetActive(false);
                quitButton.SetActive(true); // Show quit button
                break;
            case GameManagerState.GameOver:
                // Stop the time counter
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                // Stop enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnsheduleEnemySpawner();

                // Display game over
                GameOverGO.SetActive(true);

                //get current score
                int currentScore =  scoreUITextGO.GetComponent<GameScore>().Score;

                playerNameInputText.SetActive(false);
                submitButton.SetActive(false);
                newHighScoreText.SetActive(false);
                playButton.SetActive(false);
                GameTitleGO.SetActive(false);
                leaderboardUITextGO.SetActive(false);
                quitButton.SetActive(true); // Show quit button

                //check if the leaderboard has to be updated
                int thirdrankScore = highScoreManager.GetThirdRankScore();
                if (currentScore > thirdrankScore)
                {
                    //update the leaderboard
                    playerNameInputText.SetActive(true);
                    submitButton.SetActive(true);
                }

                // Update the high score
                int currentHighScore = highScoreManager.GetHighScore();
                if (currentScore > currentHighScore)
                {
                    highScoreManager.SetHighScore(currentScore);
                    newHighScoreText.GetComponent<TMP_Text>().text = "Congratulations\nYou have set a new High Score!!";
                    highScoreUITextGO.GetComponent<TMP_Text>().text = "High Score : " + currentScore.ToString();
                    newHighScoreText.SetActive(true);
                }

                // hide high score for now
                highScoreUITextGO.SetActive(false);
                //hide view leaderboard button
                viewLeaderboardButton.SetActive(false);
                
                // Show back button
                backButton.SetActive(true); 
                
                // Change game manager state to opening state
                //Invoke("GoBackToOpening", 8f);
                break;
            case GameManagerState.Leaderboard:
                // Hide other UI elements and show leaderboard
                GameTitleGO.SetActive(false);
                playButton.SetActive(false);
                viewLeaderboardButton.SetActive(false);
                highScoreUITextGO.SetActive(false);
                backButton.SetActive(true); // Show back button
                leaderboardUITextGO.GetComponent<TMP_Text>().text = "Leaderboard:\n\n";
                List<KeyValuePair<string, int>> leaderboard = highScoreManager.GetLeaderboard();
                for (int i = 0; i < leaderboard.Count; i++)
                {
                    leaderboardUITextGO.GetComponent<TMP_Text>().text += (i + 1) + ". " + leaderboard[i].Key + " : " + leaderboard[i].Value + "\n";
                }
                // Show leaderboard
                leaderboardUITextGO.SetActive(true);
                GameOverGO.SetActive(false);
                newHighScoreText.SetActive(false);
                playerNameInputText.SetActive(false);
                submitButton.SetActive(false);
                break;
        }
    }

    // Function to set game manager state
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    // Function called by play button when clicked
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    // Function to change game manager state to opening state
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    // Function to view leaderboard
    public void ViewLeaderboard()
    {
        SetGameManagerState(GameManagerState.Leaderboard);
    }

    // Function to go back to opening state
    public void GoBackToOpening()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    // Function to handle the submit button click
    public void SubmitPlayerName()
    {
        playerName = playerNameInputText.GetComponent<TMP_InputField>().text; // Read input field text
        if (!string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Player Name Submitted: " + playerName);
            
            // Clear the input field
            playerNameInputText.GetComponent<TMP_InputField>().text = "";

            playerNameInputText.SetActive(false);
            submitButton.SetActive(false);
            
            // Save the player's name along with the score
            highScoreManager.UpdateLeaderboard(playerName, scoreUITextGO.GetComponent<GameScore>().Score);
        }
        else
        {
            Debug.Log("Player Name is empty. Please enter a valid name.");
        }
    }

    // Function to quit the game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You pressed quit button.");
        //EditorApplication.isPlaying = false;
    }
}
