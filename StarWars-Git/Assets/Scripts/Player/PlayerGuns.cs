using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] GameObject leftGun;
    [SerializeField] GameObject rightGun;

    private bool spawnAt = false;
    private float shootTimer = 0;

    private void Update()
    {
        this.shootTimer += Time.deltaTime;
        if (Input.GetMouseButton(0) && this.shootTimer >= .1)
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