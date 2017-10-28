using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DenQData
{
    public class FieldObjectData : ObjectBaseData
    {
        public uint maxHp;
        public uint recHp;
        public float? baseMoveSpeed;
        public float? baseSearctRange;
        public float? baseAttackRange;

        public bool isDead { get { return recHp <= 0; } }
    }

}