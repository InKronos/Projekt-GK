using UnityEngine;

public class SmallTankMove  : Enemy
{

    public Rigidbody rb;
    public float speed = 100000.0f;
    private Transform target;
    private int waypointId = 0;
    private float angleBetween = 0;
    private float rotationpercent = 0f;
    private bool rotate = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        bodyHealth = 100;
        shieldHealth = 0;
        target = WayPoints.points[0];
        rb.mass = 1000f;
        transform.Translate(target.position - transform.position);


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        
        float dist = Vector3.Distance(transform.position, target.position);

        if (Vector3.Angle(transform.forward, dir) > 40f && !rotate && dist > 7f)
        {
            angleBetween = Vector3.Angle(transform.forward, dir);
            rotate = true;
        }
        else if (Vector3.Angle(transform.forward, dir) < 30f)
        {
            rotate = false;
            rotationpercent = 0f;
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
        }

        if (rotate)
        { 
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = new Vector3(0f, 1f, 0f);
            /*Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            rotation = Quaternion.Lerp(transform.rotation, rotation, rotationpercent);
            rotationpercent += 0.5f* Time.deltaTime;
            transform.rotation = rotation;*/
            rb.velocity =(transform.forward.normalized * speed / 2);
        }
        else
        {

            rb.angularVelocity = new Vector3(0f, 0f, 0f);
            rb.velocity = (transform.forward.normalized * speed)+ new Vector3(0f, -0.1f, 0f);
        }
        if(dist < 40f)
        {
             
            GetNextWaypoint();
            
        }

        
    }
    void GetNextWaypoint()
    {
        if (waypointId < WayPoints.points.Length)
        {
            
            target = WayPoints.points[waypointId]; 
            waypointId++;
        }
        else if (waypointId < 2* WayPoints.points.Length)
        {
            
            target = WayPoints.points[2 * WayPoints.points.Length-waypointId-1];
            waypointId++;
        }
        else
        {

            waypointId = 0;
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
               timeTillRecharge = rechargeTime;
               Destroy(this);
           }

    }
}
