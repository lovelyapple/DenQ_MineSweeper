using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenQ.BaseStruct
{

    public enum PREFABU_NAME
    {
        FieldBlock = 0,
        FiedlBomb,
        FieldItem,
        max,
    }
    public enum GOD_PREFAB_NAME
    {
        RESOURCES_HOLDER,
        FIELD_ROOT,
        BOMB_MGR,
        FIELD_MGR,
    }
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
            "DenQ_SweeperPrefab/RootPrefab/BombManager",
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
        public static GameObject LoadResourcesInstance(string path, GameObject parent)
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
        public static GameObject LoadResourcesInstance(GameObject orefabObj, GameObject parent, Vector3 pos)
        {
            if (orefabObj == null || parent == null)
            {
                return null;
            }
            GameObject instance = Instantiate(orefabObj);
            instance.transform.parent = parent.transform;
            instance.transform.position = pos;
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
