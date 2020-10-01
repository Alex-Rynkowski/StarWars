using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Spaceship _spaceship;
    [SerializeField] float speed = 3f;
    [SerializeField] float scrollSensitivity = 30;

    float _minFov = 35f;
    float _maxFov = 100f;

    float xPosition;
    float yPosition;
    float zPosition;

    float xRotation;
    float yRotation;

    float orbitDistance = 10f;

    private void Start()
    {
        zPosition = _spaceship.transform.position.z;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(_spaceship.transform.position, transform.up, Input.GetAxis("Mouse X") * speed);
            transform.RotateAround(_spaceship.transform.position, transform.right, Input.GetAxis("Mouse Y") * speed);

            xPosition = Mathf.Abs(_spaceship.transform.position.x * 10);
            yPosition = Mathf.Abs(_spaceship.transform.position.y * 10);
            zPosition = Mathf.Abs(_spaceship.transform.position.z * 10);
            
            //print(xPosition + " " + yPosition + " " + zPosition);
            
            xRotation = transform.eulerAngles.x;
            yRotation = transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }

        var fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -scrollSensitivity;
        fov = Mathf.Clamp(fov, _minFov, _maxFov);
        Camera.main.fieldOfView = fov;
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(_spaceship.transform.position.x, _spaceship.transform.position.y + 5,_spaceship.transform.position.z -20);
    }

    void Orbit()
    {
        transform.position = _spaceship.transform.position + (transform.position - _spaceship.transform.position);
    }
}