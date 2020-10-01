using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private Health health;

    public Action a_healthCalc;

    private void Start()
    {
        health = GetComponent<Health>();
        a_healthCalc += CalculateHealth;
    }

    void CalculateHealth()
    {
        healthBar.fillAmount = health.CurrentHealth() / health.MaxHealth();
    }
}