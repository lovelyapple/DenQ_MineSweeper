using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQData;
public class FieldData
{
    public ulong fieldCode;
    public string name;
    public uint sizeX;
    public uint sizeZ;
    public ulong rewardGold;
    public ulong rewardExp;
    public ulong rewardDia;
    public ulong multiRewardCode;
    public ulong distributionCode;
    public ulong definedItemCode;
    public List<ulong> distributionBombMap = null;
    private uint? elementCount = null;

    public void CreateDistributionMap()
    {
        distributionBombMap = new List<ulong>();

    }
    public ulong GetFieldItemRandom()
    {
        if (!elementCount.HasValue || elementCount == null)
        {
            elementCount = (uint?)distributionBombMap.Count;
        }
        var idx = (int)Random.Range(0, elementCount.Value);
        return distributionBombMap[idx];
    }
}
