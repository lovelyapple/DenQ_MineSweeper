using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
namespace DenQ
{
    public class ObjectBaseData : MonoBehaviour
    {

        public FieldPos fieldPos = null;
        //基本データ
        private int _max_Hp = 0;
        public int max_Hp
        {
            get { return _max_Hp; }
        }
        private int _rec_Hp = 0;
        public int rec_Hp
        {
            get { return _rec_Hp; }
        }

        public float normal_MoveSpeed = 0.0f;
        public float normal_Attake = 0.0f;
        public float normal_SearchRange = 0.0f;
        public float normal_AttackRange = 0.0f;

    }
}