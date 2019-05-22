using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour 
{
	[SerializeField] float startDelay;
	[Header("Animation")]
	[SerializeField] AnimationCurve yCurve; 
	[SerializeField] AnimationCurve zCurve;
	[SerializeField] float zMovement = 1.5f;
	private Rigidbody rb;
	private MeshRenderer mr;

	public GameObject carr;

	private Vector3 startingPosition;
	private Vector3 currentPosition;

	private float cooldownTime;
	public float fireRate;

	[Header ("Parabola")]
	[SerializeField] float velY;
	[SerializeField] float velZ;
	[SerializeField] float forceZ;

	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		mr = GetComponent<MeshRenderer>();
		mr.enabled = false;
		cooldownTime = fireRate;
	}
	
	
	void Update () 
	{
		rb.AddForce(0, 0, forceZ, ForceMode.Force);
		startingPosition = carr.transform.position;
		if(!Distance.isSmash) Shoot();

		if(transform.position.y <= -12f)
		{
			transform.position = carr.transform.position;
			rb.velocity = Vector3.zero;
			mr.enabled = false;
		}
	}

	void Shoot()
	{
		if(Time.timeSinceLevelLoad < startDelay) return;

		cooldownTime -= Time.deltaTime;

		if (cooldownTime <= 0)
		{
			transform.position = startingPosition;
			mr.enabled = true;
			//rb.velocity = new Vector3 (0, -velY, -velZ);
			StartCoroutine(ArcMovement(transform, -12f, 1f, carr.transform.position));

			cooldownTime = fireRate;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			transform.position = startingPosition;
			rb.velocity = Vector3.zero;
			mr.enabled = false;		

			Debug.Log ("Colider");
		}
	}

	private IEnumerator ArcMovement(Transform transform, float posFinalY, float duration, Vector3 start)
	{
		var t = 0.0f;
		float yFactor, zFactor;
		float y, z;

			while(t < 1.0f)
			{
				t += Time.fixedDeltaTime / duration;
				t = Mathf.Clamp01(t);

				yFactor = yCurve.Evaluate(t);
				zFactor = zCurve.Evaluate(t);

				y = Mathf.Lerp(start.y, posFinalY, yFactor);
				z = start.z - (zFactor * zMovement);

				transform.position = new Vector3(transform.position.x, y, z);
				yield return new WaitForFixedUpdate();
			}
	}
}
