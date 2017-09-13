using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TableManager
{
    private static List<TableImporterBase> tableList = new List<TableImporterBase>();
    private static bool isFinished = false;
    private static uint importState = 3;///３段階のインポートを実施
                                        /// <summary>
                                        /// ここで必要なtableを追加
                                        /// </summary>
    public static void Init()
    {
        tableList.Clear();
        importState = 3;
        tableList.Add(new BombTableImporter());
        tableList.Add(new MultiRewardTableImporter());
        //tableList.Add(new DistributionTableImporter());
        //tableList.Add(new FieldTableImporter());
    }
    public static IEnumerator PreImportAll()
    {
        importState = 3;
        var e = tableList.GetEnumerator();
        while (e.MoveNext())
        {
            var importer = e.Current;
            importer.PreImportData();
            while (!importer.isFinished)
                yield return null;
        }
        DenQLogger.SDebug("Table PreImport all finished");
        importState = 2;
    }
    public static IEnumerator ImportTableAll()
    {
        importState = 2;
        var e = tableList.GetEnumerator();
        while (e.MoveNext())
        {
            var importer = e.Current;
            importer.ReadeCSVTableCore();
            while (!importer.isFinished) yield return null;//表を一個ずつ読む、順番じゃないと壊れる可能性が
        }
        DenQLogger.SDebug("Table MianImport all finished");
        importState = 1;
    }
    public static IEnumerator AfterImportTablAll()
    {
        importState = 1;
        var e = tableList.GetEnumerator();
        while (e.MoveNext())
        {
            var importer = e.Current;
            importer.AfterImportData();
            while (!importer.isFinished) yield return null;//表を一個ずつ読む、順番じゃないと壊れる可能性が
        }
        DenQLogger.SDebug("Table MianImport all finished");
        importState = 0;
    }
    public static bool IsFinished()
    {
        return importState <= 0;
    }
}
