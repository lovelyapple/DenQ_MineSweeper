using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour {
	public FuncSimpleFollow	cameraFollow = null;
	public GameObject targetObject = null;
	// Use this for initialization
	public void Initialization()
	{
		cameraFollow = GameObject.FindWithTag("MainCamera").GetComponent<FuncSimpleFollow>();
		targetObject = GameObject.FindWithTag("CameraTarget");
	}
	
	// Update is called once per frame
	void Update () {
		if(cameraFollow == null || targetObject == null)
		{
			Initialization();
		}
	}
}
