using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class ResourcesHolder : MonoBehaviour
{
    private static Dictionary<PREFABU_NAME, GameObject> PrefabsDict = null;
    void Awake()
    {
        PrefabsDict = new Dictionary<PREFABU_NAME, GameObject>();
        PrefabsDict.Clear();
		LoadBasePrefab();
    }
    private static void LoadBasePrefab()
    {
        for (int i = 0; i < (uint)PREFABU_NAME.max; i++)
        {
			GameObject tempOjb = ResourcesHelper.LoadResourcesPrefab((PREFABU_NAME)i);
			if(tempOjb == null)
			{
				continue;
			}
			PrefabsDict.Add((PREFABU_NAME)i,tempOjb);
        }
    }
	public static GameObject GetPrefabByName(PREFABU_NAME name)
	{
		GameObject tempOBj = null;
		if(!PrefabsDict.TryGetValue(name,out tempOBj))
		{
			return null;
		}
		return tempOBj;
	}
    // Use this for initialization
    void Start(){}
    // Update is called once per frame
    void Update(){}
}
