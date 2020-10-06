using System;
using System.Collections;
using UnityEngine;

public class ActivateRedScreen : MonoBehaviour, IActivateScreenShake
{
    [SerializeField] private CanvasGroup redScreen;
    [SerializeField] private float activationTimer = 1;

    private ActivateScreenShake activateScreenShake;

    private void Update()
    {
        StartCoroutine(DisplayRedScreen());
        activationTimer += Time.deltaTime;
        if (this.activationTimer >= 1)
        {
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        activateScreenShake = FindObjectOfType<ActivateScreenShake>();
        activationTimer = 0;
    }

    private void OnDisable()
    {
        if (redScreen != null)
        {
            redScreen.alpha = 0;
            activateScreenShake.Activator(this);
        }
    }

    IEnumerator DisplayRedScreen()
    {
        while (this.enabled)
        {
            redScreen.alpha = .5f;
            yield return new WaitForSeconds(.2f);
            redScreen.alpha = 0f;
            yield return new WaitForSeconds(.2f);
        }
    }

    public bool ActivateScreenShake()
    {
        return false;
    }
}