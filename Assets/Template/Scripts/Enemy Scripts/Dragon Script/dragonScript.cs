using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonScript : MonoBehaviour
{
    public float speed, bounds;
    float coolDown;
    bool moveUp,isDead;

    public GameObject fireBall;
    public Transform fireBallPos;
    Animator anim;
    Transform player;

    Rigidbody2D myBody;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Moving offsets for the dragon.
        speed = 0.01f;
        bounds = transform.position.y;     
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        if (Vector2.Distance(player.position, transform.position)<=4 && coolDown == 0)
        {
            coolDown = 2.5f;
            anim.SetTrigger("attack");
        }
        else if (Vector2.Distance(player.position, transform.position) > 4)
        {
            coolDown = 1.5f;
        }
        movement();
        coolDownTimer();
    }

    void attack()
    {
        Instantiate(fireBall, fireBallPos.position, Quaternion.identity);
    }

    void coolDownTimer()
    {
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        if (coolDown < 0)
        {
            coolDown = 0;   
        }
    }

    void death()
    {
        myBody.isKinematic = false;
        isDead = true;
    }

    void OnColliderEnter2D(Collider2D target) // Could use OnCollisionEnter2D
    {
        if(target.tag == "Ground")
        {
            myBody.isKinematic = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void movement()
    {
        if(moveUp)
        {
            Vector2 pos = transform.position;
            pos.y += speed;
            transform.position = pos;
            if(transform.position.y > bounds+0.13)
            {
                moveUp = false;
            }

        }
        else
        {
            Vector2 pos = transform.position;
            pos.y -= speed;
            transform.position = pos;
            if (transform.position.y < bounds - 0.13)
            {
                moveUp = true;
            }   
        }

        if (transform.position.x < player.position.x) transform.localScale= new Vector2(1,1);
        if (transform.position.x > player.position.x) transform.localScale = new Vector2(-1, 1);
    }
}
