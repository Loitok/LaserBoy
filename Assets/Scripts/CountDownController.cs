using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;
    private AudioSource audioSource;

    public GameObject shavaCompany;

    private void Start()
    {
        shavaCompany.GetComponent<Animator>().SetTrigger("start");
        StartCoroutine(Wait());
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(CountdownToStart());
    }
    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            audioSource.Play();

            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        audioSource.Play();
        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        shavaCompany.SetActive(false);
    }
}
