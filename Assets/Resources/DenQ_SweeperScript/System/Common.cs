using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DenQData
{
    public static class ClientSettings
    {
        ///ブロック一個あたりの幅
        public static float FieldBlockSize { get { return 2.0f; } }
        public static uint MaxFieldSize { get { return 1000; } }
        public static Vector3 SroundBlockVector { get { return Vector3.one * FieldBlockSize; } }
    }
}
// public class DenQHelper : MonoBehaviour
// {
//     public static int maxFieldBlockCount { get { return 5; } }
//     public static float fieldBlcokSize { get { return 2.0f; } }
//     public static float maxFieldSize { get { return (float)((float)maxFieldBlockCount * fieldBlcokSize); } }
//     public static long maxFieldCode
//     {
//         get
//         {
//             return (maxFieldBlockCount * 100 + maxFieldBlockCount);
//         }
//     }
//     public static Dictionary<long, FieldBlock> GetIinitiatedField()
//     {
//         var dic = new Dictionary<long, FieldBlock>();
//         for (int x = 0; x < maxFieldBlockCount; x++)
//             for (int z = 0; z < maxFieldBlockCount; z++)
//             {
//                 dic.Add(CreateCodeByPos(x, z), null);
//             }
//         return dic;
//     }
//     public static long ConvertFieldPosToCode(FieldPos fieldPos)
//     {
//         return (long)(fieldPos.posX * 100 + fieldPos.posZ);
//     }
//     public static long CreateCodeByPos(int x, int z)
//     {
//         return x * 100 + z;
//     }
//     //フィールドの番後から、開始位置を検索
//     public static Vector3 ConvertFieldPosToWorld(FieldPos pos, int fieldUnitCode)
//     {
//         return ConvertFieldPosToWorld(pos, GetUnitStartPosition(fieldUnitCode));
//     }
//     public static Vector3 ConvertFieldPosToWorld(FieldPos pos, Vector3 startPos)
//     {
//         return new Vector3(pos.posX * fieldBlcokSize, 0.0f, pos.posZ * fieldBlcokSize);
//     }

//     public static FieldPos ConverWoroldPosToFieldPos(Vector3 pos, int fieldUnitCode)
//     {
//         var fieldPos = new FieldPos(((int)pos.x / (int)fieldBlcokSize), ((int)pos.z / (int)fieldBlcokSize));
//         int fieldCodeX = fieldUnitCode % 100;
//         int fieldCodeZ = fieldUnitCode / 100;
//         fieldPos.posX -= fieldCodeX * maxFieldBlockCount;
//         fieldPos.posZ -= fieldCodeZ * maxFieldBlockCount;
//         return fieldPos;
//     }
//     public static Collider[] GetSroundedObejcts(FieldPos pos, int fieldUnitCode)
//     {
//         Vector3 center = ConvertFieldPosToWorld(pos, fieldUnitCode);
//         return Physics.OverlapBox(center, new Vector3(fieldBlcokSize, fieldBlcokSize, fieldBlcokSize));
//     }
//     public static Collider[] GetSroundedObejcts(Vector3 pos)
//     {
//         Vector3 center = pos;
//         return Physics.OverlapBox(center, new Vector3(fieldBlcokSize, fieldBlcokSize, fieldBlcokSize));
//     }
//     public static Vector2 GetUnitStartPosition(int unitCode)
//     {
//         int fieldCodeX = unitCode % 100;
//         int fieldCodeZ = unitCode / 100;
//         Vector2 startPos = new Vector2();
//         startPos.x = (float)fieldCodeX * maxFieldSize;
//         startPos.y = (float)fieldCodeZ * maxFieldSize;
//         return startPos;
//     }
// }
// public class DenQUIhelper
// {
//     //TODO null? にしたほうがいいかな
//     public static Vector2 GetPanelPosition(RectTransform canvasRect, RectTransform panelRect, Vector3 screenPos)
//     {
//         Vector2 panelPos = new Vector2();
//         Vector2 aspectCanvas = new Vector2(canvasRect.rect.width / Camera.main.pixelWidth,
//                                             canvasRect.rect.height / Camera.main.pixelHeight);
//         panelPos.x = aspectCanvas.x * screenPos.x;
//         panelPos.y = aspectCanvas.y * screenPos.y;
//         panelPos.x = panelPos.x - canvasRect.rect.width / DenQHelper.fieldBlcokSize;
//         panelPos.y = panelPos.y - canvasRect.rect.height / DenQHelper.fieldBlcokSize;
//         panelPos -= panelRect.anchoredPosition;
//         return panelPos;
//     }
//     public static Vector2 GetPanelPosition(RectTransform canvasRect, RectTransform panelRect)
//     {
//         return GetPanelPosition(canvasRect, panelRect, DenQ_Input.GetTouchPosition());
//     }
// }

// }

