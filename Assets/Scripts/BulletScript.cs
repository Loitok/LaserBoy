using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroySelf", 1f);
    }

    void FixedUpdate()
    {
        rb.AddForce(new Vector2(0, 30));
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

}
