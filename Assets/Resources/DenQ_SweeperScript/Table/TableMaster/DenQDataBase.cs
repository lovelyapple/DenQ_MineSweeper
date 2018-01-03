using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenQData
{
    public static class DenQDataBase
    {
        public static Dictionary<ulong, FieldData> fieldTable = new Dictionary<ulong, FieldData>();
		public static List<MapDistributionData> mapDistributionTable = new List<MapDistributionData>();
        public static Dictionary<ulong, MapDefinedTiemData> mapDefinedTiemData = new Dictionary<ulong, MapDefinedTiemData>();
        public static Dictionary<ulong, FieldBombData> bombTable = new Dictionary<ulong, FieldBombData>();
        public static Dictionary<ulong, MultiRewardData> multiRewardTable = new Dictionary<ulong, MultiRewardData>();
        public static Dictionary<ulong, StackItemData>  stackItemTable = new Dictionary<ulong, StackItemData>();
        public static Dictionary<ulong, FieldItemData>  fieldItemTable = new Dictionary<ulong, FieldItemData>();
        public static Dictionary<ulong, FieldBlockData> fieldBlockTable = new Dictionary<ulong, FieldBlockData>();
        public static Dictionary<ulong, SkillData>  skillTable = new Dictionary<ulong, SkillData>();
        public static Dictionary<ulong, EffectData> effectTable = new Dictionary<ulong, EffectData>();
        public static Dictionary<uint, SkillActionData> skillActionTable = new Dictionary<uint, SkillActionData>();
        public static List<SkillActionGroupData> skillActionGroupTable = new List<SkillActionGroupData>();
    }
}