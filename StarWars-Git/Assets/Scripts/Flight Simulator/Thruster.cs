using System;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public float forceMagnitude;

    
    // FixedUpdate is called a fixed amount of time per second
    void FixedUpdate()
    {
        GetComponentInParent<Rigidbody>().AddForceAtPosition(
            -this.transform.right * this.forceMagnitude,
            this.transform.position);
        var main = GetComponent<ParticleSystem>().main;
        var emission = GetComponent<ParticleSystem>().emission;
        main.startLifetimeMultiplier = this.forceMagnitude * 0.25f;
        emission.rateOverTimeMultiplier = this.forceMagnitude * 50;
    }

    void OnEnable()
    {
        GetComponent<ParticleSystem>().Play();
    }

    void OnDisable()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}