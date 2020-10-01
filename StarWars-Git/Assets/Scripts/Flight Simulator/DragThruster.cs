using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragThruster : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;


    private void Update()
    {
        
    }

    void OnMouseDown()
    {
         
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void OnMouseUp()
    {
        FindObjectOfType<Spaceship>().gameObject.AddComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("hello");
    }

    private void OnTriggerStay(Collider other)
    {
        print("i");
        //Destroy(FindObjectOfType<Spaceship>().GetComponent<Rigidbody>());
    }
}
