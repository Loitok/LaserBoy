using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text highscoreText;
    public Text scoreText;
    public GameObject gameScoreText;

    public GameObject freeContinueButton;
    public GameObject Leaderboard;

    public void Start()
    {
        gameScoreText.SetActive(false);
        highscoreText.text = "Highscore : " + PlayerPrefsSafe.GetInt("HighScore");
        scoreText.text = "Score : " + PlayerPrefsSafe.GetInt("Score");
        Leaderboard.GetComponent<GPGSLeaderboard>().UpdateLeaderboardScore();
    }
    public void Retry()
    {
        StartCoroutine(WaitRetry());        
    }

    public void Home()
    {
        StartCoroutine(WaitHome());
    }

    IEnumerator WaitRetry()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator WaitHome()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Destroy()
    {
        Destroy(freeContinueButton);
    }
}
