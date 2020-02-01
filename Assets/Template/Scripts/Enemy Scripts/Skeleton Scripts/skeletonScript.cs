using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    float minX, maxX;
    public float distance;
    int direction;
    
    Rigidbody2D myBody;
    Animator anim;

    bool patrol;
    bool detect = false;
    Transform playerPosition;

    [Header("Attack")]
    public Transform attackPos;
    public float attackRange;
    public LayerMask playerLayer;
    public int damage;


    // Sound
    public AudioClip axeSwingClip;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
        playerPosition = GameObject.Find("Player").transform;
        if (Random.value > 0.5) direction = 1;
        else direction = -1;
    }

    void Start()
    {
        maxX = transform.position.x + distance / 2;
        minX = maxX - distance;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, playerPosition.position) <= 1.5f)
        {
            patrol = false;
        }
        else
        {
            patrol = true;
        }

    }

    void FixedUpdate()
    {

        if (anim.GetBool("death"))
        {
            myBody.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            myBody.isKinematic = true;
            anim.SetBool("attack", false);  
            return;
        }

        if (myBody.velocity.x > 0)
        {
            transform.localScale = new Vector2(1.3f, transform.localScale.y);
            anim.SetBool("attack", false);
        }
        else
        {
            transform.localScale = new Vector2(-1.3f, transform.localScale.y);
            anim.SetBool("attack", false);
        }

        if (patrol)
        { 
            detect = false;
            anim.SetBool("detect", false);
            switch (direction)
            {
                case -1:
                    if (transform.position.x > minX)
                        myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
                    else
                        direction = 1;
                    break;
                case 1:
                    if (transform.position.x < maxX)
                        myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
                    else
                        direction = -1;
                    break;
            }
        }
        else
        {
            //Attack
            if(Vector2.Distance(playerPosition.position, transform.position) >= 0.25f)
            {
                if (detect == false)
                {
                    detect = true;
                    anim.SetBool("detect", true);
                }

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("detect"))
                     return;
 
                Vector2 playerDirection = (playerPosition.position - transform.position).normalized;
                if(playerDirection.x > 0)
                {
                    myBody.velocity = new Vector2(moveSpeed + 0.4f, myBody.velocity.y);
                }
                else
                {
                    myBody.velocity = new Vector2(-(moveSpeed + 0.4f), myBody.velocity.y);
                }
            }
            else if (Vector2.Distance(playerPosition.position, transform.position) <= 0.20f)
            {
                detect = false;
                anim.SetBool("detect", false);
                myBody.velocity = new Vector2(0, myBody.velocity.y);
                if (playerPosition.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                }   
                anim.SetBool("attack", true);
            }
        }
    }

    public void attack()
    {
        myBody.velocity = new Vector2(0, myBody.velocity.y);
        Collider2D attackPlayer = Physics2D.OverlapCircle(attackPos.position, attackRange, playerLayer); 
        if (attackPlayer!= null)
        {
            soundManager.instance.playSfx(axeSwingClip, 0.25f);
            if (attackPlayer.tag == "Player")
            {
                attackPlayer.gameObject.GetComponent<playerHealth>().TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}


