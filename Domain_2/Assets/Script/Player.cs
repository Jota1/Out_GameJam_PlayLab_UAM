using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPoint {
	LEFT, CENTER, RIGHT
}

public class Player : MonoBehaviour //COLOCAR A TAG DA CAMERA = "MAIN CAMERA" !!
{
	[SerializeField] float deltaX;
	public PlayerPoint point;
	[SerializeField] float duration;

	private float rotZ;
	public static bool gotHit;
	public static Vector3 Down;
	public Vector3 Up;	
	public float playerPos;
	public float playerPosY;
	public Rigidbody rb;
	public GameObject carr;
	private bool extremidade;	
	private float pontoInicial;
	public float playerVel;
	public float playerLimit;
	public static bool swipeR;
	public static bool swipeL;
	public Animator anima;  
	private float oldPos;

	public GameObject dragonRender; 
	public GameObject liraRender; 


	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		playerPos=transform.position.x;		
		playerPosY=transform.position.y;	

		extremidade = false;			

		pontoInicial = transform.position.x;		

		swipeL=false;
		swipeR=false;

		point = PlayerPoint.CENTER;
	}	
	
	void Update () 
	{
		oldPos = transform.position.x;

		playerPos=transform.position.x;		

		if (!Distance.isSmash)
		{
			Slide ();
		}		

		Destino ();	
		BackTopoint ();

		Debug.Log ("is Ismash " + Distance.isSmash);

		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotZ);

		Debug.Log ("Extremidade : " + extremidade);
	}	

	void Slide ()
	{
		// if (Input.GetMouseButtonDown (0))
		// {
		// 	//start position	
		// 	Down = Camera.main.ScreenToViewportPoint(Input.mousePosition);		
		// }

		// if (Input.GetMouseButtonUp (0))
		// {
 		// 	Up = Camera.main.ScreenToViewportPoint(Input.mousePosition);			      		
		// }	

		if(Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Began)
			{
				Up = touch.position;
				Down = touch.position;
			}
			else if(touch.phase == TouchPhase.Moved)
			{
				Up = touch.position;
			}
			else if(touch.phase == TouchPhase.Ended)
			{
				Up = touch.position;
			}
		}

 		if (Down != Up && Down != Vector3.zero && Up != Vector3.zero)
        {
            float deltaX = Up.x - Down.x;
			//Debug.Log(deltaX);           

            if (deltaX > 0.2f || deltaX < -0.2f) 
            {
                if (Down.x < Up.x) // swipe esquerda p direita
                {			
					// ChangePos_Right ();
					// deltaX = 0;
					// swipeL = true;
					StartCoroutine (MoveRight(transform));								
				}

				else //swipe pra esquerda 
				{					
					// 	ChangePos_Left ();	 
					// 	deltaX = 0;
					// 	swipeR = true;
				
					StartCoroutine (MoveLeft(transform));													
				}							
			}		
			Down = Vector3.zero;
			Up = Vector3.zero;	
		}		
	}


	private IEnumerator MoveRight(Transform _t) 
	{
		if (point == PlayerPoint.CENTER) 
		{
			point = PlayerPoint.RIGHT;

			Vector3 pos;
			var t = 0.0f;

			while(t < 1f)
			{
				t += Time.fixedDeltaTime / duration;
				t = Mathf.Clamp01(t);
				pos = new Vector3(Mathf.Lerp(0, playerLimit, t), _t.position.y, _t.position.z);
				_t.position = pos;
				yield return new WaitForFixedUpdate();
			}

		} else if (point == PlayerPoint.LEFT) {

			point = PlayerPoint.CENTER;

			Vector3 pos;
			var t = 0.0f;

			while(t < 1f)
			{
				t += Time.fixedDeltaTime / duration;
				t = Mathf.Clamp01(t);
				pos = new Vector3(Mathf.Lerp(-playerLimit, 0, t),_t.position.y, _t.position.z);
				_t.position = pos;
				yield return new WaitForFixedUpdate();
			}
		}

		yield return null;
	}

	private IEnumerator MoveLeft(Transform _t) 
	{
		if (point == PlayerPoint.CENTER) 
		{
			point = PlayerPoint.LEFT;

			Vector3 pos;
			var t = 0.0f;

			while(t < 1.0f)
			{
				t += Time.fixedDeltaTime / duration;
				t = Mathf.Clamp01(t);
				pos = new Vector3(Mathf.Lerp(0, -playerLimit, t), _t.position.y, _t.position.z);
				_t.position = pos;
				yield return new WaitForFixedUpdate();
			}

		}

		else if (point == PlayerPoint.RIGHT) 
		{
			point = PlayerPoint.CENTER;

			Vector3 pos;
			var t = 0.0f;

			while(t < 1.0f)
			{
				t += Time.fixedDeltaTime / duration;
				t = Mathf.Clamp01(t);
				pos = new Vector3(Mathf.Lerp(playerLimit, 0, t), _t.position.y, _t.position.z);
				_t.position = pos;
				yield return new WaitForFixedUpdate();
			}
		}
		yield return null;
	}


	void ChangePos_Right ()
	{
		if (Input.GetMouseButtonUp(0) && playerPos != playerLimit) 
		{
			rb.velocity = new Vector3 (playerVel,0,0);
		}

		if (playerPos >= playerLimit)
		{
			rb.velocity = Vector3.zero;
			extremidade = true;	

			playerPos = playerLimit;
		}		
	}

	void ChangePos_Left ()
	{		
		if (Input.GetMouseButtonUp(0) && playerPos != playerLimit) 
		{
			rb.velocity = new Vector3 (-playerVel,0,0);
		}

		if (playerPos<=-playerLimit)
		{
			rb.velocity = Vector3.zero;
			extremidade = true;		

			playerPos = -playerLimit;			
		}			
	}
 
	void Destino ()
	{
		if (rb.velocity.x > 0 && playerPos > pontoInicial && extremidade)
		{
			rb.velocity = Vector3.zero;
			extremidade = false;
		}
		else if (rb.velocity.x < 0 && playerPos < pontoInicial && extremidade)
		{
			rb.velocity = Vector3.zero;
			extremidade=false;
		}
	}	

	void BackTopoint ()
	{
		if (Distance.isSmash)
		{
			//  if (playerPos != pontoInicial)
			//  {				
			//  	transform.position = Vector3.Lerp(transform.position, new Vector3(pontoInicial,transform.position.y, transform.position.z), Time.deltaTime * 5);
			// 	extremidade = false;
			//  }	

			 if (playerPos != pontoInicial)
			 {				
			 	if (playerPos < pontoInicial)
			 	{
			 		rb.velocity = new Vector3 (playerVel,0,0);
			 		Invoke ("Back2", 0.1f);
			 		extremidade = false;		

					 point = PlayerPoint.CENTER;			
			 	}
			 	if (playerPos > pontoInicial)
			 	{
			 		rb.velocity = new Vector3 (-playerVel,0,0);
			 		Invoke ("Back2", 0.1f);
			 		extremidade = false;

					point = PlayerPoint.CENTER;					
			 	}
			 }				
		}
	}

	void Back2 ()
	{
		transform.position = new Vector3 (0,transform.position.y,transform.position.z);
		//transform.position = Vector3.Lerp(transform.position, new Vector3(0,transform.position.y, transform.position.z), Time.deltaTime * 5);
		rb.velocity = Vector3.zero;
		//rotY = 0;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("Bomb")) 
		{
			gotHit = true;
			Debug.Log("Acertou!");

			StartCoroutine (Blink());
		}
	}

	public IEnumerator Blink ()
	{
		liraRender.SetActive (false);
		dragonRender.SetActive (false);
		yield return new WaitForSeconds (0.15f);
		liraRender.SetActive (true);
		dragonRender.SetActive (true);
		yield return new WaitForSeconds (0.15f);
		liraRender.SetActive (false);
		dragonRender.SetActive (false);
		yield return new WaitForSeconds (0.15f);
		liraRender.SetActive (true);
		dragonRender.SetActive (true);
		yield return new WaitForSeconds (0.15f);
		liraRender.SetActive (false);
		dragonRender.SetActive (false);
		yield return new WaitForSeconds (0.15f);
		liraRender.SetActive (true);
		dragonRender.SetActive (true);
	}
}