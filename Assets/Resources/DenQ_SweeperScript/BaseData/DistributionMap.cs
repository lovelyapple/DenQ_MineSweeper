using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;

public static class DistributionMapHelper
{

}

/*
public class DistributionMap
{
    //爆弾になる確率
    public float bombTotal = 0.2f;//デフォルト値
                                   //それぞれ爆弾の種類
    private const int maxMapSize = 1000;
    //現在のデフォルト値
    private System.Random rdmBase = new System.Random(maxMapSize);
    private Dictionary<FIELD_ITEM, float> distributionData = new Dictionary<FIELD_ITEM, float>()
    {
        {FIELD_ITEM.BOMB_NORMAL,0.0f},
        {FIELD_ITEM.BOMB_DELAY,1.0f},
        //爆弾の確率
        
        {FIELD_ITEM.NONE,0.0f},//ここではまず0にする
	};

    private List<FIELD_ITEM> distributionResult = null;
    void Awake()
    {
    }
    public void InitializeDistributionData()
    {
        foreach (FIELD_ITEM item in distributionData.Keys)
        {
            switch (item)
            {
                case FIELD_ITEM.BOMB_DELAY:
                case FIELD_ITEM.BOMB_NORMAL:
                    int maxData = (int)(distributionData[item] * bombTotal * maxMapSize);
                    for (int i = 0; i < maxData; i++)
                    {
                        distributionResult.Add(item);
                    }
                    break;
                case FIELD_ITEM.NONE:
                    int countLeft = maxMapSize - distributionResult.Count;
                    for (int nCnt = 0; nCnt < countLeft; nCnt++)
                    {
                        distributionResult.Add(FIELD_ITEM.NONE);
                    }
                    break;
            }
        }
    }
    public FIELD_ITEM GetItemTypeRandam()
    {
        FIELD_ITEM item = FIELD_ITEM.NONE;

        if (distributionResult == null)
        {
            distributionResult = new List<FIELD_ITEM>();
            InitializeDistributionData();
        }
		
        item = distributionResult[rdmBase.Next(0, maxMapSize - 1)];

        return item;
    }
}
*/