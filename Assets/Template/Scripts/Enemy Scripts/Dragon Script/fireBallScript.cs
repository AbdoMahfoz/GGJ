using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallScript : MonoBehaviour
{
    Rigidbody2D myBody;
    Vector2 playerPos;

    public float force = 150;
    
    //Sound
    public AudioClip fireBallClip;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        shootPlayer();
        Destroy(gameObject, 1.75f);
    }

    void shootPlayer()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position; // Could use game object normally. 

        Vector2 heading = (playerPos - (Vector2)transform.position);
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;

        myBody.AddForce(direction * force);
        float angle = Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle); // the rotation on Z;
        soundManager.instance.playSfx(fireBallClip, 0.4f);
    }
}
