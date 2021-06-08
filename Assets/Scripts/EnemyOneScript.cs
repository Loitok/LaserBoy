using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneScript : MonoBehaviour
{
    private GameObject player;

    private float forceX = 1.5f, forceY;
    public int health = 10;

    public GameObject effect;
    public GameObject bloodSplash;

    public GameObject arrow_2;
    private GameObject clone;

    [SerializeField]
    private bool moveLeft, moveRight;

    private GameObject cam;
    private RipplePostProccesor camRipple;

    private Material matWhite;
    private Material matDefault;
    private UnityEngine.Object explosionRef;

    private CameraShake shaker;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private GameObject gameProcess;

    private UnityEngine.Object explosion;
    private GameObject instantiatedObj;

    private float h, k;

    [SerializeField]
    private GameObject originalBall;

    private GameObject ball1, ball2;
    private EnemyOneScript ball1Script, ball2Script;

    private AudioSource audioSource;
    public AudioClip secondSound;

    private Animator anim;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        camRipple = Camera.main.GetComponent<RipplePostProccesor>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        gameProcess = GameObject.Find("GameProcess");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        shaker = cam.GetComponent<CameraShake>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
        player = GameObject.FindGameObjectWithTag("Player");
        explosion = Resources.Load("Boom");
        forceY = Random.Range(5.6f, 6.5f);
        if (this.gameObject.tag != "One Small Ball")
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
            clone = (GameObject)Instantiate(arrow_2, pos, Quaternion.identity);
            anim = GetComponent<Animator>();
            health = 13;
        }
    }
    void Update()
    {
        if (this.gameObject.tag != "One Small Ball")
        {
            if (player)
            {
                if (player.transform.position.x - transform.position.x >= 0.1)
                {
                    Vector3 temp = transform.position;
                    temp.x += forceX * Time.deltaTime;
                    transform.position = temp;
                }
                else if (transform.position.x - player.transform.position.x >= 0.1)
                {
                    Vector3 temp = transform.position;
                    temp.x -= forceX * Time.deltaTime;
                    transform.position = temp;
                }
            }

            if (clone && clone.transform.position.x > transform.position.x)
            {
                Vector3 temp = clone.transform.position;
                temp.x -= 1.5f * Time.deltaTime;
                clone.transform.position = temp;
            }
            else if (clone && clone.transform.position.x < transform.position.x)
            {
                Vector3 temp = clone.transform.position;
                temp.x += 1.5f * Time.deltaTime;
                clone.transform.position = temp;
            }

            if (transform.position.y < h)
            {
                Destroy(clone);
            }
        }
        else MoveBall();

        if (player)
            if (System.Math.Abs(transform.position.y - player.transform.position.y) > 15)
                Destroy(gameObject);
    }

    void InstantiateBalls()
    {
        if (this.gameObject.tag != "One Small Ball")
        {
            ball1 = Instantiate(originalBall);
            ball2 = Instantiate(originalBall);

            ball1Script = ball1.GetComponent<EnemyOneScript>();
            ball2Script = ball2.GetComponent<EnemyOneScript>();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "One Small Ball")
        {
            if (collision.tag == "map")
            {
                Destroy(gameObject);
                instantiatedObj = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(secondSound);
                Destroy(instantiatedObj, 0.5f);
            }

            if (collision.tag == "Right Wall")
            {
                Destroy(gameObject);
                instantiatedObj = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(secondSound);
                Destroy(instantiatedObj, 0.5f);
            }

            if (collision.tag == "Left Wall")
            {
                Destroy(gameObject);
                instantiatedObj = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(secondSound);
                Destroy(instantiatedObj, 0.5f);
            }
        }

        if (collision.CompareTag("Player"))
        {
            if (this.gameObject.tag != "One Small Ball")
                KillSelf();
            else if (this.gameObject.tag == "One Small Ball")
            {                
                Destroy(gameObject);
                instantiatedObj = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(secondSound);
                Destroy(instantiatedObj, 0.5f);
            }
        }

        if (collision.CompareTag("bullet"))
        {
            audioSource.Play();
            Destroy(collision.gameObject);
            health--;
            if (this.gameObject.tag != "One Small Ball")
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
                KillSelf();
                if (gameObject.tag != "One Small Ball")
                {
                    InitializeBallsAndTurnOffCurrentBall();
                }
            }
            else
            {
                Invoke("ResetMaterial", .05f);
            }
        }

        if (collision.tag == "map")
        {
            if (this.gameObject.tag != "One Small Ball")
            {
                rb.velocity = new Vector2(0, forceY);
                anim.SetBool("isDown", true);
                StartCoroutine(Waiting());
            }
        }
    }
    void ResetMaterial()
    {
        sr.material = matDefault;
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("isDown", false);
    }
    public void KillSelf()
    {
        camRipple.RippleEffect();
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        Instantiate(effect, transform.position, Quaternion.identity);
        shaker.CameraShakeFunction();
        Destroy(gameObject);
    }
}
