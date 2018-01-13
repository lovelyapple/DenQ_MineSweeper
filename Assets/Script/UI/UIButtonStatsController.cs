using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonStatsController : MonoBehaviour {
	public bool pressing = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PushDown()
	{
		pressing = true;
	}
	public void LeaveAway()
	{
		pressing = false;
	}
	public bool GetButtonPress()
	{
		return pressing;
	}
}
