using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour 
{
	public GameObject bulletPrefab;
	public float fireRate;
	public Transform PlayerPos;	
	private float countDown;
	private Vector3 offset; 

	public static bool stopFiringAuto;	

	void Awake ()
	{
		offset = new Vector3 (-0.4f,0.8f,0);
	}

	void Start () 
	{
		countDown=fireRate;	

		stopFiringAuto = false;	
	}	
	
	void Update () 
	{		
		Counter ();			
	}

	void Counter ()
	{
		countDown-=Time.deltaTime;

		if (stopFiringAuto == false)
		{
			if (countDown <= 0.3f)
			{
				//atira
				GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("bullet"); 
  				if (bullet != null)
				{
   					bullet.transform.position = PlayerPos.position + offset;
   					bullet.transform.rotation = PlayerPos.rotation;
    				bullet.SetActive(true);
 		 		}			  						
				countDown=fireRate;
			}
		}
	}	
	
}