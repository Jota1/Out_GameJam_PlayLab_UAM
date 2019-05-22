using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour 
{
	public static ObjectPooler SharedInstance;
	
	public List<GameObject> pooledObjects;
	public List<GameObject> pooledCoins;
	public GameObject objectToPool;
	public GameObject coinsToPool;
	public GameObject coinGroupPrefab;
	public int amountToPool;
	public int amountOfCoins;

	void Awake() 
	{
  		SharedInstance = this;
	}

	void Start () 
	{
		pooledObjects = new List<GameObject>();

		for (int i = 0; i < amountToPool; i++)
	 	{
  			GameObject obj = (GameObject)Instantiate(objectToPool);
  			obj.SetActive(false); 
  			pooledObjects.Add(obj);
		}

		pooledCoins = new List<GameObject>();

		for (int i = 0; i < amountOfCoins; i++)
		{
			GameObject coins = (GameObject)Instantiate(coinsToPool);
			coins.SetActive(false);
			pooledCoins.Add(coins);
		}

		PoolHandler.Instance.AddPool(coinGroupPrefab);
		PoolHandler.Instance.AddPool(coinsToPool);		
	}	
	
	public GameObject GetPooledObject(string objectPooled) 
	{
		if(objectPooled == "bullet")
		{
			for (int i = 0; i < pooledObjects.Count; i++) 
  			{
    			if (!pooledObjects[i].activeInHierarchy) 
				{
      				return pooledObjects[i];
    			}
  			}	 
		}

		else if(objectPooled == "coin")
		{
			return PoolHandler.Instance.GetObject(coinsToPool.name); 
		}
		else if(objectPooled == "coinGroup")
		{
			return PoolHandler.Instance.GetObject(coinGroupPrefab.name);
		}

  		return null;
	}
}
