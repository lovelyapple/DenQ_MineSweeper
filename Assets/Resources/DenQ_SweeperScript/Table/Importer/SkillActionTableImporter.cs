using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class SkillActionTableImporter : TableImporterBase
{


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.skillActionTable.Clear();
        filePath = "SkillActionTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new SkillActionData();
        data.code = Read_uint("code");
        data.name = Read_string("name");
        data.skillActionType = Read_uint("skill_action_type");
        data.effectCode = Read_uint("effect_code");
        data.param01 = Read_ulong("param_01");
        data.param02 = Read_ulong("param_02");

        if (DenQDataBase.skillActionTable.ContainsKey(data.code)) return;
        DenQDataBase.skillActionTable.Add(data.code, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
}