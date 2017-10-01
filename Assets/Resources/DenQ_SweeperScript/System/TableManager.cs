using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ.Mgr;
public class TableManager : MangerBase<TableManager>
{
    private List<TableImporterBase> tableList = new List<TableImporterBase>();
    private bool isFinished = false;
    [FlagsAttribute]
    enum TABLE_INIT_STATE
    {
        INSTANCE = 1 << 0,
        ADD_TABLE = 1 << 1,
        PRE_IMPORT = 1 << 2,
        CORE_IMPORT = 1 << 3,
        AFTER_IMPORT = 1 << 4,
        ALL = INSTANCE | ADD_TABLE | PRE_IMPORT | CORE_IMPORT | AFTER_IMPORT,
        OVER = 1 << 5,
    }
    ///これ使えば、もう少し厳密にやれるけど、まあいいっか
    TABLE_INIT_STATE state = 0;
    void OnEnable()
    {
        if (GetInstance() == null)
        {
            state = 0;
            SetInstance(this);
            state |= TABLE_INIT_STATE.INSTANCE;
        }
    }
    void Init()
    {
        tableList.Clear();
        tableList.Add(new BombTableImporter());
        tableList.Add(new MultiRewardTableImporter());
        tableList.Add(new StackItemTableImporter());
        tableList.Add(new MapDistributionTableImporter());
        tableList.Add(new MapDefinedItemTableImporter());
        state |= TABLE_INIT_STATE.ADD_TABLE;
    }
    public void ReadTable()
    {
        Init();
        StartCoroutine(IeReadTable());
    }
    IEnumerator IeReadTable()
    {
        Debug.Log("start");
        yield return StartCoroutine(PreImportAll());
        yield return StartCoroutine(ImportTableAll());
        yield return StartCoroutine(AfterImportTablAll());
        while (!IsFinished())
        {
            yield return null;
        }
    }
    IEnumerator PreImportAll()
    {
        var e = tableList.GetEnumerator();
        while (e.MoveNext())
        {
            var importer = e.Current;
            importer.PreImportData();
            while (!importer.isFinished)
                yield return null;
        }
        state |= TABLE_INIT_STATE.PRE_IMPORT;
        DenQLogger.SDebug("Table PreImport all finished");
    }
    IEnumerator ImportTableAll()
    {
        var e = tableList.GetEnumerator();
        while (e.MoveNext())
        {
            var importer = e.Current;
            importer.ReadeCSVTableCore();
            while (!importer.isFinished) yield return null;//表を一個ずつ読む、順番じゃないと壊れる可能性が
        }
        state |= TABLE_INIT_STATE.CORE_IMPORT;
        DenQLogger.SDebug("Table MianImport all finished");
    }
    IEnumerator AfterImportTablAll()
    {
        var e = tableList.GetEnumerator();
        while (e.MoveNext())
        {
            var importer = e.Current;
            importer.AfterImportData();
            while (!importer.isFinished) yield return null;//表を一個ずつ読む、順番じゃないと壊れる可能性が
        }
        state |= TABLE_INIT_STATE.AFTER_IMPORT;
        DenQLogger.SDebug("Table AfterImport all finished");
    }
    public bool IsFinished()
    {
        if(0 == (state & TABLE_INIT_STATE.ALL))
        {
            state = TABLE_INIT_STATE.OVER;
            return true;
        }
        return false;
    }
}
