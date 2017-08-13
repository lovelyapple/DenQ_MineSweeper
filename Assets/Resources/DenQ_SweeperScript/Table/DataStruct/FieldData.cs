using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldRewardItem
{
    public ulong itemBaseCode;
    public uint itemAmonut;
}
public class FieldData
{
    public ulong fieldCode;
    public uint size;
    public ulong fieldDistributionMapCode;
    public DistributionMap distributionMap;

    //報酬関連
    public ulong gold;
    public ulong exp;
    public List<FieldRewardItem> fieldRewardItems;
}
