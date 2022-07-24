using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MFlight;

public class LaserFire : MonoBehaviour
{
    //remove a lot of publics after testing

    public Image heatImage;

    public float overHeatCoolDown = 5f;
    private float nextFireTime = 0f;
    public bool canFire = true;

    public float heat = 0f;
    public float maxHeat = 150f;
    public float heatTick = 10f;
    public float heatCoolDown = 5f;
    public float heatTickDelay = 1f;
    private float nextHeatTickTime = 0f;
    public bool increaseHeat = true;
    private bool firing = false;
    public float damage = 400;



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
        if(!canFire)
        {
            if (Time.time > nextFireTime)
            {
                canFire = true;
                heat = 0;
                laserLineRenderers[0].enabled = true;
                laserLineRenderers[1].enabled = true;
            }
        }

        if(increaseHeat)
        {
           heat = Mathf.Lerp(heat, maxHeat + 20, heatTick * Time.deltaTime);
           if(heat > maxHeat)
            {
                if (canFire)
                {
                    firing = false;
                    this.gameObject.GetComponent<PlayerAudio>().Stop("Beam");
                    this.gameObject.GetComponent<PlayerAudio>().Play("Overcharge");
                }
                canFire = false;
                nextFireTime = Time.time + overHeatCoolDown;
                laserLineRenderers[0].enabled = false;
                laserLineRenderers[1].enabled = false;
                
            }

        }
        else
        {
            heat = Mathf.Lerp(heat, 0, heatTick * Time.deltaTime);
        }
        heatImage.fillAmount = heat / maxHeat;
        RaycastHit hit;
        //Vector3 RayCastTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) + RayCastOrignPoint.forward * laserRange;
        Vector3 RayCastTarget =  RayCastOrignPoint.forward * laserRange;
        

        if (Input.GetButton("Fire1") && canFire && !PauseMenu.GamePaused)
        {
            //Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition) + RayCastOrignPoint.forward * laserRange, RayCastOrignPoint.forward, Color.red, 2, true);
            if (!firing)
                this.gameObject.GetComponent<PlayerAudio>().Play("Beam");
            firing = true;

            if (Time.time > nextHeatTickTime)
            {
                increaseHeat = true;
                nextHeatTickTime = Time.time + heatTickDelay;
            }
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

                else if (hit.transform.gameObject.GetComponent<Turret>())
                {
                    hit.transform.gameObject.GetComponent<Turret>().ReceiveDamage(damage * Time.deltaTime);
                }
            }
            else
            {
                for (int i = 0; i < laserLineRenderers.Length; i++)
                {
                    RayCastTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) + RayCastOrignPoint.forward * laserRange;
                    laserLineRenderers[i].SetPosition(0, laserFireOriginPoint[i].position);
                    laserLineRenderers[i].SetPosition(1, RayCastTarget);               
                }
            }
        }
        else
        {
             if (Time.time > nextHeatTickTime)
             {
                increaseHeat = false;
                nextHeatTickTime = Time.time + heatTickDelay;
            }
        }

        if ((Input.GetButtonUp("Fire1") && canFire) || PauseMenu.GamePaused)
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].gameObject.SetActive(false);
            }
            firing = false;
            this.gameObject.GetComponent<PlayerAudio>().Stop("Beam");
        }
        

      
    }
}
