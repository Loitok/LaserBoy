using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoScript : MonoBehaviour
{
    private float forceX, forceY;
    private int health = 9;
    private Rigidbody2D myBody;

    [SerializeField]
    private bool moveLeft, moveRight;

    [SerializeField]
    private GameObject originalBall;

    private GameObject ball1, ball2;
    private EnemyTwoScript ball1Script, ball2Script;

    private GameObject cam;
    private RipplePostProccesor camRipple;

    public GameObject arrow_3;
    private GameObject clone;

    private SpriteRenderer sr;

    private CameraShake shaker;
    public GameObject bloodSplash;

    public GameObject effect;
    private float h, k;

    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;
    private GameObject gameProcess;

    private AudioSource audioSource;
    private Animator anim;

    private GameObject player;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        camRipple = Camera.main.GetComponent<RipplePostProccesor>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        myBody = GetComponent<Rigidbody2D>();
        SetBallSpeed();
        moveRight = true;
        shaker = cam.GetComponent<CameraShake>();
        gameProcess = GameObject.Find("GameProcess");
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        if (this.gameObject.tag != "Smaller Ball")
        {
            if (transform.position.y > 0)
            {
                h = 5.8f;
                k = 4.5f;
            }
            else if (transform.position.y < 0)
            {
                h = -11.2f;
                k = -12.5f;
            }
            Vector2 pos = new Vector2(transform.position.x, k);
            clone = (GameObject)Instantiate(arrow_3, pos, Quaternion.identity);
        }
    }

    void Update()
    {
        MoveBall();
        if (clone && clone.transform.position.x > transform.position.x)
        {
            Vector3 temp = clone.transform.position;
            temp.x -= 2.5f * Time.deltaTime;
            clone.transform.position = temp;
        }
        else if (clone && clone.transform.position.x < transform.position.x)
        {
            Vector3 temp = clone.transform.position;
            temp.x += 2.5f * Time.deltaTime;
            clone.transform.position = temp;
        }

        if (transform.position.y < h)
        {
            Destroy(clone);
        }

        if(player)
            if (System.Math.Abs(transform.position.y - player.transform.position.y) > 15)
                Destroy(gameObject);
    }

    void InstantiateBalls()
    {
        if(this.gameObject.tag != "Smaller Ball")
        {
            ball1 = Instantiate(originalBall);
            ball2 = Instantiate(originalBall);

            ball1Script = ball1.GetComponent<EnemyTwoScript>();
            ball2Script = ball2.GetComponent<EnemyTwoScript>();
        }
    }
    void InitializeBallsAndTurnOffCurrentBall()
    {
        InstantiateBalls();

        Vector3 temp = transform.position;

        ball1.transform.position = temp;
        ball1Script.SetMoveLeft(true);

        ball2.transform.position = temp;
        ball2Script.SetMoveRight(true);

        ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f);
        ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f);
    }

    public void SetMoveLeft(bool canMoveLeft)
    {
        this.moveLeft = canMoveLeft;
        this.moveRight = !canMoveLeft;
    }
    public void SetMoveRight(bool canMoveRight)
    {
        this.moveRight = canMoveRight;
        this.moveLeft = !canMoveRight;
    }
    void MoveBall()
    {
        if(moveLeft)
        {
            Vector3 temp = transform.position;
            temp.x -= forceX * Time.deltaTime;
            transform.position = temp;
        }
        if (moveRight)
        {
            Vector3 temp = transform.position;
            temp.x += forceX * Time.deltaTime;
            transform.position = temp;
        }
    }
    void SetBallSpeed()
    {
        forceX = 2.5f;

        switch (this.gameObject.tag)
        {
            case "Largest Ball":
                forceY = Random.Range(8.5f, 10);
                break;
            case "Smaller Ball":
                forceY = Random.Range(7.8f, 9);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "map")
        {
            myBody.velocity = new Vector2(0, forceY);
            myBody.velocity = new Vector2(0, forceY);
            anim.SetBool("isDown", true);
            StartCoroutine(Waiting());
        }

        if(target.tag == "Right Wall")
        {
            SetMoveLeft(true);
        }
       
        if (target.tag == "Left Wall")
        {
            SetMoveRight(true);           
        }

        if(target.CompareTag("Player"))
        {
            camRipple.RippleEffect();
            KillSelf();
        }
        if (target.CompareTag("bullet"))
        {
            audioSource.Play();
            Destroy(target.gameObject);
            health--;
            if (this.gameObject.tag != "Smaller Ball")
            {
                gameProcess.GetComponent<SlidingNumbers>().AddToNumber(4);
            }
            else
            {
                gameProcess.GetComponent<SlidingNumbers>().AddToNumber(3);
            }
                sr.material = matWhite;
            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            if (health <= 0)
            {
                camRipple.RippleEffect();
                KillSelf();
                if(gameObject.tag != "Smaller Ball")
                {
                    InitializeBallsAndTurnOffCurrentBall();
                }
            }
            else
            {
                Invoke("ResetMaterial", .05f);
            }
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("isDown", false);
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }
    public void KillSelf()
    {
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        Instantiate(effect, transform.position, Quaternion.identity);
        shaker.CameraShakeFunction();
        Destroy(gameObject);
    }
}
