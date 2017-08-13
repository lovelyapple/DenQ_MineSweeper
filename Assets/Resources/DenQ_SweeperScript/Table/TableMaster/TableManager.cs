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
            while(!importer.isFinished) yield return null;//表を一個ずつ読む、順番じゃないと壊れる可能性が
        }
		DenQLogger.SDebug("Table Load Finished");				
        isFinished = true;
    }
    public static bool IsFinished()
    {
        return isFinished;
    }
}
