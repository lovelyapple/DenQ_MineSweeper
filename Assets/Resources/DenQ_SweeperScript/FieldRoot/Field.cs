using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
using DenQ.Prefab;
using System.Linq;
public class Field : MonoBehaviour
{
    private Dictionary<FieldPos, FieldBlock> FieldData = new Dictionary<FieldPos, FieldBlock>();
    public int unitCode = 0;
    public Vector2 startPosition = new Vector2();
    public int distributionMapId = 0;
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
        /* TODO スクリーンレイの前後順序を実験しなければならない
        このソースだと確実に一番近いものに当たる
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
            BreakOneBLock(blockData.fieldPos);
        }
    }
    public void InsertOneBlock(FieldPos fieldPos, FIELD_BLOCK type, FIELD_ITEM item)
    {
        //最大一個のユニットにサイズ20
        fieldPos.posX = Mathf.Min(fieldPos.posX, DenQHelper.maxFieldBlockCount);
        fieldPos.posZ = Mathf.Min(fieldPos.posZ, DenQHelper.maxFieldBlockCount);
        //最低0を保つ
        fieldPos.posX = Mathf.Max(fieldPos.posX, 0);
        fieldPos.posZ = Mathf.Max(fieldPos.posZ, 0);

        foreach (FieldPos _pos in FieldData.Keys)
        {
            if (_pos.CompareTo(fieldPos) == 0)
            {
                Debug.Log("error:can not plate a block where exist already");
                return;
            }
        }
        GameObject newBlockObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_BLOCK, this.gameObject, false);
        if (newBlockObj == null)
        {
            Debug.Log("error:can not plate a block,could not read from ResourceManagr!");
            return;
        }

        Vector3 Vecpos = DenQHelper.ConvertFieldPosToWorld(fieldPos);
        newBlockObj.transform.position = Vecpos;
        FieldBlock blockData = newBlockObj.GetComponent<FieldBlock>();
        blockData.InitializeFieldBlock(fieldPos.posX, fieldPos.posZ, type, item);
        FieldData.Add(fieldPos, blockData);
    }
    //TODO クソーーーここでダラダラ書きたくないよ！
    public void BreakOneBLock(FieldPos pos)
    {
        foreach (FieldPos _pos in FieldData.Keys)
        {
            if (_pos.CompareTo(pos) == 0)
            {
                FieldBlock fieldBlockTemp = FieldData[_pos];
                FIELD_ITEM itemType = fieldBlockTemp.GetBlockItemType();
                fieldBlockTemp.BreakBlock();
                switch (itemType)
                {
                    case FIELD_ITEM.NONE:
                        break;
                    case FIELD_ITEM.BOMB_DELAY:
                        GameObject bombObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_BOMB, PREFAB_NAME.ITEM_ROOT, false);
                        if (bombObj != null)
                        {
                            FieldBomb bombData = bombObj.GetComponent<FieldBomb>();
                            bombData.InitializeFieldBomb(_pos.posX, _pos.posZ, itemType);
                        }
                        break;
                    case FIELD_ITEM.BOMB_NORMAL:
                        break;
                }
                return;
            }
        }
    }
    public void RemoveOneBlock(FieldPos pos)//基本使わない
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
