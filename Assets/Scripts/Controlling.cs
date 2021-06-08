using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlling : MonoBehaviour
{
    private GameObject player;
    private GameObject leftButton;
    private GameObject rightButton;
    private GameObject fireButton;

    private bool moveLeft;
    private bool dontMove;
    private bool dontFire = true;
    private bool facingRight;

    private float speed;
    private Animator anim;
    private Rigidbody2D rb;

    private float timeBetweenShots;
    private float timestamp;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fireButton = GameObject.FindGameObjectWithTag("fire");
        speed = player.GetComponent<PlayerController>().speed;
        anim = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
        facingRight = true;
        dontMove = true;
        timeBetweenShots = player.GetComponent<PlayerController>().timeBetweenShots;
    }

    public void HandleMovement()
    { 
        if (player)
        {
            if (dontMove)
                StopMoving();
            else
            {
                if (moveLeft && facingRight)
                {
                    facingRight = !facingRight;
                    Vector3 theScale = player.transform.localScale;
                    theScale.x *= -1;
                    player.transform.localScale = theScale;
                    MoveLeft();
                }
                else if (moveLeft && !facingRight)
                {
                    MoveLeft();
                }
                else if (!moveLeft && facingRight)
                {
                    MoveRight();
                }
                else if (!moveLeft && !facingRight)
                {
                    facingRight = !facingRight;
                    Vector3 theScale = player.transform.localScale;
                    theScale.x *= -1;
                    player.transform.localScale = theScale;
                    MoveRight();
                }
            }
        }
    }

    public void OnFire()
    {
        if (Time.time >= timestamp && fireButton.GetComponent<Image>().overrideSprite == fireButton.GetComponent<Button>().spriteState.pressedSprite && !dontFire)
        {
            player.GetComponent<PlayerController>().OnFire();
            timestamp = Time.time + timeBetweenShots;
        }
    }
    public void AllowMovement(bool movement)
    {
        dontMove = false;
        moveLeft = movement;
    }

    public void DontAllowMovement()
    {
        dontMove = true;
    }

    public void AllowShooting()
    {
        dontFire = false;
    }

    public void DontAllowShooting()
    {
        dontFire = true;
    }

    public void MoveLeft()
    {
        if (gameObject)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            anim.SetBool("isRunning", true);
        }
    }

    public void MoveRight()
    {
        if (gameObject)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            anim.SetBool("isRunning", true);
        }
    }

    public void StopMoving()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        anim.SetBool("isRunning", false);
    }
    
}
