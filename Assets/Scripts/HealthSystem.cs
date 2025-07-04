using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDie;
    public event EventHandler OnHealthAmountChanged;

    [SerializeField] private int maxHealth = 1000;

    private int health;
    private bool isInvincible;

    private void Awake()
    {
        health = maxHealth;
    }

    public void Damage(int amount)
    {
        if (isInvincible) return;

        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        OnHealthAmountChanged?.Invoke(this, EventArgs.Empty);
        SoundManager.Instance.PlaySoundType(SoundManager.SoundType.Hit);

        if (health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        OnHealthAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Die()
    {
        OnDie(this, EventArgs.Empty);

        GetComponent<Animator>().SetTrigger("Die");

        FunctionTimer.Create(() =>
        {
            SoundManager.Instance.PlaySoundType(SoundManager.SoundType.Hit);
        }, 2f);

        SoundManager.Instance.PlaySoundType(SoundManager.SoundType.Die);
        Destroy(this);
        Destroy(gameObject, 2f);
    }

    public float GetHealthAmountNormalized()
    {
        return (float) health / maxHealth;
    }

    public void MakeInvinsibleForTime(float time)
    {
        isInvincible = true;
        FunctionTimer.Create(() =>
        {
            isInvincible = false;
        }, time);
    }
}
