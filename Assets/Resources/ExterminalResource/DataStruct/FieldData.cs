using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FieldData
{
    public ulong code;
    public string name;
    public uint sizeX;
    public uint sizeY;
    public ulong rewardGold;
    public ulong rewardExp;
    public ulong rewardDia;
    public ulong multiRewardCode;
    public ulong distributionCode;
    public ulong definedItemCode;

    //これ必要ある？
    //MultiRewardData _multiRewardData = null;
    //MapDefinedTiemData _mapDefinedItemData = null;
    MapDistributionData _mapDistributionData = null;

    //public MultiRewardData multiRewardData = null;
    //public MapDefinedTiemData mapDefinedItemData = null;
    public MapDistributionData mapDistributionData
    {
        private set
        {

        }
        get
        {
            if (_mapDistributionData == null)
			{
				_mapDistributionData = MapDistributionTableHelper.GetDistributionData(distributionCode);
			}
			return _mapDistributionData;

        }
    }
	public List<ulong>	distributionMap = null;
    private uint? elementCount = null;
	public void CreateDistributionMap()
	{
		distributionMap = new List<ulong>();
		var db = mapDistributionData;
		foreach(var itemCode in db.distributionDatas.Keys)
		{
			var itemRate = db.distributionDatas[itemCode];
			while(itemRate > 0)
			{
				distributionMap.Add(itemCode);
				itemRate --;
			}
		}
		for(int idx = 0,elementCount = distributionMap.Count; idx <elementCount;idx++)
		{
			var tempData = distributionMap[idx];
			var	rdmIdx = Random.Range(0,elementCount);
			distributionMap[idx] = distributionMap[rdmIdx];
			distributionMap[rdmIdx] = tempData;
		}
	}
    public ulong GetFieldItemRandom()
    {
        if(!elementCount.HasValue || elementCount == null)
        {
            elementCount = (uint?)distributionMap.Count;
        }
        var idx = (int)Random.Range(0,elementCount.Value);
        return distributionMap[idx];
    }
}
