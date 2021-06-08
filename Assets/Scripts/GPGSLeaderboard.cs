using UnityEngine;

public class GPGSLeaderboard : MonoBehaviour
{
    public void OpenLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void UpdateLeaderboardScore()
    {
        if(PlayerPrefsSafe.GetInt("HighScore", 0) == 0)
        {
            return;
        }

        Social.ReportScore(PlayerPrefsSafe.GetInt("HighScore"), GPGSIds.leaderboard_high_score, (bool success) =>
        {
            if(success)
            {
                PlayerPrefsSafe.SetInt("HighScore", PlayerPrefsSafe.GetInt("HighScore"));
            }
        });
    }
}
