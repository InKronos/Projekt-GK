using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOff : MonoBehaviour
{
    public int damage = 30;
    public float bounceStrength = 500f;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector3 force = this.transform.up * bounceStrength;
            collision.gameObject.GetComponent<Rigidbody>().velocity = force;
            collision.gameObject.GetComponent<PlayerHealth>().ReceiveDamage(damage);
        }
    }
}
