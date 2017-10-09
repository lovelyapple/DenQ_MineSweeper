using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;
/// <summary>
/// テーブルデータをインポートする基本
/// </summary>
public class TableImporterBase
{
    ///終了フラグ
    public bool isFinished = false;
    private string _filePath = "";
    /// <summary>
    /// 大体なときにこれを使う
    /// </summary>
    /// <value>The file path.</value>
    public string filePath
    {
        set
        {
            _filePath = string.Format("{0}/Resources/ExterminalResource/TableCSV/{1}.csv", GetApplicationPath(), value);
        }
        get
        {
            return _filePath;
        }
    }
    Dictionary<string, uint> cellIndx = new Dictionary<string, uint>();
    ///ファイルのPath取得
    public virtual string GetFilePath() { return ""; }
    public string GetApplicationPath() { return Application.dataPath; }
    /// インポート準備 Tableのデータ構造の初期化
    public virtual void PreImportData() { }
    /// 本インポート Tableの大体はここで読む
    public virtual void ImportData() { }//コルーチンにしたら、衝突する可能性があるので、このまま
                                        /// 事後インポート 大体はTableの中に他のTableのデーターが必要のときに追加する
    public virtual void AfterImportData() { }

    List<string> colums = new List<string>();

    public void ReadeCSVTableCore()
    {
        isFinished = false;
        ReadIndex();
        var sr = new StreamReader(filePath, Encoding.GetEncoding("SHIFT_JIS"));
        while (sr.Peek() >= 0)
        {
            colums = sr.ReadLine().Split(',').ToList();
            if (colums[0] == "#") continue;
            ImportData();
        }
        isFinished = true;
    }
    void ReadIndex()
    {
        var sr = new StreamReader(filePath, Encoding.GetEncoding("SHIFT_JIS"));
        cellIndx = new Dictionary<string, uint>();
        string[] cols = sr.ReadLine().Split(',');
        var e = cols.GetEnumerator();
        uint idx = 0;
        while (e.MoveNext())
        {
            var str = e.Current as string;
            cellIndx.Add(str, idx);
            idx++;
        }
    }
    protected string Read_string(string input)
    {
        var val = new uint();
        if (!cellIndx.TryGetValue(input, out val))
        {
            DenQLogger.SError(" could not find the name in csv source " + input);
            return null;
        }
        return colums[(int)val];

    }
    protected int Read_int(string input)
    {
        var val = new uint();
        if (!cellIndx.TryGetValue(input, out val))
        {
            DenQLogger.SError(" could not find the name in csv source " + input);
            return 0;
        }
        return int.Parse(colums[(int)val]);
    }
    protected long Read_long(string input)
    {
        var val = new uint();
        if (!cellIndx.TryGetValue(input, out val))
        {
            DenQLogger.SError(" could not find the name in csv source " + input);
            return 0;
        }
        return long.Parse(colums[(int)val]);
    }
    protected uint Read_uint(string input)
    {
        var val = new uint();
        if (!cellIndx.TryGetValue(input, out val))
        {
            DenQLogger.SError(" could not find the name in csv source " + input);
            return 0;
        }
        return uint.Parse(colums[(int)val]);
    }
    protected ulong Read_ulong(string input)
    {
        var val = new uint();
        if (!cellIndx.TryGetValue(input, out val))
        {
            DenQLogger.SError(" could not find the name in csv source " + input);
            return 0;
        }
        return ulong.Parse(colums[(int)val]);
    }
    protected float Read_float(string input)
    {
        var val = new uint();
        if (!cellIndx.TryGetValue(input, out val))
        {
            DenQLogger.SError(" could not find the name in csv source " + input);
            return 0;
        }
        return float.Parse(colums[(int)val]);
    }
}
