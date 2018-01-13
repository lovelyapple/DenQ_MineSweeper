using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class SkillActionGroupImporter : TableImporterBase
{


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.skillActionGroupTable.Clear();
        filePath = "SkillActionGroupTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new SkillActionGroupData();
        data.skillCode = Read_uint("skill_code");
        data.skillActionCode = Read_uint("skill_action_code");
        data.actionIndex = Read_uint("action_index");

        DenQDataBase.skillActionGroupTable.Add(data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
}