using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;
using DenQ.BaseStruct;
public class FieldManager : MangerBase<FieldManager>
{
    public Dictionary<int,Field> fieldsDataDic = new Dictionary<int,Field>();
    public Vector3 satrtPos = new Vector3();
    public Field fieldData = null;//Debug用に作られている
    public uint fieldSizeX = 1;//Debug用に作られている
    public uint fieldSizeZ = 1;//Debug用に作られている
    public DistributionMap DebugDitributionMap = new DistributionMap();
    
    private static bool isCreatingMap = false;
    // Use this for initialization
    void Awake()
    {
        SetInstance(this);
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void CreateField()
    {
    }
    public void ClearField()
    {
        fieldData.ClearField();
    }
    //================Debug 用機能 =========================//
    //private void Debug
    public bool DebugInitializeField()
    {
        GameObject fieldObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_FIELD, this.gameObject, false);
        if (fieldObj == null)
        {
            return false;
        }
        else
        {
            fieldData = fieldObj.GetComponent<Field>();
        }
        fieldObj.transform.position = new Vector3(0,0,0);   
        return true;   
    }
    public void DebugCreateFieldBlock()
    {
        fieldData.InsertOneBlock(new FieldPos(0, 0), FIELD_BLOCK.NORMAL, FIELD_ITEM.NONE);
    }
    public void DebugCreateFeildAll(uint sizeX, uint sizeZ)
    {
        if (!isCreatingMap)
        {
            fieldSizeX = sizeX;
            fieldSizeZ = sizeZ;
            StartCoroutine(DebugCreateFieldAllCoroutine());
        }
        else
        {
            Debug.LogError("A nothre building task is already runing");
            return;
        }
    }
    IEnumerator DebugCreateFieldAllCoroutine()
    {
        isCreatingMap = true;
        Debug.Log("start to create field");

        for (int i = 0; i < fieldSizeZ; i++)
        {
            for (int k = 0; k < fieldSizeX; k++)
            {
                //FIELD_ITEM itemType = k % 3 == 0 ? FIELD_ITEM.BOMB_DELAY : FIELD_ITEM.NONE;
                FIELD_ITEM itemType = DebugDitributionMap.GetItemTypeRandam();
                fieldData.InsertOneBlock(new FieldPos(k, i), FIELD_BLOCK.NORMAL, itemType);
                yield return null;
            }
        }
        isCreatingMap = false;

        while (true)
        {
            if (!isCreatingMap)
            {
                Debug.Log("Finished to create field");
                break;
            }
            yield return null;
        }
    }
    

}
