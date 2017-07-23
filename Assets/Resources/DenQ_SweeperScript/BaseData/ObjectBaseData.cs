using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
namespace DenQ
{
    public class ObjectBaseData : MonoBehaviour
    {
        [SerializeField] private long _objectId = 0;
        public long objectId
        {
            get
            {
                return _objectId;
            }
        }
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

        [SerializeField] private float normal_MoveSpeed = 0.0f;
        [SerializeField] private float moveSpeedRate = 1.0f;
        public  float out_moveSpeed{
            get
            {
                return normal_MoveSpeed * out_moveSpeed;
            }
        }
        [SerializeField] private float normal_Attake = 0.0f;
        [SerializeField] private float normal_SearchRange = 0.0f;
        [SerializeField] private float normal_AttackRange = 0.0f;

        void Awake()
        {
            _objectId = GameObjectsManager.GetInstance().RigistObjectId();
        }
    }
}