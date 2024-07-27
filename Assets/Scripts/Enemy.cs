using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public int XPReward = 1;
    private bool Dead;


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !Dead)
        {
            Dead = true;
            Die();
        }
    }

    void Die()
    {
        // Handle enemy death (e.g., play animation, remove from scene)
        SharedXPManager.Instance.AddXP(XPReward);
        print(this.name + " enemy died");
        this.gameObject.SetActive(false);
    }

   
}
