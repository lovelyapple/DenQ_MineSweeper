using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQData;

namespace DenQModel
{

    public class FieldModel : ModelBase
    {
        public FieldData fieldData;
        public Dictionary<long, FieldBlock> fieldBlockDic;
        public List<ulong> fieldBlockTable { get; private set; }
        public List<ulong> fieldItemTable { get; private set; }
        //UnityEngine.Random random;
        public static FieldModel Get()
        {
            return ModelManager.GetModel<FieldModel>("fieldModel");
        }
        public void SetUp(FieldData c_fieldData)
        {
            this.fieldData = c_fieldData;

            if (fieldData != null)
            {
                fieldData.CreateDistributionMap();
            }

            CreateBlockTable();
            CreateFieldItemTable();
        }
        public void SetUp(FieldData c_fieldData, Dictionary<long, FieldBlock> c_fieldBlockData)
        {
            this.fieldData = c_fieldData;
            this.fieldBlockDic = c_fieldBlockData;

            if (fieldData != null)
            {
                fieldData.CreateDistributionMap();
            }

            CreateBlockTable();
            CreateFieldItemTable();
        }
        public List<ulong> CreateBlockTable()
        {
            if (fieldData == null)
            {
                return null;
            }

            var normalBlock = FieldItemTableHelper.GetNormalBlockitemData();
            var distributionData = MapDistributionTableHelper.GetDistributionData(fieldData.mapCode);
            var mapBlocks = distributionData.Where(x => DenQDataBaseHelper.IsBlock(x.itemCode) && x.itemCode != normalBlock.masterCode).ToList();
            fieldBlockTable = new List<ulong>();

            foreach (var mapBlock in mapBlocks)
            {
                var cntRate = mapBlock.itemRate;

                while (cntRate > 0)
                {
                    fieldBlockTable.Add(mapBlock.itemCode);
                    cntRate--;
                }
            }

            var cntLeft = ClientSettings.MaxFieldItemRate - fieldBlockTable.Count;

            while (cntLeft > 0)
            {
                fieldBlockTable.Add(normalBlock.masterCode);
            }

            return fieldBlockTable;
        }

        public List<ulong> CreateFieldItemTable()
        {
            if (fieldData == null)
            {
                return null;
            }

            var distributionData = MapDistributionTableHelper.GetDistributionData(fieldData.mapCode);
            var mapItems = distributionData.Where(x => DenQDataBaseHelper.IsBomb(x.itemCode)).ToList();//今爆弾しかない
            fieldItemTable = new List<ulong>();


            foreach (var item in mapItems)
            {
                var cntRate = item.itemRate;

                while (cntRate > 0)
                {
                    fieldItemTable.Add(item.itemCode);
                    cntRate--;
                }
            }

            var cntLeft = ClientSettings.MaxFieldItemRate - fieldItemTable.Count;

            while (cntLeft > 0)
            {
                fieldItemTable.Add(0);
                cntLeft--;
            }

            return fieldItemTable;
        }
        public ulong GetBlockCodeRadam()
        {
            var idx = (int)UnityEngine.Random.Range(0, ClientSettings.MaxFieldItemRate - 1);
            return fieldBlockTable[idx];
        }
        public void CreateField(FieldData data = null)
        {
            if (data != null)
            {
                SetUp(data);
            }

            //これ嫌いなぁs
            for (uint z = 0; z < data.sizeX; z++)
            {
                for (uint x = 0; x < data.sizeX; x++)
                {
                    var itemCode = GetBlockCodeRadam();
                    InsertOneBLock(FieldPosition.CoordinateToPos(x, z), itemCode);
                }
            }
        }

        public FieldBlock InsertOneBLock(FieldPosition pos, ulong blockCode)
        {
            var posCode = pos.GetPositionCode();
            if (fieldBlockDic.ContainsKey(posCode))
            {
                Logger.GError("could not place a block where already exist " + pos.ToString());
            }

            var data = FieldBlockTableHelper.GetFieldBLockDataByID(blockCode);

            if (data == null)
            {
                return null;
            }

            var worldPos = pos.GetWorldPostion();
            var blockInfo = ResourcesManager.GetInstance().CreateFieldObjectInstance(blockCode, RootHolder.FieldBLockRootObj.transform, worldPos).GetComponent<FieldBlock>();

            if (blockInfo != null)
            {
                blockInfo.SetUpInfo(data);
            }

            fieldBlockDic.Add(posCode, blockInfo);
            return blockInfo;
        }


    }

}