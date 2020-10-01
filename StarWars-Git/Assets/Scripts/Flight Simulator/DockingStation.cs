using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingStation : MonoBehaviour
{
    [SerializeField] Spaceship spaceship;

    Rigidbody rb;

    private void Start()
    {
        rb = spaceship.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Mathf.Abs(rb.velocity.z) < 10 && Mathf.Abs(rb.velocity.y) < 10 && Mathf.Abs(rb.velocity.x) < 10)
        {
            
            if (Mathf.Abs(spaceship.transform.eulerAngles.x) < 100 && Mathf.Abs(spaceship.transform.eulerAngles.x) > 80)
            {
                print("You win asshole");
            }
            
        }
    }
}
