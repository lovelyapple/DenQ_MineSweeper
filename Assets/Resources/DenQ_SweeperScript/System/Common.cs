using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DenQ.BaseStruct
{
    public enum FIELD_BLOCK
    {
        NORMAL,
    }
    public enum FIELD_ITEM
    {
        BOMB_NORMAL,
        BOMB_DELAY,
        NONE,
    }
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

    public class DenQHelper : MonoBehaviour
    {
        public static Vector3 ConvertFieldPosToWorld(FieldPos pos)
        {
            return new Vector3(pos.posX * 2.0f, 0.0f, pos.posZ * 2.0f);
        }

        public static GameObject InstialHelper(GameObject prefab, GameObject parent)
        {
            return (GameObject)Instantiate(prefab, parent.transform);
        }
        public static GameObject InstialHelper(GameObject prefab)
        {
            return (GameObject)Instantiate(prefab);
        }
    }
}

