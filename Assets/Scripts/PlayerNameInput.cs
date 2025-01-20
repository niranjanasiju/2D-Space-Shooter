using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField nameInputField; // Reference to the Input Field
    public Button submitButton; // Reference to the Submit Button
    //private HighScoreManager highScoreManager;

    void Start()
    {
        //highScoreManager = Object.FindFirstObjectByType<HighScoreManager>(); // Updated method
        submitButton.onClick.AddListener(OnSubmit); // Add listener for the button click

        // Set Input Field and Button inactive initially
        nameInputField.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
    }

    void OnSubmit()
    {
        string playerName = nameInputField.text; // Get the player's name from the input field
        int currentScore = FindObjectOfType<GameScore>().Score; // Get the current score
        //highScoreManager.SetHighScore(currentScore, playerName); // Save the high score with the player's name
        nameInputField.gameObject.SetActive(false); // Hide the input field after submission
        submitButton.gameObject.SetActive(false); // Hide the submit button after submission
    }
}
