using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class FieldTableImporter : TableImporterBase
{


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.fieldTable.Clear();
        filePath = "FieldTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new FieldData();
        data.fieldCode = Read_ulong("field_code");
        data.name = Read_string("name");
        data.sizeX = Read_uint("size_x");
        data.sizeZ = Read_uint("size_z");
        data.rewardGold = Read_uint("reward_gold");
        data.rewardExp = Read_ulong("reward_exp");
        data.rewardDia = Read_uint("reward_diamond");
        data.multiRewardCode = Read_ulong("multi_reward_code");
        data.definedItemCode = Read_ulong("defined_item_code");
        data.distributionCode = Read_ulong("distribution_code");

        if (DenQDataBase.fieldTable.ContainsKey(data.fieldCode)) return;
        DenQDataBase.fieldTable.Add(data.fieldCode, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, FieldData> GetBombData()
    {
        return DenQDataBase.fieldTable;
    }
}
public static class FieldTableHelpfer
{
    public static FieldData GetFieldData(ulong fieldCode)
    {
        var dbs = DenQDataBase.fieldTable;
        var outData = new FieldData();
        if(!dbs.TryGetValue(fieldCode,out outData))
        {
            Logger.SError("coudl not find fieldData Code :" + fieldCode);
        }
        return outData;
    }
}