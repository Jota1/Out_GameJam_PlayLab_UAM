using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
 { 
    private const float backgroundHeight = 27f;
	public float moveSpeed;

	public Transform [] Backgrounds;  
	private Renderer render;
	private float offset;
	public float scrollSpeed;
	   

	void Start () 
	{
		render = GetComponent<Renderer>();
	}
	
	void Update () 
	{
		//MoveBackGround (Backgrounds, moveSpeed, backgroundHeight);
		
		offset = Time.time * scrollSpeed;
		render.material.mainTextureOffset = new Vector2(0, offset);
	}
	 void MoveBackGround (Transform[]objects, float moveSpeed, float height)    
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Vector3 bgPosition = objects[i].position;

            if (bgPosition.y <= height * -1)
                objects[i].position = new Vector3(bgPosition.x, bgPosition.y + height * 2, bgPosition.z); 

            objects[i].Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
	}


}

