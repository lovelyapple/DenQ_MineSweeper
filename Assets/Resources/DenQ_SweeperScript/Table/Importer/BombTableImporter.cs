using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;

using DenQ;
public class BombTableImporter : TableImporterBase
{
    private static Dictionary<ulong, BombData> bombDatas = new Dictionary<ulong, BombData>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    public override void PreImportData()
    {
        bombDatas.Clear();
        filePath = "BombTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        isFinished = false;
        var sr = new StreamReader(filePath, Encoding.GetEncoding("SHIFT_JIS"));
        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
            var top = cols[0];
            if (top != "#")
            {
                var data = new BombData();
                data.itemCode = ulong.Parse(cols[1]);
                data.name = cols[2];
                data.bombType = uint.Parse(cols[3]);
                data.level = int.Parse(cols[3]);
                data.hp = int.Parse(cols[4]);
                data.time = float.Parse(cols[5]);
                data.damage = float.Parse(cols[6]);
                data.damageRange = float.Parse(cols[7]);

                if (bombDatas.ContainsKey(data.itemCode)) continue;
                bombDatas.Add(data.itemCode, data);
                Debug.Log("bomb code" + data.itemCode + " name " + data.name);
            }
            DenQLogger.SDebug("BombTableImporter loaded!");
        }
        isFinished = true;
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, BombData> GetBombData()
    {
        return bombDatas;
    }
}
