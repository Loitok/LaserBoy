using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private GameObject player;
    public GameObject effect;
    public Vector2 spawningPlayer;
    private AudioSource audioSource;
    public AudioClip portalAudioClip;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(portalAudioClip);
        StartCoroutine(Destroy());
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.1f);   
        player.transform.position = spawningPlayer;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(4.2f);
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
