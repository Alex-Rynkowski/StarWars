using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject spaceShip;
 private void LateUpdate()
    {
        this.transform.position = spaceShip.transform.position;
    }
}