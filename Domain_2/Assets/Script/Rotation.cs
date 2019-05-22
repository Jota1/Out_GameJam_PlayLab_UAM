using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour 
{
	[SerializeField] float rotationSpeed;
	[SerializeField] Vector3 endPosition;
	public static bool canRotate;


	void Start ()
	{
		canRotate=false;
	}

	void Update () 
	{
		RotateCAM();

		//Debug.Log ("canRotate : " + canRotate);
	}

	public void RotateCAM()
	{
		//if bool true
		//colocar bool true na coroutine por alguns segundos
		if (canRotate && Player.swipeL)
		{
			Vector3 rotation = transform.eulerAngles; 
        	rotation.y += rotationSpeed * Time.deltaTime;  
        	transform.eulerAngles = rotation;
		}
		else if (canRotate && Player.swipeR)
		{
			Vector3 rotation = transform.eulerAngles; 
        	rotation.y -= rotationSpeed * Time.deltaTime;  
        	transform.eulerAngles = rotation;
		}
	}


 	public static IEnumerator RotateCam ()
 	{ 		
 		canRotate = true;
		yield return new WaitForSeconds (0.4f);
		canRotate = false;		
 	}
}
