using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{	
	public float bulletSpeed;
	public GameObject bulletPrefab;
	private Rigidbody rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}	
	
	void Update ()
	{
		Fire ();

		if (transform.position.y > 5.5f)
		{
			gameObject.SetActive (false);
		}
	}

	void Fire ()
	{
		rb.velocity = new Vector3 (0,bulletSpeed,0);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Carroca")
		{
			gameObject.SetActive (false);
			Coins.score+=1;
			//dropa moedas
		}		
	}
}
