using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour 
{	
	float posY;
	float posYFixed;
	float posZ;
	float randomPosX;

	void Start ()
	{
		posZ = transform.position.z;
		posYFixed = transform.position.y;
	}	

	void Update () 
	{
		posY = transform.position.y;

		Back ();

		transform.Translate (Vector3.down * Time.deltaTime * 12, Space.World);
	}

	void Back ()
	{
		if (this.gameObject.tag == "Tree1")
		{
			if (posY < -9)
			{
				transform.position = new Vector3  (randomPosX, 18, posZ);  
			}

			randomPosX = Random.Range (-5.3f, -3.8f);
		}

		if (this.gameObject.tag == "Tree2")
		{
			if (posY < -9)
			{
				transform.position = new Vector3  (randomPosX, 18, posZ);  
			}
			
			randomPosX = Random.Range (3.8f, 6.8f);
		}		
	}
}
