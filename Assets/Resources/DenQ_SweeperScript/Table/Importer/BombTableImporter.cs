using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;
public class BombTableImporter : TableImporterBase
{
    private static Dictionary<long, BombData> bombDatas = new Dictionary<long, BombData>();
    public override void PreImportData()
    {
        bombDatas.Clear();
        //filePath = "BombTable";
        isFinished = false;
    }
    public override void ImportData()
    {

    }
    public static void DebugWriteData()
    {
        var data = new BombData();
        data.itemCode = 0;
        data.name = "影分";
        data.level = 99;
        data.hp = 100;
        data.time = 10;
        data.damage = 10;
        data.damageRange = 10.5f;
        var appPath = Application.dataPath;
        var filepath = string.Format("{0}{1}", appPath, "/Resources/DenQ_SweeperScript/Table/Data/BombTable.csv");
        StreamWriter sw = new StreamWriter(filepath, false,Encoding.GetEncoding("UTF-8"));


        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", "",
        data.itemCode,
        data.name,
        data.level,
        data.hp,
        data.time,
        data.damage,
        data.damageRange
        );
        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", "",
        1,
        data.name,
        data.level,
        data.hp,
        data.time,
        data.damage,
        data.damageRange
        );
        sw.Close();
    }
    public static void DebugReader()
    {
        bombDatas.Clear();
        var appPath = Application.dataPath;
        var filepath = string.Format("{0}{1}", appPath, "/Resources/DenQ_SweeperScript/Table/Data/BombTable.csv");
        var sr = new StreamReader(filepath, Encoding.GetEncoding("UTF-8"));
        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
			var top = cols[0];
            if (top != "#")
            {
                var data = new BombData();
				var e = cols[1];
                data.itemCode = long.Parse(cols[1]);
                data.name = cols[2];
                data.level = int.Parse(cols[3]);
                data.hp = int.Parse(cols[4]);
                data.time = float.Parse(cols[5]);
                data.damage = float.Parse(cols[6]);
                data.damageRange = float.Parse(cols[7]);

                if (bombDatas.ContainsKey(data.itemCode)) continue;
                bombDatas.Add(data.itemCode, data);
            }
        }
        foreach (var idx in bombDatas.Keys)
        {
            Debug.Log("data " + bombDatas[idx].itemCode);
        }
    }
}
