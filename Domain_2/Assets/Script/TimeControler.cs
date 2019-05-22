using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeControler : MonoBehaviour
{
	float counter;
	[SerializeField] float PhaseTime;	


	public Image distance; 
	public RectTransform liraImage;

	public GameObject fingerAnima;
	public float distanceVel;

	 public GameObject PopUp;
	 public GameObject dragonRender;
	 public GameObject liraRender;
	 public GameObject carroRender;
	 public GameObject bombaRender;	 
	 public TextMeshProUGUI scoreText;

	void Start () 
	{
		counter = PhaseTime;

		fingerAnima.SetActive (false);
		carroRender.SetActive (true);
		dragonRender.SetActive (true);
		liraRender.SetActive (true);
		bombaRender.SetActive (true);
		PopUp.SetActive (false);

		Fire.stopFiringAuto = false;		
	}	
	
	void Update () 
	{
		counter -= Time.deltaTime;

		if (counter <= 0)
		{			
			distanceVel = 0;

			carroRender.SetActive (false);
			dragonRender.SetActive (false);
			liraRender.SetActive (false);
			bombaRender.SetActive (false);
			PopUp.SetActive (true);	

			Fire.stopFiringAuto = true;	

			scoreText.text = ("Coins: ") + Coins.score.ToString ();
			Debug.Log (Coins.score);	
		}

		distance.transform.Translate (Vector3.right * Time.deltaTime * distanceVel, Space.World);
		//liraImage.Translate (Vector3.right * Time.deltaTime * distanceVel, Space.World);
		//distance.rectTransform.pivot.Translate (Vector3.right * Time.deltaTime * distanceVel, Space.World);

		if (Distance.isSmash)
		{
			fingerAnima.SetActive (true);
		} else { fingerAnima.SetActive (false); }
	}

	public void ResetScene () 
	{
		SceneManager.LoadScene (0);
	}
}
