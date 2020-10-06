using System;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private LayerMask asteroidLayer;
    private HealthDisplay healthDisplay;

    private void Start()
    {
        this.asteroidLayer = LayerMask.GetMask("Asteroid");
        this.healthDisplay = GetComponent<HealthDisplay>();
    }

    private void OnCollisionStay(Collision other)
    {
        if (1 << other.gameObject.layer == asteroidLayer)
        {
            this.healthDisplay.a_healthCalc();
            this.gameObject.GetComponent<Health>().Damage(2f);
        }
    }
}