using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWall : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "FireBall")
        {
            int fireAttr = MechanicsUpdater.GetValueOf("Fire");

        }
    }
}
