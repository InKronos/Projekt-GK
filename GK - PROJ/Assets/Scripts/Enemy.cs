using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float maxBodyHealth;
    public float maxShieldHealth;
    public Collider shieldCollider;
    public float rechargeTime;
    public GameObject shieldSphere;

    public bool rechargeRequired;
    public float bodyHealth;
    public float shieldHealth;

    public float timeTillRecharge;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReceiveDamage(int damage)
    {
     /*   if (shieldHealth > 0)
        {
            shieldHealth -= damage;
            timeTillRecharge = rechargeTime;
        }
        else
        {
            bodyHealth -= damage;
        }

        if (shieldHealth <= 0)
        {
            rechargeRequired = true;
            shieldSphere.SetActive(false);
            shieldCollider.enabled = false;
            bodyCollider.enabled = true;
            timeTillRecharge = rechargeTime;
            Destroy(this);
        }*/

    }
}
