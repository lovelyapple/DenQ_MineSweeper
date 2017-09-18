using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class MultiRewardTableImporter : TableImporterBase
{
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQOffLineDataBase.multiRewardTable.Clear();
        filePath = "MultiRewardTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new MultiRewardData();
        data.code = Read_ulong("code");
        data.rewardDict = new Dictionary<ulong, ulong>();
        var itemCode = Read_ulong("master_code_01");
        if (itemCode != 0)
            data.rewardDict.Add(itemCode, Read_ulong("item_count_01"));
        itemCode = Read_ulong("master_code_02");
        if (itemCode != 0)
            data.rewardDict.Add(itemCode, Read_ulong("item_count_01"));
        itemCode = Read_ulong("master_code_03");
        if (itemCode != 0)
            data.rewardDict.Add(itemCode, Read_ulong("item_count_01"));

        if (DenQOffLineDataBase.multiRewardTable.ContainsKey(data.code)) return;
        DenQOffLineDataBase.multiRewardTable.Add(data.code, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, MultiRewardData> GetBombData()
    {
        return DenQOffLineDataBase.multiRewardTable;
    }
}
