using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public float maxBodyHealth = 100;
    public float maxShieldHealth = 150;
    public Collider bodyCollider;
    public Collider shieldCollider;
    public float rechargeTime = 5;
    public GameObject shieldSphere;

    private bool rechargeRequired = false;
    private float bodyHealth;
    private float shieldHealth;

    float timeTillRecharge;
    void Start()
    {
        bodyHealth = maxBodyHealth;
        shieldHealth = maxShieldHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timeTillRecharge -= Time.deltaTime;
        if (timeTillRecharge < 0 && rechargeRequired)
        {
            rechargeRequired = false;
            shieldHealth = maxShieldHealth;
            shieldCollider.enabled = true;
            bodyCollider.enabled = false;
            shieldSphere.SetActive(true);
        }

    }

    void ReceiveDamage(int damage)
    {
        if (shieldHealth > 0)
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
        }

    }
}
