using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
using System.Linq;
public class Field : MonoBehaviour
{
    //private Dictionary<FieldPos, FieldBlock> FieldData = new Dictionary<FieldPos, FieldBlock>();
    private Dictionary<long, FieldBlock> fieldData = new Dictionary<long, FieldBlock>();
    public int unitCode = 0;
    public Vector2 startPosition = new Vector2();
    public int distributionMapId = 0;
    public bool CanPlay = true;
    private TOUCH_INFO TouchInfo;

    void Awake()
    {
        fieldData = DenQHelper.GetIinitiatedField();
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
            if (blockData.IsBroken())
            {
                return;
            }
            BreakOneBLock(blockData.fieldPos);
        }
    }
    public void InsertOneBlock(FieldPos fieldPos, FIELD_BLOCK type, FIELD_ITEM item)
    {
        var fieldCode = DenQHelper.ConvertFieldPosToCode(fieldPos);
        fieldCode = (long)Mathf.Clamp(fieldCode, 0, DenQHelper.maxFieldCode);
        if (fieldData[fieldCode] != null)
        {
            DenQLogger.GError("error:can not plate a block where exist already");
        }
        GameObject newBlockObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_BLOCK, this.gameObject, false);
        if (newBlockObj == null)
        {
            DenQLogger.GError("error:can not plate a block,could not read from ResourceManagr!");
            return;
        }
        Vector3 Vecpos = DenQHelper.ConvertFieldPosToWorld(fieldPos, unitCode);
        newBlockObj.transform.position = Vecpos;
        FieldBlock blockData = newBlockObj.GetComponent<FieldBlock>();
        blockData.InitializeFieldBlock(fieldPos.posX, fieldPos.posZ, type, item, unitCode);
        fieldData[fieldCode] = blockData;
    }
    //TODO クソーーーここでダラダラ書きたくないよ！
    public void BreakOneBLock(FieldPos pos)
    {
        var code = DenQHelper.ConvertFieldPosToCode(pos);
        var data = fieldData[code];
        if (data.IsBroken()) { return; }
        var itemType = data.GetBlockItemType();
        data.BreakBlock();
        switch (itemType)
        {
            case FIELD_ITEM.NONE:
                break;
            case FIELD_ITEM.BOMB_DELAY:
                GameObject bombObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_BOMB, PREFAB_NAME.ITEM_ROOT, false);
                if (bombObj != null)
                {
                    FieldBomb bombData = bombObj.GetComponent<FieldBomb>();
                    bombData.InitializeFieldBomb(pos.posX, pos.posZ, itemType, unitCode);
                }
                break;
            case FIELD_ITEM.BOMB_NORMAL:
                break;
        }
        //ここでNONEを指定しないと、コライダーが爆弾を生成指定しまう
        itemType = FIELD_ITEM.NONE;
    }
    public void RemoveField()
    {
        GameObject.Destroy(this.gameObject);
    }
    public void ClearField()
    {
        foreach (var code in fieldData.Keys)
        {
            if(fieldData[code] != null)
            fieldData[code].Destroy();
        }
        fieldData = DenQHelper.GetIinitiatedField();
    }
}
