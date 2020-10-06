using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Container container;
    private SpaceShipController spaceShipController;

    private float highScore;

    void Start()
    {
        spaceShipController = FindObjectOfType<SpaceShipController>();
        container = FindObjectOfType<Container>();
    }

    public float Score()
    {
        return container.score;
    }

    public string CurrentLevel()
    {
        return SceneManager.GetActiveScene().name;
    }
}