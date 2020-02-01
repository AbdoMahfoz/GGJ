using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockScript : MonoBehaviour
{
    bool moving;
    Rigidbody myBody;
    Transform playerTransform;
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (moving) return;
        if(Mathf.Abs(transform.position.x - playerTransform.position.x) <= 3.0f)
        {
            myBody.isKinematic = false;
            moving = true;
        }
    }

    void OnCollisionEnter(Collision colInfo)
    {
        if(colInfo.collider.gameObject.tag == "Player")
        {
            Destroy(colInfo.collider.gameObject);
        }
    }
}
