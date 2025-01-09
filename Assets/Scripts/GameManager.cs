using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject GameTitleGO;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GMState = GameManagerState.Opening;
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
            break;
        case GameManagerState.GameOver:
            //stop the time counter
            TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

            //stop enemy spawner
            enemySpawner.GetComponent<EnemySpawner>().UnsheduleEnemySpawner();

            //display game over
            GameOverGO.SetActive(true);
            //change game manager state to opening state
            Invoke("ChangeToOpeningState",8f);
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
    
}
