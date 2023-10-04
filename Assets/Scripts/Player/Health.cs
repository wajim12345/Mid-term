using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public Action<float> OnHealthUpdated;
    public Action OnDeath;

    public bool isDead { get; private set; }
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeductHealth(float value)
    {
        if(isDead)
        {
            return; //exit method if player is dead
        }

        health -= value;

        if(health<= 0)
        {
            isDead = true;
            OnDeath();
            health = 0;
        }

        OnHealthUpdated(health);
    }
}
