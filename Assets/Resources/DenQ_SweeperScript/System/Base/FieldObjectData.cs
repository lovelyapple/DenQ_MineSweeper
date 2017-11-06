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
        public FieldObjectStateController stateController;
        void Awake()
        {
            stateController = new FieldObjectStateController(this);
        }
        public void DestroyObj()
        {
            //一旦OFFにして、コルーチンを止める
            gameObject.SetActive(false);
            GameObject.Destroy(this.gameObject);
        }
        public void KillSelf()
        {
            if (stateController == null)
            {
                stateController = new FieldObjectStateController(this);
            }
            
            stateController.KillThis();
        }
    }

}