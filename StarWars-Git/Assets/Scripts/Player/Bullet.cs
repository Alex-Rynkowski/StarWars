using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] float bulletDamage = 5;

    [Header("Rigidbody controls")] private Rigidbody rb;
    private SpaceShipController spaceShipController;
    private Vector3 moveFoward;

    [Header("Other scripts")] private Container container;

    private void Start()
    {
        spaceShipController = FindObjectOfType<SpaceShipController>();
        container = FindObjectOfType<Container>();
        rb = GetComponent<Rigidbody>();

        moveFoward = spaceShipController.transform.forward;
        Destroy(gameObject, 6);
    }

    private void Update()
    {
        rb.velocity = moveFoward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == LayerMask.GetMask("Asteroid"))
        {
            other.GetComponent<Health>().Damage(bulletDamage / 10);
            container.score += bulletDamage / 10;

        }

        if (1 << other.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            other.GetComponent<Health>().Damage(bulletDamage);
            container.score += bulletDamage;
        }
        Destroy(this.gameObject);
    }
}