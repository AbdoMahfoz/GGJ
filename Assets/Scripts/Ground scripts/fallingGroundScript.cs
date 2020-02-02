using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingGroundScript : StateChanger
{
    //GameObject startObj;
    Rigidbody2D myBody;
    Vector3 startPos;
    bool loading;
    // Start is called before the first frame update
    void Start()
    {

        myBody = GetComponent<Rigidbody2D>();
        myBody.bodyType = RigidbodyType2D.Kinematic;
        startPos = transform.position;
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
            if (!loading)
            {
                loading = true;
                Invoke(nameof(fallingDown), 1.0f);
            }

        }
    }
    void fallingDown()
    {
        if(loading)
        {
            myBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    protected override void RevertState()
    {
        myBody.isKinematic = true;
        myBody.velocity = Vector3.zero;
        loading = false;
        transform.position = startPos;
    }
}
