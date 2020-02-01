using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.gameObject.tag == "Player")
        {
            Destroy(colInfo.collider.gameObject);
        }
    }
}
