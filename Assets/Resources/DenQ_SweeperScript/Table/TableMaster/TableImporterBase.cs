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
	/// <summary>
	/// 大体なときにこれを使う
	/// </summary>
	/// <value>The file path.</value>
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
	/// <summary>
	/// 
	/// 特集のファイルパスを使うときに使用
	/// App/../Table/xxxxxx.csv
	/// </summary>
	/// <value>The file path special.</value>
	public string filePathSpecial 
	{
		set{ _filePath = string.Format("{0}/Resources/DenQ_SweeperScript/Table/{1}.csv",GetApplicationPath(),value);}
		get{ return _filePath;}
	}
    ///ファイルのPath取得
    public virtual string GetFilePath() { return ""; }
    public string GetApplicationPath() { return Application.dataPath; }
    /// <summary>
    /// インポート準備 Tableのデータ構造の初期化
    /// </summary>
    public virtual void PreImportData() { }
    /// <summary>
    /// 本インポート Tableの大体はここで読む
    /// </summary>
    public virtual void ImportData() { }//コルーチンにしたら、衝突する可能性があるので、このまま
	/// <summary>
	/// 事後インポート 大体はTableの中に他のTableのデーターが必要のときに追加する
	/// </summary>
	public virtual void AfterImportData(){ }

    public ulong ReadUlong(string str)
    {
        return ulong.Parse(str);
    }
}
