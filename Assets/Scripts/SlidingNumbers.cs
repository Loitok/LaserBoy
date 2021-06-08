using UnityEngine;
using UnityEngine.UI;

public class SlidingNumbers : MonoBehaviour
{
    public Text numberText;
    public Text coinsText;
    public float animationTime = 1.5f;

    private float desiredNumber;
    private float initialNumber;
    private float currentNumber;

    private float desiredNumberCoins;
    private float initialNumberCoins;
    private float currentNumberCoins;

    private GameObject player;

    private int score = 0;
    private float coins;
    private int highScore;

    public GameObject spawner;

    void Start()
    {
        highScore = PlayerPrefsSafe.GetInt("HighScore", highScore);
        coins = PlayerPrefsSafe.GetFloat("Coins");
        SetNumberCoins(coins);
        coinsText.text = "" + PlayerPrefsSafe.GetFloat("Coins");
        Change();
    }
    public void SetNumber(float value)
    {
        initialNumber = currentNumber;
        desiredNumber = value;
    }

    public void SetNumberCoins(float valueCoins)
    {

        initialNumberCoins = currentNumberCoins;
        desiredNumberCoins = valueCoins;
    }

    public void AddToNumber(int value)
    {
        initialNumber = currentNumber;
        desiredNumber += value;
        score += value;

        initialNumberCoins = currentNumberCoins;
        desiredNumberCoins += value;
        coins += value;
    }
    public void Update()
    {
            if (currentNumber != desiredNumber)
            {
                if (initialNumber < desiredNumber)
                {
                    currentNumber += (animationTime * Time.deltaTime) * (desiredNumber - initialNumber);
                    if (currentNumber >= desiredNumber)
                        currentNumber = desiredNumber;
                }
                else
                {
                    currentNumber -= (animationTime * Time.deltaTime) * (initialNumber - desiredNumber);
                    if (currentNumber <= desiredNumber)
                        currentNumber = desiredNumber;
                }

                numberText.text = currentNumber.ToString("0");
            }

            if (currentNumberCoins != desiredNumberCoins)
            {
                if (initialNumberCoins < desiredNumberCoins)
                {
                    currentNumberCoins += (animationTime * Time.deltaTime) * (desiredNumberCoins - initialNumberCoins);
                    if (currentNumberCoins >= desiredNumberCoins)
                        currentNumberCoins = desiredNumberCoins;
                }
                else
                {
                    currentNumberCoins -= (animationTime * Time.deltaTime) * (initialNumberCoins - desiredNumberCoins);
                    if (currentNumberCoins <= desiredNumberCoins)
                        currentNumberCoins = desiredNumberCoins;
                }

                coinsText.text = currentNumberCoins.ToString("0");
            }
        Change();

        PlayerPrefsSafe.SetFloat("Coins", coins);
        PlayerPrefsSafe.SetInt("Score", score);
        if(highScore < score)
        {            
            PlayerPrefsSafe.SetInt("HighScore", score);
            highScore = score;
            //HERE CREATE LEADERBOARD
        }
    }

    public void Change()
    {
        if (currentNumber >= 200 && currentNumber <= 600)
        {
            spawner.GetComponent<Spawner>().maxSec = 8;
            spawner.GetComponent<Spawner>().minSec = 5;
        }
        else if (currentNumber >= 600 && currentNumber <= 1000)
        {
            spawner.GetComponent<Spawner>().maxSec = 7;
            spawner.GetComponent<Spawner>().minSec = 5;
        }
        else if (currentNumber >= 1000 && currentNumber <= 1400)
        {
            spawner.GetComponent<Spawner>().maxSec = 7;
            spawner.GetComponent<Spawner>().minSec = 4;
        }
        else if (currentNumber >= 1400 && currentNumber <= 2000)
        {
            spawner.GetComponent<Spawner>().maxSec = 6;
            spawner.GetComponent<Spawner>().minSec = 3;
        }
        else if (currentNumber >= 2000)
        {
            spawner.GetComponent<Spawner>().maxSec = 5;
            spawner.GetComponent<Spawner>().minSec = 3;
        }
    }

}
