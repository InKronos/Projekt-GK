using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject explosionObject;


    public void Explode()
    {
        Instantiate(explosionObject, transform.position, transform.rotation); //Spawn in the broken version
        Destroy(gameObject); //Destroy the object to stop it getting in the way
        GameManager.Instance.ChangePoints(100);
    }
}
