using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Text countDownText;
    [SerializeField] float countDown;
    
    [Header("Components")] 
    SpaceShipController spaceShipController;
    Container container;

    private void Start()
    {
        spaceShipController = FindObjectOfType<SpaceShipController>();
        container = FindObjectOfType<Container>();
    }

    private void Update()
    {
        scoreText.text = string.Format("Score: {0:0}", container.score);
        
        if (container.paused)
        {
            spaceShipController.SetVelocity(new Vector3(0,0,0));
        }
            
        if (!container.inPlayMode && countDownText.isActiveAndEnabled)
        {
            countDown -= Time.deltaTime;
            countDownText.text = countDown.ToString("0");
        }

        if (countDown <= 0 && !container.inPlayMode && !container.paused)
        {
            countDownText.enabled = false;
            container.inPlayMode = true;
        }
    }
}
