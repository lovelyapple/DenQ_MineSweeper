﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
namespace DenQ
{
    public class BaseDataInput
    {
        int hp;
        float moveSpeed;
        float attack;
        float attackRange;
        float searchRange;
        float bodySize;
        public BaseDataInput(int _hp, float _moveSpeed, float _attack, float _attackRange, float _bodySize, float _searchRange)
        {
            hp = _hp;
            moveSpeed = _moveSpeed;
            attackRange = _attackRange;
            bodySize = _bodySize;
            attack = _attack;
            searchRange = _searchRange;
        }
    }
    /* フィールド上背景以外のオブジェクト基本データ
     *
     *
     */
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
            set { _rec_Hp = value; if (_rec_Hp <= 0) _rec_Hp = 0; }
            get { return _rec_Hp; }
        }

        [SerializeField] private float normal_MoveSpeed = 0.0f;
        [SerializeField] private float moveSpeedRate = 1.0f;
        [SerializeField] public float out_moveSpeed { get { return normal_MoveSpeed * out_moveSpeed; } }
        [SerializeField] private float normal_Attake = 10.0f;
        [SerializeField] private float normal_AttackSpeed = 10.0f;
        [SerializeField] private float normal_SearchRange = 10.0f;
        [SerializeField] private float normal_AttackRange = 10.0f;
        [SerializeField] public float out_AttackRange { get { return normal_AttackRange; } }
        [SerializeField] public float out_AttackSpeed { get { return normal_AttackSpeed; } }
        [SerializeField] public float time_Attack = 0.0f;
        [SerializeField] private float normal_bodySize = 1.0f;
        public float out_bodySize
        {
            get
            {
                return normal_bodySize;
            }
        }

        public ActionController actionCtrl = null;
        void Awake()
        {
            _objectId = GameObjectsManager.GetInstance().RigistObjectId();
        }
        public void InitActionCtrl()
        {
            actionCtrl = new ActionController();
            actionCtrl.selfData = this;
            actionCtrl.moveCtrl = gameObject.GetComponent<MoveController>();
            actionCtrl.targetCtrl = gameObject.GetComponent<TargetController>();
        }
        public void DestroyThis()
        {
            GameObject.Destroy(this.gameObject);
        }
        public bool IsDead()
        {
            if (_rec_Hp <= 0) { return true; }
            if (actionCtrl != null)
            {
                var type = actionCtrl.GetCurrentActionType();
                return type == ACTIONTYPE.dying || type == ACTIONTYPE.dead;
            }
            return false;
        }
        public void ResetAttackTime()
        {
            time_Attack = normal_AttackSpeed;
        }
    }
}