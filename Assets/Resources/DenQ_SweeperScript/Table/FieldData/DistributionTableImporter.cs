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
public class DistributionTableImporter : TableImporterBase
{
	public static Dictionary<ulong,DistributionMap> distributionTable;
	private static string _filePath = "";
	private static string _genericFileName = "FieldData/DistributionData/DistributionMap_";
	public static string filePath
	{
		set
		{
			_filePath = string.Format("{0}/Resources/DenQ_SweeperScript/Table/Data/FieldData/DistributionData/DistributionMap_{1}.csv", GetApplicationPath(), value);
		}
		get
		{
			return _filePath;
		}
	}

	//public static string GetApplicationPath() { return Application.dataPath; }
	public static bool isFinished = false;
	public static List<string>  fileList = new List<string>
	{
		"0000"
	};
	public override void PreImportData ()
	{
		distributionTable = new Dictionary<ulong, DistributionMap> ();
	}
	public string SetFileName(string id)
	{
		return string.Format ("{0}{1}",_genericFileName,id);
	}
	public void LoadDistributionTable()
	{
		isFinished = false;
		foreach (var fileId in fileList) 
		{
			filePathSpecial = SetFileName(Field);
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
}
