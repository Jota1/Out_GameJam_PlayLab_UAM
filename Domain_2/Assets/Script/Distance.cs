using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour 
{	
	public Transform PlayerPos;	
	private Vector3 offset; 
	public static bool isSmash;

	//smash variaveis
	public static float countSmash;
	public float smashTime;

	void Start () 
	{
		offset = new Vector3 (-0.4f, 0.8f, 0);		
		isSmash = false;
		countSmash = smashTime;		
	}	
	
	void Update () 
	{
		CountDown();		
		//Debug.Log(countSmash);	
	}

	void CountDown()
	{		
		if (isSmash)
		{
			SmashButton ();	
		}					
	}

	void SmashButton ()
	{		
		Fire.stopFiringAuto = true;

		//habilitar input para atirar		
		 if (Input.GetMouseButtonDown (0) && Fire.stopFiringAuto == true)
		 {
		 	GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("bullet"); 
  		 	if (bullet != null)
		 	{
   		 		bullet.transform.position = PlayerPos.position + offset;
   		 		bullet.transform.rotation = PlayerPos.rotation;
    	 		bullet.SetActive(true);
 		  	}				
		 }	

		// if (Input.touchCount > 0)
		// {
		// 	GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("bullet"); 
  		//  	if (bullet != null)
		//  	{
   		//  		bullet.transform.position = PlayerPos.position + offset;
   		//  		bullet.transform.rotation = PlayerPos.rotation;
    	//  		bullet.SetActive(true);
 		//   	}	
		// }		

		//Player.Down = Vector3.zero;

		countSmash-=Time.deltaTime;

		if (countSmash <= 0)
		{
			isSmash = false;
			Fire.stopFiringAuto = false;			
		}	
	}
}