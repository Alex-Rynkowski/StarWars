using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject spaceShip;
    [SerializeField] private int playSong;

    private void Start()
    {
        //FindObjectOfType<AudioManager>().MusicToPlay(playSong);
    }

    private void LateUpdate()
    {
        this.transform.position = spaceShip.transform.position;
    }
}