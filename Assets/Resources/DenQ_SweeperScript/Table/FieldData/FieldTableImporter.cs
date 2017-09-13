using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQData;
///>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>DataStruct
public class FieldRewardItem
{
    public ulong itemBaseCode;
    public uint itemAmonut;
}
public class FieldData
{
    public ulong fieldCode;
    public uint size;
    public ulong fieldDistributionMapCode;
    public DistributionMap distributionMap;

    //報酬関連
    public ulong gold;
    public ulong exp;
    public List<FieldRewardItem> fieldRewardItems;
}
///>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Importer
public class FieldTableImporter : TableImporterBase
{
    //private static Dictionary<ulong, FieldData> fieldDatas = new Dictionary<ulong, FieldData>();
    void Awake()
    {
        PreImportData();
    }
    public override void PreImportData()
    {
        DenQOffLineDataBase.fieldTable.Clear();
        filePath = "FieldTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        isFinished = false;
        var sr = new StreamReader(filePath, Encoding.GetEncoding("SHIFT_JIS"));
        Debug.Log("begin to read Field " + sr);
        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
            if (cols[0] != "#")
            {
                var data = new FieldData();
                data.fieldCode = ulong.Parse(cols[1]);
                data.size = uint.Parse(cols[2]);
                data.fieldDistributionMapCode = ulong.Parse(cols[3]);
                data.gold = ulong.Parse(cols[4]);
                data.exp = ulong.Parse(cols[5]);

                data.fieldRewardItems = new List<FieldRewardItem>();
                var reward = new FieldRewardItem();

                reward.itemBaseCode = ulong.Parse(cols[6]);
                reward.itemAmonut = uint.Parse(cols[7]);
                data.fieldRewardItems.Add(reward);

                reward.itemBaseCode = ulong.Parse(cols[8]);
                reward.itemAmonut = uint.Parse(cols[9]);
                data.fieldRewardItems.Add(reward);

                reward.itemBaseCode = ulong.Parse(cols[10]);
                reward.itemAmonut = uint.Parse(cols[11]);
                data.fieldRewardItems.Add(reward);

                if (!DenQOffLineDataBase.fieldTable.ContainsKey(data.fieldCode))
                    DenQOffLineDataBase.fieldTable.Add(data.fieldCode, data);

                Debug.Log("fieldDatas code" + data.fieldCode + " size " + data.size);
            }
        }
        isFinished = true;
    }
    public override void AfterImportData()
    {
        isFinished = false;
        foreach (var data in DenQOffLineDataBase.fieldTable.Keys)
        {
            var dMap = new DistributionMap();
            dMap = DistributionTableHelper.GetDistributionMap(data);
            if (dMap != null && dMap.fieldItemList.Count > 0)
            {
                DenQOffLineDataBase.fieldTable[data].distributionMap = dMap;
                Debug.Log("fieldDatasdistributionMap code" + dMap.fieldItemList[0].itemBaseCode);
            }
        }
        isFinished = true;
    }
}
public static class FieldTableHelper
{
    public static FieldData GetFieldData(ulong code)
    {
        var outValue = new FieldData();
        if(DenQOffLineDataBase.fieldTable.TryGetValue(code,out outValue))
        {
            return outValue;
        }
        DenQLogger.SError(" could not find field by id " + code);
        return null;
    }
}