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
        FieldObjectStateController _stateController;
        public FieldObjectStateController StateController{
            get{
                if(_stateController == null)
                {
                    _stateController = new FieldObjectStateController(this);
                }

                return _stateController;
            }
        }
        TargetController _targetController;
        public TargetController TargetController
        {
            get{
                if(_targetController == null)
                {
                    _targetController = new TargetController(this);
                }

                return _targetController;
            }
        }
        public void DestroyObj()
        {
            //一旦OFFにして、コルーチンを止める
            gameObject.SetActive(false);
            GameObject.Destroy(this.gameObject);
        }
        public void KillSelf()
        {
            StateController.KillThis();
        }
    }

}