using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	/*
	 * Description:
	 * This script handles parallax for background items, which do not make up he entire background
	 * Distance x and y affect how far they move when the camera moves
	 * Smoothing affects how smoothly they will move
	 */

	//Public variables
	Transform cam; // Camera reference (of its transform)
	Vector2 previousCamPos;
	public float smoothing = 1f; // Smoothing factor of parrallax effect
	public float distanceX = 0f; //Distance from camrea on theoretical z plane
	public float distanceY = 0f;

	//Private variables
	private Vector2 backgroundTargetPos;
	private float parallaxX;
	private float parallaxY;
	
	//Register camera
	void Awake()
	{
		//Register the camera at the beginning
		cam = Camera.main.transform;
		//Build the initial paralax based on start position and initial camera position
		parallaxX = (transform.position.x - cam.position.x) * distanceX;
		parallaxY = (transform.position.y - cam.position.y) * distanceY;
		backgroundTargetPos = new Vector2(transform.position.x + parallaxX, transform.position.y + parallaxY);
		transform.position = Vector3.Lerp(transform.position, backgroundTargetPos, smoothing * Time.deltaTime);
	}

	void FixedUpdate()
	{
		//Get camera movement distance and multiply by parallax effect
		parallaxX = (previousCamPos.x - cam.position.x) * distanceX;
		parallaxY = (previousCamPos.y - cam.position.y) * distanceY;
		//Newposition for the parallax object
		backgroundTargetPos = new Vector2(transform.position.x + parallaxX, transform.position.y + parallaxY);

		//Go to new position
		transform.position = Vector3.Lerp(transform.position, backgroundTargetPos, smoothing * Time.deltaTime);

		//Register curren camera position as previous camera positino for next frame
		previousCamPos = cam.position;
	}

	private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(transform.position, new Vector2(limits,limits));
    }
}
