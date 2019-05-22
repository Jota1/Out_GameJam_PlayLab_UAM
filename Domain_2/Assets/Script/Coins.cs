using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class Coins : MonoBehaviour 
{
	[SerializeField] Rigidbody rigidB;
	[SerializeField] float coinRotationSpeed;

	public static float score;
	public float coinSpeed;	

	void Start()
	{
		rigidB = GetComponent<Rigidbody>();

		score = 0;
	}

	void Update()
	{
		UpdateVelocity();

		if(transform.position.y <= -12.5) PoolHandler.Instance.StoreObject(gameObject);

		//Rotação Moeda
		Vector3 rotation = transform.eulerAngles;  
        rotation.z += coinRotationSpeed * Time.deltaTime;  
        transform.eulerAngles = rotation;		
	}

	public void SetActive(bool value)
	{
		if(!value)
		{
			rigidB.velocity = Vector3.zero;
		}
		rigidB.isKinematic = !value;
	}

	void UpdateVelocity()
	{
		if(!rigidB.isKinematic)
			rigidB.velocity = new Vector3(0, -coinSpeed, 0);
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			PoolHandler.Instance.StoreObject(gameObject);
			score += 5;			
		} 
	}	
}