using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Health : MonoBehaviour, IActivateScreenShake
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] private float currentHealth = 0;

    private float timer;
    private ActivateScreenShake activateScreenShake;
    private LayerMask playerLayerMask;

    private Container container;
    private GameOver gameOver;
    private CanvasGroup canvasGroup;
    private void Start()
    {
        this.playerLayerMask = LayerMask.GetMask("Player");
        this.currentHealth = maxHealth;
        this.container = FindObjectOfType<Container>();
        this.activateScreenShake = FindObjectOfType<ActivateScreenShake>();
        this.gameOver = FindObjectOfType<GameOver>();
        this.canvasGroup = gameOver.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if ((1 << this.gameObject.layer) != playerLayerMask && CurrentHealth() <= 0)
        {
            Destroy(this.gameObject);
        }
        else if ((1 << this.gameObject.layer) == playerLayerMask && CurrentHealth() <= 0)
        {
            container.gameOver = true;
            this.canvasGroup.interactable = true;
            this.gameOver.enabled = true;

        }
        
        
    }

    public float CurrentHealth()
    {
        return this.currentHealth;
    }

    public float Damage(float damage)
    {
        if ((1 << this.gameObject.layer) == playerLayerMask)
        {
            
            activateScreenShake.Activator(this);
        }
        
        return this.currentHealth -= damage;
    }

    public float MaxHealth()
    {
        return this.maxHealth;
    }

    public bool ActivateScreenShake()
    {
        return true;
    }
}