using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFlight;

public class LaserFire : MonoBehaviour
{
    public GameObject[] lasers; //Parent laser gameObjects
    public LineRenderer[] laserLineRenderers; //Laser Linerenders
    public float laserRange = 50f;
    public Transform[] laserFireOriginPoint;
    public Transform RayCastOrignPoint;
  
    void Start()
    {
       for(int i = 0; i < lasers.Length; i++)
        {
            lasers[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 RayCastTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) + RayCastOrignPoint.forward * laserRange;

        if(Input.GetButton("Fire1"))
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].SetActive(true);
            }

            if (Physics.Raycast(RayCastOrignPoint.position, RayCastTarget, out hit, laserRange, 3))
            {
                for (int i = 0; i < laserLineRenderers.Length; i++)
                {
                    laserLineRenderers[i].SetPosition(0, laserFireOriginPoint[i].position);
                    laserLineRenderers[i].SetPosition(1, hit.point);
                }

                if(hit.transform.gameObject.GetComponent<Asteroid>())
                {
                    hit.transform.gameObject.GetComponent<Asteroid>().Explode();
                }
            }
            else
            {
                for (int i = 0; i < laserLineRenderers.Length; i++)
                {
                    laserLineRenderers[i].SetPosition(0, laserFireOriginPoint[i].position);
                    laserLineRenderers[i].SetPosition(1, RayCastTarget);               
                }
            }
        } 

        if (Input.GetButtonUp("Fire1"))
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].gameObject.SetActive(false);
            }
        }
    }
}
