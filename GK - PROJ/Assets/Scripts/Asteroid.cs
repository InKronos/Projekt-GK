using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject objectToDisable;
    public ParticleSystem explosionObject;


    public void Explode()
    {
        explosionObject.Play();
        objectToDisable.SetActive(false);
        Destroy(gameObject, 2); //Destroy the object to stop it getting in the way
        GameManager.Instance.ChangePoints(100);
    }
}
