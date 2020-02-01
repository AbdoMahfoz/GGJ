using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float runSpeed , jumpForce;
    float jumpHeight = 0.5f;
    float moveInput;
    bool facingRight = true;
    private Animator anim;
    Vector2 range = new Vector2 (0.18f,0.05f);
    public Transform groundCheck;
    public LayerMask groundLayer;
    Rigidbody2D myBody;

    // Sound
    public AudioClip[] footStepsClips;
    public AudioClip jumpClip;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void checkColForJump()
    {
        Collider2D bottomHit = Physics2D.OverlapBox(groundCheck.position, range, 0.0f, groundLayer);
        if(bottomHit != null)
        {
            if(bottomHit.gameObject.tag == "Ground" && Input.GetKeyDown(KeyCode.Space))
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
                soundManager.instance.playSfx(jumpClip,0.3f);
                anim.SetBool("jumping", true);
            }
            else
            {
                anim.SetBool("jumping", false);
            }
        }
    }

    /* Helps you in determening the range needed to allow for jumping/touching the ground. 
    // Shows a box around your player. 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(groundCheck.position, range);
    }
    */

    void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        movement();
        checkColForJump();
    }
    void movement()
    {

        if (Input.GetKeyUp(KeyCode.Space)) // How far the player can jump.
        {
            if (myBody.velocity.y > 0)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, myBody.velocity.y * jumpHeight);
            }
        }
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
        if(myBody.velocity.y < 0)
        {
            anim.SetBool("falling", true);
        }
        else
        {
            anim.SetBool("falling", false);
        }
    }
    void flip()
    {
        facingRight = !facingRight;
        Vector2 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }

    void runningSound()
    {
        soundManager.instance.playRandSfx(footStepsClips);
    }
}
