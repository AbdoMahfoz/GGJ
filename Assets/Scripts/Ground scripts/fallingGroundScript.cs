using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallenGround : MonoBehaviour
{
    Animator anim;
    Rigidbody2D myBody;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if(colInfo.collider.gameObject.tag == "Player")
        {
            // Fixed when 2D
            anim.SetBool("falling", true);
            Invoke("fallDown", 1.0f);
        }
    }
    void fallDown()
    {
        anim.SetBool("falling", false);
        myBody.isKinematic = true;
    }
}
