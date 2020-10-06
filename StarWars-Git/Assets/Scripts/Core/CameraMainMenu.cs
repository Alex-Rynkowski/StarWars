using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainMenu : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] private float zoomSensitivity = 30;
    [SerializeField] int playMusic;

    private float xRotation;
    private float yRotation;


    [SerializeField] float minFov = 35;
    [SerializeField] float maxFov = 100;

    private void Start()
    {
        FindObjectOfType<AudioManager>().MusicToPlay(playMusic);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.visible = false;
            transform.RotateAround(Vector3.zero, transform.up, -Input.GetAxis("Mouse X") * speed);
            transform.RotateAround(Vector3.zero, transform.right, Input.GetAxis("Mouse Y") * speed);

            xRotation = transform.eulerAngles.x;
            yRotation = transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            transform.position = new Vector3(0, 0, -50);
        }
        else
        {
            Cursor.visible = true;
        }


        var zoom = Camera.main.fieldOfView;
        zoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSensitivity;
        zoom = Mathf.Clamp(zoom, minFov, maxFov);
        Camera.main.fieldOfView = zoom;
    }
}