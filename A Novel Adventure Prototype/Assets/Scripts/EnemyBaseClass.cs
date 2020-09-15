//--------------Base Enemy Class Script--------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    Rigidbody2D rb;

    //Health Variables
    public float currentHealth; //enemy's current health
    public float maxHealth; //enemy's maximum health

    //Damage Variables
    public bool knockedOut;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        knockedOut = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //--------Function used to deal damage to enemy's from other scripts--------//
    public void DealDamage(float damage)
    {
        currentHealth -= damage; //reduce health by damage value
        CheckKO();
    }

    //--------Function to check if enemy should go down after last hit--------//
    void CheckKO()
    {
        if (currentHealth <= 0 && !knockedOut)
        {
            knockedOut = true;
            rb.mass = 20;
            rb.drag = 3;
            Debug.Log("Enemy is knocked out");
        }
    }
}
