using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallenGround : MonoBehaviour
{
    Animator anim;
    Rigidbody myBody;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision colInfo)
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
