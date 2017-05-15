using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
using DenQ.Prefab;
using System.Linq;
public class Field : MonoBehaviour
{
    private Dictionary<FieldPos, FieldBlock> FieldData = new Dictionary<FieldPos, FieldBlock>();

    public bool CanPlay = true;
    private TOUCH_INFO TouchInfo;

    void Awake()
    {
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanPlay)
            return;

        TouchInfo = DenQ_Input.GetTouch();
        switch (TouchInfo)
        {
            case TOUCH_INFO.None:
                break;
            case TOUCH_INFO.Began:
                ChosseOneFieldBLock();
                break;
            case TOUCH_INFO.Moved:
                break;
            case TOUCH_INFO.Stationary:
                break;
            case TOUCH_INFO.Ended:
                break;
            case TOUCH_INFO.Canceled:
                break;
        }
    }
    public void ChosseOneFieldBLock()
    {
        var ray = DenQ_Input.GetScreenRay(Camera.main);
        /* 
		RaycastHit[] hits = Physics.RaycastAll(ray,100);
		
        if(hits.Length <= 0)
		return;
		RaycastHit hitTemp = hits[0];
        Vector3 dic = hitTemp.point - Camera.main.transform.position;
        float rangeBack = dic.magnitude;
		foreach(RaycastHit hited in hits)
		{
            Vector3 dictTemp = hited.point - Camera.main.transform.position;
            float rangeNow = dictTemp.magnitude;
            if(rangeNow < rangeBack)
            {
                rangeBack = rangeNow;
                hitTemp = hited;
            }
		}
        */
        RaycastHit hited;
        if (!Physics.Raycast(ray, out hited, 100))
            return;

        if (hited.transform.gameObject.tag == "FieldBlock")
        {
            FieldBlock blockData = hited.transform.gameObject.GetComponent<FieldBlock>();
            BreakOneBLock(blockData.Pos);
        }
    }
    public void InsertOneBlock(FieldPos fieldPos, BLOCK_TYPE type)
    {
        GameObject prefabBlock = ResourcesHolder.GetPrefabByName(PREFABU_NAME.FieldBlock);
        if (prefabBlock == null)
        {
            Debug.Log("error:can not plate a block Doesn't exist a Prefab");
            return;
        }

        foreach (FieldPos _pos in FieldData.Keys)
        {
            if (_pos.CompareTo(fieldPos) == 0)
            {
                Debug.Log("error:can not plate a block where exist already");
                return;
            }
        }
        Vector3 Vecpos = DenQHelper.ConvertFieldPosToWorld(fieldPos);
        GameObject tempObj = ResourcesHelper.CreateResourcesInstance(prefabBlock, this.gameObject, Vecpos);
        FieldBlock tempBlock = tempObj.GetComponent<FieldBlock>();
        tempBlock.InitializeFieldBlock(fieldPos.posX, fieldPos.posZ, type);
        FieldData.Add(fieldPos, tempBlock);
    }
    public void BreakOneBLock(FieldPos pos)
    {
        foreach (FieldPos _pos in FieldData.Keys)
        {
            if (_pos.CompareTo(pos) == 0)
            {
                FieldBlock fieldBlockTemp = FieldData[_pos];
                BLOCK_TYPE blockType = fieldBlockTemp.GetBLockType();
                switch (blockType)
                {
                    case BLOCK_TYPE.NONE:
                        break;
                    case BLOCK_TYPE.ITEM:
                        CreateItemOnField(fieldBlockTemp, _pos, fieldBlockTemp.transform.position);
                        break;
                }
                FieldData.Remove(_pos);
                fieldBlockTemp.Destroy();
                return;
            }
        }
    }
    void CreateItemOnField(FieldBlock blockData, FieldPos fPos, Vector3 vPos)
    {
        switch (blockData.ItemType)
        {
            case FIELD_ITEM.HEALTH:
                break;
            case FIELD_ITEM.BOMB:
                BombManger.GetInstance().CreateBomb(blockData);
                break;
        }
    }
    public void RemoveOneBlock(FieldPos pos)
    {
        foreach (FieldPos _pos in FieldData.Keys)
        {
            if (_pos.CompareTo(pos) == 0)
            {
                FieldBlock fieldBlockTemp = FieldData[_pos];
                FieldData.Remove(_pos);
                fieldBlockTemp.Destroy();
                return;
            }
        }
    }
    public void RemoveField()
    {
        GameObject.Destroy(this.gameObject);
    }
    public void ClearField()
    {
        foreach (FieldPos pos in FieldData.Keys)
        {
            FieldData[pos].Destroy();
        }
        FieldData.Clear();
    }

}
