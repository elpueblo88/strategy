using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public float xPosStart;
	public float yPosStart;
	public float zPosStart;
	
	public float xRot;
	
	public float xSpeed;
	public float zSpeed;
	
	GameObject cameraBox;
	
	
	// Use this for initialization
	void Start () 
	{
		if(xPosStart == 0)
		{
			xPosStart = 60;	
		}
		if(yPosStart == 0)
		{
			yPosStart = 35;	
		}
		if(zPosStart == 0)
		{
			zPosStart = -10;	
		}
		
		if(xRot == 0)
		{
			xRot = 45;	
		}
		
		if(xSpeed == 0)
		{
			xSpeed = 10;	
		}
		if(zSpeed == 0)
		{
			zSpeed = 10;	
		}
		
		cameraBox = new GameObject();
		cameraBox.name = "CameraBox";
		cameraBox.transform.position = new Vector3(xPosStart, yPosStart, zPosStart);
		transform.parent = cameraBox.transform;
		transform.localPosition = Vector3.zero;
		
		//transform.position = new Vector3(xPosStart, yPosStart, zPosStart);
		transform.localRotation = Quaternion.Euler(xRot, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButton("CamRight"))
		{
			cameraBox.transform.Translate(Time.deltaTime * xSpeed, 0, 0);
		}
		
		if(Input.GetButton("CamLeft"))
		{
			cameraBox.transform.Translate(Time.deltaTime * -xSpeed, 0, 0);
		}
		
		if(Input.GetButton("CamUp"))
		{
			cameraBox.transform.Translate(0, 0, Time.deltaTime * zSpeed);	
		}
		
		if(Input.GetButton("CamDown"))
		{
			cameraBox.transform.Translate(0, 0, Time.deltaTime * -zSpeed);	
		}
	}
}