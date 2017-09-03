using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;

public class SystemManager : MangerBase<SystemManager> 
{
	public TableReader tableReader = null;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		SetInstance(this);
	}
	void /// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	OnEnable()
	{
		if(tableReader == null)
		tableReader = GetComponent<TableReader>();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		DenQLogger.Runing();
	}
	public void ReadTable()
	{
		if(tableReader != null)
		tableReader.ReadTable();
	}
}
