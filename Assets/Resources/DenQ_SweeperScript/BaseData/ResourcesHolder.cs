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

    public static bool ExistList()
    {
        if(PrefabsDict == null)
        return false;
        return PrefabsDict.Count > 0;
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
  /*
    public static bool LoadCompleted = false;
    public enum PREFAB_NAME
    {
        //------------マネージャー系
        RESOURCES_HOLDER = 0,
        FIELD_ROOT,
        ITEM_ROOT,
        BOMB_ROOT,
        FIELD_MGR,

        //------------普通のプレハブ

        FIELD_BLOCK,
        FIELD_BOMB,
        FIELD_ITEM,//暫定

        PREFAB_MAX,
    };
  
    private static Dictionary<PREFAB_NAME, string> PrefabPath = new Dictionary<PREFAB_NAME, string>()
        {
            {PREFAB_NAME.RESOURCES_HOLDER,
            "DenQ_SweeperPrefab/ManagerPrefab/ResourceHolder"},
            {PREFAB_NAME.FIELD_ROOT,
            "DenQ_SweeperPrefab/RootPrefab/FieldRoot"},
            {PREFAB_NAME.ITEM_ROOT,
            "DenQ_SweeperPrefab/RootPrefab/ItemRoot"},
            {PREFAB_NAME.BOMB_ROOT,
            "DDenQ_SweeperPrefab/RootPrefab/BombRoot"},
            {PREFAB_NAME.FIELD_MGR,
            "DenQ_SweeperPrefab/ManagerPrefab/FieldManager"},


            {PREFAB_NAME.FIELD_BLOCK,
            "DenQ_SweeperPrefab/FieldObejctRootPrefab/FieldBlock"},
            {PREFAB_NAME.FIELD_BOMB,
            "DenQ_SweeperPrefab/FieldObejctRootPrefab/FieldBomb"},
            {PREFAB_NAME.FIELD_BOMB,
            "DenQ_SweeperPrefab/FieldObejctRootPrefab/FieldItem"},
        };
    private static Dictionary<PREFAB_NAME, GameObject> PrefabHolderDict = null;
    void Awake()
    {
        LoadCompleted = false;
        PrefabHolderDict.Clear();

    }
    IEnumerator LoadBaseicResources()
    {
        if (PrefabPath.Count < (int)PREFAB_NAME.PREFAB_MAX)
            yield break;
        foreach (PREFAB_NAME name in Enum.GetValues(typeof(PREFAB_NAME)))
        {
            if (name == PREFAB_NAME.PREFAB_MAX)
            {
                yield break;
            }
            GameObject obj = LoadPrefabFromPath(PrefabPath[name]);
            PrefabHolderDict.Add(name, obj);
            yield return null;
        }
        yield return null;
    }

    GameObject LoadPrefabFromPath(string path)
    {
        GameObject _prefab = (GameObject)Resources.Load(path);
        if (_prefab == null)
        {
            Debug.Log("can not read" + path);
            return null;
        }
        return _prefab;
    }

    public GameObject CreateInstanceFromPrefab(PREFAB_NAME name, GameObject parent, Vector3 pos)
    {
        if (PrefabHolderDict.Count <= 0)
        {
            Debug.LogError("There is noth in holder");
            return null;
        }
        if (name == PREFAB_NAME.PREFAB_MAX)
        {
            Debug.LogError("wrong PREFABNAME");
            return null;
        }
        GameObject prefab;
        if (!PrefabHolderDict.TryGetValue(name, out prefab))
        {
            Debug.LogError("Failed; to Find the Prefab");
            return null;
        }
        GameObject instance = Instantiate(prefab);
        if(instance == null)
        {
            Debug.Log("Failed to Create an instance");
        }
        if(parent != null)
        {
            instance.transform.parent = parent.transform;
        }
        if(pos != Vector3.zero)
        {
            instance.transform.position = pos;
        }
        return instance;
    }
    */