using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        // explode animation
        ScoreManager.instance.ChangePoints(10);
        Debug.Log("Enemy Destroyed");
        Destroy(gameObject);
    }
}
