using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    float gizRange = 0.3f;
    bool dashAllowance;

    public float runSpeed , jumpForce, fireForce,dashForce;
    float moveInput;
    bool facingRight = true;
    private Animator anim;
    Vector2 range = new Vector2 (0.18f,0.1f);
    public Transform groundCheck;
    public LayerMask groundLayer;
    Rigidbody2D myBody;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashAllowance = true;
    }


    // Shows a box around your player. 
    // Helps you in determening the range needed to allow for jumping/touching the ground. 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(groundCheck.position, gizRange);
    }
    

    void checkColForJump()
    {
        Collider2D bottomHit = Physics2D.OverlapBox(groundCheck.position, range, 0.0f, groundLayer);
        Debug.Log(bottomHit);
        if(bottomHit != null)
        {
            if(bottomHit.gameObject.tag == "Ground" && Input.GetKeyDown(KeyCode.Space))
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
                anim.SetBool("jumping", true);
            }
            else
            {
                anim.SetBool("jumping", false);
            }
        }

    }


    void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        movement();
        checkColForJump();
        canDash();
    }
    void movement()
    {

        moveInput = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Dealing with the running/idle animation.
        if (moveInput != 0)
        {
                anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        myBody.velocity = new Vector2(moveInput, myBody.velocity.y);
        if (moveInput > 0 && !facingRight || moveInput<0 && facingRight)
        {
            flip();
        }
    }
    void flip()
    {
        facingRight = !facingRight;
        Vector2 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }

    void canDash()
    {
        Debug.Log(dashAllowance);
         
        if (dashAllowance && Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("dashing");
            myBody.velocity = new Vector2(dashForce, myBody.velocity.y);
            Invoke("allowDashing", 2.0f);
            dashAllowance = false;
        }
    }
    void allowDashing()
    {
        dashAllowance = true;
    }
}
