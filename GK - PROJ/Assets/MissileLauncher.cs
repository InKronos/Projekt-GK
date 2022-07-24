using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
	private Transform target;
	private Enemy targetEnemy;


	public float range = 60f;

	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;


	public int damageOverTime = 30;
	public float slowAmount = .5f; 


	public string playerTag = "Player";

	public Transform partToRotate;
	public float turnSpeed = 10f;

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
			Debug.Log("Hello: ");
			float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
			Debug.Log(distanceToPlayer);
			if (distanceToPlayer <= range)
			{
				target = player.transform;
				other.gameObject.GetComponent<PlayerHealth>().ReceiveDamage((int)(damageOverTime * Time.deltaTime));
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
		if (target == null)
		{
			return;
		}

		LockOnTarget();

		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;
	

	}

	void LockOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}


	void Shoot()
	{
		//GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		//Bullet bullet = bulletGO.GetComponent<Bullet>();

		//if (bullet != null)
			//bullet.Seek(target);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
