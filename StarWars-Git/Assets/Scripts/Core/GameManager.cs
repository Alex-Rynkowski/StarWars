using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private Container container;
    private SpaceShipController spaceShipController;

    void Start()
    {
        spaceShipController = FindObjectOfType<SpaceShipController>();
        container = FindObjectOfType<Container>();
    }
    private float Score()
    {
        return container.score;
    }
}