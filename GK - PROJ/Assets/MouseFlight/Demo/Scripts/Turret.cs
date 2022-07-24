using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

	private Transform target;

	private PlayerHealth playerHealthTarget;

	public float range = 60f;

	public string playerTag = "Player";

	public Transform partToRotate;
	public float turnSpeed = 50f;

	public int damageOverTime = 30;
	public float slowAmount = .5f;
	public float health = 10f;
	public LineRenderer lineRenderer;
	


	public Transform firePoint;

	// Use this for initialization
	void Start()
	{
		//InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	
	void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player")
        {
			GameObject player = GameObject.FindWithTag(playerTag);
	
			float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
			if (distanceToPlayer <= range)
			{
				target = player.transform;
			}
			else
			{
				target = null;
			}
		}
    }

	void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag == "Player")
        {
			target = null;
        }

	}

	// Update is called once per frame
	void Update()
	{
		if (health <= 0)
		{
			GameManager.Instance.enemies.Remove(this.gameObject);
			Destroy(gameObject);
		}

		if (target == null)
        {
			if (lineRenderer.enabled)
			{
				lineRenderer.enabled = false;
			}
			return;
		}

		LockOnTarget();
		Laser();
		target.gameObject.GetComponent<PlayerHealth>().ReceiveDamage((int)(damageOverTime * Time.deltaTime));
	}

	void LockOnTarget()
    {

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
	}

	void Laser()
	{


		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;
		}

		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);
		
		Vector3 dir = firePoint.position - target.position;
	}



	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	public void ReceiveDamage(float damage)
    {
		health -= damage;
    }
}
