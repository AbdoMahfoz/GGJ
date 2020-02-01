using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarScript : MonoBehaviour
{

    public int health;
    public int damage;
    Collider myCol;
    void Awake()
    {
        health = 100;
        myCol = GetComponent<Collider>();
    }

    void Update()
    {
        if (health <= 20)
        {
            myCol.enabled = false;
        }
        else if (health <= 60)
        {
            myCol.transform.position = new Vector3(myCol.transform.position.x, 0.6f, myCol.transform.position.z);
        }
    }

    void OnCollisionEnter(Collision colInfo)
    {
        // Fix in 2D
        if (colInfo.collider.gameObject.tag == "Player")
        {
            health -= damage;
            //Debug.Log(health);
        }
    }
}
