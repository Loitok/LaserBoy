using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject effect;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Destroy());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(4.2f);
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
