using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGroup : MonoBehaviour 
{
	[Header("References")]
	[SerializeField] GameObject coinPrefab;
	public Transform startPoint;
	[SerializeField] Vector3[] coinOffsets;
	[SerializeField] float distanceMultMin;
	[SerializeField] float distanceMultMax;
	[SerializeField] float zMovement = 1.5f;

	[Header("Animation")]
	[SerializeField] float duration = 1.0f;
	[SerializeField] AnimationCurve xCurve;
	[SerializeField] AnimationCurve yCurve;
	[SerializeField] AnimationCurve zCurve;

	bool setupDone;

	void Update()
	{
		if(transform.childCount == 0 && setupDone)
		{
			PoolHandler.Instance.StoreObject(gameObject);
		} 
	}

	void OnDisable()
	{
		setupDone = false;
	}

	public void SpawnCoins(Vector3 startPosition, float min, float max)
	{
		GameObject obj;
		Vector3 targetPos;
		for (int i = 0; i < coinOffsets.Length; i++)
		{
			obj = PoolHandler.Instance.GetObject(coinPrefab.name, startPosition);
			obj.transform.parent = transform;
			targetPos = transform.position + (coinOffsets[i] * Random.Range(min, max));
			StartCoroutine(MoveCoin_Routine(obj.GetComponent<Coins>(), startPosition, targetPos));
		}

		setupDone = true;
	}
	public void SpawnCoins(Vector3 startPosition)
	{
		SpawnCoins(startPosition, distanceMultMin, distanceMultMax);
	}
	public void SpawnCoins()
	{
		SpawnCoins(startPoint.position);
	}

	IEnumerator MoveCoin_Routine(Coins coin, Vector3 start, Vector3 endPos)
	{
		var t = 0.0f;
		float xFactor, yFactor, zFactor;
		float x, y, z;
		coin.SetActive(false);

		while(t < 1.0f)
		{
			t += Time.fixedDeltaTime / duration;
			Mathf.Clamp01(t);

			xFactor = xCurve.Evaluate(t); 
			yFactor = yCurve.Evaluate(t); 
			zFactor = zCurve.Evaluate(t);

			x = Mathf.Lerp(start.x, endPos.x, xFactor);
			y = Mathf.Lerp(start.y, endPos.y, yFactor);
			z = start.z - (zFactor * zMovement);

			coin.transform.position = new Vector3(x, y, z); 
			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(0.02f);
		coin.SetActive(true);
	}	
}
