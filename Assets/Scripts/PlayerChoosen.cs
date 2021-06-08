using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoosen : MonoBehaviour
{
    public GameObject ReaperMan;
    public GameObject FallenAngelWhite;
    public GameObject FallenAngelCorona;
    public GameObject Goblin;
    public GameObject Ogre;
    public GameObject Orc;
    public GameObject GolemWhite;
    public GameObject GolemBlack;

    public GameObject currentPlayer;
    private float curPlayer;

    static bool loaded;
    void Awake()
    {
        if (!loaded)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);

        loaded = true;
    }
    public GameObject CurrentPlayer()
    {
        curPlayer = PlayerPrefs.GetInt("CurrentPlayer");
        switch(curPlayer)
        {
            case 0: currentPlayer = ReaperMan; break;
            case 1: currentPlayer = FallenAngelWhite; break;
            case 2: currentPlayer = FallenAngelCorona; break;
            case 3: currentPlayer = Goblin; break;
            case 4: currentPlayer = Ogre; break;
            case 5: currentPlayer = Orc; break;
            case 6: currentPlayer = GolemWhite; break;
            case 7: currentPlayer = GolemBlack; break;
        }
        return currentPlayer;
    }

    
        

}
