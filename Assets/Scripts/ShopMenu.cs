using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ShopMenu : MonoBehaviour
{
    public Button[] buttons;
    public Text[] texts;

    public int currentPlayer = 0;
    private float coins;

    public GameObject golemWhitePaidButton;
    public GameObject golemWhiteButton;
    public GameObject golemWhiteCoinsButton;

    public GameObject golemBlackPaidButton;
    public GameObject golemBlackButton;
    public GameObject golemBlackCoinsButton;

    public GameObject menuManager;
    void Start()
    {
        coins = PlayerPrefsSafe.GetFloat("Coins");

        if(PlayerPrefsSafe.HasKey("CurrentPlayer"))
        {
            currentPlayer = PlayerPrefsSafe.GetInt("CurrentPlayer");
        }
        else
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 0);
        }

        if (PlayerPrefsSafe.GetInt("FallenAngelWhite") != 0)
            texts[1].text = "USE";
        if (PlayerPrefsSafe.GetInt("FallenAngelCorona") != 0)
            texts[2].text = "USE";
        if (PlayerPrefsSafe.GetInt("Goblin") != 0)
            texts[3].text = "USE";
        if (PlayerPrefsSafe.GetInt("Ogre") != 0)
            texts[4].text = "USE";
        if (PlayerPrefsSafe.GetInt("Orc") == 1)
            texts[5].text = "USE";
        if (PlayerPrefsSafe.GetInt("GolemWhite") == 1)
        {
            golemWhiteButton.SetActive(true);
            texts[6].text = "USE";
            Destroy(golemWhiteCoinsButton);
            Destroy(golemWhitePaidButton);
        }
        if (PlayerPrefsSafe.GetInt("GolemBlack") == 1)
        {
            golemBlackButton.SetActive(true);
            texts[7].text = "USE";
            Destroy(golemBlackCoinsButton);
            Destroy(golemBlackPaidButton);
        }

        for (int i = 0; i < texts.Length; i++)
            if (i == currentPlayer)
            {
                texts[i].text = "USING";
            }

        Change();
    }

    void Update()
    {
        //monetu
    }
    public void ReaperMan()
    {
        PlayerPrefsSafe.SetInt("CurrentPlayer", 0);
        texts[0].text = "USING";
        Change();
    } 

    public void FallenAngelWhite()
    {
        if(coins >= 5000 && PlayerPrefsSafe.GetInt("FallenAngelWhite") != 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 1);
            PlayerPrefsSafe.SetInt("FallenAngelWhite", 1);
            texts[1].text = "USING";
            PlayerPrefsSafe.SetFloat("Coins", coins - 5000);
            Change();
        }

        if(PlayerPrefsSafe.GetInt("FallenAngelWhite") == 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 1);
            texts[1].text = "USING";
            Change();
        }
        else if(coins < 5000)
        {
            menuManager.GetComponent<MainMenuScript>().NotEnoughMoney();
        }
    }

    public void FallenAngelCorona()
    {
        if (coins >= 20000 && PlayerPrefsSafe.GetInt("FallenAngelCorona") != 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 2);
            PlayerPrefsSafe.SetInt("FallenAngelCorona", 1);
            texts[2].text = "USING";
            PlayerPrefsSafe.SetFloat("Coins", coins - 20000);
            Change();
        }

        if (PlayerPrefsSafe.GetInt("FallenAngelCorona") == 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 2);
            texts[2].text = "USING";
            Change();
        }
        else if (coins < 20000)
        {
            menuManager.GetComponent<MainMenuScript>().NotEnoughMoney();
        }
    }

    public void Goblin()
    {
        if (coins >= 50000 && PlayerPrefsSafe.GetInt("Goblin") != 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 3);
            PlayerPrefsSafe.SetInt("Goblin", 1);
            texts[3].text = "USING";
            PlayerPrefsSafe.SetFloat("Coins", coins - 50000);
            Change();
        }

        if (PlayerPrefsSafe.GetInt("Goblin") == 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 3);
            texts[3].text = "USING";
            Change();
        }
        else if (coins < 50000)
        {
            menuManager.GetComponent<MainMenuScript>().NotEnoughMoney();
        }
    }

    public void Ogre()
    {
        if (coins >= 125000 && PlayerPrefsSafe.GetInt("Ogre") != 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 4);
            PlayerPrefsSafe.SetInt("Ogre", 1);
            texts[4].text = "USING";
            PlayerPrefsSafe.SetFloat("Coins", coins - 125000);
            Change();
        }

        if (PlayerPrefsSafe.GetInt("Ogre") == 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 4);
            texts[4].text = "USING";
            Change();
        }
        else if (coins < 125000)
        {
            menuManager.GetComponent<MainMenuScript>().NotEnoughMoney();
        }
    }

    public void Orc()
    {
        if (coins >= 250000 && PlayerPrefsSafe.GetInt("Orc") != 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 5);
            PlayerPrefsSafe.SetInt("Orc", 1);
            texts[5].text = "USING";
            PlayerPrefsSafe.SetFloat("Coins", coins - 250000);
            Change();
        }

        if (PlayerPrefsSafe.GetInt("Orc") == 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 5);
            texts[5].text = "USING";
            Change();
        }
        else if (coins < 250000)
        {
            menuManager.GetComponent<MainMenuScript>().NotEnoughMoney();
        }
    }

    public void GolemWhite()
    {
        if (coins >= 500000 && PlayerPrefsSafe.GetInt("GolemWhite") != 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 6);
            PlayerPrefsSafe.SetInt("GolemWhite", 1);
            golemWhiteButton.SetActive(true);
            texts[6].text = "USING";
            PlayerPrefsSafe.SetFloat("Coins", coins - 500000);
            Change();
            Destroy(golemWhiteCoinsButton);
            Destroy(golemWhitePaidButton);
        }

        else if (coins < 500000)
        {
            menuManager.GetComponent<MainMenuScript>().NotEnoughMoney();
        }
    }

    public void GolemWhitePaid()
    {
        if (PlayerPrefsSafe.GetInt("GolemWhite") != 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 6);
            PlayerPrefsSafe.SetInt("GolemWhite", 1);
            golemWhiteButton.SetActive(true);
            texts[6].text = "USING";
            Change();
            Destroy(golemWhiteCoinsButton);
            Destroy(golemWhitePaidButton);
        }
    }

    public void GolemWhiteNew()
    {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 6);
            texts[6].text = "USING";
            Change();
    }


    public void GolemBlack()
    {
        if (coins >= 1000000 && PlayerPrefsSafe.GetInt("GolemBlack") != 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 7);
            PlayerPrefsSafe.SetInt("GolemBlack", 1);
            golemBlackButton.SetActive(true);
            texts[7].text = "USING";
            PlayerPrefsSafe.SetFloat("Coins", coins - 1000000);
            Change();
            Destroy(golemBlackCoinsButton);
            Destroy(golemBlackPaidButton);
        }

        else if (coins < 1000000)
        {
            menuManager.GetComponent<MainMenuScript>().NotEnoughMoney();
        }
    }

    public void GolemBlackPaid()
    {
        if (PlayerPrefsSafe.GetInt("GolemBlack") != 1)
        {
            PlayerPrefsSafe.SetInt("CurrentPlayer", 7);
            PlayerPrefsSafe.SetInt("GolemBlack", 1);
            golemBlackButton.SetActive(true);
            texts[7].text = "USING";
            Change();
            Destroy(golemBlackCoinsButton);
            Destroy(golemBlackPaidButton);
        }
    }

    public void GolemBlackNew()
    {
        PlayerPrefsSafe.SetInt("CurrentPlayer", 7);
        texts[7].text = "USING";
        Change();
    }

    public void Change()
    {
        for (int i = 0; i < texts.Length; i++)
            if (i != PlayerPrefsSafe.GetInt("CurrentPlayer"))
                if (!texts[i].text.Contains('0'))
                    texts[i].text = "USE";
    }
}
