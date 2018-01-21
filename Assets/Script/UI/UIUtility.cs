using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUtility
{
    ///子供に対して操作を行う
    public static void TransformRecursively(Transform parent, Action<Transform> action)
    {
        if (parent == null) return;

        action(parent);
        var chiltdrenCnt = parent.childCount;

        for (int i = 0; i < chiltdrenCnt; i++)
        {
            Transform c = parent.GetChild(i);
            TransformRecursively(c, action);
        }
    }
    ///instance を作成してコンポーネントを取得
    public static T InstantiateGetComponent<T>(GameObject parent, GameObject prefab) where T : Component
    {
        GameObject go = Instantiate(parent, prefab);

        return GetInstanceComponent<T>(go);
    }
    static public T GetInstanceComponent<T>(GameObject instance) where T : Component
    {
        return instance != null ? instance.GetComponent<T>() : null;
    }
    public static GameObject Instantiate(GameObject parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        {
            Transform t = go.transform;
            t.name = prefab.name;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            SetLayerAllChildren(t.gameObject, parent.layer);
        }

        return go;
    }
    public static void SetLayerAllChildren(GameObject parent, int layer)
    {
        if (parent == null) return;
        if (parent.layer == layer) return;

        TransformRecursively(parent.transform, (c) =>
         {
             c.gameObject.layer = layer;
         });
    }
}
