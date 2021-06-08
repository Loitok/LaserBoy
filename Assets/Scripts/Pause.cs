using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool isPaused;

    public GameObject PauseMenu;
    public GameObject PauseButton;
    private GameObject player;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
            gameObject.SetActive(false);
        if (isPaused)
        {
            PauseMenu.SetActive(true);
            PauseButton.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            PauseMenu.SetActive(false);
            PauseButton.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void Escape()
    {
        isPaused = !isPaused;
    }

    public void PauseClick()
    {
        isPaused = true;
    }

    public void Retry()
    {
        isPaused = false;
        StartCoroutine(WaitRetry());
    }

    public void Home()
    {
        isPaused = false;
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

}
