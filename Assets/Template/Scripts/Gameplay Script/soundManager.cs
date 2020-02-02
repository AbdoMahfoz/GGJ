using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager instance; // To access it everywhere 

    public AudioSource sfxMgr, sfxMgr2;
    public AudioSource themeSongMgr;

    public AudioClip themeSong;


    void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(!themeSongMgr.isPlaying)
        {
            themeSongMgr.clip = themeSong;
            themeSongMgr.Play();
        }
    }

    public void playSfx(AudioClip clip,float vol)
    {
        if (!sfxMgr.isPlaying)
        {
            sfxMgr.clip = clip;
            sfxMgr.volume = vol;
            sfxMgr.Play();
        }
        else
        {
            sfxMgr2.clip = clip;
            sfxMgr2.volume = vol;
            sfxMgr2.Play();
        }
    }
}
