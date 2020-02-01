using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public float speed = 0.05f;
    bool facingRight;

    public AudioClip arrowHitClip;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            facingRight = true;
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            facingRight = false;
        }
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
        {
            transform.Translate(Vector2.right * speed);
        }
        else
            transform.Translate(Vector2.left * speed);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Enemy")
        {
            Destroy(gameObject);
            target.GetComponent<skeletonHealth>().takeDamage(20);
            soundManager.instance.playSfx(arrowHitClip, 0.4f);
        }
    }
}
