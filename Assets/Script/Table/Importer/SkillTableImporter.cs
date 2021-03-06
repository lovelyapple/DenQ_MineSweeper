﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class SkillTableImporter : TableImporterBase
{


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.skillTable.Clear();
        filePath = "SkillTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new SkillData();
        data.code = Read_ulong("code");
        data.name = Read_string("name");
        data.type = Read_uint("type");
        data.targetCount = Read_uint("target_count");
        data.range = Read_uint("range");
        data.baseDamage = Read_uint("base_damage");
        data.coolTime = Read_uint("cool_time");
  

        if (DenQDataBase.skillTable.ContainsKey(data.code)) return;
        DenQDataBase.skillTable.Add(data.code, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
}