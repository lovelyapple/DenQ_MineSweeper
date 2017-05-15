using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenQ.BaseStruct
{
    public enum BLOCK_TYPE
    {
        ITEM,
        NONE,
    }
    public enum FIELD_BOMB
    {
        BOMB_NORMAL,
        BOMB_DELAY,

    }
    public enum FIELD_ITEM
    {
        BOMB = 0,
        HEALTH,
        NONE,
    }
    public enum BOMB_TYPE
    {
        NORMAL = (int)FIELD_BOMB.BOMB_NORMAL,
        DELAY = (int)FIELD_BOMB.BOMB_DELAY,
    }
    public enum PREFABU_NAME
    {
        FieldBlock,
        FiedlBomb,
        FieldItem,
        max,
    }
    public enum GOD_PREFAB_NAME
    {
        RESOURCES_HOLDER,
        FIELD_ROOT,
        ITEM_ROOT,
        BOMB_ROOT,
        FIELD_MGR,
    }
    //Down=========================- FIEL PATH LCASS ================================Down//
    public class FilePath
    {
        public static string[] NormalPrefabPath = new string[]
       {
            "DenQ_SweeperPrefab/FieldObejctRootPrefab/FieldBlock",
            "DenQ_SweeperPrefab/FieldObejctRootPrefab/FieldBomb",
            "DenQ_SweeperPrefab/FieldObejctRootPrefab/FieldItem",
       };
        public static string[] GodPrefabPath = new string[]
        {
            "DenQ_SweeperPrefab/ManagerPrefab/ResourceHolder",
            "DenQ_SweeperPrefab/RootPrefab/FieldRoot",
            "DenQ_SweeperPrefab/RootPrefab/ItemRoot",
            "DenQ_SweeperPrefab/RootPrefab/BombRoot",
            "DenQ_SweeperPrefab/ManagerPrefab/FieldManager",
        };
        public static string GetPrefabPath(PREFABU_NAME name)
        {
            return NormalPrefabPath[(uint)name];
        }
        public static string GetGodPrefabPath(GOD_PREFAB_NAME name)
        {
            return GodPrefabPath[(uint)name];
        }
    }
    //Up==========================- FIEL PATH LCASS =================================Up//

    //Down========================- DIELD POS LCASS ===============================Down//
    public class FieldPos : IComparable<FieldPos>
    {
        public int posX;
        //{set { posX = 0; } get { return posX; }  }
        public int posZ;
        //{set { posZ = 0; } get { return posZ; }  }
        public FieldPos()
        {
        }
        public FieldPos(int x, int z)
        {

            this.posX = Mathf.Max(x, 0);
            this.posZ = Mathf.Max(z, 0);
        }
        public int CompareTo(FieldPos other)
        {
            if (other == null)
                return -1;
            if (other.posX == this.posX && other.posZ == this.posZ)
                return 0;
            else
                return 1;
        }

    }
    //UP==========================- DIELD POS LCASS =================================Up//
    //Prefabを読み込む、ほぼゲーム初期化する時のみに実行
    //軽いオブジェクトのPrefabはListでRescourcesHolderに保存している。
    public class ResourcesHelper : MonoBehaviour
    {
        public static GameObject LoadResourcesPrefab(string path)
        {
            GameObject _prefab = (GameObject)Resources.Load(path);
            if (_prefab == null)
            {
                Debug.Log("can not read" + path);
                return null;
            }
            return _prefab;
        }
        public static GameObject LoadResourcesPrefab(PREFABU_NAME name)
        {
            string path = FilePath.GetPrefabPath(name);
            GameObject _prefab = (GameObject)Resources.Load(path);
            if (_prefab == null)
            {
                Debug.Log("can not read" + path);
                return null;
            }
            return _prefab;
        }
        //Prefabからゲームオブジェクトのインスタンスを作る
        public static GameObject CreateResourcesInstance(string path, GameObject parent)
        {
            if (parent == null)
            {
                return null;
            }
            GameObject _prefabTemp = LoadResourcesPrefab(path);
            if (_prefabTemp == null)
            {
                return null;
            }
            GameObject instance = Instantiate(_prefabTemp);
            if (parent != null)
            {
                instance.transform.parent = parent.transform;
            }
            return instance;
        }
        public static GameObject CreateResourcesInstance(GameObject prefabObj, GameObject parent, Vector3 pos)
        {
            if (prefabObj == null)
            {
                return null;
            }

            GameObject instance = Instantiate(prefabObj);
            if (parent == null)
            {
                Debug.LogWarning("there is no parent when create new instance of " + prefabObj.name);
            }
            else
            {
                instance.transform.parent = parent.transform;
            }
            instance.transform.position = pos;
            return instance;
        }
        public static GameObject CreateResourcesInstance(GameObject prefabObj, GameObject parent)
        {
            if (prefabObj == null)
            {
                return null;
            }

            GameObject instance = Instantiate(prefabObj);
            if (parent == null)
            {
                Debug.LogWarning("there is no parent when create new instance of " + prefabObj.name);
            }
            else
            {
                instance.transform.parent = parent.transform;
            }
            return instance;
        }
        public static GameObject CreatePrefabinstance(uint prefabID, bool isGod, GameObject parent, Vector3 pos)
        {
            if (!ResourcesHolder.ExistList())
            {
                Debug.LogError("There is noth in holder");
                return null;
            }
            if (isGod)
            {
                Debug.LogError("Build GodObejct is not avaliable recently");
                return null;
            }
            else
            {
                if (prefabID >= (uint)PREFABU_NAME.max)
                {
                    Debug.LogError("wrong id while creating ID");
                    return null;
                }
                return CreateResourcesInstance(ResourcesHolder.GetPrefabByName((PREFABU_NAME)prefabID), parent, pos);
            }
        }
        public static GameObject CreatePrefabinstance(uint prefabID, bool isGod, GameObject parent)
        {
            if (!ResourcesHolder.ExistList())
            {
                Debug.LogError("There is noth in holder");
                return null;
            }
            GameObject instance = new GameObject();
            if (isGod)
            {
                Debug.LogError("Build GodObejct is not avaliable recently");
                return null;
            }
            else
            {
                if (prefabID >= (uint)PREFABU_NAME.max)
                {
                    Debug.LogError("wrong id while creating ID");
                    return null;
                }
                instance = CreateResourcesInstance(ResourcesHolder.GetPrefabByName((PREFABU_NAME)prefabID), parent);

            }
            return instance;
        }
    }
    public class DenQHelper : MonoBehaviour
    {
        public static Vector3 ConvertFieldPosToWorld(FieldPos pos)
        {
            return new Vector3(pos.posX * 1.0f, 0.0f, pos.posZ * 1.0f);
        }
    }
}

