using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DenQ.BaseStruct
{
    public enum PARTICLE_TYPE
    {
        ONCE_ONLY,
        RECYCLE,
        TIMES,
    }
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
    public enum FIELD_ITEM_TOKEN
    {
        FIELD_ITEM_BLOCK = 1000,
        FIELD_ITEM_BOMB = 2000,
        NONE,
    }
    //Down======================== FIELD POS LCASS ===============================Down//
    public class FieldPos : IComparable<FieldPos>
    {
        public int unitCode = 0;
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
    //UP==========================- IFELD POS CLASS =================================Up//S
    public class DenQHelper : MonoBehaviour
    {
        public static int maxFieldBlockCount { get { return 5; } }
        public static float fieldBlcokSize { get { return 2.0f; } }
        public static float maxFieldSize { get { return (float)((float)maxFieldBlockCount * fieldBlcokSize); } }
        public static long maxFieldCode{
            get{
                return (maxFieldBlockCount * 100 + maxFieldBlockCount);
            }
        }
        public static Dictionary<long, FieldBlock> GetIinitiatedField()
        {
            var dic = new Dictionary<long, FieldBlock>();
            for (int x = 0; x < maxFieldBlockCount; x++)
                for (int z = 0; z < maxFieldBlockCount; z++)
                {
                    dic.Add(CreateCodeByPos(x,z),null);
                }
            return dic;
        }
        public static long ConvertFieldPosToCode(FieldPos fieldPos)
        {
            return (long)(fieldPos.posX * 100 + fieldPos.posZ);
        }
        public static long CreateCodeByPos(int x,int z)
        {
            return x * 100 + z;
        }
        //フィールドの番後から、開始位置を検索
        public static Vector3 ConvertFieldPosToWorld(FieldPos pos, int fieldUnitCode)
        {
            return ConvertFieldPosToWorld(pos, GetUnitStartPosition(fieldUnitCode));
        }
        public static Vector3 ConvertFieldPosToWorld(FieldPos pos, Vector3 startPos)
        {
            return new Vector3(pos.posX * fieldBlcokSize, 0.0f, pos.posZ * fieldBlcokSize);
        }

        public static FieldPos ConverWoroldPosToFieldPos(Vector3 pos, int fieldUnitCode)
        {
            var fieldPos = new FieldPos(((int)pos.x / (int)fieldBlcokSize), ((int)pos.z / (int)fieldBlcokSize));
            int fieldCodeX = fieldUnitCode % 100;
            int fieldCodeZ = fieldUnitCode / 100;
            fieldPos.posX -= fieldCodeX * maxFieldBlockCount;
            fieldPos.posZ -= fieldCodeZ * maxFieldBlockCount;
            return fieldPos;
        }
        public static GameObject InstialHelper(GameObject prefab, GameObject parent)
        {
            return (GameObject)Instantiate(prefab, parent.transform);
        }
        public static GameObject InstialHelper(GameObject prefab)
        {
            return (GameObject)Instantiate(prefab);
        }

        public static Collider[] GetSroundedObejcts(FieldPos pos, int fieldUnitCode)
        {
            Vector3 center = ConvertFieldPosToWorld(pos, fieldUnitCode);
            return Physics.OverlapBox(center, new Vector3(fieldBlcokSize, fieldBlcokSize, fieldBlcokSize));
        }
        public static Collider[] GetSroundedObejcts(Vector3 pos)
        {
            Vector3 center = pos;
            return Physics.OverlapBox(center, new Vector3(fieldBlcokSize, fieldBlcokSize, fieldBlcokSize));
        }
        public static Vector2 GetUnitStartPosition(int unitCode)
        {
            int fieldCodeX = unitCode % 100;
            int fieldCodeZ = unitCode / 100;
            Vector2 startPos = new Vector2();
            startPos.x = (float)fieldCodeX * maxFieldSize;
            startPos.y = (float)fieldCodeZ * maxFieldSize;
            return startPos;
        }
    }
    public class DenQUIhelper
    {
        //TODO null? にしたほうがいいかな
        public static Vector2 GetPanelPosition(RectTransform canvasRect, RectTransform panelRect, Vector3 screenPos)
        {
            Vector2 panelPos = new Vector2();
            Vector2 aspectCanvas = new Vector2(canvasRect.rect.width / Camera.main.pixelWidth,
                                                canvasRect.rect.height / Camera.main.pixelHeight);
            panelPos.x = aspectCanvas.x * screenPos.x;
            panelPos.y = aspectCanvas.y * screenPos.y;
            panelPos.x = panelPos.x - canvasRect.rect.width / DenQHelper.fieldBlcokSize;
            panelPos.y = panelPos.y - canvasRect.rect.height / DenQHelper.fieldBlcokSize;
            panelPos -= panelRect.anchoredPosition;
            return panelPos;
        }
        public static Vector2 GetPanelPosition(RectTransform canvasRect, RectTransform panelRect)
        {
            return GetPanelPosition(canvasRect, panelRect, DenQ_Input.GetTouchPosition());
        }
    }

}

