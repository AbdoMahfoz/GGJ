using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class continueMoving : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "Player")
        {
            player.GetComponent<playerController>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(4, 0);
            Invoke("loadEnding", 3.0f);
        }
    }
    void loadEnding()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
