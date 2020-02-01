using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager instance; // To access it everywhere 

    public AudioSource sfxMgr, sfxMgr2;
    public AudioSource themeSongMgr;

    public AudioClip[] themeSongs;


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
            themeSongMgr.clip = themeSongs[Random.Range(0, themeSongs.Length)];
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
    public void playRandSfx(AudioClip[] audioClips)
    {
        if (!sfxMgr.isPlaying)
        {
            sfxMgr.clip = audioClips[Random.Range(0,audioClips.Length)];
            sfxMgr.volume = Random.Range(0.4f,0.9f);
            sfxMgr.Play();
        }
        /*else
        {
            sfxMgr.clip = audioClips[Random.Range(0, audioClips.Length)];
            sfxMgr.volume = Random.Range(0.3f, 0.6f);
            sfxMgr2.Play();
        }*/
    }
}
