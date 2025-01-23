using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Add this line for LINQ functionality
using System.IO; // Add this line for file operations

public class HighScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";
    private const string LeaderboardKey = "Leaderboard";
    private const string FilePath = "Assets/Scripts/leaderboard.txt"; // Path to the leaderboard file
    private Dictionary<string, int> leaderboard = new Dictionary<string, int>(); // Store player names and scores

    void Start()
    {
        LoadLeaderboard(); // Load the leaderboard when the game starts
    }

    // Method to reset high score to zero
    public void ResetHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, 0);
        PlayerPrefs.Save();
    }

    // Method to set a new high score
    public void SetHighScore(int score)
    {
        int currentHighScore = GetHighScore();
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.Save();
        }
    }

    // Method to get the leaderboard
    public List<KeyValuePair<string, int>> GetLeaderboard()
    {
        List<KeyValuePair<string, int>> sortedLeaderboard = new List<KeyValuePair<string, int>>(leaderboard);
        sortedLeaderboard.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value)); // Sort in descending order
        return sortedLeaderboard;
    }

    // Method to get the score of the third-ranked player
    public int GetThirdRankScore()
    {
        List<KeyValuePair<string, int>> sortedLeaderboard = GetLeaderboard();
        if (sortedLeaderboard.Count >= 3)
        {
            return sortedLeaderboard[2].Value; // Return the score of the third-ranked player
        }
        return 0; // Return 0 if there are less than 3 players
    }

    // Method to get the current high score
    public int GetHighScore()
    {
        List<KeyValuePair<string, int>> sortedLeaderboard = GetLeaderboard();
        if (sortedLeaderboard.Count >= 1)
        {
            return sortedLeaderboard[0].Value; // Return the score of the first-ranked player
        }
        return 0; // Return 0 if there are no players
    }

    // Method to update the leaderboard
    public void UpdateLeaderboard(string playerName, int score)
    {
        leaderboard[playerName] = score; // Update or add the player's score

        // Keep only the top 3 scores
        if (leaderboard.Count > 3)
        {
            var lowestScore = leaderboard.OrderBy(pair => pair.Value).First();
            leaderboard.Remove(lowestScore.Key);
        }

        // Save the updated leaderboard to the file
        SaveLeaderboardToFile();
    }

    // Method to save the leaderboard to a text file
    private void SaveLeaderboardToFile()
    {
        using (StreamWriter writer = new StreamWriter(FilePath))
        {
            foreach (var pair in leaderboard)
            {
                writer.WriteLine($"{pair.Key},{pair.Value}"); // Write player name and score
            }
        }
    }

    // Method to load the leaderboard from a text file
    private void LoadLeaderboard()
    {
        if (!File.Exists(FilePath))
        {
            // Create the file if it does not exist
            File.Create(FilePath).Close();
        }

        using (StreamReader reader = new StreamReader(FilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                {
                    leaderboard[parts[0]] = score; // Add player name and score to the dictionary
                }
            }
        }
    }
}
