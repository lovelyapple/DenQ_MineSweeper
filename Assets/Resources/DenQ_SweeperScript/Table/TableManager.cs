using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TableManager
{
    private static List<TableImporterBase> tableList = new List<TableImporterBase>();
    private static bool isFinished = false;
    static void Init()
    {
        tableList.Clear();
        tableList.Add(new BombTableImporter());
    }
    public static IEnumerator ImportTableAll()
    {
        isFinished = false;
        var e = tableList.GetEnumerator();
        while (e.MoveNext())
        {
            var importer = e.Current;
            importer.PreImportData();
            importer.ImportData();
            yield return null;
        }
		DenQLogger.SDebug("Table Load Finished");				
        isFinished = true;
    }
    public static bool IsFinished()
    {
        return isFinished;
    }
}
