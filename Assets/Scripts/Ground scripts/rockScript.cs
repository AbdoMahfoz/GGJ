using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : MonoBehaviour
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
        if (moving) return;
        if(Mathf.Abs(transform.position.x - playerTransform.position.x) <= 1.0f)
        {
            myBody.bodyType = RigidbodyType2D.Dynamic;
            moving = true;
        }
    }

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if(colInfo.collider.gameObject.tag == "Player")
        {
            Destroy(colInfo.collider.gameObject);
        }
    }
}
