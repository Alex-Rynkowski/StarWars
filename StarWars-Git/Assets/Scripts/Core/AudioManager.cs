using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] music;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySong(int songToPlayer)
    {
        for (int i = 0; i < music.Length; i++)
        {
            if (music[i].isPlaying)
            {
                break;
            }
            
            music[i].Stop();
            if (songToPlayer == i)
            {
                music[i].Play();
            }
        }
    }
}