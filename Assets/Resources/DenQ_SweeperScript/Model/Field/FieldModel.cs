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
        public static FieldModel Get()
        {
            return ModelManager.GetModel<FieldModel>("fieldModel");
        }
        public void SetUp(ulong mapCode)
        {
            this.fieldData = FieldTableHelpfer.GetFieldData(mapCode);
            SetUp(this.fieldData);
        }
        public void SetUp(FieldData c_fieldData)
        {
            this.fieldData = c_fieldData;

            if (fieldData == null)
            {
                return;
            }

            fieldData.CreateDistributionMap();
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
            var distributionData = MapDistributionTableHelper.GetDistributionData(fieldData.fieldCode);
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
                cntLeft--;
            }

            return fieldBlockTable;
        }

        public List<ulong> CreateFieldItemTable()
        {
            if (fieldData == null)
            {
                return null;
            }

            var distributionData = MapDistributionTableHelper.GetDistributionData(fieldData.fieldCode);
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
        bool isCreating;
        public IEnumerator CreateField()
        {
            if (fieldData == null)
            {
                yield break;
            }

            if (fieldData.sizeX > 5 || fieldData.sizeZ > 5)
            {
                yield break;
            }

            ClearAllBlock();
            while (fieldBlockDic.Count > 0)
            {
                Debug.Log("waiting clear");
                yield return null;
            }

            //これ嫌いなぁs
            for (uint z = 0; z < fieldData.sizeZ; z++)
            {
                for (uint x = 0; x < fieldData.sizeX; x++)
                {
                    var itemCode = GetBlockCodeRadam();
                    InsertOneBLock(FieldPosition.CoordinateToPos(x, z), itemCode);
                }
            }
        }

        public void InsertOneBLock(FieldPosition pos, ulong blockCode)
        {
            var posCode = pos.GetPositionCode();

            if (fieldBlockDic.ContainsKey(posCode))
            {
                Logger.GError("could not place a block where already exist " + pos.ToString());
                return;
            }

            var data = FieldBlockTableHelper.GetFieldBLockDataByID(blockCode);

            if (data == null)
            {
                return;
            }

            var worldPos = pos.GetWorldPostion();

            var blockInfo = ResourcesManager.GetInstance().CreateFieldObjectInstance(blockCode, RootHolder.FieldBLockRootObj.transform, worldPos).GetComponent<FieldBlock>();

            if (blockInfo != null)
            {
                blockInfo.SetUpInfo(data);
            }

            fieldBlockDic.Add(posCode, blockInfo);
        }

        public void ClearAllBlock()
        {
            if (fieldBlockDic == null || fieldBlockDic.Count <= 0)
            {
                fieldBlockDic = new Dictionary<long, FieldBlock>();
                return;
            }

            foreach (var code in fieldBlockDic.Keys)
            {
                fieldBlockDic[code].gameObject.transform.parent = null;
                GameObject.Destroy(fieldBlockDic[code].gameObject);
            }

            fieldBlockDic.Clear();
            fieldBlockDic = new Dictionary<long, FieldBlock>();
        }
    }

}