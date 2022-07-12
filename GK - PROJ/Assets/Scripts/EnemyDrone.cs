using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{

    public float torque = 500f;
    public float thrust = 1000f;
    private Rigidbody rb;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }

    void Fly()
    {
        Vector3 targetDir = player.position - transform.position;

        float xyAngle = vector3AngleOnPlane(player.position, transform.position, transform.forward, transform.up);
        float yzAngle = vector3AngleOnPlane(player.position, transform.position, transform.right, transform.forward);

        if (Mathf.Abs(xyAngle) >= 1f && Mathf.Abs(yzAngle) >= 1f)
        {
            rb.AddRelativeTorque(Vector3.forward * -torque * (xyAngle / Mathf.Abs(xyAngle)));
        }
        else if(yzAngle > 1f)
        {
            rb.AddRelativeTorque(Vector3.forward * thrust);
        }

        rb.AddRelativeForce(Vector3.forward * thrust);
    }

    float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toOrientation)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toOrientation, planeNormal);

        return projectedVectorAngle;
    }
}
