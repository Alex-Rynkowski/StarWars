using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] music;

    private AudioManager[] audioManagers;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioManagers = FindObjectsOfType<AudioManager>();

        for (int i = 0; i < audioManagers.Length; i++)
        {
            if (i > 0)
            {
                Destroy(audioManagers[i].gameObject);
            }
        }
    }

    public void MusicToPlay(int musicToPlay)
    {
        if (music[musicToPlay].isPlaying) return;

        StopMusic();

        for (int i = 0; i < music.Length; i++)
        {
            if (musicToPlay == i)
            {
                print("Song to play: " + musicToPlay);
                music[i].GetComponent<AudioSource>().Play();
            }
        }
    }

    private void StopMusic()
    {
        for (int i = 0; i < music.Length; i++)
        {
            music[i].GetComponent<AudioSource>().Stop();
        }
    }
}