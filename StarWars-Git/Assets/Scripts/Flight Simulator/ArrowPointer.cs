using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPointer : MonoBehaviour
{
    DockingStation dockingStation;
    private Spaceship spaceship;
    private void Start()
    {
        dockingStation = FindObjectOfType<DockingStation>();
        spaceship = FindObjectOfType<Spaceship>();
    }

    private void Update()
    {
        this.transform.LookAt(dockingStation.transform.position, Vector3.up);
        this.transform.position = new Vector3(spaceship.transform.position.x, spaceship.transform.position.y + 3, spaceship.transform.position.z);
    }
}
