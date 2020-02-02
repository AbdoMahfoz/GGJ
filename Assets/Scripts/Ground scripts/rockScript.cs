using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : StateChanger
{
    bool moving;
    Rigidbody2D myBody;
    Transform playerTransform;
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckIfRevertRequested();
        if (moving) return;
        if(Mathf.Abs(transform.position.x - playerTransform.position.x) <= 1.0f)
        {
            myBody.bodyType = RigidbodyType2D.Dynamic;
            myBody.simulated = true;
            moving = true;
        }
    }

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if(colInfo.collider.gameObject.tag == "Player")
        {
            StateChanger.RevertToDefaultState();
        }
    }

    protected override void RevertState()
    {
        myBody.bodyType = RigidbodyType2D.Kinematic;
        myBody.simulated = false;
        moving = false;
        transform.position = new Vector3(70.71196f, 6.009922f,0);
    }
}
