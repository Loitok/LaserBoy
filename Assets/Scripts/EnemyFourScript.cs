using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFourScript : MonoBehaviour
{
    private float forceX, forceY;
    private int health = 13;
    private Rigidbody2D myBody;

    public GameObject effect;
    [SerializeField]
    private bool moveLeft, moveRight;

    public GameObject arrow_4;
    private GameObject clone;

    private RipplePostProccesor camRipple;

    private GameObject cam;
    private CameraShake shaker;
    public GameObject bloodSplash;

    private SpriteRenderer sr;

    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;
    private int f;
    private float h,k;
    private GameObject gameProcess;
    private AudioSource audioSource;
    private Animator anim;
    private GameObject player;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camRipple = Camera.main.GetComponent<RipplePostProccesor>();
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
        if (transform.position.y > 0)
        {
            h = 5.8f;
            k = 4.5f;
        }
        else if(transform.position.y < 0)
        {
            h = -11.2f;
            k = -12.5f;
        }
        Vector2 pos = new Vector2(transform.position.x, k);
        clone = (GameObject)Instantiate(arrow_4, pos, Quaternion.identity);
    }

    void Update()
    {
        f = Random.Range(0, 2);
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
         
        if(transform.position.y < h)
        {
            Destroy(clone);
        }
        if(player)
            if (System.Math.Abs(transform.position.y - player.transform.position.y) > 15) 
                Destroy(gameObject);
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
        if (moveLeft)
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
        forceY = Random.Range(6.3f, 7.6f);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "map")
        {
            myBody.velocity = new Vector2(0, forceY);
            anim.SetBool("isDown", true);
            StartCoroutine(Waiting());
        }

        if (target.tag == "Right Wall")
        {
            SetMoveLeft(true);
        }

        if (target.tag == "Left Wall")
        {
            SetMoveRight(true);
        }

        if (target.tag == "Line")
        {
            if (f == 0)
            {
                SetMoveLeft(true);
            }
            else
            {
                SetMoveRight(true);
            }
            
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
            gameProcess.GetComponent<SlidingNumbers>().AddToNumber(4);
            sr.material = matWhite;
            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            if (health <= 0)
            {
                camRipple.RippleEffect();
                KillSelf();
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
