using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScreenShake : MonoBehaviour
{
    private ActivateRedScreen activateRedScreen;
    private Camera camera;

    private void Start()
    {
        activateRedScreen = FindObjectOfType<ActivateRedScreen>();
        camera = Camera.main;
    }

    public void Activator(IActivateScreenShake activateScreenShake)
    {
        if (activateScreenShake.ActivateScreenShake())
        {
            activateRedScreen.enabled = true;
            camera.transform.localPosition = new Vector3(UnityEngine.Random.Range(-.5f, .5f),
                UnityEngine.Random.Range(-.5f, .5f), -30);
        }
        else
        {
            camera.transform.localPosition = new Vector3(0f, 0f, -30);
        }
    }
}