using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Text highscoreText;
    public Text coinsText;

    public GameObject coinsMenu;
    public GameObject shopMenu;

    public GameObject freeButton;
    private int taps;

    public GameObject shavaCompany;
    void Start()
    {
        taps = 0;
        shavaCompany.GetComponent<Animator>().SetTrigger("start");
        highscoreText.text = "Highscore : " + PlayerPrefsSafe.GetInt("HighScore");
        coinsText.text = "Coins : " + PlayerPrefsSafe.GetFloat("Coins");
        StartCoroutine(Wait());       
    }

    void Update()
    {
        coinsText.text = "Coins : " + PlayerPrefsSafe.GetFloat("Coins");
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        shavaCompany.SetActive(false);
    }

    public void NotEnoughMoney()
    {
        shopMenu.SetActive(false);
        coinsMenu.SetActive(true);
    }

    public void InstagramPressed()
    {
        Application.OpenURL("http://instagram.com/shava_company");
    }

    public void YouTubePressed()
    {
        Application.OpenURL("https://www.youtube.com/channel/UC2ujd6F5xUzHLdZohf75Wpg");
    }

    public void Taps()
    {
        taps++;
        if(taps > 4)
        {
            Destroy(freeButton);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
