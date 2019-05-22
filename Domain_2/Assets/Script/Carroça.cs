using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carroça : MonoBehaviour 
{	
	[SerializeField] private float carroPos;
	public static float carroPosY;	
	private float posInicioY;
	private Rigidbody rb;
	private int bulletCount = 0;
	public int bulletMax;	
	public float carrLimit;
	public float carrVel;
	public float lowerLimit;
	public float forwardVel;
	public float forwardTimer;
	public float approxVel;

	public bool backStart;
	[SerializeField] Vector3 velocity;
	[SerializeField] StateType currentState;
	[SerializeField] Distance distance;

	public enum StateType
	{
		Stopped,
		ZigZag,
		GoingUp
	}

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		SetVelocity(new Vector3 (carrVel,0,0));
		posInicioY = transform.position.y;

		backStart = false;
		currentState = StateType.ZigZag;
	}	
	
	void Update () 
	{
		carroPos = transform.position.x;
		carroPosY = transform.position.y;		
		
		//rb.velocity = new Vector3 (2,0,0);
		
		Aproxima ();
		VoltaInicio ();	
		Move ();	

		rb.velocity = velocity;	
	}

	//indo pra direita e pra esquerda + pra baixo aos poucos 
	void Move ()
	{
		if(currentState != StateType.ZigZag) return;

		if(carroPos >= carrLimit && !Player.gotHit)
			SetVelocity(new Vector3 (-carrVel,-approxVel,0));
		if(carroPos <= -carrLimit && !Player.gotHit)
			SetVelocity(new Vector3 (carrVel,-approxVel,0));

		if(carroPos >= carrLimit && Player.gotHit)
		{
			SetVelocity(new Vector3 (-carrVel, forwardVel,0));
			Invoke("GotHitTimer", forwardTimer);
		}
		if(carroPos <= -carrLimit && Player.gotHit)
		{
			SetVelocity(new Vector3 (carrVel, forwardVel,0));
			Invoke("GotHitTimer", forwardTimer);
		}
	}

	//aproximar ao longo do tempo>
	void Aproxima ()
	{		
		if (carroPosY < -lowerLimit && currentState != StateType.Stopped)
		{
			if(carroPos >= -0.1 && carroPos <= 0.1)
			{
				currentState = StateType.Stopped;
				Distance.isSmash = true;			
				SetVelocity(Vector3.zero);	
			}			
		}		 		
	}

	void VoltaInicio ()
	{				
		if (Distance.countSmash <= 0 && currentState == StateType.Stopped)
		{			
			SetVelocity(new Vector3 (0, carrVel, 0));		
			currentState = StateType.GoingUp;
			Invoke("ResetSmash", 0.8f);	
		}
		if(currentState == StateType.GoingUp && carroPosY >= posInicioY)
		{			
			SetVelocity(new Vector3 (carrVel, 0, 0));				
			currentState = StateType.ZigZag;
		}
	}

	void SetVelocity(Vector3 velocity)
	{
		this.velocity = velocity;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("Bullet"))
		{
			bulletCount++;
			if(bulletCount == bulletMax)
			{
				GameObject coinObj = ObjectPooler.SharedInstance.GetPooledObject("coinGroup"); 
  				if (coinObj != null)
				{
					var pos = new Vector3(0f, transform.position.y, transform.position.z);
   					coinObj.transform.position = pos;
   					coinObj.transform.rotation = transform.rotation;
					
					coinObj.GetComponent<CoinGroup>().SpawnCoins(transform.position);
 				}
				bulletCount = 0;	
			}
		}	  		
	}

	void GotHitTimer()
	{
		Player.gotHit = false;
	}

	void ResetSmash()
	{
		Distance.countSmash = distance.smashTime;
	}	
}