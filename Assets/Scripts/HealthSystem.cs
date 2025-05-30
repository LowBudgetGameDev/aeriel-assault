using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDie;

    [SerializeField] private int maxHealth = 1000;

    private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void Damage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void Die()
    {
        OnDie(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public float GetHealthAmountNormalized()
    {
        return (float) health / maxHealth;
    }
}
