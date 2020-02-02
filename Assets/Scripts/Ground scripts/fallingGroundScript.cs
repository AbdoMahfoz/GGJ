using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingGroundScript : StateChanger
{
    Rigidbody2D myBody;
    Transform startPos; 
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.bodyType = RigidbodyType2D.Kinematic;
        startPos.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfRevertRequested();
    }

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if(colInfo.collider.gameObject.tag == "Player")
        {
            StartCoroutine(fallingDown);
        }
    }
    IEnumerator fallingDown()
    {
        yield return new WaitForSeconds(1.0f);
        myBody.bodyType = RigidbodyType2D.Dynamic;

    }

    protected override void RevertState()
    {
        myBody.bodyType = RigidbodyType2D.Kinematic;
        transform.position = startPos.position;
    }
}
