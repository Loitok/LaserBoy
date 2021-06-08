using UnityEngine;
public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    [SerializeField]
    private GameObject gameOverUI;

    public GameObject ReaperMan; 
    public GameObject FallenAngelWhite;
    public GameObject FallenAngelCorona; 
    public GameObject Goblin;
    public GameObject Ogre; 
    public GameObject Orc;
    public GameObject GolemWhite; 
    public GameObject GolemBlack;

    public GameObject WhiteEffect;
    public GameObject BlackEffect;
    public GameObject GreenEffect;

    public GameObject pauseButton;
    private GameObject currentPlayer;
    private GameObject currentEffect;

    private AudioSource audioSource;

    private float curPlayer;
    public GameObject spawner;
    public GameObject spawnerDown;
    public Transform playerPosition;
    private GameObject player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        curPlayer = PlayerPrefsSafe.GetInt("CurrentPlayer");
        switch (curPlayer)
        {
            case 0: currentPlayer = ReaperMan; currentEffect = BlackEffect; break;
            case 1: currentPlayer = FallenAngelWhite; currentEffect = WhiteEffect; break;
            case 2: currentPlayer = FallenAngelCorona; currentEffect = WhiteEffect; break;
            case 3: currentPlayer = Goblin; currentEffect = GreenEffect; break;
            case 4: currentPlayer = Ogre; currentEffect = GreenEffect; break;
            case 5: currentPlayer = Orc; currentEffect = GreenEffect; break;
            case 6: currentPlayer = GolemWhite; currentEffect = WhiteEffect; break;
            case 7: currentPlayer = GolemBlack; currentEffect = BlackEffect; break;
        }

        Instantiate(currentPlayer, playerPosition.position, Quaternion.identity);
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player)
        {
            if (player.transform.position.y > -10)
            {
                spawnerDown.SetActive(false);
                spawner.SetActive(true);
            }
            else if (player.transform.position.y < -10)
            {
                spawnerDown.SetActive(true);
                spawner.SetActive(false);
            }
        }
    }
    public GameObject Effect()
    {
        return currentEffect;
    }

    public GameObject Player()
    {
        return currentPlayer;
    }
    public static void KillPlayer(PlayerController player)
    {
        Destroy(player.gameObject);
        gm.GameOver();
    }

    public void GameOver()
    {
        audioSource.Play();
        pauseButton.SetActive(false);
        spawner.SetActive(false);
        spawnerDown.SetActive(false);
        gameOverUI.SetActive(true);
        gameOverUI.GetComponent<GameOver>().Start();
    }
}
