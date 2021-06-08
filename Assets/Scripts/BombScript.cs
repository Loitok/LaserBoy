using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private UnityEngine.Object explosion;
    private GameObject instantiatedObj;
    private RipplePostProccesor camRipple;

    void Start()
    {
        explosion = Resources.Load("bombExplosion");
        camRipple = Camera.main.GetComponent<RipplePostProccesor>();
        Invoke("Detonate", 2);
    }

    void Detonate()
    {
        Destroy(gameObject);
        instantiatedObj = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
        camRipple.RippleEffect();
        Destroy(instantiatedObj, 0.5f);
    }

}
