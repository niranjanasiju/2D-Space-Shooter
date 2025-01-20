using UnityEngine;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";
    private const string LeaderboardKey = "Leaderboard";

    // Method to get the current high score
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
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
    public List<int> GetLeaderboard()
    {
        List<int> leaderboard = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            leaderboard.Add(PlayerPrefs.GetInt(LeaderboardKey + i, 0));
        }
        return leaderboard;
    }

    // Method to update the leaderboard
    public void UpdateLeaderboard(int score)
    {
        List<int> leaderboard = GetLeaderboard();
        leaderboard.Add(score);
        leaderboard.Sort((a, b) => b.CompareTo(a)); // Sort in descending order

        // Keep only the top 3 scores
        while (leaderboard.Count > 3)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1);
        }

        // Save the updated leaderboard
        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetInt(LeaderboardKey + i, leaderboard[i]);
        }
        PlayerPrefs.Save();
    }
}
