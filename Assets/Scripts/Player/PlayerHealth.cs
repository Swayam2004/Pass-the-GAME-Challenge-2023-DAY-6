using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    /* Day3 developer here:
    * This script was written by the previous developers and it handles the health of the player.
    * You shouldn't really touch this, at least try to no to break it as it is super important for the rest 
    * of the project structure :D
    */

    public static PlayerHealth instance;

    public int maxHealth;
    public bool isdead;
    public int health;

    public event Action DamageTaken;
    public event Action HealthUpgraded;

    public CounterHandler counter;

    public int Health
    {
        get
        {
            return health;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage()
    {
        if(health <= 0)
        {
            return;
        }
        health -= 1;
        if(DamageTaken != null)
        {
            DamageTaken();
        }
        if (health <= 0)
        {
            GetComponent<Player>().diamondsCollected += GetComponent<Player>().diamondAmount;
            PlayerPrefs.SetInt("Diamonds", GetComponent<Player>().diamondsCollected);
            isdead = true;
            CounterHandler.lastDistance = counter.distance;
            return;
        }
    }

    public void Heal()
    {
        if (health >= maxHealth)
        {
            return;
        }
        health += 1;
        if (DamageTaken != null)
        {
            DamageTaken();
        }
    }

    public void UpgradeHealth()
    {
        if(maxHealth <= 14)
        {
            maxHealth++;
        }
        if(HealthUpgraded != null)
        {
            HealthUpgraded();
        }
    }

}
