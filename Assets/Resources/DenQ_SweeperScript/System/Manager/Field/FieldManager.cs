using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ;
using DenQData;
using DenQModel;
public class FieldManager : ManagerBase<FieldManager>
{
    //public Vector3 satrtPos = new Vector3();

    //private static bool isCreatingMap = false;
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
        FieldModel.Get().SetUp(fieldCode);
        StartCoroutine(FieldModel.Get().CreateField());
    }
    public void ClearField()
    {
    }

    public void ObjectTouched(ObjectBaseData objData)
    {
        if (!objData.masterCode.HasValue)
        {
            return;
        }

        var token = DenQDataBaseHelper.GetFieldItemToken(objData.masterCode.Value);

        switch ((FIELD_ITEM_TOKEN)token)
        {
            case FIELD_ITEM_TOKEN.FIELD_ITEM_BLOCK:
                var fieldBlock = objData as FieldBlock;
                fieldBlock.BreakBlock();

                break;
            case FIELD_ITEM_TOKEN.FIELD_ITEM_BOMB:
                break;
        }
    }
}
