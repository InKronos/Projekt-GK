using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLifetime : MonoBehaviour
{
    // Start is called before the first frame update

    public float lifetime;
    void Awake()
    {
        Destroy(this.gameObject, lifetime);
    }

}
