using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// テーブルデータをインポートする基本
/// </summary>
public class TableImporterBase
{
    ///終了フラグ
    public bool isFinished = false;
    private string _filePath = "";
    public string filePath
    {
        set
        {
            _filePath = string.Format("{0}/Resources/DenQ_SweeperScript/Table/Data/{1}.csv", GetApplicationPath(), value);
        }
        get
        {
            return _filePath;
        }
    }
    ///ファイルのPath取得
    public virtual string GetFilePath() { return ""; }
    public string GetApplicationPath() { return Application.dataPath; }
    ///インポート初期化
    public virtual void PreImportData() { }
    ///本番インポート
    public virtual void ImportData() { }//コルーチンにしたら、衝突する可能性があるので、このまま
}
