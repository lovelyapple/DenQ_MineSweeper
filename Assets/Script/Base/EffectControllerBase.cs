using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
using DenQData;

public class EffectControllerBase : MonoBehaviour 
{
	public EffectData effectData;
	public float lifeTIme;
	public ulong fieldEffectId;
	public void SetUp(EffectData data,ulong idx)
	{
		this.effectData = data;
		this.fieldEffectId = idx;
		if(data.lifeTime != 0)
		{
			this.lifeTIme = data.lifeTime;
		}
	}
	void Update()
	{
		if(lifeTIme > 0)
		{
			lifeTIme -= Time.deltaTime;
			return;
		}
		Destroy(this.gameObject);
	}

	void OnDisable()
	{
		FieldEffectManager.GetInstance().RemoveOneEffect(fieldEffectId);
	}
}
