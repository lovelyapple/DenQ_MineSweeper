using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
using DenQData;
public class FieldEffectManager :  ManagerBase <FieldEffectManager>
{
	void Awake()
	{
		SetInstance(this);		
	}
	ulong idxRecently = 0;
	public Dictionary<ulong,EffectControllerBase>	fieldEffectList = new Dictionary<ulong, EffectControllerBase>();

	void Update()
	{
		if(Input.anyKeyDown)
		{
			CreateFieldEffect(10000000,Vector3.zero);
		}	
	}
	public EffectData CreateFieldEffect(ulong effectCode ,Vector3 pos)
	{
		var go = ResourcesManager.GetInstance().CreateEffectObjectInstance(effectCode,this.gameObject.transform,pos);
		
		if(go != null)
		{
			var effectCtrl = go.GetComponent<EffectControllerBase>();

			if(effectCtrl == null)
			{
				Logger.SError("could not find effect controller in effect " + effectCode);
				return null;
			}

			var data = EffectTableHelper.GetEffectDataById(effectCode);

			if(data == null)
			{
				return null;
			}
			
			effectCtrl.SetUp(data,idxRecently);
			fieldEffectList.Add(idxRecently,effectCtrl);
			idxRecently ++;
		}
		
		return null;
	}

	public void ClearAllEffect()
	{
		foreach(var idx in fieldEffectList.Keys)
		{
			try
			{
				var data = fieldEffectList[idx];
				data.gameObject.transform.parent = null;
				Destroy(data.gameObject);
			}
			catch
			{
				// do noth
			}
		}
		fieldEffectList.Clear();
		idxRecently = 0;
	}

	public void RemoveOneEffect(ulong effectId)
	{
		if(fieldEffectList.ContainsKey(effectId))
		{
			var data = fieldEffectList[effectId];
			fieldEffectList.Remove(effectId);
			data.gameObject.transform.parent = null;
		}
	}
}
