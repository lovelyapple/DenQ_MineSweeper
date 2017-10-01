using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.Mgr;
using DenQ.BaseStruct;
public class FieldManager : MangerBase<FieldManager>
{
    public Dictionary<int,Field> fieldsDataDic = new Dictionary<int,Field>();
    public Vector3 satrtPos = new Vector3();
    public Field fieldData = null;
    
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
    public void CreateField(ulong fieldCode)
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
        // GameObject fieldObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_FIELD, this.gameObject, false);
        // if (fieldObj == null)
        // {
        //     return false;
        // }
        // else
        // {
        //     fieldData = fieldObj.GetComponent<Field>();
        // }
        // fieldObj.transform.position = new Vector3(0,0,0);   
        return true;   
    }
    public void DebugCreateFieldBlock()
    {
        fieldData.InsertOneBlock(new FieldPos(0, 0), FIELD_BLOCK.NORMAL, FIELD_ITEM.NONE);
    }
}
