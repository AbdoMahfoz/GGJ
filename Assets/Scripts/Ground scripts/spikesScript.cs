using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesScript : MonoBehaviour
{

    Animator anim;

    void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.gameObject.tag == "Player")
        {
            //anim.SetBool("death",true);
            StateChanger.RevertToDefaultState();        
        }
    }
}
