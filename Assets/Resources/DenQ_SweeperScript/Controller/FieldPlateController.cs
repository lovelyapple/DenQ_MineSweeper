using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DenQ.BaseStruct;
public class FieldPlateController : MonoBehaviour
{

    public FieldNumbersController numberCtrl = null;
    public int fieldUnitCode = 0;
    public FieldPos fieldPos = new FieldPos();
    public int bombCnt = 0;
    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
    }
    //TODO :クソーーーFieldMgr使いたい！
    public void InitializePlate(int fieldCode)
    {
        this.fieldUnitCode = fieldCode;
        fieldPos = DenQHelper.ConverWoroldPosToFieldPos(this.gameObject.transform.position,fieldUnitCode);

        RequestUpdate();
    }
    public void RequestUpdate()
    {
        bombCnt = 0;
        Collider[] cols = DenQHelper.GetSroundedObejcts(this.fieldPos,fieldUnitCode);
        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == "FieldBlock")
            {
                FieldBlock blockData = col.gameObject.GetComponent<FieldBlock>();
                if (!blockData.IsBroken() && blockData.ExistBomb())
                {
                    bombCnt++;
                }
            }
        }
        if (numberCtrl == null)
        {
            GameObject numbersObj = ResourcesManager.GetInstance().CreateInstance(PREFAB_NAME.FIELD_NUMBERS, this.gameObject, false);
            numbersObj.transform.position = this.gameObject.transform.position;
            numberCtrl = numbersObj.GetComponent<FieldNumbersController>();
            numberCtrl.UpdateNumber(bombCnt);
        }else
        {
            numberCtrl.UpdateNumber(bombCnt);
        }
        
    }
}
