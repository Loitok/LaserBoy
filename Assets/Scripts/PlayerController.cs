using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool rightBullet = true;

    private AudioSource audioSource;
    public AudioClip secondSound;
    public AudioClip poisonSound;

    public int health;
    public float timeBetweenShots = 0.3333f;

    private GameObject healthBarSprite;

    public GameObject bulletRef;

    private GameObject effect;

    public Transform leftEye, rightEye;

    public HealthBar healthBar;

    HealthSystem healthSystem;

    private GameObject gm;

    private GameObject controlling;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM");
        audioSource = GetComponent<AudioSource>();
        healthBarSprite = GameObject.FindGameObjectWithTag("HealthBar");
        healthSystem = new HealthSystem(health);
        healthBar.Setup(healthSystem);
        controlling = GameObject.FindGameObjectWithTag("control");
    }

    void Update()
    {
        if (gameObject)
        {
            controlling.GetComponent<Controlling>().HandleMovement();
            controlling.GetComponent<Controlling>().OnFire();
        }
    }

    public void OnFire()
    {
        GameObject bullet = (GameObject)Instantiate(bulletRef);
        if (rightBullet)
        {
            bullet.transform.position = new Vector3(rightEye.position.x, rightEye.position.y, rightEye.position.z);
            audioSource.Play();
            rightBullet = false;
        }
        else
        {
            bullet.transform.position = new Vector3(leftEye.position.x, leftEye.position.y, leftEye.position.z);
            audioSource.Play();
            rightBullet = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") || collision.CompareTag("Largest Ball"))
        {
            healthSystem.Damage(50);
            DamagePlayer(50);
            audioSource.PlayOneShot(secondSound);
        }

        if (collision.CompareTag("Smaller Ball"))
        {
            healthSystem.Damage(20);
                DamagePlayer(20);
            audioSource.PlayOneShot(secondSound);
        }

        if(collision.CompareTag("One Small Ball"))
        {
            healthSystem.Damage(15);
            DamagePlayer(15);
        }

        if (collision.CompareTag("bomb"))
        {
            healthSystem.Damage(10);
            DamagePlayer(10);
        }

        if(collision.CompareTag("Poison"))
        {
            healthSystem.Damage(-15);
            DamagePlayer(-15);
            audioSource.PlayOneShot(poisonSound);
                
        }
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            effect = gm.GetComponent<GameMaster>().Effect();
            Instantiate(effect, transform.position, Quaternion.identity);
            GameMaster.KillPlayer(this);
        }
    }

}

