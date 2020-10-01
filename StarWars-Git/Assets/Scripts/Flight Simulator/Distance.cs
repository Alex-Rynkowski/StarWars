using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI distanceText;
    Spaceship spaceship;
    DockingStation dockingStation;

    private void Start()
    {
        spaceship = FindObjectOfType<Spaceship>();
        dockingStation = FindObjectOfType<DockingStation>();
    }

    private void Update()
    {
        distanceText.text = $"Distance: {(CaluculateDistance() * 100).ToString("0")}m";
    }

    float CaluculateDistance()
    {
        return Vector3.Distance(spaceship.transform.position, dockingStation.transform.position);
    }
}
