using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenQData
{
    public class DenQOffLineDataBase
    {
        public static Dictionary<ulong, FieldData> fieldTable = new Dictionary<ulong, FieldData>();
		public static Dictionary<ulong,DistributionMap> distributionTable = new Dictionary<ulong, DistributionMap>();
        public static Dictionary<ulong, BombData> bombTable = new Dictionary<ulong, BombData>();
        public static Dictionary<ulong, MultiRewardData> multiRewardTable = new Dictionary<ulong, MultiRewardData>();
        public static Dictionary<ulong, StackItemData>  stackItemTable = new Dictionary<ulong, StackItemData>();
        public static Dictionary<ulong, FieldItemData>  fieldItemTable = new Dictionary<ulong, FieldItemData>();
    }
}