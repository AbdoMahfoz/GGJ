using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesScript : MonoBehaviour
{
    void OnCollisionEnter(Collision colInfo)
    {
        if (colInfo.collider.gameObject.tag == "Player")
        {
            Debug.Log("Death");
            // Fixed when 2D
        }
    }
}
