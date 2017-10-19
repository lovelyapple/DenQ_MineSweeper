using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
namespace DenQData
{
    /* 
     * オブジェクトベース
     *
     */

    public class ObjectBaseData : MonoBehaviour
    {
        [SerializeField] private ulong? _objectId;
        public FieldPosition fieldPos = null;
        public ulong objectId
        {
            set
            {
                _objectId = value;
            }
            get
            {
                if (!_objectId.HasValue)
                {
                    _objectId = ObjectIndexer.Getnumber(this);
                }

                return _objectId.Value;
            }
        }
        void Awake()
        {
            if (_objectId == null)
            {
                _objectId = ObjectIndexer.Getnumber(this);
            }
        }
    }
}