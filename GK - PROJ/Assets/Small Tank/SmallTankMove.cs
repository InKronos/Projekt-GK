using UnityEngine;

public class SmallTankMove  : Enemy
{

    public float speed = 100000.0f;
    private Transform target;
    private int waypointId = 0;
    private float angleBetween = 0;
    private float rotationpercent = 0f;
    private bool rotate = true;
    // Start is called before the first frame update
    void Start()
    {
        bodyHealth = 100;
        shieldHealth = 0;
        target = WayPoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        
        float dist = Vector3.Distance(transform.position, target.position);

        if (Vector3.Angle(transform.forward, dir) > 30f && !rotate)
        {
            Debug.Log("You spin me right 'round, baby, right 'round");
            angleBetween = Vector3.Angle(transform.forward, dir);
            rotate = true;
        }
        else if (Vector3.Angle(transform.forward, dir) <5f)
        {
            Debug.Log("Koniec");
            rotate = false;
            rotationpercent = 0f;
        }
        if(rotate)
        { 
            Debug.Log(angleBetween);
            /*transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * 10*speed, Space.World);*/

            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            rotation = Quaternion.Lerp(transform.rotation, rotation, rotationpercent);
            rotationpercent += 0.5f* Time.deltaTime;
            transform.rotation = rotation;
            transform.Translate(transform.forward.normalized * speed / 4 * Time.deltaTime, Space.World);
        }
        else
        {
           
            
            transform.Translate(transform.forward.normalized * speed * Time.deltaTime, Space.World);
        }
        if(dist < 1f)
        {
             
            GetNextWaypoint();
        }
    }
    void GetNextWaypoint()
    {
        if (waypointId < WayPoints.points.Length)
        {
            waypointId++;
            target = WayPoints.points[waypointId]; 
        }
        else if (waypointId < 2* WayPoints.points.Length)
        {
            waypointId++;
            target = WayPoints.points[2 * WayPoints.points.Length-waypointId];
        }
        else
        {

            waypointId = 0;
        }

    }

    void ReceiveDamage(int damage)
    {
        Debug.Log("GIT");
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
               timeTillRecharge = rechargeTime;
               Destroy(this);
           }

    }
}
