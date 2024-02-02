using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float health;
    public bool isDead {  get; private set; }

    public Action<float> OnHealthUpdated;
    public Action OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        OnHealthUpdated(maxHealth);
    }

    public void DeductHealth(float value)
    {
        if(isDead) return;

        health -= value;
        if(health <= 0)
        {
            isDead = true;
            OnDeath();
            health = 0;
        }

        OnHealthUpdated(health);

    }
}
