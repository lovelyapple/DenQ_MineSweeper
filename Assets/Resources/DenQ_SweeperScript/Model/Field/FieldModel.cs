using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenQModel
{

    public class FieldModel : ModelBase
    {
        public FieldData fieldData;
        public Dictionary<long, FieldBlock> fieldBlockDic;
        public static FieldModel Get()
        {
            return ModelManager.GetModel<FieldModel>("fieldModel");
        }
        public void SetUp(FieldData c_fieldData)
        {
            this.fieldData = c_fieldData;

            if(fieldData != null)
            {
                fieldData.CreateDistributionMap();
            }
        }
        public void SetUp(FieldData c_fieldData,Dictionary<long,FieldBlock> c_fieldBlockData)
        {
            this.fieldData = c_fieldData;
            this.fieldBlockDic = c_fieldBlockData;

            if(fieldData != null)
            {
                fieldData.CreateDistributionMap();
            }
        }

    }

}