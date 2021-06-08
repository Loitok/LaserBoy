using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private Animator transitionAnim;

    void Start()
    {
        transitionAnim = GetComponent<Animator>();
    }

    public void StartTransition()
    {
        transitionAnim.SetTrigger("start");
    }

    public void EndTransition()
    {
        transitionAnim.SetTrigger("end");
    }
}
