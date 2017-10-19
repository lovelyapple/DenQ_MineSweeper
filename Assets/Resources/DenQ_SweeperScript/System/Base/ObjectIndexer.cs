using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;
public static class ObjectIndexer
{
    static Dictionary<ulong, ObjectBaseData> objDict = new Dictionary<ulong, ObjectBaseData>();

    public static ulong Getnumber(ObjectBaseData objData)
    {
        ulong idx = 0;
        ObjectBaseData data;

        while (objDict.TryGetValue(idx, out data))
        {
            idx++;
        }

        objDict.Add(idx, objData);
        return idx;
    }
}
