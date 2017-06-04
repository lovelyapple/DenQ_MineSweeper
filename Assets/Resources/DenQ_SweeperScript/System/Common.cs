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
        public static FieldPos ConverWoroldPosToFieldPos(Vector3 pos)
        {
            var fieldPos = new FieldPos(((int)pos.x / 2), ((int)pos.z / 2));
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

        public static Collider[] GetSroundedObejcts(FieldPos pos)
        {
            Vector3 center = ConvertFieldPosToWorld(pos);
            return Physics.OverlapBox(center, new Vector3(2.0f, 2.0f, 2.0f));
        }
        public static Collider[] GetSroundedObejcts(Vector3 pos)
        {
            Vector3 center = pos;
            return Physics.OverlapBox(center, new Vector3(2.0f, 2.0f, 2.0f));
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
            panelPos.x = panelPos.x - canvasRect.rect.width / 2.0f;
            panelPos.y = panelPos.y - canvasRect.rect.height / 2.0f;
            panelPos  -= panelRect.anchoredPosition;
            return panelPos;
        }
        public static Vector2 GetPanelPosition(RectTransform canvasRect, RectTransform panelRect)
        {           
            return GetPanelPosition(canvasRect,panelRect,DenQ_Input.GetTouchPosition());
        }
    }

}

