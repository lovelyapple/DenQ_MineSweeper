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
	public MultiRewardData  multiRewardData = null;
	public MapDefinedTiemData mapDefinedItemData = null;
	public MapDistributionData mapDistributionData = null;
}
