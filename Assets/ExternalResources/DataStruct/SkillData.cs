using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DenQData
{
    public enum SkillType
    {
        OneShot = 0,        //一回のみ放出
    }
    public class SkillData
    {
        public ulong code;
        public string name;
        public uint type;
        public uint targetCount;
        public uint range;
        public uint baseDamage;
        public uint coolTime;

    }

}