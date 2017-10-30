using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public ActionBase actionCurrently { get; private set; }
	public string actionNameCurrently;
	public void Update()
	{
		actionNameCurrently = actionCurrently == null? " Doing Noth " : actionCurrently.ToString();
	}

	//TODO FieldObjectStat と　AI　の実装
}
