using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;

public struct FieldItem 
{
    public long itemBaseCode;
    public float rate;
    public uint amountLeft;
}
public class DistributionMap
{
	public List<FieldItem> fieldItemList;
	public DistributionMap()
	{
		fieldItemList = new List<FieldItem> ();
		fieldItemList.Clear ();
	}
}
///ファイルの構成を考え、独自のインポーターを作成
/// やっぱ、統一しようか
public class DistributionTableImporter : TableImporterBase
{
	public static Dictionary<ulong,DistributionMap> distributionTable;
	private static string _filePath = "";
	private static string _genericFileName = "FieldData/DistributionData/DistributionMap_";
	//public static string GetApplicationPath() { return Application.dataPath; }
	public static bool isFinished = false;
	public static List<string>  fileList = new List<string>
	{
		"0000"
	};
	///特殊なファイルパス設定
	public string SetFileName(string id)
	{
		return string.Format ("{0}{1}.csv",_genericFileName,id);
	}
	public void PreImportData()
	{
		filePathSpecial = 
		distributionTable = new Dictionary<ulong, DistributionMap> ();
		isFinished = false;
		foreach (var fileId in fileList) 
		{
			filePathSpecial = SetFileName(fileId);
			var _distributionData = new DistributionMap ();
			var sr = new StreamReader(filePath, Encoding.GetEncoding ("SHIFT_JIS"));
			while (sr.Peek () >= 0) 
			{
				string[] cols = sr.ReadLine ().Split (',');
				if (cols [0] != "#") 
				{
					var data = new FieldItem ();
					data.itemBaseCode = ulong.Parse (cols[1]);
					data.rate = float.Parse (cols [2]);
					data.amountLeft = uint.Parse (cols [3]);
					_distributionData.fieldItemList.Add (data);
					distributionTable.Add (ulong.Parse (fileId), _distributionData);
				}
			}
		}
	}
	public override void ImportData ()
	{
		//do noth
	}
	public override void AfterImportData ()
	{
		//do noth
	}
}
public class DistributionTableHelper
{
	public static DistributionMap GetDistributionMap(ulong id)
	{
		return DistributionTableImporter.distributionTable.ContainsKey(id)?
			DistributionTableImporter.distributionTable[id] : null;
	}
}
