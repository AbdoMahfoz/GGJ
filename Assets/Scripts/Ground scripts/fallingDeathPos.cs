using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingDeathPos : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D traget)
    {
        StateChanger.RevertToDefaultState();
    }
}
