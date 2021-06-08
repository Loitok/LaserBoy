using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTouch : MonoBehaviour
{
    public void button_touch()
    {
        StartCoroutine(Button_touch());
    }
    public IEnumerator Button_touch()
    {
        GetComponent<Animation>().Play("Button_Touch");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }
}
