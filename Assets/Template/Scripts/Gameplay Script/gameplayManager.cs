using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameplayManager : MonoBehaviour
{
    // Deals with managing the Levels.

    GameObject[] Enemies;
    bool levelComp;

    // Update is called once per frame
    void Update()
    {
        if(levelComp) Invoke("replayLevel", 5.0f);
        Enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        if(Enemies.Length < 1  && !levelComp)
        {
            levelComp = true;
            Enemies = GameObject.FindGameObjectsWithTag("Dead");
            Debug.Log("Congratulations." + " You have defeated: "+ Enemies.Length +" enemies.");
        }
    }

    void replayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
