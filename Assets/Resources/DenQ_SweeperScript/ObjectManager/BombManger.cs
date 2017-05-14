using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;
using DenQ.BaseStruct;

public class BombManger : MangerBase<FieldMgr>
{
	private static List<FieldBomb> BombList = null;
	static void Awake()
	{
		BombList = new List<FieldBomb>();
	}
	public static void CreateBomb(BOMB_TYPE bomtType)
	{
		//ResourcesHelper.CreateResourcesInstance()
	}
}
