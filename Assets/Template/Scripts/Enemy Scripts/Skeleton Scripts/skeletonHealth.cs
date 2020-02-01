using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonHealth : MonoBehaviour
{
    public int health = 150;
    Animator anim;
    Rigidbody2D myBody;
    void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (transform.position.y <= -5) gameObject.tag = "Dead";
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        FindObjectOfType<cameraShaking>().shakeItLow();
        if (health <= 0)
        {
            anim.SetBool("death", true);
            gameObject.tag = "Dead";
        }
        else
        {
            myBody.velocity = new Vector2(0, 0); // Needs some modification.
            anim.SetBool("areHit", true);
            Invoke("resetAnimation", 1.0f);
        }
    }

    void resetAnimation()
    {
        anim.SetBool("areHit", false);
    }
}
