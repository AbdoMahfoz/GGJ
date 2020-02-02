using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tireScript : StateChanger
{
    bool moving;
    Rigidbody2D myBody;
    Transform playerTransform;
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        CheckIfRevertRequested();
        if (moving) return;
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) <= 1.0f)
        {
            myBody = gameObject.AddComponent<Rigidbody2D>();
            moving = true;
        }
    }

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.gameObject.tag == "Player")
        {
            StateChanger.RevertToDefaultState();
        }
    }

    protected override void RevertState()
    {
        DestroyImmediate(myBody);
        moving = false;
        transform.position = new Vector3(70.71196f, 6.009922f, 0);
    }
}
