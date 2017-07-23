using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DenQ;
public class MoveController : MonoBehaviour
{
    private CharacterController _characterCtrl = null;
    private ObjectBaseData _selfData = null;
    public MoveController(ObjectBaseData selfData)
    {
        _selfData = selfData;
    }
    void OnEnable()
    {
        if (_characterCtrl == null)
        {
            _characterCtrl = GetComponent<CharacterController>();
            if (_characterCtrl == null)
            {
                DenQLogger.SErrorId(_selfData.objectId, "can not find characterController");
            }
        }
    }
    public bool lookAtTarget = true;
    [SerializeField] private Vector3? _moveLocalDirection = null;
    [SerializeField] private Vector3 _moveGlobalDirection = Vector3.zero;
    public void UpdateMoveController()
    {
        if (_selfData == null) { return; }
        if (!_moveLocalDirection.HasValue) { return; }
        if (lookAtTarget)//TODO: && _selfData.HasTarget())
        {
			//TODO 追加する
			//transform.LookAt(_selfData.GetTargetPosition)
            _moveGlobalDirection = transform.TransformDirection(_moveLocalDirection.Value);
        }
        else
            _moveGlobalDirection = _moveLocalDirection.Value;

        _moveGlobalDirection.y = 0.0f;
        transform.position = transform.position + _moveGlobalDirection * Time.deltaTime * _selfData.out_moveSpeed;
		//TODO :characterController 使わなくても行けそう
	}

}
