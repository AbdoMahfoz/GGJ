using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingDeathPos : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "Player")
        StateChanger.RevertToDefaultState();
    }
}
