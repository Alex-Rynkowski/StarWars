using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] private GameObject leftGun;
    [SerializeField] private GameObject rightGun;

    private bool spawnAt = false;
    private Vector3 positions;
    private float shootTimer = 0;

    private void Update()
    {
        this.shootTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && this.shootTimer >= .1)
        {
            Shoot();
            shootTimer = 0;
        }
    }

    void Shoot()
    {
        if (spawnAt)
        {
            Instantiate(bullet, leftGun.transform.position, Quaternion.identity);
            spawnAt = false;
        }
        else
        {
            Instantiate(bullet, rightGun.transform.position, Quaternion.identity);
            spawnAt = true;
        }
    }
}