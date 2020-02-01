using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    float gizRange;
    bool dashAllowance,isDashing;

    float dashAnimationTiming;

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
        dashAnimationTiming = 0.2f;
        gizRange = 0.3f;
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
        // dealing with dashing.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (dashAllowance)
            {
                dash();
                anim.SetTrigger("dashing");
                Invoke("endDash", dashAnimationTiming);
            }
        }
        if (isDashing)
        {
            if (facingRight)
            {
                myBody.velocity = new Vector2(dashForce, 0);
            }
            else
                myBody.velocity = new Vector2(-dashForce, 0);

            if (dashAllowance)
            {
                dashAllowance = false;
                Invoke("allowDashing", 2.0f);
            }
        }

        // Dealing with falling animation.
        if (myBody.velocity.y<0) anim.SetBool("falling", true);
        else anim.SetBool("falling", false);
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

    
    void allowDashing()
    {
        dashAllowance = true;
    }

    void dash()
    {
        isDashing = true;
    }
    void endDash()
    {
        isDashing = false;
        Invoke("allowDashing", 1.0f);
    }
}
